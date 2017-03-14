namespace StationCalculator
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
            this.minCostLabel = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.ULLabel = new System.Windows.Forms.Label();
            this.UMLable = new System.Windows.Forms.Label();
            this.URLabel = new System.Windows.Forms.Label();
            this.DCLabel = new System.Windows.Forms.Label();
            this.DLLabel = new System.Windows.Forms.Label();
            this.DRLabel = new System.Windows.Forms.Label();
            this.positionLabel = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.currentTimeLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // minCostLabel
            // 
            this.minCostLabel.AutoSize = true;
            this.minCostLabel.Location = new System.Drawing.Point(369, 24);
            this.minCostLabel.Name = "minCostLabel";
            this.minCostLabel.Size = new System.Drawing.Size(47, 13);
            this.minCostLabel.TabIndex = 0;
            this.minCostLabel.Text = "MinTime";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(41, 104);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(49, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "First mag";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(23, 188);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Second mag";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(96, 52);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(530, 172);
            this.pictureBox1.TabIndex = 3;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBox1_Paint);
            // 
            // pictureBox2
            // 
            this.pictureBox2.Location = new System.Drawing.Point(632, 52);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(309, 171);
            this.pictureBox2.TabIndex = 4;
            this.pictureBox2.TabStop = false;
            this.pictureBox2.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBox2_Paint);
            // 
            // ULLabel
            // 
            this.ULLabel.AutoSize = true;
            this.ULLabel.Location = new System.Drawing.Point(661, 79);
            this.ULLabel.Name = "ULLabel";
            this.ULLabel.Size = new System.Drawing.Size(13, 13);
            this.ULLabel.TabIndex = 5;
            this.ULLabel.Text = "1";
            // 
            // UMLable
            // 
            this.UMLable.AutoSize = true;
            this.UMLable.Location = new System.Drawing.Point(711, 79);
            this.UMLable.Name = "UMLable";
            this.UMLable.Size = new System.Drawing.Size(13, 13);
            this.UMLable.TabIndex = 6;
            this.UMLable.Text = "2";
            // 
            // URLabel
            // 
            this.URLabel.AutoSize = true;
            this.URLabel.Location = new System.Drawing.Point(759, 79);
            this.URLabel.Name = "URLabel";
            this.URLabel.Size = new System.Drawing.Size(13, 13);
            this.URLabel.TabIndex = 7;
            this.URLabel.Text = "3";
            // 
            // DCLabel
            // 
            this.DCLabel.AutoSize = true;
            this.DCLabel.Location = new System.Drawing.Point(711, 141);
            this.DCLabel.Name = "DCLabel";
            this.DCLabel.Size = new System.Drawing.Size(13, 13);
            this.DCLabel.TabIndex = 8;
            this.DCLabel.Text = "5";
            // 
            // DLLabel
            // 
            this.DLLabel.AutoSize = true;
            this.DLLabel.Location = new System.Drawing.Point(661, 141);
            this.DLLabel.Name = "DLLabel";
            this.DLLabel.Size = new System.Drawing.Size(13, 13);
            this.DLLabel.TabIndex = 9;
            this.DLLabel.Text = "4";
            // 
            // DRLabel
            // 
            this.DRLabel.AutoSize = true;
            this.DRLabel.Location = new System.Drawing.Point(759, 141);
            this.DRLabel.Name = "DRLabel";
            this.DRLabel.Size = new System.Drawing.Size(13, 13);
            this.DRLabel.TabIndex = 10;
            this.DRLabel.Text = "6";
            // 
            // positionLabel
            // 
            this.positionLabel.AutoSize = true;
            this.positionLabel.Location = new System.Drawing.Point(629, 25);
            this.positionLabel.Name = "positionLabel";
            this.positionLabel.Size = new System.Drawing.Size(121, 13);
            this.positionLabel.TabIndex = 11;
            this.positionLabel.Text = "Perfect position number:";
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(813, 22);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(73, 21);
            this.comboBox1.TabIndex = 12;
            this.comboBox1.TextChanged += new System.EventHandler(this.comboBox1_TextChanged);
            // 
            // currentTimeLabel
            // 
            this.currentTimeLabel.AutoSize = true;
            this.currentTimeLabel.Location = new System.Drawing.Point(829, 92);
            this.currentTimeLabel.Name = "currentTimeLabel";
            this.currentTimeLabel.Size = new System.Drawing.Size(122, 13);
            this.currentTimeLabel.TabIndex = 13;
            this.currentTimeLabel.Text = "Time for current variant: ";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(972, 312);
            this.Controls.Add(this.currentTimeLabel);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.positionLabel);
            this.Controls.Add(this.DRLabel);
            this.Controls.Add(this.DLLabel);
            this.Controls.Add(this.DCLabel);
            this.Controls.Add(this.URLabel);
            this.Controls.Add(this.UMLable);
            this.Controls.Add(this.ULLabel);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.minCostLabel);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label minCostLabel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Label ULLabel;
        private System.Windows.Forms.Label UMLable;
        private System.Windows.Forms.Label URLabel;
        private System.Windows.Forms.Label DCLabel;
        private System.Windows.Forms.Label DLLabel;
        private System.Windows.Forms.Label DRLabel;
        private System.Windows.Forms.Label positionLabel;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label currentTimeLabel;
    }
}

