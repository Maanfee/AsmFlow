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
            btnErrorTest = new Button();
            SuspendLayout();
            // 
            // txtResult
            // 
            txtResult.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            txtResult.Font = new Font("Consolas", 9F, FontStyle.Regular, GraphicsUnit.Point);
            txtResult.Location = new Point(12, 41);
            txtResult.Name = "txtResult";
            txtResult.Size = new Size(762, 376);
            txtResult.TabIndex = 0;
            txtResult.Text = "";
            // 
            // btnExamples
            // 
            btnExamples.Location = new Point(12, 12);
            btnExamples.Name = "btnExamples";
            btnExamples.Size = new Size(120, 23);
            btnExamples.TabIndex = 1;
            btnExamples.Text = "&Run All Examples";
            btnExamples.UseVisualStyleBackColor = true;
            btnExamples.Click += btnExamples_Click;
            // 
            // btnErrorTest
            // 
            btnErrorTest.Location = new Point(138, 12);
            btnErrorTest.Name = "btnErrorTest";
            btnErrorTest.Size = new Size(120, 23);
            btnErrorTest.TabIndex = 2;
            btnErrorTest.Text = "&Test Errors";
            btnErrorTest.UseVisualStyleBackColor = true;
            btnErrorTest.Click += btnErrorTest_Click;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(786, 429);
            Controls.Add(btnErrorTest);
            Controls.Add(btnExamples);
            Controls.Add(txtResult);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Name = "MainForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "AsmFlow Examples";
            WindowState = FormWindowState.Maximized;
            ResumeLayout(false);
        }

        #endregion

        private RichTextBox txtResult;
        private Button btnExamples;
        private Button btnErrorTest;
    }
}