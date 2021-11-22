
namespace HourBoxDemoApp
{
    partial class DemonstrationForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DemonstrationForm));
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.hourBox4 = new HourBoxControl.HourBox();
            this.hourBox3 = new HourBoxControl.HourBox();
            this.hourBox2 = new HourBoxControl.HourBox();
            this.hourBox1 = new HourBoxControl.HourBox();
            this.SuspendLayout();
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label7.Location = new System.Drawing.Point(35, 320);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(303, 17);
            this.label7.TabIndex = 19;
            this.label7.Text = "•ToolTipOnFocus - displays a tooltip when focused";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label6.Location = new System.Drawing.Point(35, 270);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(370, 17);
            this.label6.TabIndex = 18;
            this.label6.Text = "• ShowMaskOnlyOnFocus - hides the mask if the field is empty";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label5.Location = new System.Drawing.Point(14, 199);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(311, 21);
            this.label5.TabIndex = 17;
            this.label5.Text = "Additional settings (in misc properties):";
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label4.Location = new System.Drawing.Point(17, 109);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(396, 76);
            this.label4.TabIndex = 16;
            this.label4.Text = resources.GetString("label4.Text");
            // 
            // linkLabel1
            // 
            this.linkLabel1.AccessibleDescription = "mooshmores github profile";
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.linkLabel1.LinkColor = System.Drawing.Color.Black;
            this.linkLabel1.Location = new System.Drawing.Point(139, 27);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(95, 21);
            this.linkLabel1.TabIndex = 15;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "mooshmore";
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LinkLabel1_LinkClicked);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label3.Location = new System.Drawing.Point(122, 29);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(22, 17);
            this.label3.TabIndex = 14;
            this.label3.Text = "by";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label2.Location = new System.Drawing.Point(12, 18);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(116, 32);
            this.label2.TabIndex = 13;
            this.label2.Text = "HourBox";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label1.Location = new System.Drawing.Point(35, 220);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(337, 17);
            this.label1.TabIndex = 12;
            this.label1.Text = "• HourLeadingZero - option to enable hour leading zero";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(164, 77);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(90, 13);
            this.label11.TabIndex = 31;
            this.label11.Text = "TimeSpan value:";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(164, 60);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(38, 13);
            this.label10.TabIndex = 30;
            this.label10.Text = "Value:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(260, 77);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(44, 13);
            this.label9.TabIndex = 29;
            this.label9.Text = "label12";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(260, 60);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(44, 13);
            this.label8.TabIndex = 28;
            this.label8.Text = "label13";
            // 
            // hourBox4
            // 
            this.hourBox4.Location = new System.Drawing.Point(54, 342);
            this.hourBox4.Mask = "00:00";
            this.hourBox4.Name = "hourBox4";
            this.hourBox4.PromptChar = ' ';
            this.hourBox4.ShowMaskOnlyOnFocus = false;
            this.hourBox4.Size = new System.Drawing.Size(100, 22);
            this.hourBox4.TabIndex = 23;
            this.hourBox4.TimeSpan = null;
            this.hourBox4.ToolTipOnFocus = "Yep, I\'m a tooltip.";
            this.hourBox4.Value = "";
            // 
            // hourBox3
            // 
            this.hourBox3.Location = new System.Drawing.Point(54, 292);
            this.hourBox3.Mask = "00:00";
            this.hourBox3.Name = "hourBox3";
            this.hourBox3.PromptChar = ' ';
            this.hourBox3.ShowMaskOnlyOnFocus = true;
            this.hourBox3.Size = new System.Drawing.Size(100, 22);
            this.hourBox3.TabIndex = 22;
            this.hourBox3.TimeSpan = null;
            this.hourBox3.ToolTipOnFocus = null;
            this.hourBox3.Value = "";
            // 
            // hourBox2
            // 
            this.hourBox2.HourLeadingZero = true;
            this.hourBox2.Location = new System.Drawing.Point(54, 242);
            this.hourBox2.Mask = "00:00";
            this.hourBox2.Name = "hourBox2";
            this.hourBox2.PromptChar = ' ';
            this.hourBox2.ShowMaskOnlyOnFocus = false;
            this.hourBox2.Size = new System.Drawing.Size(100, 22);
            this.hourBox2.TabIndex = 21;
            this.hourBox2.TimeSpan = null;
            this.hourBox2.ToolTipOnFocus = null;
            this.hourBox2.Value = "";
            // 
            // hourBox1
            // 
            this.hourBox1.Location = new System.Drawing.Point(28, 68);
            this.hourBox1.Mask = "00:00";
            this.hourBox1.Name = "hourBox1";
            this.hourBox1.PromptChar = ' ';
            this.hourBox1.ShowMaskOnlyOnFocus = false;
            this.hourBox1.Size = new System.Drawing.Size(100, 22);
            this.hourBox1.TabIndex = 20;
            this.hourBox1.TimeSpan = null;
            this.hourBox1.ToolTipOnFocus = null;
            this.hourBox1.Value = "";
            this.hourBox1.TextChanged += new System.EventHandler(this.hourBox1_TextChanged);
            // 
            // DemonstrationForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(425, 434);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.hourBox4);
            this.Controls.Add(this.hourBox3);
            this.Controls.Add(this.hourBox2);
            this.Controls.Add(this.hourBox1);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.linkLabel1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.Name = "DemonstrationForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "HourBox demo";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private HourBoxControl.HourBox hourBox1;
        private HourBoxControl.HourBox hourBox2;
        private HourBoxControl.HourBox hourBox3;
        private HourBoxControl.HourBox hourBox4;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
    }
}

