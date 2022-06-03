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
    public partial class mainForm : Form
    {
        Uart uart;
        private static int trackNum = 4;
        Deck[] decks;
        PictureBox[] selectPictures;

        // Current track index that variable resistor points to
        int currentTrack = 0;

        // uart communication rules
        const int Adc = 0, Btn1 = 2, Btn2 = 1;
        const String UseWinform = "A", UseStm32 = "B";

        public mainForm()
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
                    
                    try {
                        dataString = rows[i].Split(separator, StringSplitOptions.RemoveEmptyEntries);
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
                        return;
                    }

                    if (data[0] == Adc)
                    {
                        if (data.Length < 4)
                            return;

                        setCurrentTrack(data[3]);
                        decks[currentTrack].changeVolume(data[2]);
                        decks[currentTrack + 1].changeVolume(data[1]);
                    }
                    else if (data[0] == Btn1)
                    {
                        if (data.Length < 2)
                            return;
                        Console.Write(line);
                        //if (data[1] == 0)
                        //{
                        //    decks[currentTrack].pause();
                        //}
                        //else
                        //{
                        //    decks[currentTrack].play();
                        //}
                        decks[currentTrack].toggle();
                    }
                    else if (data[0] == Btn2)
                    {
                        if (data.Length < 2)
                            return;
                        //if (data[1] == 0)
                        //{
                        //    decks[currentTrack + 1].pause();
                        //}
                        //else
                        //{
                        //    decks[currentTrack + 1].play();
                        //}
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
    }
}
