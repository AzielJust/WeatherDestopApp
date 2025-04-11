using System.ComponentModel;

namespace Weather_Application_v1
{
    partial class ForecastPanel
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }

            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.ForecastIcon = new System.Windows.Forms.PictureBox();
            this.ForecastDate = new System.Windows.Forms.Label();
            this.ForecastTemp = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.ForecastIcon)).BeginInit();
            this.SuspendLayout();
            // 
            // ForecastIcon
            // 
            this.ForecastIcon.BackColor = System.Drawing.Color.Transparent;
            this.ForecastIcon.Location = new System.Drawing.Point(7, 11);
            this.ForecastIcon.Name = "ForecastIcon";
            this.ForecastIcon.Size = new System.Drawing.Size(70, 67);
            this.ForecastIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.ForecastIcon.TabIndex = 0;
            this.ForecastIcon.TabStop = false;
            // 
            // ForecastDate
            // 
            this.ForecastDate.BackColor = System.Drawing.Color.Transparent;
            this.ForecastDate.Font = new System.Drawing.Font("Roboto", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForecastDate.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.ForecastDate.Location = new System.Drawing.Point(95, 16);
            this.ForecastDate.Name = "ForecastDate";
            this.ForecastDate.Size = new System.Drawing.Size(141, 33);
            this.ForecastDate.TabIndex = 1;
            this.ForecastDate.Text = "[Date]";
            this.ForecastDate.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ForecastTemp
            // 
            this.ForecastTemp.Font = new System.Drawing.Font("Roboto", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForecastTemp.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.ForecastTemp.Location = new System.Drawing.Point(97, 45);
            this.ForecastTemp.Name = "ForecastTemp";
            this.ForecastTemp.Size = new System.Drawing.Size(139, 33);
            this.ForecastTemp.TabIndex = 2;
            this.ForecastTemp.Text = "92/24";
            this.ForecastTemp.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ForecastPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(43)))), ((int)(((byte)(59)))));
            this.Controls.Add(this.ForecastTemp);
            this.Controls.Add(this.ForecastDate);
            this.Controls.Add(this.ForecastIcon);
            this.Name = "ForecastPanel";
            this.Size = new System.Drawing.Size(251, 88);
            ((System.ComponentModel.ISupportInitialize)(this.ForecastIcon)).EndInit();
            this.ResumeLayout(false);
        }

        public System.Windows.Forms.Label ForecastTemp;

        public System.Windows.Forms.PictureBox ForecastIcon;
        public System.Windows.Forms.Label ForecastDate;

        #endregion
    }
}