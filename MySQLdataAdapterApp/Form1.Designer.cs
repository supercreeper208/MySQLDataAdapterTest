namespace MySQLdataAdapterApp
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
            this.button1 = new System.Windows.Forms.Button();
            this.DgvProducten = new System.Windows.Forms.DataGridView();
            this.BtnUpdateTabel = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.DgvProducten)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 387);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(178, 51);
            this.button1.TabIndex = 0;
            this.button1.Text = "Select * from producten";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // DgvProducten
            // 
            this.DgvProducten.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.DgvProducten.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DgvProducten.Location = new System.Drawing.Point(12, 12);
            this.DgvProducten.Name = "DgvProducten";
            this.DgvProducten.RowHeadersWidth = 51;
            this.DgvProducten.RowTemplate.Height = 24;
            this.DgvProducten.Size = new System.Drawing.Size(1023, 369);
            this.DgvProducten.TabIndex = 1;
            // 
            // BtnUpdateTabel
            // 
            this.BtnUpdateTabel.Location = new System.Drawing.Point(197, 388);
            this.BtnUpdateTabel.Name = "BtnUpdateTabel";
            this.BtnUpdateTabel.Size = new System.Drawing.Size(198, 50);
            this.BtnUpdateTabel.TabIndex = 2;
            this.BtnUpdateTabel.Text = "Update wijzigingen";
            this.BtnUpdateTabel.UseVisualStyleBackColor = true;
            this.BtnUpdateTabel.Click += new System.EventHandler(this.BtnUpdateTabel_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1047, 450);
            this.Controls.Add(this.BtnUpdateTabel);
            this.Controls.Add(this.DgvProducten);
            this.Controls.Add(this.button1);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.DgvProducten)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.DataGridView DgvProducten;
        private System.Windows.Forms.Button BtnUpdateTabel;
    }
}

