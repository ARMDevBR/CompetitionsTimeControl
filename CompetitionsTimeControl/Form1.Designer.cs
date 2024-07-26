﻿namespace CompetitionsTimeControl
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
            BtnStopCompetition = new Button();
            GroupBoxMusicCtrls = new GroupBox();
            ToggleSeeDetails = new CheckBox();
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
            LblCompetitionTotalTime = new Label();
            BtnConfigTest = new Button();
            BeepMediaPlayer = new AxWMPLib.AxWindowsMediaPlayer();
            MenuStrip = new MenuStrip();
            arquivoToolStripMenuItem = new ToolStripMenuItem();
            sobreToolStripMenuItem = new ToolStripMenuItem();
            GroupBoxBeepsCtrls = new GroupBox();
            GroupBoxControlsAndTest = new GroupBox();
            LblBeepVolumePercent = new Label();
            TBBeepVolume = new TrackBar();
            BtnStartBeepTest = new Button();
            BtnCountdownBeepTest = new Button();
            LblTestMessages = new Label();
            NumUDTimeBeforePlayBeeps = new NumericUpDown();
            NumUDTimeForResumeMusics = new NumericUpDown();
            NumUDAmountOfBeeps = new NumericUpDown();
            NumUDTimeForEachBeep = new NumericUpDown();
            ComboBoxBeepPair = new ComboBox();
            GroupBoxProgram = new GroupBox();
            ComboBoxRepeatPlaylist = new ComboBox();
            NumUDCompetitionAmountIntervals = new NumericUpDown();
            NumUDCompetitionIntervalSeconds = new NumericUpDown();
            ComboBoxInitialization = new ComboBox();
            ComboBoxProgramming = new ComboBox();
            ((System.ComponentModel.ISupportInitialize)MusicMediaPlayer).BeginInit();
            ((System.ComponentModel.ISupportInitialize)TBMusicVolumeMin).BeginInit();
            GroupBoxMusicCtrls.SuspendLayout();
            GroupBoxMusicVolume.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)TBMusicCurrentVol).BeginInit();
            GroupBoxMusicVols.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)TBMusicVolumeMax).BeginInit();
            ((System.ComponentModel.ISupportInitialize)NumUDTimeToVolMin).BeginInit();
            ((System.ComponentModel.ISupportInitialize)BeepMediaPlayer).BeginInit();
            MenuStrip.SuspendLayout();
            GroupBoxBeepsCtrls.SuspendLayout();
            GroupBoxControlsAndTest.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)TBBeepVolume).BeginInit();
            ((System.ComponentModel.ISupportInitialize)NumUDTimeBeforePlayBeeps).BeginInit();
            ((System.ComponentModel.ISupportInitialize)NumUDTimeForResumeMusics).BeginInit();
            ((System.ComponentModel.ISupportInitialize)NumUDAmountOfBeeps).BeginInit();
            ((System.ComponentModel.ISupportInitialize)NumUDTimeForEachBeep).BeginInit();
            GroupBoxProgram.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)NumUDCompetitionAmountIntervals).BeginInit();
            ((System.ComponentModel.ISupportInitialize)NumUDCompetitionIntervalSeconds).BeginInit();
            SuspendLayout();
            // 
            // MusicMediaPlayer
            // 
            MusicMediaPlayer.Enabled = true;
            MusicMediaPlayer.Location = new Point(485, 127);
            MusicMediaPlayer.Name = "MusicMediaPlayer";
            MusicMediaPlayer.OcxState = (AxHost.State)resources.GetObject("MusicMediaPlayer.OcxState");
            MusicMediaPlayer.Size = new Size(420, 90);
            MusicMediaPlayer.TabIndex = 35;
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
            TBMusicVolumeMin.TabIndex = 30;
            TBMusicVolumeMin.TickFrequency = 5;
            TBMusicVolumeMin.Value = -30;
            TBMusicVolumeMin.ValueChanged += TBMusicVolumeMin_ValueChanged;
            // 
            // BtnStartCompetition
            // 
            BtnStartCompetition.Enabled = false;
            BtnStartCompetition.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            BtnStartCompetition.Location = new Point(702, 22);
            BtnStartCompetition.Name = "BtnStartCompetition";
            BtnStartCompetition.Size = new Size(68, 30);
            BtnStartCompetition.TabIndex = 55;
            BtnStartCompetition.Text = "Iniciar";
            ToolTip.SetToolTip(BtnStartCompetition, "Inicia/Pausa/Retoma a competição.");
            BtnStartCompetition.UseVisualStyleBackColor = true;
            BtnStartCompetition.Click += BtnStartCompetition_Click;
            // 
            // BtnStopCompetition
            // 
            BtnStopCompetition.Enabled = false;
            BtnStopCompetition.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            BtnStopCompetition.Location = new Point(774, 22);
            BtnStopCompetition.Name = "BtnStopCompetition";
            BtnStopCompetition.Size = new Size(68, 30);
            BtnStopCompetition.TabIndex = 56;
            BtnStopCompetition.Text = "Parar";
            ToolTip.SetToolTip(BtnStopCompetition, "Encerra a competição.");
            BtnStopCompetition.UseVisualStyleBackColor = true;
            BtnStopCompetition.Click += BtnStopCompetition_Click;
            // 
            // GroupBoxMusicCtrls
            // 
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
            GroupBoxMusicCtrls.Location = new Point(15, 224);
            GroupBoxMusicCtrls.Name = "GroupBoxMusicCtrls";
            GroupBoxMusicCtrls.Size = new Size(920, 300);
            GroupBoxMusicCtrls.TabIndex = 20;
            GroupBoxMusicCtrls.TabStop = false;
            GroupBoxMusicCtrls.Text = "CONTROLES DAS MÚSICAS";
            // 
            // ToggleSeeDetails
            // 
            ToggleSeeDetails.Appearance = Appearance.Button;
            ToggleSeeDetails.Checked = true;
            ToggleSeeDetails.CheckState = CheckState.Checked;
            ToggleSeeDetails.Location = new Point(325, 257);
            ToggleSeeDetails.Name = "ToggleSeeDetails";
            ToggleSeeDetails.Size = new Size(130, 30);
            ToggleSeeDetails.TabIndex = 26;
            ToggleSeeDetails.Text = "Ver simplificada";
            ToggleSeeDetails.TextAlign = ContentAlignment.MiddleCenter;
            ToolTip.SetToolTip(ToggleSeeDetails, "Alterna entre visualização simplificada e detalhada da lista de músicas.");
            ToggleSeeDetails.UseVisualStyleBackColor = true;
            ToggleSeeDetails.CheckedChanged += ToggleSeeDetails_CheckedChanged;
            // 
            // ToggleMarkAndExclude
            // 
            ToggleMarkAndExclude.Appearance = Appearance.Button;
            ToggleMarkAndExclude.Enabled = false;
            ToggleMarkAndExclude.Location = new Point(325, 25);
            ToggleMarkAndExclude.Name = "ToggleMarkAndExclude";
            ToggleMarkAndExclude.Size = new Size(130, 30);
            ToggleMarkAndExclude.TabIndex = 23;
            ToggleMarkAndExclude.Text = "Marque e exclua";
            ToggleMarkAndExclude.TextAlign = ContentAlignment.MiddleCenter;
            ToolTip.SetToolTip(ToggleMarkAndExclude, "Clique para ativar a função de exclusão das músicas na lista.");
            ToggleMarkAndExclude.UseVisualStyleBackColor = true;
            ToggleMarkAndExclude.CheckedChanged += ToggleMarkAndExclude_CheckedChanged;
            // 
            // BtnClearMusicsList
            // 
            BtnClearMusicsList.Enabled = false;
            BtnClearMusicsList.Font = new Font("Segoe UI", 10F);
            BtnClearMusicsList.Location = new Point(170, 25);
            BtnClearMusicsList.Name = "BtnClearMusicsList";
            BtnClearMusicsList.Size = new Size(130, 30);
            BtnClearMusicsList.TabIndex = 22;
            BtnClearMusicsList.Text = "Excluir músicas";
            ToolTip.SetToolTip(BtnClearMusicsList, "Exclui todas as músicas da lista.");
            BtnClearMusicsList.UseVisualStyleBackColor = true;
            BtnClearMusicsList.Click += BtnClearMusicsList_Click;
            // 
            // BtnAddMusics
            // 
            BtnAddMusics.Font = new Font("Segoe UI", 10F);
            BtnAddMusics.Location = new Point(15, 24);
            BtnAddMusics.Name = "BtnAddMusics";
            BtnAddMusics.Size = new Size(130, 30);
            BtnAddMusics.TabIndex = 21;
            BtnAddMusics.Text = "Incluir músicas";
            ToolTip.SetToolTip(BtnAddMusics, "Adiciona mais músicas na lista.");
            BtnAddMusics.UseVisualStyleBackColor = true;
            BtnAddMusics.Click += BtnAddMusics_Click;
            // 
            // ListViewMusics
            // 
            ListViewMusics.HeaderStyle = ColumnHeaderStyle.Nonclickable;
            ListViewMusics.Location = new Point(15, 61);
            ListViewMusics.Name = "ListViewMusics";
            ListViewMusics.Size = new Size(440, 190);
            ListViewMusics.TabIndex = 27;
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
            GroupBoxMusicVolume.Location = new Point(485, 214);
            GroupBoxMusicVolume.Name = "GroupBoxMusicVolume";
            GroupBoxMusicVolume.Size = new Size(420, 73);
            GroupBoxMusicVolume.TabIndex = 35;
            GroupBoxMusicVolume.TabStop = false;
            // 
            // LblMusicCurrentVolPercent
            // 
            LblMusicCurrentVolPercent.Font = new Font("Segoe UI", 9F);
            LblMusicCurrentVolPercent.Location = new Point(372, 30);
            LblMusicCurrentVolPercent.Name = "LblMusicCurrentVolPercent";
            LblMusicCurrentVolPercent.Size = new Size(35, 15);
            LblMusicCurrentVolPercent.TabIndex = 38;
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
            LblMusicCurrentVol.TabIndex = 36;
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
            TBMusicCurrentVol.TabIndex = 37;
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
            GroupBoxMusicVols.Location = new Point(485, 15);
            GroupBoxMusicVols.Name = "GroupBoxMusicVols";
            GroupBoxMusicVols.Size = new Size(420, 106);
            GroupBoxMusicVols.TabIndex = 28;
            GroupBoxMusicVols.TabStop = false;
            GroupBoxMusicVols.Text = "Limite dos Volumes";
            // 
            // LblMusicVolMaxPercent
            // 
            LblMusicVolMaxPercent.Font = new Font("Segoe UI", 9F);
            LblMusicVolMaxPercent.Location = new Point(372, 65);
            LblMusicVolMaxPercent.Name = "LblMusicVolMaxPercent";
            LblMusicVolMaxPercent.Size = new Size(35, 15);
            LblMusicVolMaxPercent.TabIndex = 34;
            LblMusicVolMaxPercent.Text = "70%";
            LblMusicVolMaxPercent.TextAlign = ContentAlignment.TopRight;
            // 
            // LblMusicVolMinPercent
            // 
            LblMusicVolMinPercent.Font = new Font("Segoe UI", 9F);
            LblMusicVolMinPercent.Location = new Point(372, 30);
            LblMusicVolMinPercent.Name = "LblMusicVolMinPercent";
            LblMusicVolMinPercent.Size = new Size(35, 15);
            LblMusicVolMinPercent.TabIndex = 31;
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
            LblMusicVolMax.TabIndex = 32;
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
            TBMusicVolumeMax.TabIndex = 33;
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
            LblMusicVolMin.TabIndex = 29;
            LblMusicVolMin.Text = "Mínimo";
            ToolTip.SetToolTip(LblMusicVolMin, "Define o volume mínimo para as músicas enquanto os beeps tocam.");
            // 
            // TogglePlaylistMode
            // 
            TogglePlaylistMode.Appearance = Appearance.Button;
            TogglePlaylistMode.Location = new Point(170, 257);
            TogglePlaylistMode.Name = "TogglePlaylistMode";
            TogglePlaylistMode.Size = new Size(130, 30);
            TogglePlaylistMode.TabIndex = 25;
            TogglePlaylistMode.Text = "Lista sequencial";
            TogglePlaylistMode.TextAlign = ContentAlignment.MiddleCenter;
            ToolTip.SetToolTip(TogglePlaylistMode, "Playlist vai tocar sequencialmente.");
            TogglePlaylistMode.UseVisualStyleBackColor = true;
            TogglePlaylistMode.CheckedChanged += TogglePlaylistMode_CheckedChanged;
            // 
            // TogglePlayMusicBySelection
            // 
            TogglePlayMusicBySelection.Appearance = Appearance.Button;
            TogglePlayMusicBySelection.Enabled = false;
            TogglePlayMusicBySelection.Location = new Point(15, 257);
            TogglePlayMusicBySelection.Name = "TogglePlayMusicBySelection";
            TogglePlayMusicBySelection.Size = new Size(130, 30);
            TogglePlayMusicBySelection.TabIndex = 24;
            TogglePlayMusicBySelection.Text = "Selecionar e ouvir";
            TogglePlayMusicBySelection.TextAlign = ContentAlignment.MiddleCenter;
            ToolTip.SetToolTip(TogglePlayMusicBySelection, "Habilita tocar a música ao selecioná-la na lista.");
            TogglePlayMusicBySelection.UseVisualStyleBackColor = true;
            TogglePlayMusicBySelection.CheckedChanged += TogglePlayMusicBySelection_CheckedChanged;
            // 
            // NumUDTimeToVolMin
            // 
            NumUDTimeToVolMin.Enabled = false;
            NumUDTimeToVolMin.Location = new Point(410, 22);
            NumUDTimeToVolMin.Maximum = new decimal(new int[] { 5, 0, 0, 0 });
            NumUDTimeToVolMin.Minimum = new decimal(new int[] { 2, 0, 0, 0 });
            NumUDTimeToVolMin.Name = "NumUDTimeToVolMin";
            NumUDTimeToVolMin.Size = new Size(45, 25);
            NumUDTimeToVolMin.TabIndex = 47;
            NumUDTimeToVolMin.TextAlign = HorizontalAlignment.Center;
            NumUDTimeToVolMin.Value = new decimal(new int[] { 3, 0, 0, 0 });
            NumUDTimeToVolMin.ValueChanged += NumUDTimeToVolMin_ValueChanged;
            // 
            // LblTimeToChangeVolume
            // 
            LblTimeToChangeVolume.AutoSize = true;
            LblTimeToChangeVolume.Location = new Point(257, 25);
            LblTimeToChangeVolume.Name = "LblTimeToChangeVolume";
            LblTimeToChangeVolume.Size = new Size(153, 19);
            LblTimeToChangeVolume.TabIndex = 46;
            LblTimeToChangeVolume.Text = "Tempo troca de volume";
            ToolTip.SetToolTip(LblTimeToChangeVolume, "Tempo em segundos para alternar volumes das músicas antes ou após evento dos beeps.");
            // 
            // ToolTip
            // 
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
            LblBeepPair.TabIndex = 1;
            LblBeepPair.Text = "Par de sons dos beeps";
            ToolTip.SetToolTip(LblBeepPair, "Define o par dos beeps (tonalidade) a tocar.");
            // 
            // LblTimeBeforePlayBeeps
            // 
            LblTimeBeforePlayBeeps.AutoSize = true;
            LblTimeBeforePlayBeeps.Location = new Point(15, 55);
            LblTimeBeforePlayBeeps.Name = "LblTimeBeforePlayBeeps";
            LblTimeBeforePlayBeeps.Size = new Size(187, 19);
            LblTimeBeforePlayBeeps.TabIndex = 3;
            LblTimeBeforePlayBeeps.Text = "Tempo extra antes dos beeps";
            ToolTip.SetToolTip(LblTimeBeforePlayBeeps, "Tempo antes dos beeps e após volume mínimo das músicas.");
            // 
            // LblAmountOfBeeps
            // 
            LblAmountOfBeeps.AutoSize = true;
            LblAmountOfBeeps.Location = new Point(15, 85);
            LblAmountOfBeeps.Name = "LblAmountOfBeeps";
            LblAmountOfBeeps.Size = new Size(140, 19);
            LblAmountOfBeeps.TabIndex = 5;
            LblAmountOfBeeps.Text = "Quantidade de beeps";
            ToolTip.SetToolTip(LblAmountOfBeeps, "Quantidade de beeps (incluindo o último beep de início da prova).");
            // 
            // LblTimeForEachBeep
            // 
            LblTimeForEachBeep.AutoSize = true;
            LblTimeForEachBeep.Location = new Point(15, 115);
            LblTimeForEachBeep.Name = "LblTimeForEachBeep";
            LblTimeForEachBeep.Size = new Size(147, 19);
            LblTimeForEachBeep.TabIndex = 7;
            LblTimeForEachBeep.Text = "Tempo para cada beep";
            ToolTip.SetToolTip(LblTimeForEachBeep, "Tempo em segundos para tocar outro beep de contagem.");
            // 
            // LblTimeForResumeMusics
            // 
            LblTimeForResumeMusics.AutoSize = true;
            LblTimeForResumeMusics.Location = new Point(15, 145);
            LblTimeForResumeMusics.Name = "LblTimeForResumeMusics";
            LblTimeForResumeMusics.Size = new Size(186, 19);
            LblTimeForResumeMusics.TabIndex = 9;
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
            LblBeepVolume.TabIndex = 12;
            LblBeepVolume.Text = "Volume";
            ToolTip.SetToolTip(LblBeepVolume, "Define o volume para os beeps.");
            // 
            // LblProgramming
            // 
            LblProgramming.AutoSize = true;
            LblProgramming.Location = new Point(15, 25);
            LblProgramming.Name = "LblProgramming";
            LblProgramming.Size = new Size(90, 19);
            LblProgramming.TabIndex = 40;
            LblProgramming.Text = "Programação";
            ToolTip.SetToolTip(LblProgramming, "Define se haverá música de fundo com os beeps na competição.");
            // 
            // LblInitialization
            // 
            LblInitialization.AutoSize = true;
            LblInitialization.Location = new Point(15, 55);
            LblInitialization.Name = "LblInitialization";
            LblInitialization.Size = new Size(80, 19);
            LblInitialization.TabIndex = 42;
            LblInitialization.Text = "Inicialização";
            ToolTip.SetToolTip(LblInitialization, "Define se a playlist começará do início ou continua da música que está tocando.");
            // 
            // LblCompetitionIntervalTime
            // 
            LblCompetitionIntervalTime.AutoSize = true;
            LblCompetitionIntervalTime.Location = new Point(480, 55);
            LblCompetitionIntervalTime.Name = "LblCompetitionIntervalTime";
            LblCompetitionIntervalTime.Size = new Size(158, 19);
            LblCompetitionIntervalTime.TabIndex = 52;
            LblCompetitionIntervalTime.Text = "Tempo de cada intervalo";
            ToolTip.SetToolTip(LblCompetitionIntervalTime, "Tempo em segundos de cada tiro (Máximo de 360s [6 mins]).");
            // 
            // CheckBoxStopMusicsAtEnd
            // 
            CheckBoxStopMusicsAtEnd.AutoSize = true;
            CheckBoxStopMusicsAtEnd.Enabled = false;
            CheckBoxStopMusicsAtEnd.Location = new Point(259, 85);
            CheckBoxStopMusicsAtEnd.Name = "CheckBoxStopMusicsAtEnd";
            CheckBoxStopMusicsAtEnd.Size = new Size(198, 23);
            CheckBoxStopMusicsAtEnd.TabIndex = 49;
            CheckBoxStopMusicsAtEnd.Text = "Competição finaliza músicas";
            ToolTip.SetToolTip(CheckBoxStopMusicsAtEnd, "Se as músicas param ao final da competição ou continuam tocando até terminar a playlist.");
            CheckBoxStopMusicsAtEnd.UseVisualStyleBackColor = true;
            CheckBoxStopMusicsAtEnd.CheckedChanged += CheckBoxStopMusicsAtEnd_CheckedChanged;
            // 
            // LblCompetitionAmountIntervals
            // 
            LblCompetitionAmountIntervals.AutoSize = true;
            LblCompetitionAmountIntervals.Location = new Point(480, 25);
            LblCompetitionAmountIntervals.Name = "LblCompetitionAmountIntervals";
            LblCompetitionAmountIntervals.Size = new Size(167, 19);
            LblCompetitionAmountIntervals.TabIndex = 50;
            LblCompetitionAmountIntervals.Text = "Qtde. de Intervalos (Tiros)";
            ToolTip.SetToolTip(LblCompetitionAmountIntervals, "Quantos tiros terá a competição.");
            // 
            // ProgressBarCompetitionElapsedTime
            // 
            ProgressBarCompetitionElapsedTime.Location = new Point(703, 85);
            ProgressBarCompetitionElapsedTime.Name = "ProgressBarCompetitionElapsedTime";
            ProgressBarCompetitionElapsedTime.Size = new Size(138, 23);
            ProgressBarCompetitionElapsedTime.TabIndex = 59;
            ToolTip.SetToolTip(ProgressBarCompetitionElapsedTime, "Progresso da competição.");
            // 
            // LblIntervalsElapsed
            // 
            LblIntervalsElapsed.BackColor = Color.LimeGreen;
            LblIntervalsElapsed.BorderStyle = BorderStyle.FixedSingle;
            LblIntervalsElapsed.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            LblIntervalsElapsed.Location = new Point(847, 22);
            LblIntervalsElapsed.Name = "LblIntervalsElapsed";
            LblIntervalsElapsed.Size = new Size(58, 58);
            LblIntervalsElapsed.TabIndex = 58;
            LblIntervalsElapsed.Text = "100/100";
            LblIntervalsElapsed.TextAlign = ContentAlignment.MiddleCenter;
            ToolTip.SetToolTip(LblIntervalsElapsed, "Qual o tiro atual em relação ao total de tiros.");
            // 
            // ProgressBarCurrentIntervalElapsed
            // 
            ProgressBarCurrentIntervalElapsed.Location = new Point(703, 57);
            ProgressBarCurrentIntervalElapsed.Name = "ProgressBarCurrentIntervalElapsed";
            ProgressBarCurrentIntervalElapsed.Size = new Size(138, 23);
            ProgressBarCurrentIntervalElapsed.TabIndex = 57;
            ToolTip.SetToolTip(ProgressBarCurrentIntervalElapsed, "Progressão do intervalo atual.");
            // 
            // CheckBoxStartWithBeeps
            // 
            CheckBoxStartWithBeeps.AutoSize = true;
            CheckBoxStartWithBeeps.Enabled = false;
            CheckBoxStartWithBeeps.Location = new Point(259, 55);
            CheckBoxStartWithBeeps.Name = "CheckBoxStartWithBeeps";
            CheckBoxStartWithBeeps.Size = new Size(197, 23);
            CheckBoxStartWithBeeps.TabIndex = 48;
            CheckBoxStartWithBeeps.Text = "Prova já começa com beeps";
            ToolTip.SetToolTip(CheckBoxStartWithBeeps, "Se o programa realiza beeps antes de inicar o contador ou inica sem beeps.");
            CheckBoxStartWithBeeps.UseVisualStyleBackColor = true;
            CheckBoxStartWithBeeps.CheckedChanged += CheckBoxStartWithBeeps_CheckedChanged;
            // 
            // LblCompetitionElapsedTime
            // 
            LblCompetitionElapsedTime.BackColor = Color.DeepSkyBlue;
            LblCompetitionElapsedTime.BorderStyle = BorderStyle.FixedSingle;
            LblCompetitionElapsedTime.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            LblCompetitionElapsedTime.Location = new Point(847, 84);
            LblCompetitionElapsedTime.Name = "LblCompetitionElapsedTime";
            LblCompetitionElapsedTime.Size = new Size(58, 25);
            LblCompetitionElapsedTime.TabIndex = 60;
            LblCompetitionElapsedTime.Text = "00:00:00";
            LblCompetitionElapsedTime.TextAlign = ContentAlignment.MiddleCenter;
            ToolTip.SetToolTip(LblCompetitionElapsedTime, "Tempo total decorrido.");
            // 
            // LblRepeatPlaylist
            // 
            LblRepeatPlaylist.AutoSize = true;
            LblRepeatPlaylist.Location = new Point(15, 85);
            LblRepeatPlaylist.Name = "LblRepeatPlaylist";
            LblRepeatPlaylist.Size = new Size(98, 19);
            LblRepeatPlaylist.TabIndex = 44;
            LblRepeatPlaylist.Text = "Repetir Playlist";
            ToolTip.SetToolTip(LblRepeatPlaylist, "Como recomeçar a playlist se ela terminar antes do fim da prova.");
            // 
            // LblCompetitionTotalTime
            // 
            LblCompetitionTotalTime.BorderStyle = BorderStyle.Fixed3D;
            LblCompetitionTotalTime.Font = new Font("Segoe UI", 9F);
            LblCompetitionTotalTime.Location = new Point(483, 81);
            LblCompetitionTotalTime.Name = "LblCompetitionTotalTime";
            LblCompetitionTotalTime.Size = new Size(209, 30);
            LblCompetitionTotalTime.TabIndex = 54;
            LblCompetitionTotalTime.Text = "Tempo total da competição 00:00:00";
            LblCompetitionTotalTime.TextAlign = ContentAlignment.MiddleCenter;
            ToolTip.SetToolTip(LblCompetitionTotalTime, "Tempo total no formato de horas, minutos e segundos (hh:mm:ss).");
            // 
            // BtnConfigTest
            // 
            BtnConfigTest.Enabled = false;
            BtnConfigTest.Font = new Font("Segoe UI", 10F);
            BtnConfigTest.Location = new Point(15, 60);
            BtnConfigTest.Name = "BtnConfigTest";
            BtnConfigTest.Size = new Size(120, 30);
            BtnConfigTest.TabIndex = 15;
            BtnConfigTest.Text = "Testar config.";
            BtnConfigTest.UseVisualStyleBackColor = true;
            BtnConfigTest.Click += BtnConfigTest_Click;
            BtnConfigTest.MouseHover += BtnConfigTest_MouseHover;
            // 
            // BeepMediaPlayer
            // 
            BeepMediaPlayer.Enabled = true;
            BeepMediaPlayer.Location = new Point(485, 125);
            BeepMediaPlayer.Name = "BeepMediaPlayer";
            BeepMediaPlayer.OcxState = (AxHost.State)resources.GetObject("BeepMediaPlayer.OcxState");
            BeepMediaPlayer.Size = new Size(420, 45);
            BeepMediaPlayer.TabIndex = 19;
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
            GroupBoxBeepsCtrls.Location = new Point(15, 35);
            GroupBoxBeepsCtrls.Name = "GroupBoxBeepsCtrls";
            GroupBoxBeepsCtrls.Size = new Size(920, 184);
            GroupBoxBeepsCtrls.TabIndex = 0;
            GroupBoxBeepsCtrls.TabStop = false;
            GroupBoxBeepsCtrls.Text = "CONTROLES DOS BEEPS";
            // 
            // GroupBoxControlsAndTest
            // 
            GroupBoxControlsAndTest.Controls.Add(LblBeepVolumePercent);
            GroupBoxControlsAndTest.Controls.Add(LblBeepVolume);
            GroupBoxControlsAndTest.Controls.Add(TBBeepVolume);
            GroupBoxControlsAndTest.Controls.Add(BtnConfigTest);
            GroupBoxControlsAndTest.Controls.Add(BtnStartBeepTest);
            GroupBoxControlsAndTest.Controls.Add(BtnCountdownBeepTest);
            GroupBoxControlsAndTest.Controls.Add(LblTestMessages);
            GroupBoxControlsAndTest.FlatStyle = FlatStyle.System;
            GroupBoxControlsAndTest.Font = new Font("Segoe UI", 10F);
            GroupBoxControlsAndTest.Location = new Point(485, 13);
            GroupBoxControlsAndTest.Name = "GroupBoxControlsAndTest";
            GroupBoxControlsAndTest.Size = new Size(420, 105);
            GroupBoxControlsAndTest.TabIndex = 11;
            GroupBoxControlsAndTest.TabStop = false;
            GroupBoxControlsAndTest.Text = "Controles e testes";
            // 
            // LblBeepVolumePercent
            // 
            LblBeepVolumePercent.Font = new Font("Segoe UI", 9F);
            LblBeepVolumePercent.Location = new Point(372, 28);
            LblBeepVolumePercent.Name = "LblBeepVolumePercent";
            LblBeepVolumePercent.Size = new Size(35, 15);
            LblBeepVolumePercent.TabIndex = 14;
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
            TBBeepVolume.TabIndex = 13;
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
            BtnStartBeepTest.TabIndex = 17;
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
            BtnCountdownBeepTest.TabIndex = 16;
            BtnCountdownBeepTest.Text = "Beep contagem";
            BtnCountdownBeepTest.UseVisualStyleBackColor = true;
            BtnCountdownBeepTest.Click += BtnCountdownBeepTest_Click;
            // 
            // LblTestMessages
            // 
            LblTestMessages.BorderStyle = BorderStyle.FixedSingle;
            LblTestMessages.Font = new Font("Segoe UI", 9F);
            LblTestMessages.Location = new Point(15, 60);
            LblTestMessages.Name = "LblTestMessages";
            LblTestMessages.Size = new Size(390, 30);
            LblTestMessages.TabIndex = 18;
            LblTestMessages.Text = "Antes dos beeps 00.000 | Até último beep 00.000 | Após beeps 00.000";
            LblTestMessages.TextAlign = ContentAlignment.MiddleCenter;
            LblTestMessages.Visible = false;
            // 
            // NumUDTimeBeforePlayBeeps
            // 
            NumUDTimeBeforePlayBeeps.Enabled = false;
            NumUDTimeBeforePlayBeeps.Location = new Point(410, 52);
            NumUDTimeBeforePlayBeeps.Maximum = new decimal(new int[] { 5, 0, 0, 0 });
            NumUDTimeBeforePlayBeeps.Name = "NumUDTimeBeforePlayBeeps";
            NumUDTimeBeforePlayBeeps.Size = new Size(45, 25);
            NumUDTimeBeforePlayBeeps.TabIndex = 4;
            NumUDTimeBeforePlayBeeps.TextAlign = HorizontalAlignment.Center;
            NumUDTimeBeforePlayBeeps.ValueChanged += NumUDTimeBeforePlayBeeps_ValueChanged;
            // 
            // NumUDTimeForResumeMusics
            // 
            NumUDTimeForResumeMusics.Enabled = false;
            NumUDTimeForResumeMusics.Location = new Point(410, 145);
            NumUDTimeForResumeMusics.Maximum = new decimal(new int[] { 5, 0, 0, 0 });
            NumUDTimeForResumeMusics.Name = "NumUDTimeForResumeMusics";
            NumUDTimeForResumeMusics.Size = new Size(45, 25);
            NumUDTimeForResumeMusics.TabIndex = 10;
            NumUDTimeForResumeMusics.TextAlign = HorizontalAlignment.Center;
            NumUDTimeForResumeMusics.Value = new decimal(new int[] { 2, 0, 0, 0 });
            NumUDTimeForResumeMusics.ValueChanged += NumUDTimeForResumeMusics_ValueChanged;
            // 
            // NumUDAmountOfBeeps
            // 
            NumUDAmountOfBeeps.Enabled = false;
            NumUDAmountOfBeeps.Location = new Point(410, 83);
            NumUDAmountOfBeeps.Maximum = new decimal(new int[] { 10, 0, 0, 0 });
            NumUDAmountOfBeeps.Minimum = new decimal(new int[] { 2, 0, 0, 0 });
            NumUDAmountOfBeeps.Name = "NumUDAmountOfBeeps";
            NumUDAmountOfBeeps.Size = new Size(45, 25);
            NumUDAmountOfBeeps.TabIndex = 6;
            NumUDAmountOfBeeps.TextAlign = HorizontalAlignment.Center;
            NumUDAmountOfBeeps.Value = new decimal(new int[] { 5, 0, 0, 0 });
            NumUDAmountOfBeeps.ValueChanged += NumUDAmountOfBeeps_ValueChanged;
            // 
            // NumUDTimeForEachBeep
            // 
            NumUDTimeForEachBeep.DecimalPlaces = 2;
            NumUDTimeForEachBeep.Enabled = false;
            NumUDTimeForEachBeep.Increment = new decimal(new int[] { 25, 0, 0, 131072 });
            NumUDTimeForEachBeep.Location = new Point(410, 114);
            NumUDTimeForEachBeep.Maximum = new decimal(new int[] { 3, 0, 0, 0 });
            NumUDTimeForEachBeep.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            NumUDTimeForEachBeep.Name = "NumUDTimeForEachBeep";
            NumUDTimeForEachBeep.Size = new Size(45, 25);
            NumUDTimeForEachBeep.TabIndex = 8;
            NumUDTimeForEachBeep.TextAlign = HorizontalAlignment.Center;
            NumUDTimeForEachBeep.Value = new decimal(new int[] { 1, 0, 0, 0 });
            NumUDTimeForEachBeep.ValueChanged += NumUDTimeForEachBeep_ValueChanged;
            // 
            // ComboBoxBeepPair
            // 
            ComboBoxBeepPair.DropDownStyle = ComboBoxStyle.DropDownList;
            ComboBoxBeepPair.FormattingEnabled = true;
            ComboBoxBeepPair.Location = new Point(165, 22);
            ComboBoxBeepPair.Name = "ComboBoxBeepPair";
            ComboBoxBeepPair.Size = new Size(290, 25);
            ComboBoxBeepPair.TabIndex = 2;
            ComboBoxBeepPair.SelectedIndexChanged += ComboBoxBeepPair_SelectedIndexChanged;
            // 
            // GroupBoxProgram
            // 
            GroupBoxProgram.Controls.Add(NumUDTimeToVolMin);
            GroupBoxProgram.Controls.Add(LblRepeatPlaylist);
            GroupBoxProgram.Controls.Add(ComboBoxRepeatPlaylist);
            GroupBoxProgram.Controls.Add(LblCompetitionElapsedTime);
            GroupBoxProgram.Controls.Add(ProgressBarCompetitionElapsedTime);
            GroupBoxProgram.Controls.Add(LblIntervalsElapsed);
            GroupBoxProgram.Controls.Add(LblCompetitionTotalTime);
            GroupBoxProgram.Controls.Add(LblCompetitionAmountIntervals);
            GroupBoxProgram.Controls.Add(BtnStartCompetition);
            GroupBoxProgram.Controls.Add(BtnStopCompetition);
            GroupBoxProgram.Controls.Add(LblCompetitionIntervalTime);
            GroupBoxProgram.Controls.Add(NumUDCompetitionAmountIntervals);
            GroupBoxProgram.Controls.Add(NumUDCompetitionIntervalSeconds);
            GroupBoxProgram.Controls.Add(CheckBoxStopMusicsAtEnd);
            GroupBoxProgram.Controls.Add(CheckBoxStartWithBeeps);
            GroupBoxProgram.Controls.Add(LblInitialization);
            GroupBoxProgram.Controls.Add(ComboBoxInitialization);
            GroupBoxProgram.Controls.Add(LblProgramming);
            GroupBoxProgram.Controls.Add(ComboBoxProgramming);
            GroupBoxProgram.Controls.Add(ProgressBarCurrentIntervalElapsed);
            GroupBoxProgram.Controls.Add(LblTimeToChangeVolume);
            GroupBoxProgram.FlatStyle = FlatStyle.System;
            GroupBoxProgram.Font = new Font("Segoe UI", 10F);
            GroupBoxProgram.Location = new Point(15, 529);
            GroupBoxProgram.Name = "GroupBoxProgram";
            GroupBoxProgram.Size = new Size(920, 123);
            GroupBoxProgram.TabIndex = 39;
            GroupBoxProgram.TabStop = false;
            GroupBoxProgram.Text = "PROGRAMAÇÃO E CONTROLE DOS INTERVALOS";
            // 
            // ComboBoxRepeatPlaylist
            // 
            ComboBoxRepeatPlaylist.DropDownStyle = ComboBoxStyle.DropDownList;
            ComboBoxRepeatPlaylist.Enabled = false;
            ComboBoxRepeatPlaylist.FormattingEnabled = true;
            ComboBoxRepeatPlaylist.Items.AddRange(new object[] { "Lista aleatória", "Lista sequencial", "Inverter anterior" });
            ComboBoxRepeatPlaylist.Location = new Point(115, 82);
            ComboBoxRepeatPlaylist.Name = "ComboBoxRepeatPlaylist";
            ComboBoxRepeatPlaylist.Size = new Size(130, 25);
            ComboBoxRepeatPlaylist.TabIndex = 45;
            ComboBoxRepeatPlaylist.SelectedIndexChanged += ComboBoxRepeatPlaylist_SelectedIndexChanged;
            // 
            // NumUDCompetitionAmountIntervals
            // 
            NumUDCompetitionAmountIntervals.Enabled = false;
            NumUDCompetitionAmountIntervals.Location = new Point(647, 22);
            NumUDCompetitionAmountIntervals.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            NumUDCompetitionAmountIntervals.Name = "NumUDCompetitionAmountIntervals";
            NumUDCompetitionAmountIntervals.Size = new Size(45, 25);
            NumUDCompetitionAmountIntervals.TabIndex = 51;
            NumUDCompetitionAmountIntervals.TextAlign = HorizontalAlignment.Center;
            NumUDCompetitionAmountIntervals.Value = new decimal(new int[] { 1, 0, 0, 0 });
            NumUDCompetitionAmountIntervals.ValueChanged += NumUDCompetitionAmountIntervals_ValueChanged;
            // 
            // NumUDCompetitionIntervalSeconds
            // 
            NumUDCompetitionIntervalSeconds.Enabled = false;
            NumUDCompetitionIntervalSeconds.Increment = new decimal(new int[] { 15, 0, 0, 0 });
            NumUDCompetitionIntervalSeconds.Location = new Point(647, 52);
            NumUDCompetitionIntervalSeconds.Maximum = new decimal(new int[] { 360, 0, 0, 0 });
            NumUDCompetitionIntervalSeconds.Minimum = new decimal(new int[] { 15, 0, 0, 0 });
            NumUDCompetitionIntervalSeconds.Name = "NumUDCompetitionIntervalSeconds";
            NumUDCompetitionIntervalSeconds.Size = new Size(45, 25);
            NumUDCompetitionIntervalSeconds.TabIndex = 53;
            NumUDCompetitionIntervalSeconds.TextAlign = HorizontalAlignment.Center;
            NumUDCompetitionIntervalSeconds.Value = new decimal(new int[] { 15, 0, 0, 0 });
            NumUDCompetitionIntervalSeconds.ValueChanged += NumUDCompetitionIntervalSeconds_ValueChanged;
            // 
            // ComboBoxInitialization
            // 
            ComboBoxInitialization.DropDownStyle = ComboBoxStyle.DropDownList;
            ComboBoxInitialization.Enabled = false;
            ComboBoxInitialization.FormattingEnabled = true;
            ComboBoxInitialization.Items.AddRange(new object[] { "Música tocando", "Começo da lista" });
            ComboBoxInitialization.Location = new Point(115, 52);
            ComboBoxInitialization.Name = "ComboBoxInitialization";
            ComboBoxInitialization.Size = new Size(130, 25);
            ComboBoxInitialization.TabIndex = 43;
            ComboBoxInitialization.SelectedIndexChanged += ComboBoxInitialization_SelectedIndexChanged;
            // 
            // ComboBoxProgramming
            // 
            ComboBoxProgramming.DropDownStyle = ComboBoxStyle.DropDownList;
            ComboBoxProgramming.Enabled = false;
            ComboBoxProgramming.FormattingEnabled = true;
            ComboBoxProgramming.Items.AddRange(new object[] { "Somente beeps", "Músicas e beeps" });
            ComboBoxProgramming.Location = new Point(115, 22);
            ComboBoxProgramming.Name = "ComboBoxProgramming";
            ComboBoxProgramming.Size = new Size(130, 25);
            ComboBoxProgramming.TabIndex = 41;
            ComboBoxProgramming.SelectedIndexChanged += ComboBoxProgramming_SelectedIndexChanged;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSize = true;
            ClientSize = new Size(950, 661);
            Controls.Add(GroupBoxBeepsCtrls);
            Controls.Add(GroupBoxMusicCtrls);
            Controls.Add(MenuStrip);
            Controls.Add(GroupBoxProgram);
            FormBorderStyle = FormBorderStyle.FixedSingle;
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
            GroupBoxControlsAndTest.ResumeLayout(false);
            GroupBoxControlsAndTest.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)TBBeepVolume).EndInit();
            ((System.ComponentModel.ISupportInitialize)NumUDTimeBeforePlayBeeps).EndInit();
            ((System.ComponentModel.ISupportInitialize)NumUDTimeForResumeMusics).EndInit();
            ((System.ComponentModel.ISupportInitialize)NumUDAmountOfBeeps).EndInit();
            ((System.ComponentModel.ISupportInitialize)NumUDTimeForEachBeep).EndInit();
            GroupBoxProgram.ResumeLayout(false);
            GroupBoxProgram.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)NumUDCompetitionAmountIntervals).EndInit();
            ((System.ComponentModel.ISupportInitialize)NumUDCompetitionIntervalSeconds).EndInit();
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
        private Label LblTestMessages;
        private Button BtnAddMusics;
        private Button BtnClearMusicsList;
        private NumericUpDown NumUDTimeToVolMin;
        private Label LblTimeToChangeVolume;
        private Button button3;
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
        private Label LblCompetitionTotalTime;
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
    }
}
