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
            BtnStartCompetition = new Button();
            ImageListBeepsAndCompetition = new ImageList(components);
            BtnStopCompetition = new Button();
            GroupBoxMusicCtrls = new GroupBox();
            PBMusicPlayerVolumeBanner = new PictureBox();
            LblListViewMusicsStatus = new Label();
            ToggleSeeDetails = new CheckBox();
            ImageListMusics = new ImageList(components);
            ToggleMarkAndExclude = new CheckBox();
            BtnClearMusicsList = new Button();
            BtnAddMusics = new Button();
            ListViewMusics = new ListView();
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
            TogglePlaylistMode = new CheckBox();
            TogglePlayMusicBySelection = new CheckBox();
            NumUDTimeToVolMin = new NumericUpDown();
            LblTimeToChangeVolume = new Label();
            ToolTip = new ToolTip(components);
            LblBeepPair = new Label();
            LblTimeBeforePlayBeeps = new Label();
            LblAmountOfBeeps = new Label();
            LblTimeForEachBeep = new Label();
            LblTimeForResumeMusics = new Label();
            LblBeepVolume = new Label();
            LblProgramming = new Label();
            LblInitialization = new Label();
            LblCompetitionIntervalTime = new Label();
            CheckBoxStopMusicsAtEnd = new CheckBox();
            LblCompetitionAmountIntervals = new Label();
            ProgressBarCompetitionElapsedTime = new ProgressBar();
            LblIntervalsElapsed = new Label();
            ProgressBarCurrentIntervalElapsed = new ProgressBar();
            CheckBoxStartWithBeeps = new CheckBox();
            LblCompetitionElapsedTime = new Label();
            LblRepeatPlaylist = new Label();
            LblIntervalAndCompetitionFormatedTime = new Label();
            LblCurrentIntervalElapsedTime = new Label();
            LblTestMessages = new Label();
            LblHalfIntervalBeep = new Label();
            BtnStartBeepTest = new Button();
            BtnCountdownBeepTest = new Button();
            BeepMediaPlayer = new AxWMPLib.AxWindowsMediaPlayer();
            MenuStrip = new MenuStrip();
            ToolStripMenuItemConfiguration = new ToolStripMenuItem();
            ToolStripMenuItemOpenConfiguration = new ToolStripMenuItem();
            ToolStripMenuItemSaveConfiguration = new ToolStripMenuItem();
            ToolStripMenuItemSaveConfigurationAs = new ToolStripMenuItem();
            ToolStripMenuItemEnableTextAndButtonsTips = new ToolStripMenuItem();
            ToolStripMenuItemReloadBeepList = new ToolStripMenuItem();
            ToolStripMenuItemAbout = new ToolStripMenuItem();
            ToolStripMenuItemCloseProgram = new ToolStripMenuItem();
            GroupBoxBeepsCtrls = new GroupBox();
            ComboBoxHalfIntervalBeep = new ComboBox();
            BtnBeepsTest = new Button();
            ComboBoxBeepPair = new ComboBox();
            GroupBoxBeepsAmountAndTimes = new GroupBox();
            NumUDTimeForEachBeep = new NumericUpDown();
            NumUDAmountOfBeeps = new NumericUpDown();
            NumUDTimeForResumeMusics = new NumericUpDown();
            NumUDTimeBeforePlayBeeps = new NumericUpDown();
            GroupBoxBeepsVolume = new GroupBox();
            LblBeepVolumePercent = new Label();
            TBBeepVolume = new TrackBar();
            GroupBoxProgram = new GroupBox();
            GroupBoxTransitionsControls = new GroupBox();
            GroupBoxCompetitionControls = new GroupBox();
            NumUDCompetitionIntervalSeconds = new NumericUpDown();
            NumUDCompetitionAmountIntervals = new NumericUpDown();
            GroupBoxProgramming = new GroupBox();
            ComboBoxProgramming = new ComboBox();
            ComboBoxRepeatPlaylist = new ComboBox();
            ComboBoxInitialization = new ComboBox();
            ((System.ComponentModel.ISupportInitialize)MusicMediaPlayer).BeginInit();
            ((System.ComponentModel.ISupportInitialize)TBMusicVolumeMin).BeginInit();
            GroupBoxMusicCtrls.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)PBMusicPlayerVolumeBanner).BeginInit();
            GroupBoxMusicVolume.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)TBMusicCurrentVol).BeginInit();
            GroupBoxMusicVols.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)TBMusicVolumeMax).BeginInit();
            ((System.ComponentModel.ISupportInitialize)NumUDTimeToVolMin).BeginInit();
            ((System.ComponentModel.ISupportInitialize)BeepMediaPlayer).BeginInit();
            MenuStrip.SuspendLayout();
            GroupBoxBeepsCtrls.SuspendLayout();
            GroupBoxBeepsAmountAndTimes.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)NumUDTimeForEachBeep).BeginInit();
            ((System.ComponentModel.ISupportInitialize)NumUDAmountOfBeeps).BeginInit();
            ((System.ComponentModel.ISupportInitialize)NumUDTimeForResumeMusics).BeginInit();
            ((System.ComponentModel.ISupportInitialize)NumUDTimeBeforePlayBeeps).BeginInit();
            GroupBoxBeepsVolume.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)TBBeepVolume).BeginInit();
            GroupBoxProgram.SuspendLayout();
            GroupBoxTransitionsControls.SuspendLayout();
            GroupBoxCompetitionControls.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)NumUDCompetitionIntervalSeconds).BeginInit();
            ((System.ComponentModel.ISupportInitialize)NumUDCompetitionAmountIntervals).BeginInit();
            GroupBoxProgramming.SuspendLayout();
            SuspendLayout();
            // 
            // MusicMediaPlayer
            // 
            MusicMediaPlayer.Enabled = true;
            MusicMediaPlayer.Location = new Point(480, 129);
            MusicMediaPlayer.Name = "MusicMediaPlayer";
            MusicMediaPlayer.OcxState = (AxHost.State)resources.GetObject("MusicMediaPlayer.OcxState");
            MusicMediaPlayer.Size = new Size(425, 89);
            MusicMediaPlayer.TabIndex = 44;
            MusicMediaPlayer.PlayStateChange += MusicMediaPlayer_PlayStateChange;
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
            TBMusicVolumeMin.Size = new Size(315, 30);
            TBMusicVolumeMin.SmallChange = 5;
            TBMusicVolumeMin.TabIndex = 39;
            TBMusicVolumeMin.TickFrequency = 5;
            TBMusicVolumeMin.Value = -30;
            TBMusicVolumeMin.ValueChanged += TBMusicVolumeMin_ValueChanged;
            // 
            // BtnStartCompetition
            // 
            BtnStartCompetition.Enabled = false;
            BtnStartCompetition.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            BtnStartCompetition.ImageAlign = ContentAlignment.MiddleLeft;
            BtnStartCompetition.ImageKey = "StartCompetition.png";
            BtnStartCompetition.ImageList = ImageListBeepsAndCompetition;
            BtnStartCompetition.Location = new Point(695, 22);
            BtnStartCompetition.Name = "BtnStartCompetition";
            BtnStartCompetition.Size = new Size(84, 33);
            BtnStartCompetition.TabIndex = 65;
            BtnStartCompetition.Text = " Iniciar";
            BtnStartCompetition.TextImageRelation = TextImageRelation.ImageBeforeText;
            ToolTip.SetToolTip(BtnStartCompetition, "Inicia/Pausa/Retoma a competição.");
            BtnStartCompetition.UseVisualStyleBackColor = true;
            BtnStartCompetition.Click += BtnStartCompetition_Click;
            // 
            // ImageListBeepsAndCompetition
            // 
            ImageListBeepsAndCompetition.ColorDepth = ColorDepth.Depth32Bit;
            ImageListBeepsAndCompetition.ImageStream = (ImageListStreamer)resources.GetObject("ImageListBeepsAndCompetition.ImageStream");
            ImageListBeepsAndCompetition.TransparentColor = Color.Transparent;
            ImageListBeepsAndCompetition.Images.SetKeyName(0, "TestBeeps.png");
            ImageListBeepsAndCompetition.Images.SetKeyName(1, "CountBeep.png");
            ImageListBeepsAndCompetition.Images.SetKeyName(2, "StartBeep.png");
            ImageListBeepsAndCompetition.Images.SetKeyName(3, "StartCompetition.png");
            ImageListBeepsAndCompetition.Images.SetKeyName(4, "PauseCompetition.png");
            ImageListBeepsAndCompetition.Images.SetKeyName(5, "StopCompetition.png");
            // 
            // BtnStopCompetition
            // 
            BtnStopCompetition.Enabled = false;
            BtnStopCompetition.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            BtnStopCompetition.ImageAlign = ContentAlignment.MiddleLeft;
            BtnStopCompetition.ImageKey = "StopCompetition.png";
            BtnStopCompetition.ImageList = ImageListBeepsAndCompetition;
            BtnStopCompetition.Location = new Point(781, 22);
            BtnStopCompetition.Name = "BtnStopCompetition";
            BtnStopCompetition.Size = new Size(64, 33);
            BtnStopCompetition.TabIndex = 66;
            BtnStopCompetition.Text = "Parar";
            BtnStopCompetition.TextImageRelation = TextImageRelation.ImageBeforeText;
            ToolTip.SetToolTip(BtnStopCompetition, "Encerra a competição.");
            BtnStopCompetition.UseVisualStyleBackColor = true;
            BtnStopCompetition.Click += BtnStopCompetition_Click;
            // 
            // GroupBoxMusicCtrls
            // 
            GroupBoxMusicCtrls.Controls.Add(PBMusicPlayerVolumeBanner);
            GroupBoxMusicCtrls.Controls.Add(LblListViewMusicsStatus);
            GroupBoxMusicCtrls.Controls.Add(ToggleSeeDetails);
            GroupBoxMusicCtrls.Controls.Add(ToggleMarkAndExclude);
            GroupBoxMusicCtrls.Controls.Add(BtnClearMusicsList);
            GroupBoxMusicCtrls.Controls.Add(BtnAddMusics);
            GroupBoxMusicCtrls.Controls.Add(ListViewMusics);
            GroupBoxMusicCtrls.Controls.Add(GroupBoxMusicVolume);
            GroupBoxMusicCtrls.Controls.Add(GroupBoxMusicVols);
            GroupBoxMusicCtrls.Controls.Add(MusicMediaPlayer);
            GroupBoxMusicCtrls.Controls.Add(TogglePlaylistMode);
            GroupBoxMusicCtrls.Controls.Add(TogglePlayMusicBySelection);
            GroupBoxMusicCtrls.FlatStyle = FlatStyle.System;
            GroupBoxMusicCtrls.Font = new Font("Segoe UI", 10F);
            GroupBoxMusicCtrls.Location = new Point(15, 212);
            GroupBoxMusicCtrls.Name = "GroupBoxMusicCtrls";
            GroupBoxMusicCtrls.Size = new Size(920, 300);
            GroupBoxMusicCtrls.TabIndex = 29;
            GroupBoxMusicCtrls.TabStop = false;
            GroupBoxMusicCtrls.Text = "CONTROLES DAS MÚSICAS";
            // 
            // PBMusicPlayerVolumeBanner
            // 
            PBMusicPlayerVolumeBanner.Image = (Image)resources.GetObject("PBMusicPlayerVolumeBanner.Image");
            PBMusicPlayerVolumeBanner.Location = new Point(590, 184);
            PBMusicPlayerVolumeBanner.Name = "PBMusicPlayerVolumeBanner";
            PBMusicPlayerVolumeBanner.Size = new Size(178, 32);
            PBMusicPlayerVolumeBanner.TabIndex = 66;
            PBMusicPlayerVolumeBanner.TabStop = false;
            // 
            // LblListViewMusicsStatus
            // 
            LblListViewMusicsStatus.BorderStyle = BorderStyle.Fixed3D;
            LblListViewMusicsStatus.Font = new Font("Segoe UI", 9F);
            LblListViewMusicsStatus.Location = new Point(15, 257);
            LblListViewMusicsStatus.Name = "LblListViewMusicsStatus";
            LblListViewMusicsStatus.Size = new Size(440, 30);
            LblListViewMusicsStatus.TabIndex = 65;
            LblListViewMusicsStatus.Text = "Músicas Válidas: 9999 - Inválidas: 9999 - Tempo total da playlist válida: 00:00:00";
            LblListViewMusicsStatus.TextAlign = ContentAlignment.MiddleCenter;
            ToolTip.SetToolTip(LblListViewMusicsStatus, "Quantidade de músicas e tempo total aproximado (hh:mm:ss) da playlist.");
            // 
            // ToggleSeeDetails
            // 
            ToggleSeeDetails.Appearance = Appearance.Button;
            ToggleSeeDetails.Checked = true;
            ToggleSeeDetails.CheckState = CheckState.Checked;
            ToggleSeeDetails.ImageIndex = 7;
            ToggleSeeDetails.ImageList = ImageListMusics;
            ToggleSeeDetails.Location = new Point(404, 25);
            ToggleSeeDetails.Name = "ToggleSeeDetails";
            ToggleSeeDetails.Size = new Size(52, 46);
            ToggleSeeDetails.TabIndex = 34;
            ToggleSeeDetails.TextAlign = ContentAlignment.MiddleCenter;
            ToolTip.SetToolTip(ToggleSeeDetails, "Alterna entre visualização simplificada (1 coluna) e detalhada (várias colunas) da lista de músicas.");
            ToggleSeeDetails.UseVisualStyleBackColor = true;
            ToggleSeeDetails.CheckedChanged += ToggleSeeDetails_CheckedChanged;
            // 
            // ImageListMusics
            // 
            ImageListMusics.ColorDepth = ColorDepth.Depth32Bit;
            ImageListMusics.ImageStream = (ImageListStreamer)resources.GetObject("ImageListMusics.ImageStream");
            ImageListMusics.TransparentColor = Color.Transparent;
            ImageListMusics.Images.SetKeyName(0, "AddMusic.png");
            ImageListMusics.Images.SetKeyName(1, "RemoveAllMusics.png");
            ImageListMusics.Images.SetKeyName(2, "RemoveMusics.png");
            ImageListMusics.Images.SetKeyName(3, "SelectToPlay.png");
            ImageListMusics.Images.SetKeyName(4, "CancelSelectToPlay.png");
            ImageListMusics.Images.SetKeyName(5, "SequentialPlaylist.png");
            ImageListMusics.Images.SetKeyName(6, "ShufflePlaylist.png");
            ImageListMusics.Images.SetKeyName(7, "DetailedView.png");
            ImageListMusics.Images.SetKeyName(8, "SimplifiedView.png");
            // 
            // ToggleMarkAndExclude
            // 
            ToggleMarkAndExclude.Appearance = Appearance.Button;
            ToggleMarkAndExclude.Enabled = false;
            ToggleMarkAndExclude.ImageIndex = 2;
            ToggleMarkAndExclude.ImageList = ImageListMusics;
            ToggleMarkAndExclude.Location = new Point(129, 25);
            ToggleMarkAndExclude.Name = "ToggleMarkAndExclude";
            ToggleMarkAndExclude.Size = new Size(104, 46);
            ToggleMarkAndExclude.TabIndex = 32;
            ToggleMarkAndExclude.Text = "Excluir seleção";
            ToggleMarkAndExclude.TextAlign = ContentAlignment.MiddleCenter;
            ToggleMarkAndExclude.TextImageRelation = TextImageRelation.ImageBeforeText;
            ToolTip.SetToolTip(ToggleMarkAndExclude, "Clique para ativar a função de exclusão das músicas na lista.");
            ToggleMarkAndExclude.UseVisualStyleBackColor = true;
            ToggleMarkAndExclude.CheckedChanged += ToggleMarkAndExclude_CheckedChanged;
            // 
            // BtnClearMusicsList
            // 
            BtnClearMusicsList.Enabled = false;
            BtnClearMusicsList.Font = new Font("Segoe UI", 10F);
            BtnClearMusicsList.ImageIndex = 1;
            BtnClearMusicsList.ImageList = ImageListMusics;
            BtnClearMusicsList.Location = new Point(72, 25);
            BtnClearMusicsList.Name = "BtnClearMusicsList";
            BtnClearMusicsList.Size = new Size(52, 46);
            BtnClearMusicsList.TabIndex = 31;
            ToolTip.SetToolTip(BtnClearMusicsList, "Remove todas as músicas da lista.");
            BtnClearMusicsList.UseVisualStyleBackColor = true;
            BtnClearMusicsList.Click += BtnClearMusicsList_Click;
            // 
            // BtnAddMusics
            // 
            BtnAddMusics.Font = new Font("Segoe UI", 10F);
            BtnAddMusics.ImageIndex = 0;
            BtnAddMusics.ImageList = ImageListMusics;
            BtnAddMusics.Location = new Point(15, 25);
            BtnAddMusics.Name = "BtnAddMusics";
            BtnAddMusics.Size = new Size(52, 46);
            BtnAddMusics.TabIndex = 30;
            ToolTip.SetToolTip(BtnAddMusics, "Adiciona mais músicas na lista.");
            BtnAddMusics.UseVisualStyleBackColor = true;
            BtnAddMusics.Click += BtnAddMusics_Click;
            // 
            // ListViewMusics
            // 
            ListViewMusics.HeaderStyle = ColumnHeaderStyle.Nonclickable;
            ListViewMusics.Location = new Point(15, 77);
            ListViewMusics.Name = "ListViewMusics";
            ListViewMusics.Size = new Size(440, 173);
            ListViewMusics.TabIndex = 36;
            ListViewMusics.UseCompatibleStateImageBehavior = false;
            ListViewMusics.ItemChecked += ListViewMusics_ItemChecked;
            ListViewMusics.ItemSelectionChanged += ListViewMusics_ItemSelectionChanged;
            // 
            // GroupBoxMusicVolume
            // 
            GroupBoxMusicVolume.Controls.Add(LblMusicCurrentVolPercent);
            GroupBoxMusicVolume.Controls.Add(LblMusicCurrentVol);
            GroupBoxMusicVolume.Controls.Add(TBMusicCurrentVol);
            GroupBoxMusicVolume.FlatStyle = FlatStyle.System;
            GroupBoxMusicVolume.Font = new Font("Segoe UI", 10F);
            GroupBoxMusicVolume.Location = new Point(480, 215);
            GroupBoxMusicVolume.Name = "GroupBoxMusicVolume";
            GroupBoxMusicVolume.Size = new Size(425, 73);
            GroupBoxMusicVolume.TabIndex = 45;
            GroupBoxMusicVolume.TabStop = false;
            // 
            // LblMusicCurrentVolPercent
            // 
            LblMusicCurrentVolPercent.Font = new Font("Segoe UI", 9F);
            LblMusicCurrentVolPercent.Location = new Point(377, 30);
            LblMusicCurrentVolPercent.Name = "LblMusicCurrentVolPercent";
            LblMusicCurrentVolPercent.Size = new Size(35, 15);
            LblMusicCurrentVolPercent.TabIndex = 48;
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
            LblMusicCurrentVol.TabIndex = 46;
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
            TBMusicCurrentVol.Size = new Size(315, 30);
            TBMusicCurrentVol.SmallChange = 5;
            TBMusicCurrentVol.TabIndex = 47;
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
            GroupBoxMusicVols.FlatStyle = FlatStyle.System;
            GroupBoxMusicVols.Font = new Font("Segoe UI", 10F);
            GroupBoxMusicVols.Location = new Point(480, 17);
            GroupBoxMusicVols.Name = "GroupBoxMusicVols";
            GroupBoxMusicVols.Size = new Size(425, 106);
            GroupBoxMusicVols.TabIndex = 37;
            GroupBoxMusicVols.TabStop = false;
            GroupBoxMusicVols.Text = "Limite dos Volumes";
            // 
            // LblMusicVolMaxPercent
            // 
            LblMusicVolMaxPercent.Font = new Font("Segoe UI", 9F);
            LblMusicVolMaxPercent.Location = new Point(377, 65);
            LblMusicVolMaxPercent.Name = "LblMusicVolMaxPercent";
            LblMusicVolMaxPercent.Size = new Size(35, 15);
            LblMusicVolMaxPercent.TabIndex = 43;
            LblMusicVolMaxPercent.Text = "70%";
            LblMusicVolMaxPercent.TextAlign = ContentAlignment.TopRight;
            // 
            // LblMusicVolMinPercent
            // 
            LblMusicVolMinPercent.Font = new Font("Segoe UI", 9F);
            LblMusicVolMinPercent.Location = new Point(377, 30);
            LblMusicVolMinPercent.Name = "LblMusicVolMinPercent";
            LblMusicVolMinPercent.Size = new Size(35, 15);
            LblMusicVolMinPercent.TabIndex = 40;
            LblMusicVolMinPercent.Text = "30%";
            LblMusicVolMinPercent.TextAlign = ContentAlignment.TopRight;
            // 
            // LblMusicVolMax
            // 
            LblMusicVolMax.AutoSize = true;
            LblMusicVolMax.Font = new Font("Segoe UI", 10F);
            LblMusicVolMax.Location = new Point(15, 65);
            LblMusicVolMax.Name = "LblMusicVolMax";
            LblMusicVolMax.Size = new Size(58, 19);
            LblMusicVolMax.TabIndex = 41;
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
            TBMusicVolumeMax.Size = new Size(315, 30);
            TBMusicVolumeMax.SmallChange = 5;
            TBMusicVolumeMax.TabIndex = 42;
            TBMusicVolumeMax.TickFrequency = 5;
            TBMusicVolumeMax.Value = -70;
            TBMusicVolumeMax.ValueChanged += TBMusicVolumeMax_ValueChanged;
            // 
            // LblMusicVolMin
            // 
            LblMusicVolMin.AutoSize = true;
            LblMusicVolMin.Font = new Font("Segoe UI", 10F);
            LblMusicVolMin.Location = new Point(15, 30);
            LblMusicVolMin.Name = "LblMusicVolMin";
            LblMusicVolMin.Size = new Size(56, 19);
            LblMusicVolMin.TabIndex = 38;
            LblMusicVolMin.Text = "Mínimo";
            ToolTip.SetToolTip(LblMusicVolMin, "Define o volume mínimo para as músicas enquanto os beeps tocam.");
            // 
            // TogglePlaylistMode
            // 
            TogglePlaylistMode.Appearance = Appearance.Button;
            TogglePlaylistMode.ImageIndex = 5;
            TogglePlaylistMode.ImageList = ImageListMusics;
            TogglePlaylistMode.Location = new Point(347, 25);
            TogglePlaylistMode.Name = "TogglePlaylistMode";
            TogglePlaylistMode.Size = new Size(52, 46);
            TogglePlaylistMode.TabIndex = 34;
            TogglePlaylistMode.TextAlign = ContentAlignment.MiddleCenter;
            ToolTip.SetToolTip(TogglePlaylistMode, "Playlist vai tocar sequencialmente.");
            TogglePlaylistMode.UseVisualStyleBackColor = true;
            TogglePlaylistMode.CheckedChanged += TogglePlaylistMode_CheckedChanged;
            // 
            // TogglePlayMusicBySelection
            // 
            TogglePlayMusicBySelection.Appearance = Appearance.Button;
            TogglePlayMusicBySelection.Enabled = false;
            TogglePlayMusicBySelection.ImageIndex = 3;
            TogglePlayMusicBySelection.ImageList = ImageListMusics;
            TogglePlayMusicBySelection.Location = new Point(238, 25);
            TogglePlayMusicBySelection.Name = "TogglePlayMusicBySelection";
            TogglePlayMusicBySelection.Size = new Size(104, 46);
            TogglePlayMusicBySelection.TabIndex = 33;
            TogglePlayMusicBySelection.Text = "Ouvir seleção";
            TogglePlayMusicBySelection.TextAlign = ContentAlignment.MiddleCenter;
            TogglePlayMusicBySelection.TextImageRelation = TextImageRelation.ImageBeforeText;
            ToolTip.SetToolTip(TogglePlayMusicBySelection, "Habilita tocar a música ao selecioná-la na lista.");
            TogglePlayMusicBySelection.UseVisualStyleBackColor = true;
            TogglePlayMusicBySelection.CheckedChanged += TogglePlayMusicBySelection_CheckedChanged;
            // 
            // NumUDTimeToVolMin
            // 
            NumUDTimeToVolMin.Enabled = false;
            NumUDTimeToVolMin.Location = new Point(155, 16);
            NumUDTimeToVolMin.Maximum = new decimal(new int[] { 5, 0, 0, 0 });
            NumUDTimeToVolMin.Minimum = new decimal(new int[] { 2, 0, 0, 0 });
            NumUDTimeToVolMin.Name = "NumUDTimeToVolMin";
            NumUDTimeToVolMin.Size = new Size(40, 25);
            NumUDTimeToVolMin.TabIndex = 57;
            NumUDTimeToVolMin.TextAlign = HorizontalAlignment.Center;
            NumUDTimeToVolMin.Value = new decimal(new int[] { 3, 0, 0, 0 });
            NumUDTimeToVolMin.ValueChanged += NumUDTimeToVolMin_ValueChanged;
            // 
            // LblTimeToChangeVolume
            // 
            LblTimeToChangeVolume.AutoSize = true;
            LblTimeToChangeVolume.Location = new Point(4, 18);
            LblTimeToChangeVolume.Name = "LblTimeToChangeVolume";
            LblTimeToChangeVolume.Size = new Size(153, 19);
            LblTimeToChangeVolume.TabIndex = 56;
            LblTimeToChangeVolume.Text = "Tempo troca de volume";
            ToolTip.SetToolTip(LblTimeToChangeVolume, "Tempo em segundos para alternar volumes das músicas antes ou após evento dos beeps.");
            // 
            // ToolTip
            // 
            ToolTip.Active = false;
            ToolTip.AutomaticDelay = 1000;
            ToolTip.AutoPopDelay = 6000;
            ToolTip.InitialDelay = 1000;
            ToolTip.IsBalloon = true;
            ToolTip.ReshowDelay = 1000;
            ToolTip.ToolTipIcon = ToolTipIcon.Info;
            ToolTip.ToolTipTitle = "Informação";
            // 
            // LblBeepPair
            // 
            LblBeepPair.AutoSize = true;
            LblBeepPair.Location = new Point(15, 25);
            LblBeepPair.Name = "LblBeepPair";
            LblBeepPair.Size = new Size(146, 19);
            LblBeepPair.TabIndex = 10;
            LblBeepPair.Text = "Par de sons dos beeps";
            ToolTip.SetToolTip(LblBeepPair, "Define o par dos beeps (tonalidade) a tocar.");
            // 
            // LblTimeBeforePlayBeeps
            // 
            LblTimeBeforePlayBeeps.AutoSize = true;
            LblTimeBeforePlayBeeps.Location = new Point(4, 18);
            LblTimeBeforePlayBeeps.Name = "LblTimeBeforePlayBeeps";
            LblTimeBeforePlayBeeps.Size = new Size(187, 19);
            LblTimeBeforePlayBeeps.TabIndex = 12;
            LblTimeBeforePlayBeeps.Text = "Tempo extra antes dos beeps";
            ToolTip.SetToolTip(LblTimeBeforePlayBeeps, "Tempo antes dos beeps e após volume mínimo das músicas.");
            // 
            // LblAmountOfBeeps
            // 
            LblAmountOfBeeps.AutoSize = true;
            LblAmountOfBeeps.Location = new Point(241, 18);
            LblAmountOfBeeps.Name = "LblAmountOfBeeps";
            LblAmountOfBeeps.Size = new Size(140, 19);
            LblAmountOfBeeps.TabIndex = 14;
            LblAmountOfBeeps.Text = "Quantidade de beeps";
            ToolTip.SetToolTip(LblAmountOfBeeps, "Quantidade de beeps (incluindo o último beep de início da prova).");
            // 
            // LblTimeForEachBeep
            // 
            LblTimeForEachBeep.AutoSize = true;
            LblTimeForEachBeep.Location = new Point(241, 48);
            LblTimeForEachBeep.Name = "LblTimeForEachBeep";
            LblTimeForEachBeep.Size = new Size(147, 19);
            LblTimeForEachBeep.TabIndex = 16;
            LblTimeForEachBeep.Text = "Tempo para cada beep";
            ToolTip.SetToolTip(LblTimeForEachBeep, "Tempo em segundos para tocar outro beep de contagem.");
            // 
            // LblTimeForResumeMusics
            // 
            LblTimeForResumeMusics.AutoSize = true;
            LblTimeForResumeMusics.Location = new Point(4, 48);
            LblTimeForResumeMusics.Name = "LblTimeForResumeMusics";
            LblTimeForResumeMusics.Size = new Size(186, 19);
            LblTimeForResumeMusics.TabIndex = 18;
            LblTimeForResumeMusics.Text = "Tempo para retomar músicas";
            ToolTip.SetToolTip(LblTimeForResumeMusics, "Tempo após último beep para começar a retomar volume máximo das músicas.");
            // 
            // LblBeepVolume
            // 
            LblBeepVolume.AutoSize = true;
            LblBeepVolume.Font = new Font("Segoe UI", 10F);
            LblBeepVolume.Location = new Point(15, 27);
            LblBeepVolume.Name = "LblBeepVolume";
            LblBeepVolume.Size = new Size(55, 19);
            LblBeepVolume.TabIndex = 21;
            LblBeepVolume.Text = "Volume";
            ToolTip.SetToolTip(LblBeepVolume, "Define o volume geral para os beeps.");
            // 
            // LblProgramming
            // 
            LblProgramming.AutoSize = true;
            LblProgramming.Location = new Point(4, 18);
            LblProgramming.Name = "LblProgramming";
            LblProgramming.Size = new Size(90, 19);
            LblProgramming.TabIndex = 50;
            LblProgramming.Text = "Programação";
            ToolTip.SetToolTip(LblProgramming, "Define se haverá música de fundo com os beeps na competição.");
            // 
            // LblInitialization
            // 
            LblInitialization.AutoSize = true;
            LblInitialization.Location = new Point(4, 50);
            LblInitialization.Name = "LblInitialization";
            LblInitialization.Size = new Size(80, 19);
            LblInitialization.TabIndex = 52;
            LblInitialization.Text = "Inicialização";
            ToolTip.SetToolTip(LblInitialization, "Define se a playlist começará do início ou continua da música que está tocando.");
            // 
            // LblCompetitionIntervalTime
            // 
            LblCompetitionIntervalTime.AutoSize = true;
            LblCompetitionIntervalTime.Location = new Point(4, 48);
            LblCompetitionIntervalTime.Name = "LblCompetitionIntervalTime";
            LblCompetitionIntervalTime.Size = new Size(158, 19);
            LblCompetitionIntervalTime.TabIndex = 62;
            LblCompetitionIntervalTime.Text = "Tempo de cada intervalo";
            ToolTip.SetToolTip(LblCompetitionIntervalTime, "Tempo em segundos de cada tiro (Máximo de 360s [6 mins]).");
            // 
            // CheckBoxStopMusicsAtEnd
            // 
            CheckBoxStopMusicsAtEnd.AutoSize = true;
            CheckBoxStopMusicsAtEnd.Enabled = false;
            CheckBoxStopMusicsAtEnd.Location = new Point(8, 78);
            CheckBoxStopMusicsAtEnd.Name = "CheckBoxStopMusicsAtEnd";
            CheckBoxStopMusicsAtEnd.Size = new Size(181, 23);
            CheckBoxStopMusicsAtEnd.TabIndex = 59;
            CheckBoxStopMusicsAtEnd.Text = "Parar músicas ao finalizar";
            ToolTip.SetToolTip(CheckBoxStopMusicsAtEnd, "Se as músicas param ao final da competição ou a música atual continua tocando até terminar.");
            CheckBoxStopMusicsAtEnd.UseVisualStyleBackColor = true;
            CheckBoxStopMusicsAtEnd.CheckedChanged += CheckBoxStopMusicsAtEnd_CheckedChanged;
            // 
            // LblCompetitionAmountIntervals
            // 
            LblCompetitionAmountIntervals.AutoSize = true;
            LblCompetitionAmountIntervals.Location = new Point(4, 18);
            LblCompetitionAmountIntervals.Name = "LblCompetitionAmountIntervals";
            LblCompetitionAmountIntervals.Size = new Size(160, 19);
            LblCompetitionAmountIntervals.TabIndex = 60;
            LblCompetitionAmountIntervals.Text = "Qtd. de Intervalos (Tiros)";
            ToolTip.SetToolTip(LblCompetitionAmountIntervals, "Quantos tiros terá a competição.");
            // 
            // ProgressBarCompetitionElapsedTime
            // 
            ProgressBarCompetitionElapsedTime.Location = new Point(696, 95);
            ProgressBarCompetitionElapsedTime.Name = "ProgressBarCompetitionElapsedTime";
            ProgressBarCompetitionElapsedTime.Size = new Size(148, 31);
            ProgressBarCompetitionElapsedTime.TabIndex = 70;
            ToolTip.SetToolTip(ProgressBarCompetitionElapsedTime, "Progresso da competição.");
            // 
            // LblIntervalsElapsed
            // 
            LblIntervalsElapsed.BackColor = Color.LimeGreen;
            LblIntervalsElapsed.BorderStyle = BorderStyle.FixedSingle;
            LblIntervalsElapsed.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            LblIntervalsElapsed.Location = new Point(848, 23);
            LblIntervalsElapsed.Name = "LblIntervalsElapsed";
            LblIntervalsElapsed.Size = new Size(57, 31);
            LblIntervalsElapsed.TabIndex = 67;
            LblIntervalsElapsed.Text = "100/100";
            LblIntervalsElapsed.TextAlign = ContentAlignment.MiddleCenter;
            ToolTip.SetToolTip(LblIntervalsElapsed, "Qual o tiro atual em relação ao total de tiros.");
            // 
            // ProgressBarCurrentIntervalElapsed
            // 
            ProgressBarCurrentIntervalElapsed.Location = new Point(696, 59);
            ProgressBarCurrentIntervalElapsed.Name = "ProgressBarCurrentIntervalElapsed";
            ProgressBarCurrentIntervalElapsed.Size = new Size(148, 31);
            ProgressBarCurrentIntervalElapsed.TabIndex = 68;
            ToolTip.SetToolTip(ProgressBarCurrentIntervalElapsed, "Progressão do intervalo atual.");
            // 
            // CheckBoxStartWithBeeps
            // 
            CheckBoxStartWithBeeps.AutoSize = true;
            CheckBoxStartWithBeeps.Enabled = false;
            CheckBoxStartWithBeeps.Location = new Point(8, 48);
            CheckBoxStartWithBeeps.Name = "CheckBoxStartWithBeeps";
            CheckBoxStartWithBeeps.Size = new Size(181, 23);
            CheckBoxStartWithBeeps.TabIndex = 58;
            CheckBoxStartWithBeeps.Text = "Prova já inicia com beeps";
            ToolTip.SetToolTip(CheckBoxStartWithBeeps, "Se o programa realiza beeps antes de inicar o contador ou inica sem beeps.");
            CheckBoxStartWithBeeps.UseVisualStyleBackColor = true;
            CheckBoxStartWithBeeps.CheckedChanged += CheckBoxStartWithBeeps_CheckedChanged;
            // 
            // LblCompetitionElapsedTime
            // 
            LblCompetitionElapsedTime.BackColor = Color.DeepSkyBlue;
            LblCompetitionElapsedTime.BorderStyle = BorderStyle.FixedSingle;
            LblCompetitionElapsedTime.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            LblCompetitionElapsedTime.Location = new Point(848, 95);
            LblCompetitionElapsedTime.Name = "LblCompetitionElapsedTime";
            LblCompetitionElapsedTime.Size = new Size(57, 31);
            LblCompetitionElapsedTime.TabIndex = 71;
            LblCompetitionElapsedTime.Text = "00:00:00";
            LblCompetitionElapsedTime.TextAlign = ContentAlignment.MiddleCenter;
            ToolTip.SetToolTip(LblCompetitionElapsedTime, "Tempo total decorrido.");
            // 
            // LblRepeatPlaylist
            // 
            LblRepeatPlaylist.AutoSize = true;
            LblRepeatPlaylist.Location = new Point(4, 82);
            LblRepeatPlaylist.Name = "LblRepeatPlaylist";
            LblRepeatPlaylist.Size = new Size(98, 19);
            LblRepeatPlaylist.TabIndex = 54;
            LblRepeatPlaylist.Text = "Repetir Playlist";
            ToolTip.SetToolTip(LblRepeatPlaylist, "Como recomeçar a playlist se ela terminar antes do fim da prova.");
            // 
            // LblIntervalAndCompetitionFormatedTime
            // 
            LblIntervalAndCompetitionFormatedTime.BorderStyle = BorderStyle.Fixed3D;
            LblIntervalAndCompetitionFormatedTime.Font = new Font("Segoe UI", 9F);
            LblIntervalAndCompetitionFormatedTime.Location = new Point(8, 76);
            LblIntervalAndCompetitionFormatedTime.Name = "LblIntervalAndCompetitionFormatedTime";
            LblIntervalAndCompetitionFormatedTime.Size = new Size(193, 30);
            LblIntervalAndCompetitionFormatedTime.TabIndex = 64;
            LblIntervalAndCompetitionFormatedTime.Text = "Tiro de: 00:00 - Prova de: 00:00:00";
            LblIntervalAndCompetitionFormatedTime.TextAlign = ContentAlignment.MiddleCenter;
            ToolTip.SetToolTip(LblIntervalAndCompetitionFormatedTime, "Tempo do intervalo em minutos e segundos e tempo total da prova (formato hh:mm:ss).");
            // 
            // LblCurrentIntervalElapsedTime
            // 
            LblCurrentIntervalElapsedTime.BackColor = Color.Yellow;
            LblCurrentIntervalElapsedTime.BorderStyle = BorderStyle.FixedSingle;
            LblCurrentIntervalElapsedTime.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            LblCurrentIntervalElapsedTime.Location = new Point(848, 59);
            LblCurrentIntervalElapsedTime.Name = "LblCurrentIntervalElapsedTime";
            LblCurrentIntervalElapsedTime.Size = new Size(57, 31);
            LblCurrentIntervalElapsedTime.TabIndex = 69;
            LblCurrentIntervalElapsedTime.Text = "00:00:00";
            LblCurrentIntervalElapsedTime.TextAlign = ContentAlignment.MiddleCenter;
            ToolTip.SetToolTip(LblCurrentIntervalElapsedTime, "Tempo do tiro atual.");
            // 
            // LblTestMessages
            // 
            LblTestMessages.BorderStyle = BorderStyle.FixedSingle;
            LblTestMessages.Font = new Font("Segoe UI", 9F);
            LblTestMessages.Location = new Point(480, 88);
            LblTestMessages.Name = "LblTestMessages";
            LblTestMessages.Size = new Size(425, 30);
            LblTestMessages.TabIndex = 27;
            LblTestMessages.Text = "Antes dos beeps 00.000 | Até antes do último beep 00.000 | Após beeps 00.000";
            LblTestMessages.TextAlign = ContentAlignment.MiddleCenter;
            ToolTip.SetToolTip(LblTestMessages, "Tempos de cada etapa do evento dos beeps, em segundos e milissegundos.");
            LblTestMessages.Visible = false;
            // 
            // LblHalfIntervalBeep
            // 
            LblHalfIntervalBeep.AutoSize = true;
            LblHalfIntervalBeep.Location = new Point(15, 57);
            LblHalfIntervalBeep.Name = "LblHalfIntervalBeep";
            LblHalfIntervalBeep.Size = new Size(149, 19);
            LblHalfIntervalBeep.TabIndex = 31;
            LblHalfIntervalBeep.Text = "Beep de meio intervalo";
            ToolTip.SetToolTip(LblHalfIntervalBeep, "Se haverá um beep indicador que divide o tempo do intervalo em metade esforço e metade descanso.");
            // 
            // BtnStartBeepTest
            // 
            BtnStartBeepTest.Enabled = false;
            BtnStartBeepTest.Font = new Font("Segoe UI", 10F);
            BtnStartBeepTest.ImageIndex = 2;
            BtnStartBeepTest.ImageList = ImageListBeepsAndCompetition;
            BtnStartBeepTest.Location = new Point(775, 88);
            BtnStartBeepTest.Name = "BtnStartBeepTest";
            BtnStartBeepTest.Size = new Size(131, 30);
            BtnStartBeepTest.TabIndex = 26;
            BtnStartBeepTest.Text = "Beep de início";
            BtnStartBeepTest.TextImageRelation = TextImageRelation.ImageBeforeText;
            ToolTip.SetToolTip(BtnStartBeepTest, "Aciona uma vez o som utilizado como beep de início do intervalo.");
            BtnStartBeepTest.UseVisualStyleBackColor = true;
            BtnStartBeepTest.Click += BtnStartBeepTest_Click;
            // 
            // BtnCountdownBeepTest
            // 
            BtnCountdownBeepTest.Enabled = false;
            BtnCountdownBeepTest.Font = new Font("Segoe UI", 10F);
            BtnCountdownBeepTest.ImageIndex = 1;
            BtnCountdownBeepTest.ImageList = ImageListBeepsAndCompetition;
            BtnCountdownBeepTest.Location = new Point(627, 88);
            BtnCountdownBeepTest.Name = "BtnCountdownBeepTest";
            BtnCountdownBeepTest.Size = new Size(131, 30);
            BtnCountdownBeepTest.TabIndex = 25;
            BtnCountdownBeepTest.Text = "Beep contagem";
            BtnCountdownBeepTest.TextImageRelation = TextImageRelation.ImageBeforeText;
            ToolTip.SetToolTip(BtnCountdownBeepTest, "Aciona uma vez o som utilizado como beep de contagem e meio intervalo.");
            BtnCountdownBeepTest.UseVisualStyleBackColor = true;
            BtnCountdownBeepTest.Click += BtnCountdownBeepTest_Click;
            // 
            // BeepMediaPlayer
            // 
            BeepMediaPlayer.Enabled = true;
            BeepMediaPlayer.Location = new Point(480, 127);
            BeepMediaPlayer.Name = "BeepMediaPlayer";
            BeepMediaPlayer.OcxState = (AxHost.State)resources.GetObject("BeepMediaPlayer.OcxState");
            BeepMediaPlayer.Size = new Size(425, 30);
            BeepMediaPlayer.TabIndex = 28;
            // 
            // MenuStrip
            // 
            MenuStrip.Font = new Font("Segoe UI", 10F);
            MenuStrip.Items.AddRange(new ToolStripItem[] { ToolStripMenuItemConfiguration, ToolStripMenuItemAbout, ToolStripMenuItemCloseProgram });
            MenuStrip.Location = new Point(0, 0);
            MenuStrip.Name = "MenuStrip";
            MenuStrip.RenderMode = ToolStripRenderMode.Professional;
            MenuStrip.Size = new Size(950, 27);
            MenuStrip.TabIndex = 0;
            // 
            // ToolStripMenuItemConfiguration
            // 
            ToolStripMenuItemConfiguration.DropDownItems.AddRange(new ToolStripItem[] { ToolStripMenuItemOpenConfiguration, ToolStripMenuItemSaveConfiguration, ToolStripMenuItemSaveConfigurationAs, ToolStripMenuItemEnableTextAndButtonsTips, ToolStripMenuItemReloadBeepList });
            ToolStripMenuItemConfiguration.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            ToolStripMenuItemConfiguration.Name = "ToolStripMenuItemConfiguration";
            ToolStripMenuItemConfiguration.Size = new Size(111, 23);
            ToolStripMenuItemConfiguration.Text = "Configuração";
            // 
            // ToolStripMenuItemOpenConfiguration
            // 
            ToolStripMenuItemOpenConfiguration.Font = new Font("Segoe UI", 10F);
            ToolStripMenuItemOpenConfiguration.Name = "ToolStripMenuItemOpenConfiguration";
            ToolStripMenuItemOpenConfiguration.Size = new Size(280, 24);
            ToolStripMenuItemOpenConfiguration.Text = "Abrir configuração";
            ToolStripMenuItemOpenConfiguration.Click += ToolStripMenuItemOpenConfiguration_Click;
            // 
            // ToolStripMenuItemSaveConfiguration
            // 
            ToolStripMenuItemSaveConfiguration.Enabled = false;
            ToolStripMenuItemSaveConfiguration.Font = new Font("Segoe UI", 10F);
            ToolStripMenuItemSaveConfiguration.Name = "ToolStripMenuItemSaveConfiguration";
            ToolStripMenuItemSaveConfiguration.Size = new Size(280, 24);
            ToolStripMenuItemSaveConfiguration.Text = "Salvar configuração atual";
            ToolStripMenuItemSaveConfiguration.Click += ToolStripMenuItemSaveConfiguration_Click;
            // 
            // ToolStripMenuItemSaveConfigurationAs
            // 
            ToolStripMenuItemSaveConfigurationAs.Font = new Font("Segoe UI", 10F);
            ToolStripMenuItemSaveConfigurationAs.Name = "ToolStripMenuItemSaveConfigurationAs";
            ToolStripMenuItemSaveConfigurationAs.Size = new Size(280, 24);
            ToolStripMenuItemSaveConfigurationAs.Text = "Salvar configuração como";
            ToolStripMenuItemSaveConfigurationAs.Click += ToolStripMenuItemSaveConfigurationAs_Click;
            // 
            // ToolStripMenuItemEnableTextAndButtonsTips
            // 
            ToolStripMenuItemEnableTextAndButtonsTips.Font = new Font("Segoe UI", 10F);
            ToolStripMenuItemEnableTextAndButtonsTips.Name = "ToolStripMenuItemEnableTextAndButtonsTips";
            ToolStripMenuItemEnableTextAndButtonsTips.Size = new Size(280, 24);
            ToolStripMenuItemEnableTextAndButtonsTips.Text = "Habilitar dicas de textos e botões";
            ToolStripMenuItemEnableTextAndButtonsTips.Click += ToolStripMenuItemEnableTextAndButtonsTips_Click;
            // 
            // ToolStripMenuItemReloadBeepList
            // 
            ToolStripMenuItemReloadBeepList.Font = new Font("Segoe UI", 10F);
            ToolStripMenuItemReloadBeepList.Name = "ToolStripMenuItemReloadBeepList";
            ToolStripMenuItemReloadBeepList.Size = new Size(280, 24);
            ToolStripMenuItemReloadBeepList.Text = "Recarregar lista de beeps";
            ToolStripMenuItemReloadBeepList.Click += ToolStripMenuItemReloadBeepList_Click;
            // 
            // ToolStripMenuItemAbout
            // 
            ToolStripMenuItemAbout.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            ToolStripMenuItemAbout.Name = "ToolStripMenuItemAbout";
            ToolStripMenuItemAbout.Size = new Size(61, 23);
            ToolStripMenuItemAbout.Text = "Sobre";
            ToolStripMenuItemAbout.Click += ToolStripMenuItemAbout_Click;
            // 
            // ToolStripMenuItemCloseProgram
            // 
            ToolStripMenuItemCloseProgram.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            ToolStripMenuItemCloseProgram.Name = "ToolStripMenuItemCloseProgram";
            ToolStripMenuItemCloseProgram.Size = new Size(47, 23);
            ToolStripMenuItemCloseProgram.Text = "Sair";
            ToolStripMenuItemCloseProgram.Click += ToolStripMenuItemCloseProgram_Click;
            // 
            // GroupBoxBeepsCtrls
            // 
            GroupBoxBeepsCtrls.Anchor = AnchorStyles.Left;
            GroupBoxBeepsCtrls.Controls.Add(LblHalfIntervalBeep);
            GroupBoxBeepsCtrls.Controls.Add(ComboBoxHalfIntervalBeep);
            GroupBoxBeepsCtrls.Controls.Add(BtnStartBeepTest);
            GroupBoxBeepsCtrls.Controls.Add(BtnCountdownBeepTest);
            GroupBoxBeepsCtrls.Controls.Add(BtnBeepsTest);
            GroupBoxBeepsCtrls.Controls.Add(BeepMediaPlayer);
            GroupBoxBeepsCtrls.Controls.Add(LblBeepPair);
            GroupBoxBeepsCtrls.Controls.Add(ComboBoxBeepPair);
            GroupBoxBeepsCtrls.Controls.Add(GroupBoxBeepsAmountAndTimes);
            GroupBoxBeepsCtrls.Controls.Add(GroupBoxBeepsVolume);
            GroupBoxBeepsCtrls.Controls.Add(LblTestMessages);
            GroupBoxBeepsCtrls.FlatStyle = FlatStyle.System;
            GroupBoxBeepsCtrls.Font = new Font("Segoe UI", 10F);
            GroupBoxBeepsCtrls.Location = new Point(15, 35);
            GroupBoxBeepsCtrls.Name = "GroupBoxBeepsCtrls";
            GroupBoxBeepsCtrls.Size = new Size(920, 172);
            GroupBoxBeepsCtrls.TabIndex = 9;
            GroupBoxBeepsCtrls.TabStop = false;
            GroupBoxBeepsCtrls.Text = "CONTROLES DOS BEEPS";
            // 
            // ComboBoxHalfIntervalBeep
            // 
            ComboBoxHalfIntervalBeep.DropDownStyle = ComboBoxStyle.DropDownList;
            ComboBoxHalfIntervalBeep.Enabled = false;
            ComboBoxHalfIntervalBeep.FormattingEnabled = true;
            ComboBoxHalfIntervalBeep.Items.AddRange(new object[] { "NÃO - Descanso é o tempo restante do tiro", "SIM - Transição do esforço para o descanso" });
            ComboBoxHalfIntervalBeep.Location = new Point(168, 54);
            ComboBoxHalfIntervalBeep.Name = "ComboBoxHalfIntervalBeep";
            ComboBoxHalfIntervalBeep.Size = new Size(287, 25);
            ComboBoxHalfIntervalBeep.TabIndex = 30;
            ComboBoxHalfIntervalBeep.SelectedIndexChanged += ComboBoxHalfIntervalBeep_SelectedIndexChanged;
            // 
            // BtnBeepsTest
            // 
            BtnBeepsTest.Enabled = false;
            BtnBeepsTest.Font = new Font("Segoe UI", 10F);
            BtnBeepsTest.ImageIndex = 0;
            BtnBeepsTest.ImageList = ImageListBeepsAndCompetition;
            BtnBeepsTest.Location = new Point(479, 88);
            BtnBeepsTest.Name = "BtnBeepsTest";
            BtnBeepsTest.Size = new Size(131, 30);
            BtnBeepsTest.TabIndex = 24;
            BtnBeepsTest.Text = "Testar beeps";
            BtnBeepsTest.TextImageRelation = TextImageRelation.ImageBeforeText;
            BtnBeepsTest.UseVisualStyleBackColor = true;
            BtnBeepsTest.Click += BtnBeepsTest_Click;
            BtnBeepsTest.MouseHover += BtnBeepTest_MouseHover;
            // 
            // ComboBoxBeepPair
            // 
            ComboBoxBeepPair.DropDownStyle = ComboBoxStyle.DropDownList;
            ComboBoxBeepPair.Enabled = false;
            ComboBoxBeepPair.FormattingEnabled = true;
            ComboBoxBeepPair.Location = new Point(168, 22);
            ComboBoxBeepPair.Name = "ComboBoxBeepPair";
            ComboBoxBeepPair.Size = new Size(287, 25);
            ComboBoxBeepPair.TabIndex = 11;
            ComboBoxBeepPair.SelectedIndexChanged += ComboBoxBeepPair_SelectedIndexChanged;
            // 
            // GroupBoxBeepsAmountAndTimes
            // 
            GroupBoxBeepsAmountAndTimes.Controls.Add(NumUDTimeForEachBeep);
            GroupBoxBeepsAmountAndTimes.Controls.Add(NumUDAmountOfBeeps);
            GroupBoxBeepsAmountAndTimes.Controls.Add(LblAmountOfBeeps);
            GroupBoxBeepsAmountAndTimes.Controls.Add(NumUDTimeForResumeMusics);
            GroupBoxBeepsAmountAndTimes.Controls.Add(NumUDTimeBeforePlayBeeps);
            GroupBoxBeepsAmountAndTimes.Controls.Add(LblTimeForResumeMusics);
            GroupBoxBeepsAmountAndTimes.Controls.Add(LblTimeForEachBeep);
            GroupBoxBeepsAmountAndTimes.Controls.Add(LblTimeBeforePlayBeeps);
            GroupBoxBeepsAmountAndTimes.Location = new Point(15, 80);
            GroupBoxBeepsAmountAndTimes.Name = "GroupBoxBeepsAmountAndTimes";
            GroupBoxBeepsAmountAndTimes.Size = new Size(440, 78);
            GroupBoxBeepsAmountAndTimes.TabIndex = 29;
            GroupBoxBeepsAmountAndTimes.TabStop = false;
            // 
            // NumUDTimeForEachBeep
            // 
            NumUDTimeForEachBeep.DecimalPlaces = 2;
            NumUDTimeForEachBeep.Enabled = false;
            NumUDTimeForEachBeep.Increment = new decimal(new int[] { 25, 0, 0, 131072 });
            NumUDTimeForEachBeep.Location = new Point(388, 46);
            NumUDTimeForEachBeep.Maximum = new decimal(new int[] { 3, 0, 0, 0 });
            NumUDTimeForEachBeep.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            NumUDTimeForEachBeep.Name = "NumUDTimeForEachBeep";
            NumUDTimeForEachBeep.Size = new Size(45, 25);
            NumUDTimeForEachBeep.TabIndex = 17;
            NumUDTimeForEachBeep.TextAlign = HorizontalAlignment.Center;
            NumUDTimeForEachBeep.Value = new decimal(new int[] { 1, 0, 0, 0 });
            NumUDTimeForEachBeep.ValueChanged += NumUDTimeForEachBeep_ValueChanged;
            // 
            // NumUDAmountOfBeeps
            // 
            NumUDAmountOfBeeps.Enabled = false;
            NumUDAmountOfBeeps.Location = new Point(388, 16);
            NumUDAmountOfBeeps.Maximum = new decimal(new int[] { 10, 0, 0, 0 });
            NumUDAmountOfBeeps.Minimum = new decimal(new int[] { 2, 0, 0, 0 });
            NumUDAmountOfBeeps.Name = "NumUDAmountOfBeeps";
            NumUDAmountOfBeeps.Size = new Size(45, 25);
            NumUDAmountOfBeeps.TabIndex = 15;
            NumUDAmountOfBeeps.TextAlign = HorizontalAlignment.Center;
            NumUDAmountOfBeeps.Value = new decimal(new int[] { 5, 0, 0, 0 });
            NumUDAmountOfBeeps.ValueChanged += NumUDAmountOfBeeps_ValueChanged;
            // 
            // NumUDTimeForResumeMusics
            // 
            NumUDTimeForResumeMusics.Enabled = false;
            NumUDTimeForResumeMusics.Location = new Point(190, 46);
            NumUDTimeForResumeMusics.Maximum = new decimal(new int[] { 5, 0, 0, 0 });
            NumUDTimeForResumeMusics.Name = "NumUDTimeForResumeMusics";
            NumUDTimeForResumeMusics.Size = new Size(45, 25);
            NumUDTimeForResumeMusics.TabIndex = 19;
            NumUDTimeForResumeMusics.TextAlign = HorizontalAlignment.Center;
            NumUDTimeForResumeMusics.ValueChanged += NumUDTimeForResumeMusics_ValueChanged;
            // 
            // NumUDTimeBeforePlayBeeps
            // 
            NumUDTimeBeforePlayBeeps.Enabled = false;
            NumUDTimeBeforePlayBeeps.Location = new Point(190, 16);
            NumUDTimeBeforePlayBeeps.Maximum = new decimal(new int[] { 5, 0, 0, 0 });
            NumUDTimeBeforePlayBeeps.Name = "NumUDTimeBeforePlayBeeps";
            NumUDTimeBeforePlayBeeps.Size = new Size(45, 25);
            NumUDTimeBeforePlayBeeps.TabIndex = 13;
            NumUDTimeBeforePlayBeeps.TextAlign = HorizontalAlignment.Center;
            NumUDTimeBeforePlayBeeps.ValueChanged += NumUDTimeBeforePlayBeeps_ValueChanged;
            // 
            // GroupBoxBeepsVolume
            // 
            GroupBoxBeepsVolume.Controls.Add(LblBeepVolumePercent);
            GroupBoxBeepsVolume.Controls.Add(LblBeepVolume);
            GroupBoxBeepsVolume.Controls.Add(TBBeepVolume);
            GroupBoxBeepsVolume.FlatStyle = FlatStyle.System;
            GroupBoxBeepsVolume.Font = new Font("Segoe UI", 10F);
            GroupBoxBeepsVolume.Location = new Point(480, 14);
            GroupBoxBeepsVolume.Name = "GroupBoxBeepsVolume";
            GroupBoxBeepsVolume.Size = new Size(425, 66);
            GroupBoxBeepsVolume.TabIndex = 20;
            GroupBoxBeepsVolume.TabStop = false;
            // 
            // LblBeepVolumePercent
            // 
            LblBeepVolumePercent.Font = new Font("Segoe UI", 9F);
            LblBeepVolumePercent.Location = new Point(377, 28);
            LblBeepVolumePercent.Name = "LblBeepVolumePercent";
            LblBeepVolumePercent.Size = new Size(35, 15);
            LblBeepVolumePercent.TabIndex = 23;
            LblBeepVolumePercent.Text = "50%";
            LblBeepVolumePercent.TextAlign = ContentAlignment.TopRight;
            // 
            // TBBeepVolume
            // 
            TBBeepVolume.AutoSize = false;
            TBBeepVolume.BackColor = SystemColors.Control;
            TBBeepVolume.LargeChange = 0;
            TBBeepVolume.Location = new Point(70, 23);
            TBBeepVolume.Maximum = 0;
            TBBeepVolume.Minimum = -100;
            TBBeepVolume.Name = "TBBeepVolume";
            TBBeepVolume.RightToLeft = RightToLeft.Yes;
            TBBeepVolume.RightToLeftLayout = true;
            TBBeepVolume.Size = new Size(315, 30);
            TBBeepVolume.SmallChange = 5;
            TBBeepVolume.TabIndex = 22;
            TBBeepVolume.TickFrequency = 5;
            TBBeepVolume.Value = -50;
            TBBeepVolume.ValueChanged += TBBeepVolume_ValueChanged;
            // 
            // GroupBoxProgram
            // 
            GroupBoxProgram.Controls.Add(GroupBoxTransitionsControls);
            GroupBoxProgram.Controls.Add(BtnStopCompetition);
            GroupBoxProgram.Controls.Add(LblIntervalsElapsed);
            GroupBoxProgram.Controls.Add(BtnStartCompetition);
            GroupBoxProgram.Controls.Add(LblCurrentIntervalElapsedTime);
            GroupBoxProgram.Controls.Add(LblCompetitionElapsedTime);
            GroupBoxProgram.Controls.Add(ProgressBarCompetitionElapsedTime);
            GroupBoxProgram.Controls.Add(ProgressBarCurrentIntervalElapsed);
            GroupBoxProgram.Controls.Add(GroupBoxCompetitionControls);
            GroupBoxProgram.Controls.Add(GroupBoxProgramming);
            GroupBoxProgram.FlatStyle = FlatStyle.System;
            GroupBoxProgram.Font = new Font("Segoe UI", 10F);
            GroupBoxProgram.Location = new Point(15, 517);
            GroupBoxProgram.Name = "GroupBoxProgram";
            GroupBoxProgram.Size = new Size(920, 140);
            GroupBoxProgram.TabIndex = 49;
            GroupBoxProgram.TabStop = false;
            GroupBoxProgram.Text = "PROGRAMAÇÃO E CONTROLE DOS INTERVALOS";
            // 
            // GroupBoxTransitionsControls
            // 
            GroupBoxTransitionsControls.Controls.Add(CheckBoxStopMusicsAtEnd);
            GroupBoxTransitionsControls.Controls.Add(NumUDTimeToVolMin);
            GroupBoxTransitionsControls.Controls.Add(LblTimeToChangeVolume);
            GroupBoxTransitionsControls.Controls.Add(CheckBoxStartWithBeeps);
            GroupBoxTransitionsControls.Location = new Point(253, 14);
            GroupBoxTransitionsControls.Name = "GroupBoxTransitionsControls";
            GroupBoxTransitionsControls.Size = new Size(202, 113);
            GroupBoxTransitionsControls.TabIndex = 73;
            GroupBoxTransitionsControls.TabStop = false;
            // 
            // GroupBoxCompetitionControls
            // 
            GroupBoxCompetitionControls.Controls.Add(NumUDCompetitionIntervalSeconds);
            GroupBoxCompetitionControls.Controls.Add(NumUDCompetitionAmountIntervals);
            GroupBoxCompetitionControls.Controls.Add(LblIntervalAndCompetitionFormatedTime);
            GroupBoxCompetitionControls.Controls.Add(LblCompetitionIntervalTime);
            GroupBoxCompetitionControls.Controls.Add(LblCompetitionAmountIntervals);
            GroupBoxCompetitionControls.Location = new Point(480, 14);
            GroupBoxCompetitionControls.Name = "GroupBoxCompetitionControls";
            GroupBoxCompetitionControls.Size = new Size(208, 113);
            GroupBoxCompetitionControls.TabIndex = 72;
            GroupBoxCompetitionControls.TabStop = false;
            // 
            // NumUDCompetitionIntervalSeconds
            // 
            NumUDCompetitionIntervalSeconds.Enabled = false;
            NumUDCompetitionIntervalSeconds.Increment = new decimal(new int[] { 15, 0, 0, 0 });
            NumUDCompetitionIntervalSeconds.Location = new Point(161, 46);
            NumUDCompetitionIntervalSeconds.Maximum = new decimal(new int[] { 360, 0, 0, 0 });
            NumUDCompetitionIntervalSeconds.Minimum = new decimal(new int[] { 15, 0, 0, 0 });
            NumUDCompetitionIntervalSeconds.Name = "NumUDCompetitionIntervalSeconds";
            NumUDCompetitionIntervalSeconds.Size = new Size(40, 25);
            NumUDCompetitionIntervalSeconds.TabIndex = 63;
            NumUDCompetitionIntervalSeconds.TextAlign = HorizontalAlignment.Center;
            NumUDCompetitionIntervalSeconds.Value = new decimal(new int[] { 15, 0, 0, 0 });
            NumUDCompetitionIntervalSeconds.ValueChanged += NumUDCompetitionIntervalSeconds_ValueChanged;
            // 
            // NumUDCompetitionAmountIntervals
            // 
            NumUDCompetitionAmountIntervals.Enabled = false;
            NumUDCompetitionAmountIntervals.Location = new Point(161, 16);
            NumUDCompetitionAmountIntervals.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            NumUDCompetitionAmountIntervals.Name = "NumUDCompetitionAmountIntervals";
            NumUDCompetitionAmountIntervals.Size = new Size(40, 25);
            NumUDCompetitionAmountIntervals.TabIndex = 61;
            NumUDCompetitionAmountIntervals.TextAlign = HorizontalAlignment.Center;
            NumUDCompetitionAmountIntervals.Value = new decimal(new int[] { 1, 0, 0, 0 });
            NumUDCompetitionAmountIntervals.ValueChanged += NumUDCompetitionAmountIntervals_ValueChanged;
            // 
            // GroupBoxProgramming
            // 
            GroupBoxProgramming.Controls.Add(ComboBoxProgramming);
            GroupBoxProgramming.Controls.Add(LblProgramming);
            GroupBoxProgramming.Controls.Add(ComboBoxRepeatPlaylist);
            GroupBoxProgramming.Controls.Add(ComboBoxInitialization);
            GroupBoxProgramming.Controls.Add(LblInitialization);
            GroupBoxProgramming.Controls.Add(LblRepeatPlaylist);
            GroupBoxProgramming.Location = new Point(15, 14);
            GroupBoxProgramming.Name = "GroupBoxProgramming";
            GroupBoxProgramming.Size = new Size(231, 113);
            GroupBoxProgramming.TabIndex = 74;
            GroupBoxProgramming.TabStop = false;
            // 
            // ComboBoxProgramming
            // 
            ComboBoxProgramming.DropDownStyle = ComboBoxStyle.DropDownList;
            ComboBoxProgramming.Enabled = false;
            ComboBoxProgramming.FormattingEnabled = true;
            ComboBoxProgramming.Items.AddRange(new object[] { "Somente beeps", "Músicas e beeps" });
            ComboBoxProgramming.Location = new Point(102, 16);
            ComboBoxProgramming.Name = "ComboBoxProgramming";
            ComboBoxProgramming.Size = new Size(122, 25);
            ComboBoxProgramming.TabIndex = 51;
            ComboBoxProgramming.SelectedIndexChanged += ComboBoxProgramming_SelectedIndexChanged;
            // 
            // ComboBoxRepeatPlaylist
            // 
            ComboBoxRepeatPlaylist.DropDownStyle = ComboBoxStyle.DropDownList;
            ComboBoxRepeatPlaylist.Enabled = false;
            ComboBoxRepeatPlaylist.FormattingEnabled = true;
            ComboBoxRepeatPlaylist.Items.AddRange(new object[] { "Lista normal", "Lista randômica", "Inverter anterior" });
            ComboBoxRepeatPlaylist.Location = new Point(102, 80);
            ComboBoxRepeatPlaylist.Name = "ComboBoxRepeatPlaylist";
            ComboBoxRepeatPlaylist.Size = new Size(122, 25);
            ComboBoxRepeatPlaylist.TabIndex = 55;
            ComboBoxRepeatPlaylist.SelectedIndexChanged += ComboBoxRepeatPlaylist_SelectedIndexChanged;
            // 
            // ComboBoxInitialization
            // 
            ComboBoxInitialization.DropDownStyle = ComboBoxStyle.DropDownList;
            ComboBoxInitialization.Enabled = false;
            ComboBoxInitialization.FormattingEnabled = true;
            ComboBoxInitialization.Items.AddRange(new object[] { "Começo da lista", "Música tocando" });
            ComboBoxInitialization.Location = new Point(102, 48);
            ComboBoxInitialization.Name = "ComboBoxInitialization";
            ComboBoxInitialization.Size = new Size(122, 25);
            ComboBoxInitialization.TabIndex = 53;
            ComboBoxInitialization.SelectedIndexChanged += ComboBoxInitialization_SelectedIndexChanged;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSize = true;
            ClientSize = new Size(950, 671);
            Controls.Add(GroupBoxBeepsCtrls);
            Controls.Add(GroupBoxMusicCtrls);
            Controls.Add(MenuStrip);
            Controls.Add(GroupBoxProgram);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MainMenuStrip = MenuStrip;
            MaximizeBox = false;
            Name = "MainForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "CONTROLE DE TEMPO PARA COMPETIÇÕES INTERVALADAS";
            Activated += MainForm_Activated;
            FormClosing += MainForm_FormClosing;
            ((System.ComponentModel.ISupportInitialize)MusicMediaPlayer).EndInit();
            ((System.ComponentModel.ISupportInitialize)TBMusicVolumeMin).EndInit();
            GroupBoxMusicCtrls.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)PBMusicPlayerVolumeBanner).EndInit();
            GroupBoxMusicVolume.ResumeLayout(false);
            GroupBoxMusicVolume.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)TBMusicCurrentVol).EndInit();
            GroupBoxMusicVols.ResumeLayout(false);
            GroupBoxMusicVols.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)TBMusicVolumeMax).EndInit();
            ((System.ComponentModel.ISupportInitialize)NumUDTimeToVolMin).EndInit();
            ((System.ComponentModel.ISupportInitialize)BeepMediaPlayer).EndInit();
            MenuStrip.ResumeLayout(false);
            MenuStrip.PerformLayout();
            GroupBoxBeepsCtrls.ResumeLayout(false);
            GroupBoxBeepsCtrls.PerformLayout();
            GroupBoxBeepsAmountAndTimes.ResumeLayout(false);
            GroupBoxBeepsAmountAndTimes.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)NumUDTimeForEachBeep).EndInit();
            ((System.ComponentModel.ISupportInitialize)NumUDAmountOfBeeps).EndInit();
            ((System.ComponentModel.ISupportInitialize)NumUDTimeForResumeMusics).EndInit();
            ((System.ComponentModel.ISupportInitialize)NumUDTimeBeforePlayBeeps).EndInit();
            GroupBoxBeepsVolume.ResumeLayout(false);
            GroupBoxBeepsVolume.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)TBBeepVolume).EndInit();
            GroupBoxProgram.ResumeLayout(false);
            GroupBoxTransitionsControls.ResumeLayout(false);
            GroupBoxTransitionsControls.PerformLayout();
            GroupBoxCompetitionControls.ResumeLayout(false);
            GroupBoxCompetitionControls.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)NumUDCompetitionIntervalSeconds).EndInit();
            ((System.ComponentModel.ISupportInitialize)NumUDCompetitionAmountIntervals).EndInit();
            GroupBoxProgramming.ResumeLayout(false);
            GroupBoxProgramming.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private AxWMPLib.AxWindowsMediaPlayer MusicMediaPlayer;
        private TrackBar TBMusicVolumeMin;
        private Button BtnStartCompetition;
        private Button BtnStopCompetition;
        private GroupBox GroupBoxMusicCtrls;
        private Label LblMusicVolMin;
        private GroupBox GroupBoxMusicVols;
        private Label LblMusicVolMax;
        private ToolTip ToolTip;
        private TrackBar TBMusicVolumeMax;
        private AxWMPLib.AxWindowsMediaPlayer BeepMediaPlayer;
        private MenuStrip MenuStrip;
        private ToolStripMenuItem ToolStripMenuItemConfiguration;
        private ToolStripMenuItem ToolStripMenuItemAbout;
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
        private GroupBox GroupBoxBeepsVolume;
        private Label LblBeepVolumePercent;
        private Label LblBeepVolume;
        private TrackBar TBBeepVolume;
        private GroupBox GroupBoxMusicVolume;
        private Label LblMusicCurrentVolPercent;
        private Label LblMusicCurrentVol;
        private TrackBar TBMusicCurrentVol;
        private Label LblTestMessages;
        private Button BtnAddMusics;
        private Button BtnClearMusicsList;
        private NumericUpDown NumUDTimeToVolMin;
        private Label LblTimeToChangeVolume;
        private GroupBox GroupBoxProgram;
        private ComboBox ComboBoxProgramming;
        private Label LblProgramming;
        private Label LblInitialization;
        private ComboBox ComboBoxInitialization;
        private NumericUpDown NumUDCompetitionAmountIntervals;
        private NumericUpDown NumUDCompetitionIntervalSeconds;
        private Label LblCompetitionIntervalTime;
        private CheckBox CheckBoxStartWithBeeps;
        private CheckBox CheckBoxStopMusicsAtEnd;
        private Label LblCompetitionAmountIntervals;
        private Label LblIntervalsElapsed;
        private ProgressBar ProgressBarCompetitionElapsedTime;
        private ProgressBar ProgressBarCurrentIntervalElapsed;
        private Label LblCompetitionElapsedTime;
        private CheckBox ToggleMarkAndExclude;
        private CheckBox ToggleSeeDetails;
        private Label LblRepeatPlaylist;
        private ComboBox ComboBoxRepeatPlaylist;
        private CheckBox TogglePlaylistMode;
        private CheckBox TogglePlayMusicBySelection;
        private ListView ListViewMusics;
        private ToolStripMenuItem ToolStripMenuItemEnableTextAndButtonsTips;
        private ToolStripMenuItem ToolStripMenuItemOpenConfiguration;
        private ToolStripMenuItem ToolStripMenuItemSaveConfiguration;
        private ToolStripMenuItem ToolStripMenuItemReloadBeepList;
        private ToolStripMenuItem ToolStripMenuItemCloseProgram;
        private Label LblCurrentIntervalElapsedTime;
        private ToolStripMenuItem ToolStripMenuItemSaveConfigurationAs;
        private Label LblIntervalAndCompetitionFormatedTime;
        private ImageList ImageListMusics;
        private ImageList ImageListBeepsAndCompetition;
        private Label LblListViewMusicsStatus;
        private PictureBox PBMusicPlayerVolumeBanner;
        private GroupBox GroupBoxBeepsAmountAndTimes;
        private GroupBox GroupBoxCompetitionControls;
        private GroupBox GroupBoxTransitionsControls;
        private GroupBox GroupBoxProgramming;
        private Button BtnBeepsTest;
        private ComboBox ComboBoxHalfIntervalBeep;
        private Label LblHalfIntervalBeep;
    }
}