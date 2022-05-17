// Copyright (c) 2010-2011 SharpDX - Alexandre Mutel
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.

using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using SharpDX;
using SharpDX.MediaFoundation;
using SharpDX.XAudio2;

namespace AudioPlayerApp
{
    public class AudioPlayer
    {
        // Audio
        private XAudio2 xaudio2;
        private AudioDecoder audioDecoder;
        private SourceVoice sourceVoice;
        private AudioBuffer[] audioBuffersRing;
        private DataPointer[] memBuffers;

        // Basic Types
        private float localVolume;
        private int playCounter;
        private bool IsDisposed;
        private const int WaitPrecision = 1;

        // Events
        private AutoResetEvent bufferEndEvent;
        private ManualResetEvent playEvent;
        private ManualResetEvent waitForPlayToOutput;

        // Others
        private Stopwatch clock;
        private TimeSpan playPosition;
        private TimeSpan nextPlayPosition;
        private TimeSpan playPositionStart;
        private Task playingTask;
        
        public AudioPlayerState State { get; private set; }

        public TimeSpan Duration
        {
            get { return audioDecoder.Duration; }
        }

        public TimeSpan Position
        {
            get { return playPosition; }
            set {
                playPosition = value;
                nextPlayPosition = value;
                playPositionStart = value;
                clock.Restart();
                playCounter++;
            }
        }

        public double Progress
        {
            get { return Position.TotalMilliseconds / Duration.TotalMilliseconds; }
        }

        public bool IsRepeating { get; set; }

        public AudioPlayer(XAudio2 xaudio2, Stream audioStream)
        {
            this.xaudio2 = xaudio2;
            audioDecoder = new AudioDecoder(audioStream);
            sourceVoice = new SourceVoice(xaudio2, audioDecoder.WaveFormat);
            localVolume = 1.0f;

            sourceVoice.BufferEnd += sourceVoice_BufferEnd;
            sourceVoice.Start();

            bufferEndEvent = new AutoResetEvent(false);
            playEvent = new ManualResetEvent(false);
            waitForPlayToOutput = new ManualResetEvent(false);

            clock = new Stopwatch();

            // Pre-allocate buffers
            audioBuffersRing = new AudioBuffer[3];
            memBuffers = new DataPointer[audioBuffersRing.Length];
            for (int i = 0; i < audioBuffersRing.Length; i++)
            {
                audioBuffersRing[i] = new AudioBuffer();
                memBuffers[i].Size = 32 * 1024; // default size 32Kb
                memBuffers[i].Pointer = Utilities.AllocateMemory(memBuffers[i].Size);
            }

            // Initialize to stopped
            State = AudioPlayerState.Stopped;

            // Starts the playing thread
            playingTask = Task.Factory.StartNew(PlayAsync, TaskCreationOptions.LongRunning);
        }

        void sourceVoice_BufferEnd(IntPtr obj)
        {
            bufferEndEvent.Set();
        }

