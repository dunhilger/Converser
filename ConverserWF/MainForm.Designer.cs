namespace ConverserWF
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
            Label BrowseFileText;
            Label label2;
            Label label1;
            Label label3;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            DirectoryImportButton = new Button();
            DirectoryExportButton = new Button();
            BrowseDirectoryImportField = new TextBox();
            BrowseDirectoryExportField = new TextBox();
            ResetButton = new Button();
            DataLoadButton = new Button();
            FileSizeLabel = new Label();
            DataLoadProgressBar = new ProgressBar();
            CategoryTreePanel = new TreeView();
            CheckAll = new CheckBox();
            FeedCreatorProgressBar = new ProgressBar();
            panel4 = new Panel();
            panel8 = new Panel();
            panel7 = new Panel();
            panel6 = new Panel();
            panel5 = new Panel();
            panel9 = new Panel();
            panel10 = new Panel();
            panel11 = new Panel();
            panel12 = new Panel();
            panel13 = new Panel();
            panel1 = new Panel();
            panel3 = new Panel();
            panel14 = new Panel();
            panel15 = new Panel();
            panel16 = new Panel();
            RadioButtonPanel = new Panel();
            ExportButton = new Button();
            RadioButtonVK = new RadioButton();
            RadioButton2gis = new RadioButton();
            RadioButtonYandex = new RadioButton();
            panel17 = new Panel();
            panel18 = new Panel();
            panel19 = new Panel();
            panel20 = new Panel();
            BrowseFileText = new Label();
            label2 = new Label();
            label1 = new Label();
            label3 = new Label();
            panel4.SuspendLayout();
            panel9.SuspendLayout();
            panel1.SuspendLayout();
            RadioButtonPanel.SuspendLayout();
            SuspendLayout();
            // 
            // BrowseFileText
            // 
            BrowseFileText.AutoSize = true;
            BrowseFileText.BackColor = Color.WhiteSmoke;
            BrowseFileText.Font = new Font("Century Gothic", 10.8F, FontStyle.Regular, GraphicsUnit.Point, 204);
            BrowseFileText.ForeColor = Color.SteelBlue;
            BrowseFileText.Location = new Point(59, 31);
            BrowseFileText.Name = "BrowseFileText";
            BrowseFileText.Size = new Size(158, 21);
            BrowseFileText.TabIndex = 9;
            BrowseFileText.Text = "Исходный файл";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.BackColor = Color.WhiteSmoke;
            label2.Font = new Font("Century Gothic", 10.8F, FontStyle.Regular, GraphicsUnit.Point, 204);
            label2.ForeColor = Color.SteelBlue;
            label2.Location = new Point(836, 31);
            label2.Name = "label2";
            label2.Size = new Size(141, 21);
            label2.TabIndex = 19;
            label2.Text = "Категории и id";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = Color.WhiteSmoke;
            label1.Font = new Font("Century Gothic", 10.8F, FontStyle.Regular, GraphicsUnit.Point, 204);
            label1.ForeColor = Color.SteelBlue;
            label1.Location = new Point(60, 214);
            label1.Name = "label1";
            label1.Size = new Size(175, 21);
            label1.TabIndex = 21;
            label1.Text = "Папка назначения";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.BackColor = Color.WhiteSmoke;
            label3.Font = new Font("Century Gothic", 10.8F, FontStyle.Regular, GraphicsUnit.Point, 204);
            label3.ForeColor = Color.SteelBlue;
            label3.Location = new Point(57, 434);
            label3.Name = "label3";
            label3.Size = new Size(160, 21);
            label3.TabIndex = 23;
            label3.Text = "Целевой сервис";
            // 
            // DirectoryImportButton
            // 
            DirectoryImportButton.BackColor = Color.Transparent;
            DirectoryImportButton.ForeColor = Color.Black;
            DirectoryImportButton.Location = new Point(662, 29);
            DirectoryImportButton.Name = "DirectoryImportButton";
            DirectoryImportButton.Size = new Size(42, 29);
            DirectoryImportButton.TabIndex = 6;
            DirectoryImportButton.Text = "...\r\n";
            DirectoryImportButton.UseVisualStyleBackColor = true;
            DirectoryImportButton.Click += BrowseFileImportDirectory_Click;
            // 
            // DirectoryExportButton
            // 
            DirectoryExportButton.ForeColor = Color.Black;
            DirectoryExportButton.Location = new Point(661, 29);
            DirectoryExportButton.Name = "DirectoryExportButton";
            DirectoryExportButton.Size = new Size(42, 29);
            DirectoryExportButton.TabIndex = 7;
            DirectoryExportButton.Text = "...";
            DirectoryExportButton.UseVisualStyleBackColor = true;
            DirectoryExportButton.Click += BrowseExportDirectory_Click;
            // 
            // BrowseDirectoryImportField
            // 
            BrowseDirectoryImportField.AllowDrop = true;
            BrowseDirectoryImportField.BackColor = Color.White;
            BrowseDirectoryImportField.Font = new Font("Tahoma", 10.2F, FontStyle.Regular, GraphicsUnit.Point, 204);
            BrowseDirectoryImportField.ForeColor = Color.DimGray;
            BrowseDirectoryImportField.Location = new Point(21, 30);
            BrowseDirectoryImportField.Name = "BrowseDirectoryImportField";
            BrowseDirectoryImportField.PlaceholderText = "Перетащите сюда файл Excel или кликните для выбора файла на компьютере";
            BrowseDirectoryImportField.ReadOnly = true;
            BrowseDirectoryImportField.Size = new Size(635, 28);
            BrowseDirectoryImportField.TabIndex = 8;
            BrowseDirectoryImportField.Click += BrowseFileImportDirectory_Click;
            BrowseDirectoryImportField.DragDrop += OnDragDrop;
            BrowseDirectoryImportField.DragEnter += OnDragEnter;
            // 
            // BrowseDirectoryExportField
            // 
            BrowseDirectoryExportField.BackColor = Color.White;
            BrowseDirectoryExportField.Font = new Font("Tahoma", 10.2F);
            BrowseDirectoryExportField.ForeColor = Color.DimGray;
            BrowseDirectoryExportField.Location = new Point(20, 29);
            BrowseDirectoryExportField.Name = "BrowseDirectoryExportField";
            BrowseDirectoryExportField.PlaceholderText = "Укажите путь сохранения результата";
            BrowseDirectoryExportField.ReadOnly = true;
            BrowseDirectoryExportField.Size = new Size(635, 28);
            BrowseDirectoryExportField.TabIndex = 11;
            BrowseDirectoryExportField.Click += BrowseExportDirectory_Click;
            // 
            // ResetButton
            // 
            ResetButton.BackColor = Color.LightGray;
            ResetButton.ForeColor = SystemColors.ButtonFace;
            ResetButton.Image = (Image)resources.GetObject("ResetButton.Image");
            ResetButton.Location = new Point(734, 184);
            ResetButton.Name = "ResetButton";
            ResetButton.Size = new Size(36, 34);
            ResetButton.TabIndex = 12;
            ResetButton.UseVisualStyleBackColor = false;
            ResetButton.Click += ResetButton_Click;
            // 
            // DataLoadButton
            // 
            DataLoadButton.AccessibleName = "";
            DataLoadButton.BackColor = Color.White;
            DataLoadButton.Font = new Font("Tahoma", 10.2F, FontStyle.Regular, GraphicsUnit.Point, 204);
            DataLoadButton.ForeColor = Color.Black;
            DataLoadButton.Location = new Point(21, 75);
            DataLoadButton.Name = "DataLoadButton";
            DataLoadButton.Size = new Size(211, 44);
            DataLoadButton.TabIndex = 13;
            DataLoadButton.Text = "Загрузить данные";
            DataLoadButton.UseVisualStyleBackColor = true;
            DataLoadButton.Click += DataLoadButton_Click;
            // 
            // FileSizeLabel
            // 
            FileSizeLabel.AutoSize = true;
            FileSizeLabel.ForeColor = Color.DimGray;
            FileSizeLabel.Location = new Point(274, 87);
            FileSizeLabel.Name = "FileSizeLabel";
            FileSizeLabel.Size = new Size(0, 23);
            FileSizeLabel.TabIndex = 15;
            // 
            // DataLoadProgressBar
            // 
            DataLoadProgressBar.Location = new Point(45, 125);
            DataLoadProgressBar.Name = "DataLoadProgressBar";
            DataLoadProgressBar.RightToLeft = RightToLeft.No;
            DataLoadProgressBar.Size = new Size(162, 5);
            DataLoadProgressBar.Style = ProgressBarStyle.Continuous;
            DataLoadProgressBar.TabIndex = 14;
            DataLoadProgressBar.Visible = false;
            // 
            // CategoryTreePanel
            // 
            CategoryTreePanel.BackColor = SystemColors.MenuBar;
            CategoryTreePanel.FullRowSelect = true;
            CategoryTreePanel.Location = new Point(22, 56);
            CategoryTreePanel.Name = "CategoryTreePanel";
            CategoryTreePanel.Size = new Size(584, 543);
            CategoryTreePanel.TabIndex = 17;
            CategoryTreePanel.AfterCheck += CategoryTree_AfterCheck;
            // 
            // CheckAll
            // 
            CheckAll.AutoSize = true;
            CheckAll.ForeColor = Color.Black;
            CheckAll.Location = new Point(25, 23);
            CheckAll.Name = "CheckAll";
            CheckAll.Size = new Size(130, 27);
            CheckAll.TabIndex = 3;
            CheckAll.Text = "Выбрать все";
            CheckAll.UseVisualStyleBackColor = true;
            CheckAll.Click += CheckAll_Click;
            // 
            // FeedCreatorProgressBar
            // 
            FeedCreatorProgressBar.Location = new Point(23, 185);
            FeedCreatorProgressBar.Name = "FeedCreatorProgressBar";
            FeedCreatorProgressBar.Size = new Size(177, 10);
            FeedCreatorProgressBar.TabIndex = 16;
            FeedCreatorProgressBar.Visible = false;
            // 
            // panel4
            // 
            panel4.AutoSize = true;
            panel4.BackColor = Color.Transparent;
            panel4.Controls.Add(panel8);
            panel4.Controls.Add(panel7);
            panel4.Controls.Add(FileSizeLabel);
            panel4.Controls.Add(DataLoadProgressBar);
            panel4.Controls.Add(panel6);
            panel4.Controls.Add(panel5);
            panel4.Controls.Add(BrowseDirectoryImportField);
            panel4.Controls.Add(DataLoadButton);
            panel4.Controls.Add(DirectoryImportButton);
            panel4.ForeColor = Color.WhiteSmoke;
            panel4.Location = new Point(37, 41);
            panel4.Name = "panel4";
            panel4.Size = new Size(732, 137);
            panel4.TabIndex = 18;
            // 
            // panel8
            // 
            panel8.BackColor = Color.SteelBlue;
            panel8.Dock = DockStyle.Bottom;
            panel8.Location = new Point(1, 136);
            panel8.Name = "panel8";
            panel8.Size = new Size(730, 1);
            panel8.TabIndex = 1;
            // 
            // panel7
            // 
            panel7.BackColor = Color.SteelBlue;
            panel7.Dock = DockStyle.Right;
            panel7.Location = new Point(731, 1);
            panel7.Name = "panel7";
            panel7.Size = new Size(1, 136);
            panel7.TabIndex = 1;
            // 
            // panel6
            // 
            panel6.BackColor = Color.SteelBlue;
            panel6.Dock = DockStyle.Left;
            panel6.Location = new Point(0, 1);
            panel6.Name = "panel6";
            panel6.Size = new Size(1, 136);
            panel6.TabIndex = 1;
            // 
            // panel5
            // 
            panel5.BackColor = Color.SteelBlue;
            panel5.Dock = DockStyle.Top;
            panel5.Location = new Point(0, 0);
            panel5.Name = "panel5";
            panel5.Size = new Size(732, 1);
            panel5.TabIndex = 0;
            // 
            // panel9
            // 
            panel9.AutoSize = true;
            panel9.BackColor = Color.Transparent;
            panel9.Controls.Add(CategoryTreePanel);
            panel9.Controls.Add(panel10);
            panel9.Controls.Add(CheckAll);
            panel9.Controls.Add(panel11);
            panel9.Controls.Add(panel12);
            panel9.Controls.Add(panel13);
            panel9.ForeColor = Color.WhiteSmoke;
            panel9.Location = new Point(810, 42);
            panel9.Name = "panel9";
            panel9.Size = new Size(628, 621);
            panel9.TabIndex = 20;
            // 
            // panel10
            // 
            panel10.BackColor = Color.SteelBlue;
            panel10.Dock = DockStyle.Bottom;
            panel10.Location = new Point(1, 620);
            panel10.Name = "panel10";
            panel10.Size = new Size(626, 1);
            panel10.TabIndex = 1;
            // 
            // panel11
            // 
            panel11.BackColor = Color.SteelBlue;
            panel11.Dock = DockStyle.Right;
            panel11.Location = new Point(627, 1);
            panel11.Name = "panel11";
            panel11.Size = new Size(1, 620);
            panel11.TabIndex = 1;
            // 
            // panel12
            // 
            panel12.BackColor = Color.SteelBlue;
            panel12.Dock = DockStyle.Left;
            panel12.Location = new Point(0, 1);
            panel12.Name = "panel12";
            panel12.Size = new Size(1, 620);
            panel12.TabIndex = 1;
            // 
            // panel13
            // 
            panel13.BackColor = Color.SteelBlue;
            panel13.Dock = DockStyle.Top;
            panel13.Location = new Point(0, 0);
            panel13.Name = "panel13";
            panel13.Size = new Size(628, 1);
            panel13.TabIndex = 0;
            // 
            // panel1
            // 
            panel1.AutoSize = true;
            panel1.BackColor = Color.Transparent;
            panel1.Controls.Add(panel3);
            panel1.Controls.Add(DirectoryExportButton);
            panel1.Controls.Add(panel14);
            panel1.Controls.Add(BrowseDirectoryExportField);
            panel1.Controls.Add(panel15);
            panel1.Controls.Add(panel16);
            panel1.ForeColor = Color.WhiteSmoke;
            panel1.Location = new Point(37, 224);
            panel1.Name = "panel1";
            panel1.Size = new Size(731, 178);
            panel1.TabIndex = 22;
            // 
            // panel3
            // 
            panel3.BackColor = Color.SteelBlue;
            panel3.Dock = DockStyle.Bottom;
            panel3.Location = new Point(1, 177);
            panel3.Name = "panel3";
            panel3.Size = new Size(729, 1);
            panel3.TabIndex = 1;
            // 
            // panel14
            // 
            panel14.BackColor = Color.SteelBlue;
            panel14.Dock = DockStyle.Right;
            panel14.Location = new Point(730, 1);
            panel14.Name = "panel14";
            panel14.Size = new Size(1, 177);
            panel14.TabIndex = 1;
            // 
            // panel15
            // 
            panel15.BackColor = Color.SteelBlue;
            panel15.Dock = DockStyle.Left;
            panel15.Location = new Point(0, 1);
            panel15.Name = "panel15";
            panel15.Size = new Size(1, 177);
            panel15.TabIndex = 1;
            // 
            // panel16
            // 
            panel16.BackColor = Color.SteelBlue;
            panel16.Dock = DockStyle.Top;
            panel16.Location = new Point(0, 0);
            panel16.Name = "panel16";
            panel16.Size = new Size(731, 1);
            panel16.TabIndex = 0;
            // 
            // RadioButtonPanel
            // 
            RadioButtonPanel.AutoSize = true;
            RadioButtonPanel.BackColor = Color.Transparent;
            RadioButtonPanel.Controls.Add(ExportButton);
            RadioButtonPanel.Controls.Add(RadioButtonVK);
            RadioButtonPanel.Controls.Add(RadioButton2gis);
            RadioButtonPanel.Controls.Add(RadioButtonYandex);
            RadioButtonPanel.Controls.Add(panel17);
            RadioButtonPanel.Controls.Add(panel18);
            RadioButtonPanel.Controls.Add(panel19);
            RadioButtonPanel.Controls.Add(panel20);
            RadioButtonPanel.Controls.Add(FeedCreatorProgressBar);
            RadioButtonPanel.ForeColor = Color.WhiteSmoke;
            RadioButtonPanel.Location = new Point(37, 445);
            RadioButtonPanel.Name = "RadioButtonPanel";
            RadioButtonPanel.Size = new Size(733, 218);
            RadioButtonPanel.TabIndex = 24;
            // 
            // ExportButton
            // 
            ExportButton.ForeColor = Color.Black;
            ExportButton.Location = new Point(610, 154);
            ExportButton.Name = "ExportButton";
            ExportButton.Size = new Size(94, 29);
            ExportButton.TabIndex = 20;
            ExportButton.Text = "Экспорт";
            ExportButton.UseVisualStyleBackColor = true;
            ExportButton.Click += ExportButton_Click;
            // 
            // RadioButtonVK
            // 
            RadioButtonVK.AutoSize = true;
            RadioButtonVK.ForeColor = Color.Black;
            RadioButtonVK.Location = new Point(34, 131);
            RadioButtonVK.Name = "RadioButtonVK";
            RadioButtonVK.Size = new Size(109, 27);
            RadioButtonVK.TabIndex = 19;
            RadioButtonVK.TabStop = true;
            RadioButtonVK.Text = "Вконтакте";
            RadioButtonVK.UseVisualStyleBackColor = true;
            RadioButtonVK.CheckedChanged += RadioButton_CheckedChanged;
            // 
            // RadioButton2gis
            // 
            RadioButton2gis.AutoSize = true;
            RadioButton2gis.ForeColor = Color.Black;
            RadioButton2gis.Location = new Point(34, 84);
            RadioButton2gis.Name = "RadioButton2gis";
            RadioButton2gis.Size = new Size(72, 27);
            RadioButton2gis.TabIndex = 18;
            RadioButton2gis.TabStop = true;
            RadioButton2gis.Text = "2ГИС";
            RadioButton2gis.UseVisualStyleBackColor = true;
            RadioButton2gis.CheckedChanged += RadioButton_CheckedChanged;
            // 
            // RadioButtonYandex
            // 
            RadioButtonYandex.AutoSize = true;
            RadioButtonYandex.ForeColor = Color.Black;
            RadioButtonYandex.Location = new Point(34, 37);
            RadioButtonYandex.Name = "RadioButtonYandex";
            RadioButtonYandex.Size = new Size(85, 27);
            RadioButtonYandex.TabIndex = 17;
            RadioButtonYandex.TabStop = true;
            RadioButtonYandex.Text = "Яндекс";
            RadioButtonYandex.UseVisualStyleBackColor = true;
            RadioButtonYandex.CheckedChanged += RadioButton_CheckedChanged;
            // 
            // panel17
            // 
            panel17.BackColor = Color.SteelBlue;
            panel17.Dock = DockStyle.Bottom;
            panel17.Location = new Point(1, 217);
            panel17.Name = "panel17";
            panel17.Size = new Size(731, 1);
            panel17.TabIndex = 1;
            // 
            // panel18
            // 
            panel18.BackColor = Color.SteelBlue;
            panel18.Dock = DockStyle.Right;
            panel18.Location = new Point(732, 1);
            panel18.Name = "panel18";
            panel18.Size = new Size(1, 217);
            panel18.TabIndex = 1;
            // 
            // panel19
            // 
            panel19.BackColor = Color.SteelBlue;
            panel19.Dock = DockStyle.Left;
            panel19.Location = new Point(0, 1);
            panel19.Name = "panel19";
            panel19.Size = new Size(1, 217);
            panel19.TabIndex = 1;
            // 
            // panel20
            // 
            panel20.BackColor = Color.SteelBlue;
            panel20.Dock = DockStyle.Top;
            panel20.Location = new Point(0, 0);
            panel20.Name = "panel20";
            panel20.Size = new Size(733, 1);
            panel20.TabIndex = 0;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(9F, 23F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSize = true;
            BackColor = Color.WhiteSmoke;
            BackgroundImageLayout = ImageLayout.Zoom;
            CausesValidation = false;
            ClientSize = new Size(1478, 682);
            Controls.Add(label3);
            Controls.Add(RadioButtonPanel);
            Controls.Add(label1);
            Controls.Add(panel1);
            Controls.Add(label2);
            Controls.Add(ResetButton);
            Controls.Add(BrowseFileText);
            Controls.Add(panel4);
            Controls.Add(panel9);
            Font = new Font("Segoe UI", 10.2F, FontStyle.Regular, GraphicsUnit.Point, 204);
            ForeColor = Color.Transparent;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "MainForm";
            Text = "Converser";
            WindowState = FormWindowState.Maximized;
            panel4.ResumeLayout(false);
            panel4.PerformLayout();
            panel9.ResumeLayout(false);
            panel9.PerformLayout();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            RadioButtonPanel.ResumeLayout(false);
            RadioButtonPanel.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label DragDropElement;
        private Button DirectoryImportButton;
        private Button DirectoryExportButton;
        private TextBox BrowseDirectoryImportField;
        private Label BrowseFileText;
        private TextBox BrowseDirectoryExportField;
        private Button ResetButton;
        private Button DataLoadButton;
        private CheckBox CheckAll;
        private ProgressBar DataLoadProgressBar;
        private ProgressBar FeedCreatorProgressBar;
        private TreeView CategoryTreePanel;
        private Label FileSizeLabel;
        private Panel panel4;
        private Panel panel5;
        private Panel panel8;
        private Panel panel7;
        private Panel panel6;
        private Panel panel9;
        private Panel panel10;
        private Panel panel11;
        private Panel panel12;
        private Panel panel13;
        private Panel panel1;
        private Panel panel3;
        private Panel panel14;
        private Panel panel15;
        private Panel panel16;
        private Panel RadioButtonPanel;
        private Panel panel17;
        private Panel panel18;
        private Panel panel19;
        private Panel panel20;
        private RadioButton RadioButtonVK;
        private RadioButton RadioButton2gis;
        private RadioButton RadioButtonYandex;
        private Button ExportButton;
    }
}
