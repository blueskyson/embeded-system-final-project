using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NAudio.Wave.SampleProviders;
using NAudio.Wave;
using System.Threading;
using AudioPlayerApp;

namespace AudioMixerApp
{
    public partial class MainForm : Form
    {
        Uart uart;
        private static int trackNum = 4;
        Deck[] decks;
        PictureBox[] selectPictures;
        public List<String> SDCardFilenames;

        // Current track index that variable resistor points to
        int currentTrack = 0;

        // uart communication rules
        const int Adc = 0, Btn1 = 2, Btn2 = 1;
        const String ListWavBegin = "3", ListWav = "4";

        public String UseWinform = "A", UseStm32 = "B", Stm32LoadTrack1 = "C", Stm32LoadTrack2 = "D";

        public MainForm()
        {
            InitializeComponent();
            infoCard1.setId(1, deck1);
            infoCard2.setId(2, deck2);
            infoCard3.setId(3, deck3);
            infoCard4.setId(4, deck4);

            decks = new Deck[trackNum];
            decks[0] = deck1;
            decks[1] = deck2;
            decks[2] = deck3;
            decks[3] = deck4;

            deck1.infoCard = infoCard1;
            deck2.infoCard = infoCard2;
            deck3.infoCard = infoCard3;
            deck4.infoCard = infoCard4;

            selectPictures = new PictureBox[trackNum - 1];
            selectPictures[0] = select1;
            selectPictures[1] = select2;
            selectPictures[2] = select3;

            audioSrcComboBox.SelectedIndex = 0;

            serialTimer.Stop();
        }

        private void openSerialButton_Click(object sender, EventArgs e)
        {
            if (!Int32.TryParse(serialSpeed.Text, out int speed)) {
                MessageBox.Show("Fail to parse speed " + serialSpeed.Text);
                return;
            }

            uart = new Uart(serialLine.Text, speed);
            if (!uart.OpenSerial()) {
                MessageBox.Show("Fail to open " + serialLine.Text);
                uart = null;
                return;
            }

            if (audioSrcComboBox.SelectedIndex == 0)
            {
                uart.Send(UseWinform);
            }
            else if (audioSrcComboBox.SelectedIndex == 1)
            {
                uart.Send(UseStm32);
            }

            serialTimer.Start();
        }

        private void serialTimer_Tick(object sender, EventArgs e)
        {
            string line = uart.ReadLines();
            if (line != "")
            {
                char[] rowSeparator = { '\n' };
                string[] rows = line.Split(rowSeparator, StringSplitOptions.RemoveEmptyEntries);

                for (int i = 0; i < rows.Length; i++)
                {
                    char[] separator = { ' ' };
                    string[] dataString;
                    int[] data;
                    
                    // Parse by space
                    try
                    {
                        dataString = rows[i].Split(separator, StringSplitOptions.RemoveEmptyEntries);
                    }
                    catch (Exception)
                    {
                        return;
                    }

                    // Get SD Card WAV files
                    if (dataString[0] == ListWavBegin) 
                    {
                        SDCardFilenames = new List<String>();
                        continue;
                    }
                    else if (dataString[0] == ListWav)
                    {
                        // Constraint
                        Console.WriteLine(rows[i]);
                        if (SDCardFilenames.Count > 10 || dataString.Length < 2)
                        {
                            continue;
                        }

                        String fname = dataString[1];
                        for (int j = 2; j < dataString.Length; j++)
                        {
                            fname += " " + dataString[j];
                        }
                        SDCardFilenames.Add(fname);
                        continue;
                    }
                    else if (dataString[0] == "X")
                    {
                        Console.WriteLine(rows[i]);
                    }

                    // Parse all strings to int
                    try {
                        data = Array.ConvertAll(dataString, delegate (string s)
                        {
                            if (!Int32.TryParse(s, out int val))
                            {
                                val = -1;
                            }
                            return val;
                        });
                        
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("Exception");
                        continue;
                    }

                    if (data[0] == Adc)
                    {
                        if (data.Length < 4)
                            continue;

                        setCurrentTrack(data[3]);
                        decks[currentTrack].changeVolume(data[2]);
                        decks[currentTrack + 1].changeVolume(data[1]);
                    }
                    else if (data[0] == Btn1)
                    {
                        decks[currentTrack].toggle();
                    }
                    else if (data[0] == Btn2)
                    {
                        decks[currentTrack + 1].toggle();
                    }

                }
            }

        }

        private void closeSerialButton_Click(object sender, EventArgs e)
        {
            if (uart != null) {
                uart.CloseSerial();
                uart = null;
            }

            serialTimer.Stop();
            audioSrcComboBox.SelectedIndex = 0;
        }

        private void setCurrentTrack(int track)
        {
            if (currentTrack == track)
                return;
            selectPictures[currentTrack].Visible = false;
            selectPictures[track].Visible = true;
            currentTrack = track;
        }

        //
        // For switching audio source between stm32 and windows
        //

        private void audioSrcComboBox_DrawItem(object sender, DrawItemEventArgs e)
        {
            Font myFont = new Font("Aerial", 10, FontStyle.Regular | FontStyle.Regular);

            if (e.Index == 1 && uart == null) {
                e.Graphics.DrawString(audioSrcComboBox.Items[e.Index].ToString(), myFont, Brushes.LightGray, e.Bounds);
            } else {
                e.DrawBackground();
                e.Graphics.DrawString(audioSrcComboBox.Items[e.Index].ToString(), myFont, Brushes.Black, e.Bounds);
                e.DrawFocusRectangle();
            }
        }

        private void audioSrcComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (audioSrcComboBox.SelectedIndex == 1 && uart == null)
                audioSrcComboBox.SelectedIndex = 0;

            if (uart != null)
            {
                if (audioSrcComboBox.SelectedIndex == 0) {
                    uart.Send(UseWinform);
                } else if (audioSrcComboBox.SelectedIndex == 1) {
                    uart.Send(UseStm32);
                }
            }
        }

        public String audioSource()
        {
            if (audioSrcComboBox.SelectedIndex == 0)
            {
                return UseWinform;
            }
            else if (audioSrcComboBox.SelectedIndex == 1)
            {
                return UseStm32;
            }
            return UseWinform;
        }

        //
        // For telling stm32 which song to load
        //
        public void stm32LoadFile(int trackId, int fileId)
        {
            if (uart != null)
            {
                if (trackId == 1)
                {
                    String line = Stm32LoadTrack1 + fileId.ToString();
                    uart.Send(line);
                }
                else if (trackId == 2)
                {
                    String line = Stm32LoadTrack2 + fileId.ToString();
                    uart.Send(line);
                }
            }
        }
    }
}
