using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FftSharp;
using System.Drawing;

namespace AudioMixerApp.ControlService
{
    class WaveFormFFT
    {
        WaveStream waveStream;
        private int samplesPerPixel;
        private int channels;
        private int bytesPerSample;
        private int chunkBytes;
        private int fftPixel = 1;
        
        private Color lowColor;
        private Color midColor;
        private Color highColor;
        private Color defaultColor;
        private Color[] rgbs;

        public WaveFormFFT(WaveStream waveStream, int samplesPerPixel)
        {
            this.waveStream = waveStream;
            this.samplesPerPixel = samplesPerPixel;

            channels = waveStream.WaveFormat.Channels;
            bytesPerSample = (waveStream.WaveFormat.BitsPerSample / 8) * channels;
            chunkBytes = samplesPerPixel * bytesPerSample * fftPixel;  // 1024 bytes
            rgbs = new Color[waveStream.Length / chunkBytes];
            for (int i = 0; i < rgbs.Length; i++)
                rgbs[i] = defaultColor;

            lowColor = ColorTranslator.FromHtml("#FFDFD9");
            midColor = ColorTranslator.FromHtml("#0099FF");
            highColor = ColorTranslator.FromHtml("#FFE300");
            defaultColor = Color.Orange;
        }

        public async Task InitAsync()
        {
            double[] AudioValues = new double[(chunkBytes / 2) / channels];
            byte[] waveData = new byte[waveStream.Length];
            waveStream.Position = 0;
            waveStream.Read(waveData, 0, (int)waveStream.Length);

            await Task.Run(() => {
                int index = 0;
                for (int pos = 0; pos + chunkBytes < waveData.Length; pos += chunkBytes) {
                    for (int i = 0; i < AudioValues.Length; i++)
                        AudioValues[i] = BitConverter.ToInt16(waveData, pos + i * 2 * channels);

                    // FFT
                    double[] paddedAudio = Pad.ZeroPad(AudioValues);
                    double[] fftMag = Transform.FFTpower(paddedAudio);
                    double fftPeriod = Transform.FFTfreqPeriod(44100, fftMag.Length);

                    // Calculate max
                    double midThreshold = 600.0, highThreshold = 4000.0;
                    double lowMax = 0, midMax = 0, highMax = 0;
                    int lowCount = 0, midCount = 0, highCount = 0;
                    for (int i = 0; i * fftPeriod < midThreshold; i++, lowCount++)
                        lowMax = Math.Max(fftMag[i], lowMax);
                    for (int i = lowCount; i * fftPeriod < highThreshold; i++, midCount++)
                        midMax = Math.Max(fftMag[i], midMax);
                    for (int i = midCount; i < fftMag.Length; i++, highCount++)
                        highMax = Math.Max(fftMag[i], highMax);

                    double red = 255 * lowMax + 127 * midMax + 63 * highMax;
                    double green = 127 * lowMax + 255 * midMax + 63 * highMax;
                    double blue = 127 * lowMax + 63 * midMax + 63 * highMax;
                    double max = Math.Max(red, Math.Max(green, blue));

                    // Convert to Colors
                    if (max > 0) {
                        red /= max; green /= max; blue /= max;
                    }
                    rgbs[index] = Color.FromArgb(Convert.ToInt32(red * 255), Convert.ToInt32(green * 255), Convert.ToInt32(blue * 255));
                    index++;
                }
            });
        }

        public Color getColor(int position) {
            int index = position / chunkBytes;
            if (index >= rgbs.Length) {
                return defaultColor;
            }
            return rgbs[index];
        }
    }
}
