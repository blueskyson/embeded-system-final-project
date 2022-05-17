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

        public Deck()
        {
            InitializeComponent();

            // This is mandatory when using any of SharpDX.MediaFoundation classes
            MediaManager.Startup();

            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(5);
            timer.Tick += timer_Tick;
        }

        private void volumnTrackbar_Scroll(object sender, EventArgs e)
        {
            lock (lockAudio)
            {
                if (audioPlayer != null) {
                    var volume = (float)volumnTrackbar.Value / volumnTrackbar.Maximum;
                    audioPlayer.Volume = volume;
                }
            }
        }

        private void loadButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Title = "Select audio file";
            dialog.Filter = "wav (*.wav)|*.wav|mp3 (*.mp3)|*.mp3";
            DialogResult res = dialog.ShowDialog();
            if (res == DialogResult.OK) {
                try
                {
                    lock (lockAudio)
                    {
                        if (audioPlayer != null) {
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
                        if (fileStream == null) {
                            MessageBox.Show("Error reading file");
                            return;
                        }
                        audioPlayer = new AudioPlayer(xaudio2, fileStream);

                        // Draw infoCard
                        infoCard.LoadWaveStream(dialog.FileName, audioPlayer.Duration);
                    }
                } finally {
                    playIcon.Image = Properties.Resources.play;
                    pauseIcon.Image = Properties.Resources.pause2;
                }
            } else if (res == DialogResult.No) {
                MessageBox.Show("Error opening file");
            }
        }

        private void playIcon_Click(object sender, EventArgs e)
        {
            lock (lockAudio) {
                if (audioPlayer != null && audioPlayer.State != AudioPlayerState.Playing) {
                    var volume = (float)volumnTrackbar.Value / volumnTrackbar.Maximum;
                    audioPlayer.Volume = volume;
                    audioPlayer.Play();
                    timer.Start();
                    playIcon.Image = Properties.Resources.play2;
                    pauseIcon.Image = Properties.Resources.pause;
                }
            }

        }

        private void pauseIcon_Click(object sender, EventArgs e)
        {
            lock (lockAudio) {
                if (audioPlayer != null) {
                    audioPlayer.Pause();
                    timer.Stop();
                    playIcon.Image = Properties.Resources.play;
                    pauseIcon.Image = Properties.Resources.pause2;
                }
            }

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
                }
            }
        }

        private void Deck_Load(object sender, EventArgs e)
        {

        }
    }
}
