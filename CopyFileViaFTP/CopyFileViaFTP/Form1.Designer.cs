namespace CopyFileViaFTP
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.btn_Config = new System.Windows.Forms.Button();
            this.rtb_Monitor = new System.Windows.Forms.RichTextBox();
            this.btn_Log = new System.Windows.Forms.Button();
            this.btn_Image = new System.Windows.Forms.Button();
            this.bgw_Give_Log = new System.ComponentModel.BackgroundWorker();
            this.tm_Clock = new System.Windows.Forms.Timer(this.components);
            this.lb_Time = new System.Windows.Forms.Label();
            this.tm_Process = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // btn_Config
            // 
            this.btn_Config.Location = new System.Drawing.Point(494, 12);
            this.btn_Config.Name = "btn_Config";
            this.btn_Config.Size = new System.Drawing.Size(127, 49);
            this.btn_Config.TabIndex = 0;
            this.btn_Config.Text = "CONFIG";
            this.btn_Config.UseVisualStyleBackColor = true;
            this.btn_Config.Click += new System.EventHandler(this.btn_Config_Click);
            // 
            // rtb_Monitor
            // 
            this.rtb_Monitor.Location = new System.Drawing.Point(12, 10);
            this.rtb_Monitor.Name = "rtb_Monitor";
            this.rtb_Monitor.Size = new System.Drawing.Size(476, 201);
            this.rtb_Monitor.TabIndex = 1;
            this.rtb_Monitor.Text = "";
            // 
            // btn_Log
            // 
            this.btn_Log.Location = new System.Drawing.Point(494, 88);
            this.btn_Log.Name = "btn_Log";
            this.btn_Log.Size = new System.Drawing.Size(127, 49);
            this.btn_Log.TabIndex = 2;
            this.btn_Log.Text = "GIVE LOG";
            this.btn_Log.UseVisualStyleBackColor = true;
            this.btn_Log.Click += new System.EventHandler(this.btn_Log_Click);
            // 
            // btn_Image
            // 
            this.btn_Image.Location = new System.Drawing.Point(494, 162);
            this.btn_Image.Name = "btn_Image";
            this.btn_Image.Size = new System.Drawing.Size(127, 49);
            this.btn_Image.TabIndex = 3;
            this.btn_Image.Text = "GIVE IMAGE";
            this.btn_Image.UseVisualStyleBackColor = true;
            this.btn_Image.Click += new System.EventHandler(this.btn_Image_Click);
            // 
            // bgw_Give_Log
            // 
            this.bgw_Give_Log.WorkerSupportsCancellation = true;
            // 
            // tm_Clock
            // 
            this.tm_Clock.Interval = 1000;
            this.tm_Clock.Tick += new System.EventHandler(this.tm_Clock_Tick);
            // 
            // lb_Time
            // 
            this.lb_Time.AutoSize = true;
            this.lb_Time.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lb_Time.ForeColor = System.Drawing.Color.Blue;
            this.lb_Time.Location = new System.Drawing.Point(375, 230);
            this.lb_Time.Name = "lb_Time";
            this.lb_Time.Size = new System.Drawing.Size(246, 29);
            this.lb_Time.TabIndex = 4;
            this.lb_Time.Text = "01/01/2021 00:00:01";
            // 
            // tm_Process
            // 
            this.tm_Process.Interval = 1000;
            this.tm_Process.Tick += new System.EventHandler(this.tm_Process_Tick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(631, 278);
            this.Controls.Add(this.lb_Time);
            this.Controls.Add(this.btn_Image);
            this.Controls.Add(this.btn_Log);
            this.Controls.Add(this.rtb_Monitor);
            this.Controls.Add(this.btn_Config);
            this.Name = "Form1";
            this.Text = "FTP";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_Config;
        private System.Windows.Forms.RichTextBox rtb_Monitor;
        private System.Windows.Forms.Button btn_Log;
        private System.Windows.Forms.Button btn_Image;
        private System.ComponentModel.BackgroundWorker bgw_Give_Log;
        private System.Windows.Forms.Timer tm_Clock;
        private System.Windows.Forms.Label lb_Time;
        private System.Windows.Forms.Timer tm_Process;
    }
}

