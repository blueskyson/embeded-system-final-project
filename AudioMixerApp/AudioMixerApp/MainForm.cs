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
        int currentTrack = 0;

        public mainForm()
        {
            InitializeComponent();
            infoCard1.setId(1);
            infoCard2.setId(2);
            infoCard3.setId(3);
            infoCard4.setId(4);

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

            serialTimer.Stop();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

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
            if (line != "") {
                Console.Write(line);
                char[] rowSeparator = { '\n' };
                string[] rows = line.Split(rowSeparator, StringSplitOptions.RemoveEmptyEntries);

                // only use row[0], drop other rows
                char[] separator = { ' ' };
                string[] dataString;
                int[] data;
                try {
                    dataString = rows[0].Split(separator, StringSplitOptions.RemoveEmptyEntries);
                    data = Array.ConvertAll(dataString, delegate (string s) {
                        if (!Int32.TryParse(s, out int val))
                        {
                            val = -1;
                        }
                        return val;
                    });
                } catch(Exception) {
                    Console.WriteLine("Exception");
                    return;
                }

                int Adc = 0, Btn1 = 2, Btn2 = 1;

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
                    if (data[1] == 0)
                    {
                        decks[currentTrack].pause();
                    }
                    else
                    {
                        decks[currentTrack].play();
                    }
                }
                else if (data[0] == Btn2)
                {
                    if (data.Length < 2)
                        return;
                    if (data[1] == 0)
                    {
                        decks[currentTrack + 1].pause();
                    }
                    else
                    {
                        decks[currentTrack + 1].play();
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
        }

        private void setCurrentTrack(int track)
        {
            if (currentTrack == track)
                return;
            selectPictures[currentTrack].Visible = false;
            selectPictures[track].Visible = true;
            currentTrack = track;
        }
    }
}
