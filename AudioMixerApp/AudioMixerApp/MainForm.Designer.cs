
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
            this.infoCard2 = new AudioMixerApp.InfoCard();
            this.infoCard1 = new AudioMixerApp.InfoCard();
            this.deck2 = new AudioMixerApp.Deck();
            this.deck1 = new AudioMixerApp.Deck();
            this.SuspendLayout();
            // 
            // serialLine
            // 
            this.serialLine.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.serialLine.Location = new System.Drawing.Point(87, 9);
            this.serialLine.Name = "serialLine";
            this.serialLine.Size = new System.Drawing.Size(100, 25);
            this.serialLine.TabIndex = 4;
            this.serialLine.Text = "COM4";
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
            // infoCard2
            // 
            this.infoCard2.AutoSize = true;
            this.infoCard2.Location = new System.Drawing.Point(12, 261);
            this.infoCard2.MinimumSize = new System.Drawing.Size(500, 0);
            this.infoCard2.Name = "infoCard2";
            this.infoCard2.Size = new System.Drawing.Size(972, 204);
            this.infoCard2.TabIndex = 3;
            // 
            // infoCard1
            // 
            this.infoCard1.AutoSize = true;
            this.infoCard1.Location = new System.Drawing.Point(12, 51);
            this.infoCard1.MinimumSize = new System.Drawing.Size(500, 0);
            this.infoCard1.Name = "infoCard1";
            this.infoCard1.Size = new System.Drawing.Size(972, 204);
            this.infoCard1.TabIndex = 2;
            // 
            // deck2
            // 
            this.deck2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.deck2.infoCard = null;
            this.deck2.Location = new System.Drawing.Point(1071, 12);
            this.deck2.MinimumSize = new System.Drawing.Size(0, 480);
            this.deck2.Name = "deck2";
            this.deck2.Size = new System.Drawing.Size(75, 480);
            this.deck2.TabIndex = 1;
            // 
            // deck1
            // 
            this.deck1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.deck1.infoCard = null;
            this.deck1.Location = new System.Drawing.Point(990, 12);
            this.deck1.MinimumSize = new System.Drawing.Size(0, 480);
            this.deck1.Name = "deck1";
            this.deck1.Size = new System.Drawing.Size(75, 480);
            this.deck1.TabIndex = 0;
            // 
            // mainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1158, 518);
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
            this.Load += new System.EventHandler(this.MainForm_Load);
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
    }
}

