
namespace AudioMixerApp
{
    partial class mainForm
    {
        /// <summary>
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置受控資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 設計工具產生的程式碼

        /// <summary>
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器修改
        /// 這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.serialLine = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.serialSpeed = new System.Windows.Forms.TextBox();
            this.openSerialButton = new System.Windows.Forms.Button();
            this.serialTimer = new System.Windows.Forms.Timer(this.components);
            this.closeSerialButton = new System.Windows.Forms.Button();
            this.infoCard2 = new AudioMixerApp.InfoCard();
            this.infoCard1 = new AudioMixerApp.InfoCard();
            this.deck2 = new AudioMixerApp.Deck();
            this.deck1 = new AudioMixerApp.Deck();
            this.deck3 = new AudioMixerApp.Deck();
            this.deck4 = new AudioMixerApp.Deck();
            this.infoCard3 = new AudioMixerApp.InfoCard();
            this.infoCard4 = new AudioMixerApp.InfoCard();
            this.select1 = new System.Windows.Forms.PictureBox();
            this.select2 = new System.Windows.Forms.PictureBox();
            this.select3 = new System.Windows.Forms.PictureBox();
            this.label3 = new System.Windows.Forms.Label();
            this.audioSrcComboBox = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.select1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.select2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.select3)).BeginInit();
            this.SuspendLayout();
            // 
            // serialLine
            // 
            this.serialLine.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.serialLine.Location = new System.Drawing.Point(87, 9);
            this.serialLine.Name = "serialLine";
            this.serialLine.Size = new System.Drawing.Size(100, 25);
            this.serialLine.TabIndex = 4;
            this.serialLine.Text = "COM5";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(69, 15);
            this.label1.TabIndex = 5;
            this.label1.Text = "Serial line:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(193, 12);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(45, 15);
            this.label2.TabIndex = 6;
            this.label2.Text = "Speed:";
            // 
            // serialSpeed
            // 
            this.serialSpeed.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.serialSpeed.Location = new System.Drawing.Point(244, 9);
            this.serialSpeed.Name = "serialSpeed";
            this.serialSpeed.Size = new System.Drawing.Size(100, 25);
            this.serialSpeed.TabIndex = 7;
            this.serialSpeed.Text = "115200";
            // 
            // openSerialButton
            // 
            this.openSerialButton.Location = new System.Drawing.Point(350, 8);
            this.openSerialButton.Name = "openSerialButton";
            this.openSerialButton.Size = new System.Drawing.Size(93, 26);
            this.openSerialButton.TabIndex = 8;
            this.openSerialButton.Text = "open";
            this.openSerialButton.UseVisualStyleBackColor = true;
            this.openSerialButton.Click += new System.EventHandler(this.openSerialButton_Click);
            // 
            // serialTimer
            // 
            this.serialTimer.Enabled = true;
            this.serialTimer.Interval = 10;
            this.serialTimer.Tick += new System.EventHandler(this.serialTimer_Tick);
            // 
            // closeSerialButton
            // 
            this.closeSerialButton.Location = new System.Drawing.Point(449, 8);
            this.closeSerialButton.Name = "closeSerialButton";
            this.closeSerialButton.Size = new System.Drawing.Size(93, 26);
            this.closeSerialButton.TabIndex = 9;
            this.closeSerialButton.Text = "close";
            this.closeSerialButton.UseVisualStyleBackColor = true;
            this.closeSerialButton.Click += new System.EventHandler(this.closeSerialButton_Click);
            // 
            // infoCard2
            // 
            this.infoCard2.AutoSize = true;
            this.infoCard2.deck = null;
            this.infoCard2.Location = new System.Drawing.Point(12, 200);
            this.infoCard2.MinimumSize = new System.Drawing.Size(500, 0);
            this.infoCard2.Name = "infoCard2";
            this.infoCard2.Size = new System.Drawing.Size(950, 140);
            this.infoCard2.TabIndex = 3;
            // 
            // infoCard1
            // 
            this.infoCard1.AutoSize = true;
            this.infoCard1.deck = null;
            this.infoCard1.Location = new System.Drawing.Point(12, 51);
            this.infoCard1.MinimumSize = new System.Drawing.Size(500, 0);
            this.infoCard1.Name = "infoCard1";
            this.infoCard1.Size = new System.Drawing.Size(950, 143);
            this.infoCard1.TabIndex = 2;
            // 
            // deck2
            // 
            this.deck2.infoCard = null;
            this.deck2.Location = new System.Drawing.Point(1049, 77);
            this.deck2.MinimumSize = new System.Drawing.Size(0, 480);
            this.deck2.Name = "deck2";
            this.deck2.Size = new System.Drawing.Size(75, 480);
            this.deck2.TabIndex = 1;
            // 
            // deck1
            // 
            this.deck1.infoCard = null;
            this.deck1.Location = new System.Drawing.Point(968, 77);
            this.deck1.MinimumSize = new System.Drawing.Size(0, 480);
            this.deck1.Name = "deck1";
            this.deck1.Size = new System.Drawing.Size(75, 480);
            this.deck1.TabIndex = 0;
            // 
            // deck3
            // 
            this.deck3.infoCard = null;
            this.deck3.Location = new System.Drawing.Point(1130, 77);
            this.deck3.MinimumSize = new System.Drawing.Size(0, 480);
            this.deck3.Name = "deck3";
            this.deck3.Size = new System.Drawing.Size(75, 480);
            this.deck3.TabIndex = 10;
            // 
            // deck4
            // 
            this.deck4.infoCard = null;
            this.deck4.Location = new System.Drawing.Point(1211, 77);
            this.deck4.MinimumSize = new System.Drawing.Size(0, 480);
            this.deck4.Name = "deck4";
            this.deck4.Size = new System.Drawing.Size(75, 480);
            this.deck4.TabIndex = 11;
            // 
            // infoCard3
            // 
            this.infoCard3.AutoSize = true;
            this.infoCard3.deck = null;
            this.infoCard3.Location = new System.Drawing.Point(12, 346);
            this.infoCard3.MinimumSize = new System.Drawing.Size(500, 0);
            this.infoCard3.Name = "infoCard3";
            this.infoCard3.Size = new System.Drawing.Size(950, 140);
            this.infoCard3.TabIndex = 12;
            // 
            // infoCard4
            // 
            this.infoCard4.AutoSize = true;
            this.infoCard4.deck = null;
            this.infoCard4.Location = new System.Drawing.Point(12, 492);
            this.infoCard4.MinimumSize = new System.Drawing.Size(500, 0);
            this.infoCard4.Name = "infoCard4";
            this.infoCard4.Size = new System.Drawing.Size(950, 140);
            this.infoCard4.TabIndex = 13;
            // 
            // select1
            // 
            this.select1.Image = global::AudioMixerApp.Properties.Resources.select;
            this.select1.Location = new System.Drawing.Point(968, 21);
            this.select1.Name = "select1";
            this.select1.Size = new System.Drawing.Size(156, 50);
            this.select1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.select1.TabIndex = 14;
            this.select1.TabStop = false;
            this.select1.Visible = false;
            // 
            // select2
            // 
            this.select2.Image = global::AudioMixerApp.Properties.Resources.select;
            this.select2.Location = new System.Drawing.Point(1049, 21);
            this.select2.Name = "select2";
            this.select2.Size = new System.Drawing.Size(156, 50);
            this.select2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.select2.TabIndex = 15;
            this.select2.TabStop = false;
            this.select2.Visible = false;
            // 
            // select3
            // 
            this.select3.Image = global::AudioMixerApp.Properties.Resources.select;
            this.select3.Location = new System.Drawing.Point(1130, 21);
            this.select3.Name = "select3";
            this.select3.Size = new System.Drawing.Size(156, 50);
            this.select3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.select3.TabIndex = 16;
            this.select3.TabStop = false;
            this.select3.Visible = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(548, 12);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(89, 15);
            this.label3.TabIndex = 17;
            this.label3.Text = "Audio Source:";
            // 
            // audioSrcComboBox
            // 
            this.audioSrcComboBox.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.audioSrcComboBox.FormattingEnabled = true;
            this.audioSrcComboBox.Items.AddRange(new object[] {
            "Windows",
            "STM32F407G"});
            this.audioSrcComboBox.Location = new System.Drawing.Point(643, 9);
            this.audioSrcComboBox.Name = "audioSrcComboBox";
            this.audioSrcComboBox.Size = new System.Drawing.Size(122, 26);
            this.audioSrcComboBox.TabIndex = 18;
            this.audioSrcComboBox.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.audioSrcComboBox_DrawItem);
            this.audioSrcComboBox.SelectedIndexChanged += new System.EventHandler(this.audioSrcComboBox_SelectedIndexChanged);
            // 
            // mainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1311, 635);
            this.Controls.Add(this.audioSrcComboBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.select3);
            this.Controls.Add(this.select2);
            this.Controls.Add(this.select1);
            this.Controls.Add(this.infoCard4);
            this.Controls.Add(this.infoCard3);
            this.Controls.Add(this.deck4);
            this.Controls.Add(this.deck3);
            this.Controls.Add(this.closeSerialButton);
            this.Controls.Add(this.openSerialButton);
            this.Controls.Add(this.serialSpeed);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.serialLine);
            this.Controls.Add(this.infoCard2);
            this.Controls.Add(this.infoCard1);
            this.Controls.Add(this.deck2);
            this.Controls.Add(this.deck1);
            this.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.ImeMode = System.Windows.Forms.ImeMode.Hiragana;
            this.Name = "mainForm";
            this.Text = "AudioMixer";
            ((System.ComponentModel.ISupportInitialize)(this.select1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.select2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.select3)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Deck deck1;
        private Deck deck2;
        private InfoCard infoCard1;
        private InfoCard infoCard2;
        private System.Windows.Forms.TextBox serialLine;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox serialSpeed;
        private System.Windows.Forms.Button openSerialButton;
        private System.Windows.Forms.Timer serialTimer;
        private System.Windows.Forms.Button closeSerialButton;
        private Deck deck3;
        private Deck deck4;
        private InfoCard infoCard3;
        private InfoCard infoCard4;
        private System.Windows.Forms.PictureBox select1;
        private System.Windows.Forms.PictureBox select2;
        private System.Windows.Forms.PictureBox select3;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox audioSrcComboBox;
    }
}

