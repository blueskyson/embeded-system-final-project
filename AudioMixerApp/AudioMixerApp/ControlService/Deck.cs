using AudioMixerApp;
using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;
using System.Windows.Forms;
using AudioPlayerApp;

using SharpDX;
using SharpDX.IO;
using SharpDX.MediaFoundation;
using SharpDX.XAudio2;

using NAudio.Wave;


namespace AudioMixerApp
{
    public partial class Deck : UserControl
    {

        public InfoCard infoCard { get; set; }
        private DispatcherTimer timer;

        private XAudio2 xaudio2;
        private MasteringVoice masteringVoice;
        private Stream fileStream;
        private AudioPlayer audioPlayer;
        private object lockAudio = new object();

        private static int TrackbarMax = 150;
        private float volumeScale;
        private AudioPlayerState state = AudioPlayerState.Stopped;

        // SD Card related variables
        public int fileIndex = 0;

        public Deck()
        {
            InitializeComponent();

            // This is mandatory when using any of SharpDX.MediaFoundation classes
            MediaManager.Startup();

            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(10);
            timer.Tick += timer_Tick;

            volumnTrackbar.Maximum = TrackbarMax;
            volumnTrackbar.TickFrequency = TrackbarMax / 20;
            volumeScale = 4096 / volumnTrackbar.Maximum;
        }

        private void volumnTrackbar_ValueChanged(object sender, EventArgs e)
        {
            lock (lockAudio)
            {
                if (audioPlayer != null)
                {
                    float volume = (float)volumnTrackbar.Value / volumnTrackbar.Maximum;
                    audioPlayer.Volume = volume;
                }
            }
        }

        public void changeVolume(int val) {
            float volume = (float)val / volumeScale;
            try {
                int vol =  
                volumnTrackbar.Value = (int)volume > volumnTrackbar.Maximum ?
                                       volumnTrackbar.Maximum : 
                                       (int)volume;
            } finally {

            }
        }

        private void loadButton_Click(object sender, EventArgs e)
        {
            MainForm mainForm = this.Parent as MainForm;

            // Load music from SD Card
            if (mainForm.audioSource() == mainForm.UseStm32)
            {
                SDCardBrowser browser = new SDCardBrowser(mainForm.SDCardFilenames, this);
                DialogResult result = browser.ShowDialog(this);
                if (result == DialogResult.OK)
                {
                    mainForm.stm32LoadFile(infoCard.id, fileIndex);
                }
            }

            // Load music from computer
            else
            {
                OpenFileDialog dialog = new OpenFileDialog();
                dialog.Title = "Select audio file";
                dialog.Filter = "wav (*.wav)|*.wav|mp3 (*.mp3)|*.mp3";
                DialogResult res = dialog.ShowDialog();
                if (res == DialogResult.OK)
                {
                    try
                    {
                        lock (lockAudio)
                        {
                            if (audioPlayer != null)
                            {
                                audioPlayer.Stop();
                                audioPlayer.Close();
                                audioPlayer = null;
                            }

                            if (fileStream != null)
                            {
                                fileStream.Close();
                                fileStream = null;
                            }

                            if (xaudio2 != null)
                            {
                                masteringVoice.Dispose();
                                xaudio2.StopEngine();
                                masteringVoice = null;
                                xaudio2 = null;
                            }

                            // Starts The XAudio2 engine
                            xaudio2 = new XAudio2();
                            xaudio2.StartEngine();
                            masteringVoice = new MasteringVoice(xaudio2);

                            // Ask the user for a video or audio file to play
                            fileStream = new NativeFileStream(dialog.FileName, NativeFileMode.Open, NativeFileAccess.Read);
                            if (fileStream == null)
                            {
                                MessageBox.Show("Error reading file");
                                return;
                            }
                            audioPlayer = new AudioPlayer(xaudio2, fileStream);

                            // Draw infoCard
                            infoCard.LoadWaveStream(dialog.FileName, audioPlayer.Duration);
                        }
                    }
                    finally
                    {
                        playIcon.Image = Properties.Resources.play;
                        pauseIcon.Image = Properties.Resources.pause2;
                    }
                }
                else if (res == DialogResult.No)
                {
                    MessageBox.Show("Error opening file");
                }
            }
        }

        public void play()
        {
            lock (lockAudio)
            {
                if (audioPlayer != null && audioPlayer.State != AudioPlayerState.Playing)
                {
                    var volume = (float)volumnTrackbar.Value / volumnTrackbar.Maximum;
                    audioPlayer.Volume = volume;
                    audioPlayer.Play();
                    timer.Start();
                    playIcon.Image = Properties.Resources.play2;
                    pauseIcon.Image = Properties.Resources.pause;
                    loadButton.Enabled = false;
                    state = AudioPlayerState.Playing;
                }
            }
        }

        public void pause()
        {
            lock (lockAudio)
            {
                if (audioPlayer != null)
                {
                    audioPlayer.Pause();
                    timer.Stop();
                    playIcon.Image = Properties.Resources.play;
                    pauseIcon.Image = Properties.Resources.pause2;
                    loadButton.Enabled = true;
                    state = AudioPlayerState.Paused;
                }
            }
        }

        public void toggle()
        {
            if (state == AudioPlayerState.Playing) {
                pause();
            } else {
                play();
            }
        }

        public void move(double pos)
        {
            // range of @pos: 0 to 1.0
            lock (lockAudio) {
                if (audioPlayer != null) {
                    audioPlayer.Position = new TimeSpan((long)(audioPlayer.Duration.TotalMilliseconds * pos * 1e4));
                }
            }
        }

        private void playIcon_Click(object sender, EventArgs e)
        {
            play();
        }

        private void pauseIcon_Click(object sender, EventArgs e)
        {
            pause();
        }

        private void Deck_ControlRemoved(object sender, ControlEventArgs e)
        {
            Utilities.Dispose(ref masteringVoice);
            Utilities.Dispose(ref xaudio2);
        }

        void timer_Tick(object sender, EventArgs e)
        {
            lock (lockAudio) {
                if (audioPlayer != null) {
                    infoCard.UpdateWaveForm(audioPlayer.Position, audioPlayer.Progress);
                    if (audioPlayer.State == AudioPlayerState.Stopped) {
                        timer.Stop();
                        playIcon.Image = Properties.Resources.play;
                        pauseIcon.Image = Properties.Resources.pause2;
                        loadButton.Enabled = true;
                    }
                }
            }
        }

        private void Deck_Load(object sender, EventArgs e)
        {

        }


    }
}
