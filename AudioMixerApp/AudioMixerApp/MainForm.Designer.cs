
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
            this.deck1 = new AudioMixerApp.Deck();
            this.deck2 = new AudioMixerApp.Deck();
            this.SuspendLayout();
            // 
            // deck1
            // 
            this.deck1.Id = 0;
            this.deck1.Location = new System.Drawing.Point(0, 0);
            this.deck1.Name = "deck1";
            this.deck1.Size = new System.Drawing.Size(75, 479);
            this.deck1.TabIndex = 0;
            // 
            // deck2
            // 
            this.deck2.Id = 0;
            this.deck2.Location = new System.Drawing.Point(81, 0);
            this.deck2.Name = "deck2";
            this.deck2.Size = new System.Drawing.Size(75, 479);
            this.deck2.TabIndex = 1;
            // 
            // mainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(169, 489);
            this.Controls.Add(this.deck2);
            this.Controls.Add(this.deck1);
            this.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.Name = "mainForm";
            this.Text = "AudioMixer";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private Deck deck1;
        private Deck deck2;
    }
}

