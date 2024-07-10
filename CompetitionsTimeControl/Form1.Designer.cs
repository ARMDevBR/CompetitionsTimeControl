namespace CompetitionsTimeControl
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
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            MusicMediaPlayer = new AxWMPLib.AxWindowsMediaPlayer();
            TBMusicVolumeMin = new TrackBar();
            BtnPlayTest = new Button();
            BtnStopTest = new Button();
            GroupBoxMusicCtrls = new GroupBox();
            BtnClearMusicsList = new Button();
            BtnAddMusics = new Button();
            ListViewMusics = new ListView();
            MusicName = new ColumnHeader();
            MusicLength = new ColumnHeader();
            MusicPath = new ColumnHeader();
            GroupBoxMusicVolume = new GroupBox();
            LblMusicCurrentVolPercent = new Label();
            LblMusicCurrentVol = new Label();
            TBMusicCurrentVol = new TrackBar();
            GroupBoxMusicVols = new GroupBox();
            LblMusicVolMaxPercent = new Label();
            LblMusicVolMinPercent = new Label();
            LblMusicVolMax = new Label();
            TBMusicVolumeMax = new TrackBar();
            LblMusicVolMin = new Label();
            LblTempStatus = new Label();
            ToolTip = new ToolTip(components);
            LblBeepPair = new Label();
            LblTimeBeforePlayBeeps = new Label();
            LblAmountOfBeeps = new Label();
            LblTimeForEachBeep = new Label();
            LblTimeForResumeMusics = new Label();
            LblBeepVolume = new Label();
            BeepMediaPlayer = new AxWMPLib.AxWindowsMediaPlayer();
            BtnConfigTest = new Button();
            MenuStrip = new MenuStrip();
            arquivoToolStripMenuItem = new ToolStripMenuItem();
            sobreToolStripMenuItem = new ToolStripMenuItem();
            GroupBoxBeepsCtrls = new GroupBox();
            GroupBoxControlsAndTest = new GroupBox();
            LblTestMessages = new Label();
            LblBeepVolumePercent = new Label();
            TBBeepVolume = new TrackBar();
            BtnStartBeepTest = new Button();
            BtnCountdownBeepTest = new Button();
            NumUDTimeBeforePlayBeeps = new NumericUpDown();
            NumUDTimeForResumeMusics = new NumericUpDown();
            NumUDAmountOfBeeps = new NumericUpDown();
            NumUDTimeForEachBeep = new NumericUpDown();
            ComboBoxBeepPair = new ComboBox();
            ((System.ComponentModel.ISupportInitialize)MusicMediaPlayer).BeginInit();
            ((System.ComponentModel.ISupportInitialize)TBMusicVolumeMin).BeginInit();
            GroupBoxMusicCtrls.SuspendLayout();
            GroupBoxMusicVolume.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)TBMusicCurrentVol).BeginInit();
            GroupBoxMusicVols.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)TBMusicVolumeMax).BeginInit();
            ((System.ComponentModel.ISupportInitialize)BeepMediaPlayer).BeginInit();
            MenuStrip.SuspendLayout();
            GroupBoxBeepsCtrls.SuspendLayout();
            GroupBoxControlsAndTest.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)TBBeepVolume).BeginInit();
            ((System.ComponentModel.ISupportInitialize)NumUDTimeBeforePlayBeeps).BeginInit();
            ((System.ComponentModel.ISupportInitialize)NumUDTimeForResumeMusics).BeginInit();
            ((System.ComponentModel.ISupportInitialize)NumUDAmountOfBeeps).BeginInit();
            ((System.ComponentModel.ISupportInitialize)NumUDTimeForEachBeep).BeginInit();
            SuspendLayout();
            // 
            // MusicMediaPlayer
            // 
            MusicMediaPlayer.Enabled = true;
            MusicMediaPlayer.Location = new Point(470, 132);
            MusicMediaPlayer.Name = "MusicMediaPlayer";
            MusicMediaPlayer.OcxState = (AxHost.State)resources.GetObject("MusicMediaPlayer.OcxState");
            MusicMediaPlayer.Size = new Size(420, 90);
            MusicMediaPlayer.TabIndex = 0;
            // 
            // TBMusicVolumeMin
            // 
            TBMusicVolumeMin.AutoSize = false;
            TBMusicVolumeMin.BackColor = SystemColors.Control;
            TBMusicVolumeMin.LargeChange = 0;
            TBMusicVolumeMin.Location = new Point(70, 26);
            TBMusicVolumeMin.Maximum = 0;
            TBMusicVolumeMin.Minimum = -100;
            TBMusicVolumeMin.Name = "TBMusicVolumeMin";
            TBMusicVolumeMin.RightToLeft = RightToLeft.Yes;
            TBMusicVolumeMin.RightToLeftLayout = true;
            TBMusicVolumeMin.Size = new Size(310, 30);
            TBMusicVolumeMin.SmallChange = 5;
            TBMusicVolumeMin.TabIndex = 1;
            TBMusicVolumeMin.TickFrequency = 5;
            TBMusicVolumeMin.Value = -10;
            TBMusicVolumeMin.ValueChanged += TBMusicVolumeMin_ValueChanged;
            // 
            // BtnPlayTest
            // 
            BtnPlayTest.Font = new Font("Segoe UI", 10F);
            BtnPlayTest.Location = new Point(404, 600);
            BtnPlayTest.Name = "BtnPlayTest";
            BtnPlayTest.Size = new Size(75, 49);
            BtnPlayTest.TabIndex = 2;
            BtnPlayTest.Text = "Play";
            BtnPlayTest.UseVisualStyleBackColor = true;
            BtnPlayTest.Click += BtnPlayTest_Click;
            // 
            // BtnStopTest
            // 
            BtnStopTest.Font = new Font("Segoe UI", 10F);
            BtnStopTest.Location = new Point(505, 600);
            BtnStopTest.Name = "BtnStopTest";
            BtnStopTest.Size = new Size(75, 49);
            BtnStopTest.TabIndex = 3;
            BtnStopTest.Text = "Stop";
            BtnStopTest.UseVisualStyleBackColor = true;
            BtnStopTest.Click += BtnStopTest_Click;
            // 
            // GroupBoxMusicCtrls
            // 
            GroupBoxMusicCtrls.Controls.Add(BtnClearMusicsList);
            GroupBoxMusicCtrls.Controls.Add(BtnAddMusics);
            GroupBoxMusicCtrls.Controls.Add(ListViewMusics);
            GroupBoxMusicCtrls.Controls.Add(GroupBoxMusicVolume);
            GroupBoxMusicCtrls.Controls.Add(GroupBoxMusicVols);
            GroupBoxMusicCtrls.Controls.Add(MusicMediaPlayer);
            GroupBoxMusicCtrls.Font = new Font("Segoe UI", 10F);
            GroupBoxMusicCtrls.Location = new Point(20, 240);
            GroupBoxMusicCtrls.Name = "GroupBoxMusicCtrls";
            GroupBoxMusicCtrls.Size = new Size(910, 310);
            GroupBoxMusicCtrls.TabIndex = 4;
            GroupBoxMusicCtrls.TabStop = false;
            GroupBoxMusicCtrls.Text = "CONTROLES DAS MÚSICAS";
            // 
            // BtnClearMusicsList
            // 
            BtnClearMusicsList.Enabled = false;
            BtnClearMusicsList.Font = new Font("Segoe UI", 10F);
            BtnClearMusicsList.Location = new Point(86, 27);
            BtnClearMusicsList.Name = "BtnClearMusicsList";
            BtnClearMusicsList.Size = new Size(60, 30);
            BtnClearMusicsList.TabIndex = 19;
            BtnClearMusicsList.Text = "Clear";
            BtnClearMusicsList.UseVisualStyleBackColor = true;
            // 
            // BtnAddMusics
            // 
            BtnAddMusics.Enabled = false;
            BtnAddMusics.Font = new Font("Segoe UI", 10F);
            BtnAddMusics.Location = new Point(20, 27);
            BtnAddMusics.Name = "BtnAddMusics";
            BtnAddMusics.Size = new Size(60, 30);
            BtnAddMusics.TabIndex = 18;
            BtnAddMusics.Text = "Add";
            BtnAddMusics.UseVisualStyleBackColor = true;
            // 
            // ListViewMusics
            // 
            ListViewMusics.Columns.AddRange(new ColumnHeader[] { MusicName, MusicLength, MusicPath });
            ListViewMusics.FullRowSelect = true;
            ListViewMusics.GridLines = true;
            ListViewMusics.HeaderStyle = ColumnHeaderStyle.Nonclickable;
            ListViewMusics.Location = new Point(20, 123);
            ListViewMusics.Name = "ListViewMusics";
            ListViewMusics.Size = new Size(420, 129);
            ListViewMusics.TabIndex = 7;
            ListViewMusics.UseCompatibleStateImageBehavior = false;
            ListViewMusics.View = View.Details;
            // 
            // MusicName
            // 
            MusicName.Text = "Música";
            // 
            // MusicLength
            // 
            MusicLength.Text = "Comprimento";
            // 
            // MusicPath
            // 
            MusicPath.Text = "Caminho";
            // 
            // GroupBoxMusicVolume
            // 
            GroupBoxMusicVolume.Controls.Add(LblMusicCurrentVolPercent);
            GroupBoxMusicVolume.Controls.Add(LblMusicCurrentVol);
            GroupBoxMusicVolume.Controls.Add(TBMusicCurrentVol);
            GroupBoxMusicVolume.Font = new Font("Segoe UI", 10F);
            GroupBoxMusicVolume.Location = new Point(470, 219);
            GroupBoxMusicVolume.Name = "GroupBoxMusicVolume";
            GroupBoxMusicVolume.Size = new Size(420, 73);
            GroupBoxMusicVolume.TabIndex = 6;
            GroupBoxMusicVolume.TabStop = false;
            // 
            // LblMusicCurrentVolPercent
            // 
            LblMusicCurrentVolPercent.Font = new Font("Segoe UI", 9F);
            LblMusicCurrentVolPercent.Location = new Point(372, 30);
            LblMusicCurrentVolPercent.Name = "LblMusicCurrentVolPercent";
            LblMusicCurrentVolPercent.Size = new Size(35, 15);
            LblMusicCurrentVolPercent.TabIndex = 5;
            LblMusicCurrentVolPercent.Text = "50%";
            LblMusicCurrentVolPercent.TextAlign = ContentAlignment.TopRight;
            // 
            // LblMusicCurrentVol
            // 
            LblMusicCurrentVol.AutoSize = true;
            LblMusicCurrentVol.Font = new Font("Segoe UI", 10F);
            LblMusicCurrentVol.Location = new Point(15, 30);
            LblMusicCurrentVol.Name = "LblMusicCurrentVol";
            LblMusicCurrentVol.Size = new Size(55, 19);
            LblMusicCurrentVol.TabIndex = 2;
            LblMusicCurrentVol.Text = "Volume";
            ToolTip.SetToolTip(LblMusicCurrentVol, "Define o volume atual em teste para as músicas ou mostra o volume corrente se os beeps forem ativados pelo programa.");
            // 
            // TBMusicCurrentVol
            // 
            TBMusicCurrentVol.AutoSize = false;
            TBMusicCurrentVol.BackColor = SystemColors.Control;
            TBMusicCurrentVol.LargeChange = 0;
            TBMusicCurrentVol.Location = new Point(70, 26);
            TBMusicCurrentVol.Maximum = 0;
            TBMusicCurrentVol.Minimum = -100;
            TBMusicCurrentVol.Name = "TBMusicCurrentVol";
            TBMusicCurrentVol.RightToLeft = RightToLeft.Yes;
            TBMusicCurrentVol.RightToLeftLayout = true;
            TBMusicCurrentVol.Size = new Size(310, 30);
            TBMusicCurrentVol.SmallChange = 5;
            TBMusicCurrentVol.TabIndex = 1;
            TBMusicCurrentVol.TickFrequency = 5;
            TBMusicCurrentVol.Value = -50;
            TBMusicCurrentVol.ValueChanged += TBMusicCurrentVol_ValueChanged;
            // 
            // GroupBoxMusicVols
            // 
            GroupBoxMusicVols.Controls.Add(LblMusicVolMaxPercent);
            GroupBoxMusicVols.Controls.Add(LblMusicVolMinPercent);
            GroupBoxMusicVols.Controls.Add(LblMusicVolMax);
            GroupBoxMusicVols.Controls.Add(TBMusicVolumeMax);
            GroupBoxMusicVols.Controls.Add(LblMusicVolMin);
            GroupBoxMusicVols.Controls.Add(TBMusicVolumeMin);
            GroupBoxMusicVols.Font = new Font("Segoe UI", 10F);
            GroupBoxMusicVols.Location = new Point(470, 20);
            GroupBoxMusicVols.Name = "GroupBoxMusicVols";
            GroupBoxMusicVols.Size = new Size(420, 106);
            GroupBoxMusicVols.TabIndex = 5;
            GroupBoxMusicVols.TabStop = false;
            GroupBoxMusicVols.Text = "Limite dos Volumes";
            // 
            // LblMusicVolMaxPercent
            // 
            LblMusicVolMaxPercent.Font = new Font("Segoe UI", 9F);
            LblMusicVolMaxPercent.Location = new Point(372, 65);
            LblMusicVolMaxPercent.Name = "LblMusicVolMaxPercent";
            LblMusicVolMaxPercent.Size = new Size(35, 15);
            LblMusicVolMaxPercent.TabIndex = 6;
            LblMusicVolMaxPercent.Text = "90%";
            LblMusicVolMaxPercent.TextAlign = ContentAlignment.TopRight;
            // 
            // LblMusicVolMinPercent
            // 
            LblMusicVolMinPercent.Font = new Font("Segoe UI", 9F);
            LblMusicVolMinPercent.Location = new Point(372, 30);
            LblMusicVolMinPercent.Name = "LblMusicVolMinPercent";
            LblMusicVolMinPercent.Size = new Size(35, 15);
            LblMusicVolMinPercent.TabIndex = 5;
            LblMusicVolMinPercent.Text = "10%";
            LblMusicVolMinPercent.TextAlign = ContentAlignment.TopRight;
            // 
            // LblMusicVolMax
            // 
            LblMusicVolMax.AutoSize = true;
            LblMusicVolMax.Font = new Font("Segoe UI", 10F);
            LblMusicVolMax.Location = new Point(15, 65);
            LblMusicVolMax.Name = "LblMusicVolMax";
            LblMusicVolMax.Size = new Size(58, 19);
            LblMusicVolMax.TabIndex = 4;
            LblMusicVolMax.Text = "Máximo";
            ToolTip.SetToolTip(LblMusicVolMax, "Define o volume máximo para as músicas enquanto não há beeps.");
            // 
            // TBMusicVolumeMax
            // 
            TBMusicVolumeMax.AutoSize = false;
            TBMusicVolumeMax.BackColor = SystemColors.Control;
            TBMusicVolumeMax.LargeChange = 0;
            TBMusicVolumeMax.Location = new Point(70, 61);
            TBMusicVolumeMax.Maximum = 0;
            TBMusicVolumeMax.Minimum = -100;
            TBMusicVolumeMax.Name = "TBMusicVolumeMax";
            TBMusicVolumeMax.RightToLeft = RightToLeft.Yes;
            TBMusicVolumeMax.RightToLeftLayout = true;
            TBMusicVolumeMax.Size = new Size(310, 30);
            TBMusicVolumeMax.SmallChange = 5;
            TBMusicVolumeMax.TabIndex = 3;
            TBMusicVolumeMax.TickFrequency = 5;
            TBMusicVolumeMax.Value = -90;
            TBMusicVolumeMax.ValueChanged += TBMusicVolumeMax_ValueChanged;
            // 
            // LblMusicVolMin
            // 
            LblMusicVolMin.AutoSize = true;
            LblMusicVolMin.Font = new Font("Segoe UI", 10F);
            LblMusicVolMin.Location = new Point(15, 30);
            LblMusicVolMin.Name = "LblMusicVolMin";
            LblMusicVolMin.Size = new Size(56, 19);
            LblMusicVolMin.TabIndex = 2;
            LblMusicVolMin.Text = "Mínimo";
            ToolTip.SetToolTip(LblMusicVolMin, "Define o volume mínimo para as músicas enquanto os beeps tocam.");
            // 
            // LblTempStatus
            // 
            LblTempStatus.AutoSize = true;
            LblTempStatus.Location = new Point(59, 600);
            LblTempStatus.Name = "LblTempStatus";
            LblTempStatus.Size = new Size(39, 15);
            LblTempStatus.TabIndex = 5;
            LblTempStatus.Text = "Status";
            // 
            // ToolTip
            // 
            ToolTip.IsBalloon = true;
            ToolTip.ToolTipIcon = ToolTipIcon.Info;
            ToolTip.ToolTipTitle = "Informação";
            // 
            // LblBeepPair
            // 
            LblBeepPair.AutoSize = true;
            LblBeepPair.Location = new Point(20, 30);
            LblBeepPair.Name = "LblBeepPair";
            LblBeepPair.Size = new Size(146, 19);
            LblBeepPair.TabIndex = 10;
            LblBeepPair.Text = "Par de sons dos beeps";
            ToolTip.SetToolTip(LblBeepPair, "Define o par dos beeps (tonalidade) a tocar.");
            // 
            // LblTimeBeforePlayBeeps
            // 
            LblTimeBeforePlayBeeps.AutoSize = true;
            LblTimeBeforePlayBeeps.Location = new Point(20, 60);
            LblTimeBeforePlayBeeps.Name = "LblTimeBeforePlayBeeps";
            LblTimeBeforePlayBeeps.Size = new Size(187, 19);
            LblTimeBeforePlayBeeps.TabIndex = 19;
            LblTimeBeforePlayBeeps.Text = "Tempo extra antes dos beeps";
            ToolTip.SetToolTip(LblTimeBeforePlayBeeps, "Tempo antes dos beeps e após volume mínimo das músicas.");
            // 
            // LblAmountOfBeeps
            // 
            LblAmountOfBeeps.AutoSize = true;
            LblAmountOfBeeps.Location = new Point(20, 90);
            LblAmountOfBeeps.Name = "LblAmountOfBeeps";
            LblAmountOfBeeps.Size = new Size(140, 19);
            LblAmountOfBeeps.TabIndex = 14;
            LblAmountOfBeeps.Text = "Quantidade de beeps";
            ToolTip.SetToolTip(LblAmountOfBeeps, "Quantidade de beeps (incluindo o último beep de início da prova).");
            // 
            // LblTimeForEachBeep
            // 
            LblTimeForEachBeep.AutoSize = true;
            LblTimeForEachBeep.Location = new Point(20, 120);
            LblTimeForEachBeep.Name = "LblTimeForEachBeep";
            LblTimeForEachBeep.Size = new Size(147, 19);
            LblTimeForEachBeep.TabIndex = 12;
            LblTimeForEachBeep.Text = "Tempo para cada beep";
            ToolTip.SetToolTip(LblTimeForEachBeep, "Tempo em segundos para tocar outro beep de contagem.");
            // 
            // LblTimeForResumeMusics
            // 
            LblTimeForResumeMusics.AutoSize = true;
            LblTimeForResumeMusics.Location = new Point(20, 150);
            LblTimeForResumeMusics.Name = "LblTimeForResumeMusics";
            LblTimeForResumeMusics.Size = new Size(186, 19);
            LblTimeForResumeMusics.TabIndex = 17;
            LblTimeForResumeMusics.Text = "Tempo para retomar músicas";
            ToolTip.SetToolTip(LblTimeForResumeMusics, "Tempo após último beep para começar a retomar volume máximo das músicas.");
            // 
            // LblBeepVolume
            // 
            LblBeepVolume.AutoSize = true;
            LblBeepVolume.Font = new Font("Segoe UI", 10F);
            LblBeepVolume.Location = new Point(15, 28);
            LblBeepVolume.Name = "LblBeepVolume";
            LblBeepVolume.Size = new Size(55, 19);
            LblBeepVolume.TabIndex = 2;
            LblBeepVolume.Text = "Volume";
            ToolTip.SetToolTip(LblBeepVolume, "Define o volume para os beeps.");
            // 
            // BeepMediaPlayer
            // 
            BeepMediaPlayer.Enabled = true;
            BeepMediaPlayer.Location = new Point(470, 130);
            BeepMediaPlayer.Name = "BeepMediaPlayer";
            BeepMediaPlayer.OcxState = (AxHost.State)resources.GetObject("BeepMediaPlayer.OcxState");
            BeepMediaPlayer.Size = new Size(420, 45);
            BeepMediaPlayer.TabIndex = 6;
            // 
            // BtnConfigTest
            // 
            BtnConfigTest.Enabled = false;
            BtnConfigTest.Font = new Font("Segoe UI", 10F);
            BtnConfigTest.Location = new Point(15, 60);
            BtnConfigTest.Name = "BtnConfigTest";
            BtnConfigTest.Size = new Size(120, 30);
            BtnConfigTest.TabIndex = 7;
            BtnConfigTest.Text = "Testar config.";
            BtnConfigTest.UseVisualStyleBackColor = true;
            BtnConfigTest.Click += BtnConfigTest_Click;
            BtnConfigTest.MouseHover += BtnConfigTest_MouseHover;
            // 
            // MenuStrip
            // 
            MenuStrip.Font = new Font("Segoe UI", 10F);
            MenuStrip.Items.AddRange(new ToolStripItem[] { arquivoToolStripMenuItem, sobreToolStripMenuItem });
            MenuStrip.Location = new Point(0, 0);
            MenuStrip.Name = "MenuStrip";
            MenuStrip.Size = new Size(950, 27);
            MenuStrip.TabIndex = 9;
            MenuStrip.Text = "menuStrip1";
            // 
            // arquivoToolStripMenuItem
            // 
            arquivoToolStripMenuItem.Name = "arquivoToolStripMenuItem";
            arquivoToolStripMenuItem.Size = new Size(69, 23);
            arquivoToolStripMenuItem.Text = "Arquivo";
            // 
            // sobreToolStripMenuItem
            // 
            sobreToolStripMenuItem.Name = "sobreToolStripMenuItem";
            sobreToolStripMenuItem.Size = new Size(56, 23);
            sobreToolStripMenuItem.Text = "Sobre";
            // 
            // GroupBoxBeepsCtrls
            // 
            GroupBoxBeepsCtrls.Anchor = AnchorStyles.Left;
            GroupBoxBeepsCtrls.Controls.Add(GroupBoxControlsAndTest);
            GroupBoxBeepsCtrls.Controls.Add(NumUDTimeBeforePlayBeeps);
            GroupBoxBeepsCtrls.Controls.Add(LblTimeBeforePlayBeeps);
            GroupBoxBeepsCtrls.Controls.Add(NumUDTimeForResumeMusics);
            GroupBoxBeepsCtrls.Controls.Add(LblTimeForResumeMusics);
            GroupBoxBeepsCtrls.Controls.Add(LblAmountOfBeeps);
            GroupBoxBeepsCtrls.Controls.Add(NumUDAmountOfBeeps);
            GroupBoxBeepsCtrls.Controls.Add(BeepMediaPlayer);
            GroupBoxBeepsCtrls.Controls.Add(LblTimeForEachBeep);
            GroupBoxBeepsCtrls.Controls.Add(NumUDTimeForEachBeep);
            GroupBoxBeepsCtrls.Controls.Add(LblBeepPair);
            GroupBoxBeepsCtrls.Controls.Add(ComboBoxBeepPair);
            GroupBoxBeepsCtrls.FlatStyle = FlatStyle.System;
            GroupBoxBeepsCtrls.Font = new Font("Segoe UI", 10F);
            GroupBoxBeepsCtrls.Location = new Point(20, 40);
            GroupBoxBeepsCtrls.Name = "GroupBoxBeepsCtrls";
            GroupBoxBeepsCtrls.Size = new Size(910, 194);
            GroupBoxBeepsCtrls.TabIndex = 9;
            GroupBoxBeepsCtrls.TabStop = false;
            GroupBoxBeepsCtrls.Text = "CONTROLES DOS BEEPS";
            // 
            // GroupBoxControlsAndTest
            // 
            GroupBoxControlsAndTest.Controls.Add(LblTestMessages);
            GroupBoxControlsAndTest.Controls.Add(LblBeepVolumePercent);
            GroupBoxControlsAndTest.Controls.Add(LblBeepVolume);
            GroupBoxControlsAndTest.Controls.Add(TBBeepVolume);
            GroupBoxControlsAndTest.Controls.Add(BtnConfigTest);
            GroupBoxControlsAndTest.Controls.Add(BtnStartBeepTest);
            GroupBoxControlsAndTest.Controls.Add(BtnCountdownBeepTest);
            GroupBoxControlsAndTest.Font = new Font("Segoe UI", 10F);
            GroupBoxControlsAndTest.Location = new Point(470, 18);
            GroupBoxControlsAndTest.Name = "GroupBoxControlsAndTest";
            GroupBoxControlsAndTest.Size = new Size(420, 105);
            GroupBoxControlsAndTest.TabIndex = 7;
            GroupBoxControlsAndTest.TabStop = false;
            GroupBoxControlsAndTest.Text = "Controles e testes";
            // 
            // LblTestMessages
            // 
            LblTestMessages.BorderStyle = BorderStyle.FixedSingle;
            LblTestMessages.Font = new Font("Segoe UI", 9F);
            LblTestMessages.Location = new Point(15, 60);
            LblTestMessages.Name = "LblTestMessages";
            LblTestMessages.Size = new Size(390, 30);
            LblTestMessages.TabIndex = 17;
            LblTestMessages.Text = "Antes dos beeps 00.000 | Até último beep 00.000 | Após beeps 00.000";
            LblTestMessages.TextAlign = ContentAlignment.MiddleCenter;
            LblTestMessages.Visible = false;
            // 
            // LblBeepVolumePercent
            // 
            LblBeepVolumePercent.Font = new Font("Segoe UI", 9F);
            LblBeepVolumePercent.Location = new Point(372, 28);
            LblBeepVolumePercent.Name = "LblBeepVolumePercent";
            LblBeepVolumePercent.Size = new Size(35, 15);
            LblBeepVolumePercent.TabIndex = 5;
            LblBeepVolumePercent.Text = "50%";
            LblBeepVolumePercent.TextAlign = ContentAlignment.TopRight;
            // 
            // TBBeepVolume
            // 
            TBBeepVolume.AutoSize = false;
            TBBeepVolume.BackColor = SystemColors.Control;
            TBBeepVolume.LargeChange = 0;
            TBBeepVolume.Location = new Point(70, 24);
            TBBeepVolume.Maximum = 0;
            TBBeepVolume.Minimum = -100;
            TBBeepVolume.Name = "TBBeepVolume";
            TBBeepVolume.RightToLeft = RightToLeft.Yes;
            TBBeepVolume.RightToLeftLayout = true;
            TBBeepVolume.Size = new Size(310, 30);
            TBBeepVolume.SmallChange = 5;
            TBBeepVolume.TabIndex = 1;
            TBBeepVolume.TickFrequency = 5;
            TBBeepVolume.Value = -50;
            TBBeepVolume.ValueChanged += TBBeepVolume_ValueChanged;
            // 
            // BtnStartBeepTest
            // 
            BtnStartBeepTest.Enabled = false;
            BtnStartBeepTest.Font = new Font("Segoe UI", 10F);
            BtnStartBeepTest.Location = new Point(285, 60);
            BtnStartBeepTest.Name = "BtnStartBeepTest";
            BtnStartBeepTest.Size = new Size(120, 30);
            BtnStartBeepTest.TabIndex = 16;
            BtnStartBeepTest.Text = "Beep de início";
            BtnStartBeepTest.UseVisualStyleBackColor = true;
            BtnStartBeepTest.Click += BtnStartBeepTest_Click;
            // 
            // BtnCountdownBeepTest
            // 
            BtnCountdownBeepTest.Enabled = false;
            BtnCountdownBeepTest.Font = new Font("Segoe UI", 10F);
            BtnCountdownBeepTest.Location = new Point(150, 60);
            BtnCountdownBeepTest.Name = "BtnCountdownBeepTest";
            BtnCountdownBeepTest.Size = new Size(120, 30);
            BtnCountdownBeepTest.TabIndex = 15;
            BtnCountdownBeepTest.Text = "Beep contagem";
            BtnCountdownBeepTest.UseVisualStyleBackColor = true;
            BtnCountdownBeepTest.Click += BtnCountdownBeepTest_Click;
            // 
            // NumUDTimeBeforePlayBeeps
            // 
            NumUDTimeBeforePlayBeeps.Location = new Point(395, 57);
            NumUDTimeBeforePlayBeeps.Maximum = new decimal(new int[] { 5, 0, 0, 0 });
            NumUDTimeBeforePlayBeeps.Name = "NumUDTimeBeforePlayBeeps";
            NumUDTimeBeforePlayBeeps.Size = new Size(45, 25);
            NumUDTimeBeforePlayBeeps.TabIndex = 20;
            NumUDTimeBeforePlayBeeps.TextAlign = HorizontalAlignment.Center;
            NumUDTimeBeforePlayBeeps.ValueChanged += NumUDTimeBeforePlayBeeps_ValueChanged;
            // 
            // NumUDTimeForResumeMusics
            // 
            NumUDTimeForResumeMusics.Location = new Point(395, 150);
            NumUDTimeForResumeMusics.Maximum = new decimal(new int[] { 5, 0, 0, 0 });
            NumUDTimeForResumeMusics.Name = "NumUDTimeForResumeMusics";
            NumUDTimeForResumeMusics.Size = new Size(45, 25);
            NumUDTimeForResumeMusics.TabIndex = 18;
            NumUDTimeForResumeMusics.TextAlign = HorizontalAlignment.Center;
            NumUDTimeForResumeMusics.Value = new decimal(new int[] { 4, 0, 0, 0 });
            NumUDTimeForResumeMusics.ValueChanged += NumUDTimeForResumeMusics_ValueChanged;
            // 
            // NumUDAmountOfBeeps
            // 
            NumUDAmountOfBeeps.Location = new Point(395, 88);
            NumUDAmountOfBeeps.Maximum = new decimal(new int[] { 10, 0, 0, 0 });
            NumUDAmountOfBeeps.Minimum = new decimal(new int[] { 2, 0, 0, 0 });
            NumUDAmountOfBeeps.Name = "NumUDAmountOfBeeps";
            NumUDAmountOfBeeps.Size = new Size(45, 25);
            NumUDAmountOfBeeps.TabIndex = 13;
            NumUDAmountOfBeeps.TextAlign = HorizontalAlignment.Center;
            NumUDAmountOfBeeps.Value = new decimal(new int[] { 5, 0, 0, 0 });
            NumUDAmountOfBeeps.ValueChanged += NumUDAmountOfBeeps_ValueChanged;
            // 
            // NumUDTimeForEachBeep
            // 
            NumUDTimeForEachBeep.DecimalPlaces = 2;
            NumUDTimeForEachBeep.Increment = new decimal(new int[] { 25, 0, 0, 131072 });
            NumUDTimeForEachBeep.Location = new Point(395, 119);
            NumUDTimeForEachBeep.Maximum = new decimal(new int[] { 3, 0, 0, 0 });
            NumUDTimeForEachBeep.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            NumUDTimeForEachBeep.Name = "NumUDTimeForEachBeep";
            NumUDTimeForEachBeep.Size = new Size(45, 25);
            NumUDTimeForEachBeep.TabIndex = 11;
            NumUDTimeForEachBeep.TextAlign = HorizontalAlignment.Center;
            NumUDTimeForEachBeep.Value = new decimal(new int[] { 1, 0, 0, 0 });
            NumUDTimeForEachBeep.ValueChanged += NumUDTimeForEachBeep_ValueChanged;
            // 
            // ComboBoxBeepPair
            // 
            ComboBoxBeepPair.DropDownStyle = ComboBoxStyle.DropDownList;
            ComboBoxBeepPair.FormattingEnabled = true;
            ComboBoxBeepPair.Location = new Point(170, 27);
            ComboBoxBeepPair.Name = "ComboBoxBeepPair";
            ComboBoxBeepPair.Size = new Size(270, 25);
            ComboBoxBeepPair.TabIndex = 0;
            ComboBoxBeepPair.SelectedIndexChanged += ComboBoxBeepPair_SelectedIndexChanged;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSize = true;
            ClientSize = new Size(950, 661);
            Controls.Add(GroupBoxBeepsCtrls);
            Controls.Add(LblTempStatus);
            Controls.Add(GroupBoxMusicCtrls);
            Controls.Add(BtnStopTest);
            Controls.Add(BtnPlayTest);
            Controls.Add(MenuStrip);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MainMenuStrip = MenuStrip;
            MaximizeBox = false;
            Name = "MainForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Controle de competições";
            Activated += MainForm_Activated;
            FormClosing += MainForm_FormClosing;
            ((System.ComponentModel.ISupportInitialize)MusicMediaPlayer).EndInit();
            ((System.ComponentModel.ISupportInitialize)TBMusicVolumeMin).EndInit();
            GroupBoxMusicCtrls.ResumeLayout(false);
            GroupBoxMusicVolume.ResumeLayout(false);
            GroupBoxMusicVolume.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)TBMusicCurrentVol).EndInit();
            GroupBoxMusicVols.ResumeLayout(false);
            GroupBoxMusicVols.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)TBMusicVolumeMax).EndInit();
            ((System.ComponentModel.ISupportInitialize)BeepMediaPlayer).EndInit();
            MenuStrip.ResumeLayout(false);
            MenuStrip.PerformLayout();
            GroupBoxBeepsCtrls.ResumeLayout(false);
            GroupBoxBeepsCtrls.PerformLayout();
            GroupBoxControlsAndTest.ResumeLayout(false);
            GroupBoxControlsAndTest.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)TBBeepVolume).EndInit();
            ((System.ComponentModel.ISupportInitialize)NumUDTimeBeforePlayBeeps).EndInit();
            ((System.ComponentModel.ISupportInitialize)NumUDTimeForResumeMusics).EndInit();
            ((System.ComponentModel.ISupportInitialize)NumUDAmountOfBeeps).EndInit();
            ((System.ComponentModel.ISupportInitialize)NumUDTimeForEachBeep).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private AxWMPLib.AxWindowsMediaPlayer MusicMediaPlayer;
        private TrackBar TBMusicVolumeMin;
        private Button BtnPlayTest;
        private Button BtnStopTest;
        private GroupBox GroupBoxMusicCtrls;
        private Label LblMusicVolMin;
        private GroupBox GroupBoxMusicVols;
        private Label LblMusicVolMax;
        private ToolTip ToolTip;
        private TrackBar TBMusicVolumeMax;
        private AxWMPLib.AxWindowsMediaPlayer BeepMediaPlayer;
        private Button BtnConfigTest;
        private GroupBox GroupBoxBeepCtrls;
        private MenuStrip MenuStrip;
        private ToolStripMenuItem arquivoToolStripMenuItem;
        private ToolStripMenuItem sobreToolStripMenuItem;
        private GroupBox GroupBoxBeepsCtrls;
        private ComboBox ComboBoxBeepPair;
        private Label LblBeepPair;
        private NumericUpDown NumUDTimeForEachBeep;
        private Label LblTimeForEachBeep;
        private Label LblAmountOfBeeps;
        private NumericUpDown NumUDAmountOfBeeps;
        private Button BtnStartBeepTest;
        private Button BtnCountdownBeepTest;
        private NumericUpDown NumUDTimeForResumeMusics;
        private Label LblTimeForResumeMusics;
        private NumericUpDown NumUDTimeBeforePlayBeeps;
        private Label LblTimeBeforePlayBeeps;
        private Label LblMusicVolMinPercent;
        private Label LblMusicVolMaxPercent;
        private GroupBox GroupBoxControlsAndTest;
        private Label LblBeepVolumePercent;
        private Label LblBeepVolume;
        private TrackBar TBBeepVolume;
        private GroupBox GroupBoxMusicVolume;
        private Label LblMusicCurrentVolPercent;
        private Label LblMusicCurrentVol;
        private TrackBar TBMusicCurrentVol;
        private ListView ListViewMusics;
        private ColumnHeader MusicName;
        private ColumnHeader MusicLength;
        private ColumnHeader MusicPath;
        private Label LblTestMessages;
        private Button BtnAddMusics;
        private Button BtnClearMusicsList;
        public Label LblTempStatus;
    }
}
