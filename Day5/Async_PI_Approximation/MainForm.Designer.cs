namespace Async_PI_Approximation
{
	partial class MainForm
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
			this.InputBox = new System.Windows.Forms.TextBox();
			this.RunButton = new System.Windows.Forms.Button();
			this.InfoLabel = new System.Windows.Forms.Label();
			this.DrawPanel = new System.Windows.Forms.Panel();
			this.richTextBox1 = new System.Windows.Forms.RichTextBox();
			this.SuspendLayout();
			// 
			// InputBox
			// 
			this.InputBox.Font = new System.Drawing.Font("Roboto", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
			this.InputBox.Location = new System.Drawing.Point(48, 57);
			this.InputBox.Name = "InputBox";
			this.InputBox.Size = new System.Drawing.Size(178, 23);
			this.InputBox.TabIndex = 0;
			this.InputBox.Text = "1000";
			// 
			// RunButton
			// 
			this.RunButton.Font = new System.Drawing.Font("Roboto Medium", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
			this.RunButton.Location = new System.Drawing.Point(232, 51);
			this.RunButton.Name = "RunButton";
			this.RunButton.Size = new System.Drawing.Size(80, 32);
			this.RunButton.TabIndex = 1;
			this.RunButton.Text = "Run";
			this.RunButton.UseVisualStyleBackColor = true;
			this.RunButton.Click += new System.EventHandler(this.RunButton_Click);
			// 
			// InfoLabel
			// 
			this.InfoLabel.AutoSize = true;
			this.InfoLabel.Font = new System.Drawing.Font("Roboto", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
			this.InfoLabel.Location = new System.Drawing.Point(44, 9);
			this.InfoLabel.Name = "InfoLabel";
			this.InfoLabel.Size = new System.Drawing.Size(721, 19);
			this.InfoLabel.TabIndex = 2;
			this.InfoLabel.Text = "Instruction: Input the amount of points, then press Run. It is recommended to use" +
    " at least 1000 points";
			this.InfoLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// DrawPanel
			// 
			this.DrawPanel.Location = new System.Drawing.Point(48, 99);
			this.DrawPanel.Name = "DrawPanel";
			this.DrawPanel.Size = new System.Drawing.Size(400, 400);
			this.DrawPanel.TabIndex = 3;
			this.DrawPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.DrawPanel_Paint);
			// 
			// richTextBox1
			// 
			this.richTextBox1.Font = new System.Drawing.Font("Roboto", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
			this.richTextBox1.Location = new System.Drawing.Point(454, 99);
			this.richTextBox1.Name = "richTextBox1";
			this.richTextBox1.Size = new System.Drawing.Size(334, 400);
			this.richTextBox1.TabIndex = 4;
			this.richTextBox1.Text = "In the meantime, try typing something in here!";
			this.richTextBox1.Enter += new System.EventHandler(this.richTextBox1_Enter);
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(800, 511);
			this.Controls.Add(this.richTextBox1);
			this.Controls.Add(this.DrawPanel);
			this.Controls.Add(this.InfoLabel);
			this.Controls.Add(this.RunButton);
			this.Controls.Add(this.InputBox);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
			this.Name = "MainForm";
			this.Text = "PI Approx";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox InputBox;
		private System.Windows.Forms.Button RunButton;
		private System.Windows.Forms.Label InfoLabel;
		private System.Windows.Forms.Panel DrawPanel;
		private System.Windows.Forms.RichTextBox richTextBox1;
	}
}

