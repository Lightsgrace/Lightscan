namespace Lightscan
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.btnScanNow = new System.Windows.Forms.Button();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtPath = new System.Windows.Forms.TextBox();
            this.btnProcessScan = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnScanNow
            // 
            this.btnScanNow.Location = new System.Drawing.Point(382, 15);
            this.btnScanNow.Name = "btnScanNow";
            this.btnScanNow.Size = new System.Drawing.Size(85, 28);
            this.btnScanNow.TabIndex = 0;
            this.btnScanNow.Text = "File Scan...";
            this.btnScanNow.UseVisualStyleBackColor = true;
            this.btnScanNow.Click += new System.EventHandler(this.btnScanNow_Click);
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(12, 68);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(773, 370);
            this.richTextBox1.TabIndex = 1;
            this.richTextBox1.Text = "";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(25, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(36, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Folder";
            // 
            // txtPath
            // 
            this.txtPath.Location = new System.Drawing.Point(67, 20);
            this.txtPath.Name = "txtPath";
            this.txtPath.Size = new System.Drawing.Size(309, 20);
            this.txtPath.TabIndex = 3;
            this.txtPath.Text = "C:\\";
            // 
            // btnProcessScan
            // 
            this.btnProcessScan.Location = new System.Drawing.Point(473, 15);
            this.btnProcessScan.Name = "btnProcessScan";
            this.btnProcessScan.Size = new System.Drawing.Size(97, 28);
            this.btnProcessScan.TabIndex = 4;
            this.btnProcessScan.Text = "Process Scan...";
            this.btnProcessScan.UseVisualStyleBackColor = true;
            this.btnProcessScan.Click += new System.EventHandler(this.btnProcessScan_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnProcessScan);
            this.Controls.Add(this.txtPath);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.btnScanNow);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Lightscan";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnScanNow;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtPath;
        private System.Windows.Forms.Button btnProcessScan;
    }
}

