
namespace Triangulation
{
    partial class Form2
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
            this.pointGenerationButton = new System.Windows.Forms.Button();
            this.textBoxWithAmountOfPoints = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.triangulationConstructionButton = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pointGenerationButton
            // 
            this.pointGenerationButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.pointGenerationButton.Location = new System.Drawing.Point(179, 392);
            this.pointGenerationButton.Name = "pointGenerationButton";
            this.pointGenerationButton.Size = new System.Drawing.Size(145, 46);
            this.pointGenerationButton.TabIndex = 0;
            this.pointGenerationButton.Text = "Сгенерировать точки";
            this.pointGenerationButton.UseVisualStyleBackColor = true;
            this.pointGenerationButton.Click += new System.EventHandler(this.pointGenerationButton_Click);
            // 
            // textBoxWithAmountOfPoints
            // 
            this.textBoxWithAmountOfPoints.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.textBoxWithAmountOfPoints.Location = new System.Drawing.Point(12, 414);
            this.textBoxWithAmountOfPoints.Multiline = true;
            this.textBoxWithAmountOfPoints.Name = "textBoxWithAmountOfPoints";
            this.textBoxWithAmountOfPoints.Size = new System.Drawing.Size(132, 24);
            this.textBoxWithAmountOfPoints.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 392);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(151, 15);
            this.label1.TabIndex = 2;
            this.label1.Text = "Введите количество точек";
            // 
            // triangulationConstructionButton
            // 
            this.triangulationConstructionButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.triangulationConstructionButton.Location = new System.Drawing.Point(617, 392);
            this.triangulationConstructionButton.Name = "triangulationConstructionButton";
            this.triangulationConstructionButton.Size = new System.Drawing.Size(152, 46);
            this.triangulationConstructionButton.TabIndex = 3;
            this.triangulationConstructionButton.Text = "Построить триангуляцию ";
            this.triangulationConstructionButton.UseVisualStyleBackColor = true;
            this.triangulationConstructionButton.Click += new System.EventHandler(this.triangulationConstructionButton_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox1.Location = new System.Drawing.Point(12, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(776, 374);
            this.pictureBox1.TabIndex = 4;
            this.pictureBox1.TabStop = false;
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.triangulationConstructionButton);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBoxWithAmountOfPoints);
            this.Controls.Add(this.pointGenerationButton);
            this.Name = "Form2";
            this.Text = "Form2";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button pointGenerationButton;
        private System.Windows.Forms.TextBox textBoxWithAmountOfPoints;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button triangulationConstructionButton;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}