using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AudioMixerApp
{
    public partial class SDCardBrowser : Form
    {
        Deck parentDeck;

        public SDCardBrowser(List<String> fnames, Deck deck)
        {
            InitializeComponent();
            wavList.Items.Clear();
            foreach (String fname in fnames)
            {
                wavList.Items.Add(fname);
            }
            parentDeck = deck;
            
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            parentDeck.fileIndex = wavList.SelectedIndex;
            Close();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
