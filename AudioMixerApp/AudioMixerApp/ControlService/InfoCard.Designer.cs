
namespace AudioMixerApp
{
    partial class InfoCard
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
            this.title = new System.Windows.Forms.Label();
            this.waveform = new AudioMixerApp.ControlService.WaveForm();
            this.timeLabel = new System.Windows.Forms.Label();
            this.durationLabel = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // title
            // 
            this.title.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.title.Location = new System.Drawing.Point(3, 3);
            this.title.Name = "title";
            this.title.Size = new System.Drawing.Size(86, 23);
            this.title.TabIndex = 1;
            this.title.Text = "Track";
            // 
            // waveform
            // 
            this.waveform.BackColor = System.Drawing.SystemColors.ControlDark;
            this.waveform.Location = new System.Drawing.Point(95, 3);
            this.waveform.Name = "waveform";
            this.waveform.SamplesPerPixel = 128;
            this.waveform.Size = new System.Drawing.Size(714, 166);
            this.waveform.StartPosition = ((long)(0));
            this.waveform.TabIndex = 2;
            this.waveform.WaveStream = null;
            // 
            // timeLabel
            // 
            this.timeLabel.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.timeLabel.ForeColor = System.Drawing.Color.Blue;
            this.timeLabel.Location = new System.Drawing.Point(4, 50);
            this.timeLabel.Name = "timeLabel";
            this.timeLabel.Size = new System.Drawing.Size(66, 24);
            this.timeLabel.TabIndex = 3;
            this.timeLabel.Text = "00.00.00";
            // 
            // durationLabel
            // 
            this.durationLabel.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.durationLabel.ForeColor = System.Drawing.Color.Blue;
            this.durationLabel.Location = new System.Drawing.Point(4, 98);
            this.durationLabel.Name = "durationLabel";
            this.durationLabel.Size = new System.Drawing.Size(66, 24);
            this.durationLabel.TabIndex = 4;
            this.durationLabel.Text = "00.00.00";
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(4, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(86, 24);
            this.label1.TabIndex = 5;
            this.label1.Text = "Elapsed";
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(4, 74);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(86, 24);
            this.label2.TabIndex = 6;
            this.label2.Text = "Duartion";
            // 
            // InfoCard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.durationLabel);
            this.Controls.Add(this.timeLabel);
            this.Controls.Add(this.waveform);
            this.Controls.Add(this.title);
            this.Name = "InfoCard";
            this.Size = new System.Drawing.Size(812, 172);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label title;
        private ControlService.WaveForm waveform;
        private System.Windows.Forms.Label timeLabel;
        private System.Windows.Forms.Label durationLabel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}
