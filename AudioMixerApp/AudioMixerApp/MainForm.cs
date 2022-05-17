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

        public mainForm()
        {
            InitializeComponent();
            infoCard1.setId(1);
            infoCard2.setId(2);
            deck1.infoCard = infoCard1;
            deck2.infoCard = infoCard2;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            // Test uart
            // TODO: Use a seperated thread
            Uart u = new Uart("COM4", 9600);
            u.OpenSerial();

            for (int i = 0; i < 10; i++)
            {
                string line = u.ReadLines();
                if (line != "")
                {
                    Console.Write(line);
                }
                Thread.Sleep(1000);
            }

            u.CloseSerial();
        }
    }
}
