using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using NAudio.Wave;

namespace AudioMixerApp.ControlService
{
    public partial class WaveForm : UserControl
    {
        private WaveStream waveStream;
        private int bytesPerSample;
        private int samplesPerPixel = 128;
        private long startPosition;
        private long currentPosition;

        // FFT
        private WaveFormFFT fft;

        public WaveForm()
        {
            InitializeComponent();
            this.DoubleBuffered = true;
        }

        public void readWaveFile(string path)
        {
            waveStream = new WaveFileReader(path);
            if (waveStream != null)
            {
                bytesPerSample = (waveStream.WaveFormat.BitsPerSample / 8) * waveStream.WaveFormat.Channels;
                currentPosition = 0;

                // Perform FFT
                fft = new WaveFormFFT(waveStream, samplesPerPixel);
                _ = fft.InitAsync();
            }

            this.Invalidate();
        }

        public int SamplesPerPixel
        {
            get { return samplesPerPixel; }
            set { samplesPerPixel = value; }
        }

        public long StartPosition
        {
            get { return startPosition; }
            set { startPosition = value; }
        }

        public void DrawWave(double progress) {
            currentPosition = Convert.ToInt64(waveStream.Length * progress);
            this.Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            if (waveStream != null) {
                long xMiddle = this.Width / 2;
                long startOffset = 0;
                if (currentPosition < xMiddle) {
                    startPosition = 0;
                    startOffset = xMiddle - currentPosition;
                } else {
                    startPosition = currentPosition - xMiddle;
                    startOffset = 0;
                }

                // Set start position of audio stream to read
                waveStream.Position = startPosition + (bytesPerSample * samplesPerPixel);
                
                byte[] waveData = new byte[samplesPerPixel * bytesPerSample];

                for (float x = e.ClipRectangle.X + startOffset; x < e.ClipRectangle.Right; x += 1) {
                    short low = 0;
                    short high = 0;
                    long bytesRead = waveStream.Read(waveData, 0, samplesPerPixel * bytesPerSample);
                    if (bytesRead == 0)
                        break;

                    for (int n = 0; n < bytesRead; n += 2) {
                        short sample = BitConverter.ToInt16(waveData, n);
                        if (sample < low) low = sample;
                        if (sample > high) high = sample;
                    }
                    float lowPercent = (((float)low) - short.MinValue) / ushort.MaxValue;
                    float highPercent = (((float)high) - short.MinValue) / ushort.MaxValue;
                    
                    Pen pen = new Pen(fft.getColor((int)(startPosition + x)));
                    e.Graphics.DrawLine(Pens.Orange, x, this.Height * lowPercent, x, this.Height * highPercent);
                }
                e.Graphics.DrawLine(Pens.White, xMiddle, 0, xMiddle, this.Height);
            }

            base.OnPaint(e);
        }

    }
}