        private void PlayAsync()
        {
            int currentPlayCounter = 0;
            int nextBuffer = 0;

            try {
                while (true) {
                    // Check that this instanced is not disposed
                    while (!IsDisposed) {
                        if (playEvent.WaitOne(WaitPrecision))
                            break;
                    }

                    if (IsDisposed)
                        break;

                    clock.Restart();
                    playPositionStart = nextPlayPosition;
                    playPosition = playPositionStart;
                    currentPlayCounter = playCounter;

                    // Get the decoded samples from the specified starting position.
                    var sampleIterator = audioDecoder.GetSamples(playPositionStart).GetEnumerator();

                    bool isFirstTime = true;
                    bool endOfSong = false;

                    // Playing all the samples
                    while (true) {
                        while (!IsDisposed && State != AudioPlayerState.Stopped) {
                            if (playEvent.WaitOne(WaitPrecision))
                                break;
                        }

                        // If the player is stopped or disposed, then break of this loop
                        if (IsDisposed || State == AudioPlayerState.Stopped) {
                            nextPlayPosition = TimeSpan.Zero;
                            break;
                        }

                        // If ring buffer queued is full, wait for the end of a buffer.
                        while (sourceVoice.State.BuffersQueued == audioBuffersRing.Length && !IsDisposed && State != AudioPlayerState.Stopped)
                            bufferEndEvent.WaitOne(WaitPrecision);

                        // If the player is stopped or disposed, then break of this loop
                        if (IsDisposed || State == AudioPlayerState.Stopped) {
                            nextPlayPosition = TimeSpan.Zero;
                            break;
                        }

                        // Check that there is a next sample
                        if (!sampleIterator.MoveNext()) {
                            endOfSong = true;
                            break;
                        }

                        // Retrieve a pointer to the sample data
                        var bufferPointer = sampleIterator.Current;

                        // If there was a change in the play position, restart the sample iterator.
                        if (currentPlayCounter != playCounter)
                            break;

                        // Check that our ring buffer has enough space to store the audio buffer.
                        if (bufferPointer.Size > memBuffers[nextBuffer].Size) {
                            if (memBuffers[nextBuffer].Pointer != IntPtr.Zero)
                                Utilities.FreeMemory(memBuffers[nextBuffer].Pointer);

                            memBuffers[nextBuffer].Pointer = Utilities.AllocateMemory(bufferPointer.Size);
                            memBuffers[nextBuffer].Size = bufferPointer.Size;
                        }

                        // Copy the memory from MediaFoundation AudioDecoder to the buffer that is going to be played.
                        Utilities.CopyMemory(memBuffers[nextBuffer].Pointer, bufferPointer.Pointer, bufferPointer.Size);

                        // Set the pointer to the data.
                        audioBuffersRing[nextBuffer].AudioDataPointer = memBuffers[nextBuffer].Pointer;
                        audioBuffersRing[nextBuffer].AudioBytes = bufferPointer.Size;

                        // If this is a first play, restart the clock and notify play method.
                        if (isFirstTime)
                        {
                            clock.Restart();
                            isFirstTime = false;

                            waitForPlayToOutput.Set();
                        }

                        // Update the current position used for sync
                        playPosition = new TimeSpan(playPositionStart.Ticks + clock.Elapsed.Ticks);

                        // Submit the audio buffer to xaudio2
                        sourceVoice.SubmitSourceBuffer(audioBuffersRing[nextBuffer], null);

                        // Go to next entry in the ringg audio buffer
                        nextBuffer = ++nextBuffer % audioBuffersRing.Length;
                    }

                    // If the song is not looping (by default), then stop the audio player.
                    if (endOfSong && !IsRepeating && State == AudioPlayerState.Playing) {
                        Stop();
                    }
                }
            } finally {
                DisposePlayer();
            }
        }

        public float Volume
        {
            get { return localVolume; }
            set {
                localVolume = value;
                sourceVoice.SetVolume(value);
            }
        }

        public void Play()
        {
            if (State != AudioPlayerState.Playing) {
                bool waitForFirstPlay = false;
                if (State == AudioPlayerState.Stopped) {
                    playCounter++;
                    waitForPlayToOutput.Reset();
                    waitForFirstPlay = true;
                } else {
                    // The song was paused
                    clock.Start();
                }

                State = AudioPlayerState.Playing;
                playEvent.Set();

                if (waitForFirstPlay) {
                    waitForPlayToOutput.WaitOne();
                }
            }
        }

        public void Pause()
        {
            if (State == AudioPlayerState.Playing)
            {
                clock.Stop();
                State = AudioPlayerState.Paused;
                playEvent.Reset();
            }
        }

        public void Stop()
        {
            if (State != AudioPlayerState.Stopped) {
                playPosition = TimeSpan.Zero;
                nextPlayPosition = TimeSpan.Zero;
                playPositionStart = TimeSpan.Zero;
                clock.Stop();
                playCounter++;
                State = AudioPlayerState.Stopped;
                playEvent.Reset();
            }
        }

        public void Wait()
        {
            playingTask.Wait();
        }

        public void Close()
        {
            Stop();
            IsDisposed = true;
            Wait();
        }

        private void DisposePlayer()
        {
            audioDecoder.Dispose();
            audioDecoder = null;

            sourceVoice.Dispose();
            sourceVoice = null;

            for (int i = 0; i < audioBuffersRing.Length; i++) {
                Utilities.FreeMemory(memBuffers[i].Pointer);
                memBuffers[i].Pointer = IntPtr.Zero;
            }
        }
    }
}