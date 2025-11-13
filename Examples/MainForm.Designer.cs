namespace Examples
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            txtResult = new RichTextBox();
            btnExamples = new Button();
            SuspendLayout();
            // 
            // txtResult
            // 
            txtResult.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            txtResult.Location = new Point(12, 41);
            txtResult.Name = "txtResult";
            txtResult.Size = new Size(562, 276);
            txtResult.TabIndex = 0;
            txtResult.Text = "";
            // 
            // btnExamples
            // 
            btnExamples.Location = new Point(12, 12);
            btnExamples.Name = "btnExamples";
            btnExamples.Size = new Size(75, 23);
            btnExamples.TabIndex = 1;
            btnExamples.Text = "&Examples";
            btnExamples.UseVisualStyleBackColor = true;
            btnExamples.Click += btnExamples_Click;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(586, 329);
            Controls.Add(btnExamples);
            Controls.Add(txtResult);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Name = "MainForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Main ";
            WindowState = FormWindowState.Maximized;
            ResumeLayout(false);
        }

        #endregion

        private RichTextBox txtResult;
        private Button btnExamples;
    }
}
