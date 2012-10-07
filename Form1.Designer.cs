namespace SimplexMethodSolver
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
            this.tbVariables = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tbConstrains = new System.Windows.Forms.TextBox();
            this.btnCreateTbx = new System.Windows.Forms.Button();
            this.rtbResult = new System.Windows.Forms.RichTextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // tbVariables
            // 
            this.tbVariables.Location = new System.Drawing.Point(12, 35);
            this.tbVariables.Name = "tbVariables";
            this.tbVariables.Size = new System.Drawing.Size(100, 20);
            this.tbVariables.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(132, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Количество переменных";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(157, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(133, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Количество ограничений";
            // 
            // tbConstrains
            // 
            this.tbConstrains.Location = new System.Drawing.Point(160, 35);
            this.tbConstrains.Name = "tbConstrains";
            this.tbConstrains.Size = new System.Drawing.Size(100, 20);
            this.tbConstrains.TabIndex = 3;
            // 
            // btnCreateTbx
            // 
            this.btnCreateTbx.Location = new System.Drawing.Point(317, 12);
            this.btnCreateTbx.Name = "btnCreateTbx";
            this.btnCreateTbx.Size = new System.Drawing.Size(85, 35);
            this.btnCreateTbx.TabIndex = 4;
            this.btnCreateTbx.Text = "Построить сетку";
            this.btnCreateTbx.UseVisualStyleBackColor = true;
            this.btnCreateTbx.Click += new System.EventHandler(this.btnCreateTbx_Click);
            // 
            // rtbResult
            // 
            this.rtbResult.Location = new System.Drawing.Point(317, 83);
            this.rtbResult.Name = "rtbResult";
            this.rtbResult.Size = new System.Drawing.Size(217, 107);
            this.rtbResult.TabIndex = 6;
            this.rtbResult.Text = "";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(318, 62);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(128, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Рассчитанное значение";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(574, 353);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.rtbResult);
            this.Controls.Add(this.btnCreateTbx);
            this.Controls.Add(this.tbConstrains);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tbVariables);
            this.Name = "Form1";
            this.Text = "SimplexSolver";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbVariables;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbConstrains;
        private System.Windows.Forms.Button btnCreateTbx;
        private System.Windows.Forms.RichTextBox rtbResult;
        private System.Windows.Forms.Label label3;
    }
}

