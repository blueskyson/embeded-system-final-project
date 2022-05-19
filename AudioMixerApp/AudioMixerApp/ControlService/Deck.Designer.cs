
namespace AudioMixerApp
{
    partial class Deck
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

        #region 元件設計工具產生的程式碼

        /// <summary> 
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器修改
        /// 這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            this.pauseIcon = new System.Windows.Forms.PictureBox();
            this.playIcon = new System.Windows.Forms.PictureBox();
            this.loadButton = new System.Windows.Forms.Button();
            this.volumnTrackbar = new System.Windows.Forms.TrackBar();
            ((System.ComponentModel.ISupportInitialize)(this.pauseIcon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.playIcon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.volumnTrackbar)).BeginInit();
            this.SuspendLayout();
            // 
            // pauseIcon
            // 
            this.pauseIcon.Image = global::AudioMixerApp.Properties.Resources.pause2;
            this.pauseIcon.InitialImage = global::AudioMixerApp.Properties.Resources.play;
            this.pauseIcon.Location = new System.Drawing.Point(10, 342);
            this.pauseIcon.Name = "pauseIcon";
            this.pauseIcon.Size = new System.Drawing.Size(51, 51);
            this.pauseIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pauseIcon.TabIndex = 4;
            this.pauseIcon.TabStop = false;
            this.pauseIcon.Click += new System.EventHandler(this.pauseIcon_Click);
            // 
            // playIcon
            // 
            this.playIcon.BackColor = System.Drawing.SystemColors.Control;
            this.playIcon.Image = global::AudioMixerApp.Properties.Resources.play;
            this.playIcon.InitialImage = global::AudioMixerApp.Properties.Resources.play;
            this.playIcon.Location = new System.Drawing.Point(10, 284);
            this.playIcon.Name = "playIcon";
            this.playIcon.Size = new System.Drawing.Size(51, 52);
            this.playIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.playIcon.TabIndex = 3;
            this.playIcon.TabStop = false;
            this.playIcon.Click += new System.EventHandler(this.playIcon_Click);
            // 
            // loadButton
            // 
            this.loadButton.Location = new System.Drawing.Point(11, 399);
            this.loadButton.Name = "loadButton";
            this.loadButton.Size = new System.Drawing.Size(50, 53);
            this.loadButton.TabIndex = 5;
            this.loadButton.Text = "open wav";
            this.loadButton.UseVisualStyleBackColor = true;
            this.loadButton.Click += new System.EventHandler(this.loadButton_Click);
            // 
            // volumnTrackbar
            // 
            this.volumnTrackbar.BackColor = System.Drawing.SystemColors.Control;
            this.volumnTrackbar.Cursor = System.Windows.Forms.Cursors.Default;
            this.volumnTrackbar.Location = new System.Drawing.Point(10, 3);
            this.volumnTrackbar.Maximum = 500;
            this.volumnTrackbar.Name = "volumnTrackbar";
            this.volumnTrackbar.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.volumnTrackbar.Size = new System.Drawing.Size(56, 275);
            this.volumnTrackbar.TabIndex = 1;
            this.volumnTrackbar.TickFrequency = 5;
            this.volumnTrackbar.TickStyle = System.Windows.Forms.TickStyle.Both;
            this.volumnTrackbar.ValueChanged += new System.EventHandler(this.volumnTrackbar_ValueChanged);
            // 
            // Deck
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.loadButton);
            this.Controls.Add(this.volumnTrackbar);
            this.Controls.Add(this.pauseIcon);
            this.Controls.Add(this.playIcon);
            this.Name = "Deck";
            this.Size = new System.Drawing.Size(75, 479);
            this.Load += new System.EventHandler(this.Deck_Load);
            this.ControlRemoved += new System.Windows.Forms.ControlEventHandler(this.Deck_ControlRemoved);
            ((System.ComponentModel.ISupportInitialize)(this.pauseIcon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.playIcon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.volumnTrackbar)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pauseIcon;
        private System.Windows.Forms.PictureBox playIcon;
        private System.Windows.Forms.Button loadButton;
        private System.Windows.Forms.TrackBar volumnTrackbar;
    }
}
