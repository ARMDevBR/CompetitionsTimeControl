namespace CompetitionsTimeControl
{
    partial class AboutForm
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
            LinkLabelIcons8 = new LinkLabel();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            LinkLabelCopilotDesigner = new LinkLabel();
            label5 = new Label();
            PictureBoxLogo = new PictureBox();
            LblSeparator1 = new Label();
            LblSoftwareNameAndVersion = new Label();
            ButtonOkClose = new Button();
            label6 = new Label();
            ((System.ComponentModel.ISupportInitialize)PictureBoxLogo).BeginInit();
            SuspendLayout();
            // 
            // LinkLabelIcons8
            // 
            LinkLabelIcons8.AutoSize = true;
            LinkLabelIcons8.Location = new Point(180, 425);
            LinkLabelIcons8.Name = "LinkLabelIcons8";
            LinkLabelIcons8.Size = new Size(159, 15);
            LinkLabelIcons8.TabIndex = 0;
            LinkLabelIcons8.TabStop = true;
            LinkLabelIcons8.Text = "Exemplo de ícones utilizados";
            LinkLabelIcons8.LinkClicked += LinkLabelIcons8_LinkClicked;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(87, 320);
            label1.Name = "label1";
            label1.Size = new Size(327, 15);
            label1.TabIndex = 1;
            label1.Text = "Software desenvolvido por ADEMILSON RODRIGO MOREIRA.";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(63, 340);
            label2.Name = "label2";
            label2.Size = new Size(375, 15);
            label2.TabIndex = 2;
            label2.Text = "Com base na concepção inicial de LEANDRO MEDEIROS DE ALMEIDA.";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(50, 405);
            label3.Name = "label3";
            label3.Size = new Size(133, 15);
            label3.TabIndex = 3;
            label3.Text = "Logotipo gerado por IA:";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(50, 380);
            label4.Name = "label4";
            label4.Size = new Size(123, 15);
            label4.TabIndex = 4;
            label4.Text = "RECURSOS EXTERNOS";
            // 
            // LinkLabelCopilotDesigner
            // 
            LinkLabelCopilotDesigner.AutoSize = true;
            LinkLabelCopilotDesigner.Location = new Point(244, 405);
            LinkLabelCopilotDesigner.Name = "LinkLabelCopilotDesigner";
            LinkLabelCopilotDesigner.Size = new Size(95, 15);
            LinkLabelCopilotDesigner.TabIndex = 5;
            LinkLabelCopilotDesigner.TabStop = true;
            LinkLabelCopilotDesigner.Text = "Copilot Designer";
            LinkLabelCopilotDesigner.LinkClicked += LinkLabelCopilotDesigner_LinkClicked;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(50, 425);
            label5.Name = "label5";
            label5.Size = new Size(122, 15);
            label5.TabIndex = 6;
            label5.Text = "Site de ícones Icons 8:";
            // 
            // PictureBoxLogo
            // 
            PictureBoxLogo.Image = Properties.Resources.ProgramLogo;
            PictureBoxLogo.Location = new Point(125, 1);
            PictureBoxLogo.Name = "PictureBoxLogo";
            PictureBoxLogo.Size = new Size(250, 250);
            PictureBoxLogo.SizeMode = PictureBoxSizeMode.StretchImage;
            PictureBoxLogo.TabIndex = 7;
            PictureBoxLogo.TabStop = false;
            // 
            // LblSeparator1
            // 
            LblSeparator1.BorderStyle = BorderStyle.Fixed3D;
            LblSeparator1.Location = new Point(50, 307);
            LblSeparator1.Name = "LblSeparator1";
            LblSeparator1.Size = new Size(400, 2);
            LblSeparator1.TabIndex = 8;
            // 
            // LblSoftwareNameAndVersion
            // 
            LblSoftwareNameAndVersion.AutoSize = true;
            LblSoftwareNameAndVersion.Location = new Point(76, 260);
            LblSoftwareNameAndVersion.Name = "LblSoftwareNameAndVersion";
            LblSoftwareNameAndVersion.Size = new Size(348, 15);
            LblSoftwareNameAndVersion.TabIndex = 9;
            LblSoftwareNameAndVersion.Text = "Controle de Tempo para Competições Intervaladas - Versão: 1.1.1";
            // 
            // ButtonOkClose
            // 
            ButtonOkClose.Location = new Point(413, 442);
            ButtonOkClose.Name = "ButtonOkClose";
            ButtonOkClose.Size = new Size(75, 23);
            ButtonOkClose.TabIndex = 10;
            ButtonOkClose.Text = "OK";
            ButtonOkClose.UseVisualStyleBackColor = true;
            ButtonOkClose.Click += ButtonOkClose_Click;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(163, 280);
            label6.Name = "label6";
            label6.Size = new Size(174, 15);
            label6.TabIndex = 11;
            label6.Text = "Software Livre - Licença GPL 3.0";
            // 
            // AboutForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(500, 477);
            Controls.Add(label6);
            Controls.Add(ButtonOkClose);
            Controls.Add(LblSoftwareNameAndVersion);
            Controls.Add(LblSeparator1);
            Controls.Add(PictureBoxLogo);
            Controls.Add(label5);
            Controls.Add(LinkLabelCopilotDesigner);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label1);
            Controls.Add(label2);
            Controls.Add(LinkLabelIcons8);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            Name = "AboutForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "SOBRE";
            ((System.ComponentModel.ISupportInitialize)PictureBoxLogo).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private LinkLabel LinkLabelIcons8;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private LinkLabel LinkLabelCopilotDesigner;
        private Label label5;
        private PictureBox PictureBoxLogo;
        private Label LblSeparator1;
        private Label LblSoftwareNameAndVersion;
        private Button ButtonOkClose;
        private Label label6;
    }
}