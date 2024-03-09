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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            YandexFeedButton = new Button();
            TwoGisFeedButton = new Button();
            VKFeedButton = new Button();
            DirectoryImportButton = new Button();
            DirectoryExportButton = new Button();
            BrowseDirectoryImportField = new TextBox();
            BrowseFileText = new Label();
            SaveFileText = new Label();
            BrowseDirectoryExportField = new TextBox();
            FieldDataResetButton = new Button();
            DataLoadButton = new Button();
            panel1 = new Panel();
            FileSizeLabel = new Label();
            DataLoadProgressBar = new ProgressBar();
            panel2 = new Panel();
            panel3 = new Panel();
            CategoryTree = new TreeView();
            CheckAll = new CheckBox();
            CategoryListText = new Label();
            label1 = new Label();
            FeedCreatorProgressBar = new ProgressBar();
            panel1.SuspendLayout();
            panel2.SuspendLayout();
            panel3.SuspendLayout();
            SuspendLayout();
            // 
            // YandexFeedButton
            // 
            YandexFeedButton.BackColor = SystemColors.GrayText;
            YandexFeedButton.BackgroundImage = Properties.Resources.original_yry2;
            YandexFeedButton.Location = new Point(1004, 224);
            YandexFeedButton.Name = "YandexFeedButton";
            YandexFeedButton.Size = new Size(238, 117);
            YandexFeedButton.TabIndex = 2;
            YandexFeedButton.UseVisualStyleBackColor = false;
            YandexFeedButton.Click += YandexFeedButton_Click;
            // 
            // TwoGisFeedButton
            // 
            TwoGisFeedButton.BackgroundImage = Properties.Resources._2_GIS_Logo_Color_3afd0055671;
            TwoGisFeedButton.Location = new Point(1273, 224);
            TwoGisFeedButton.Name = "TwoGisFeedButton";
            TwoGisFeedButton.Size = new Size(246, 117);
            TwoGisFeedButton.TabIndex = 3;
            TwoGisFeedButton.UseVisualStyleBackColor = true;
            TwoGisFeedButton.Click += TwoGisFeedButton_Click;
            // 
            // VKFeedButton
            // 
            VKFeedButton.BackgroundImage = Properties.Resources.vk_2;
            VKFeedButton.Location = new Point(1554, 222);
            VKFeedButton.Name = "VKFeedButton";
            VKFeedButton.Size = new Size(257, 117);
            VKFeedButton.TabIndex = 4;
            VKFeedButton.UseVisualStyleBackColor = true;
            VKFeedButton.Click += VKFeedButton_Click;
            // 
            // DirectoryImportButton
            // 
            DirectoryImportButton.Location = new Point(678, 49);
            DirectoryImportButton.Name = "DirectoryImportButton";
            DirectoryImportButton.Size = new Size(36, 29);
            DirectoryImportButton.TabIndex = 6;
            DirectoryImportButton.Text = "...";
            DirectoryImportButton.UseVisualStyleBackColor = true;
            DirectoryImportButton.Click += BrowseFileImportDirectory_Click;
            // 
            // DirectoryExportButton
            // 
            DirectoryExportButton.Location = new Point(678, 48);
            DirectoryExportButton.Name = "DirectoryExportButton";
            DirectoryExportButton.Size = new Size(36, 29);
            DirectoryExportButton.TabIndex = 7;
            DirectoryExportButton.Text = "...";
            DirectoryExportButton.UseVisualStyleBackColor = true;
            DirectoryExportButton.Click += BrowseExportDirectory_Click;
            // 
            // BrowseDirectoryImportField
            // 
            BrowseDirectoryImportField.AllowDrop = true;
            BrowseDirectoryImportField.Location = new Point(33, 49);
            BrowseDirectoryImportField.Name = "BrowseDirectoryImportField";
            BrowseDirectoryImportField.PlaceholderText = "Перетащите сюда файл Excel или кликните для выбора файла на компьютере";
            BrowseDirectoryImportField.ReadOnly = true;
            BrowseDirectoryImportField.Size = new Size(632, 30);
            BrowseDirectoryImportField.TabIndex = 8;
            BrowseDirectoryImportField.Click += BrowseFileImportDirectory_Click;
            BrowseDirectoryImportField.DragDrop += OnDragDrop;
            BrowseDirectoryImportField.DragEnter += OnDragEnter;
            // 
            // BrowseFileText
            // 
            BrowseFileText.AutoSize = true;
            BrowseFileText.Location = new Point(33, 9);
            BrowseFileText.Name = "BrowseFileText";
            BrowseFileText.Size = new Size(135, 23);
            BrowseFileText.TabIndex = 9;
            BrowseFileText.Text = "Исходный файл";
            // 
            // SaveFileText
            // 
            SaveFileText.AutoSize = true;
            SaveFileText.Location = new Point(33, 12);
            SaveFileText.Name = "SaveFileText";
            SaveFileText.Size = new Size(157, 23);
            SaveFileText.TabIndex = 10;
            SaveFileText.Text = "Папка назначения";
            // 
            // BrowseDirectoryExportField
            // 
            BrowseDirectoryExportField.Location = new Point(33, 47);
            BrowseDirectoryExportField.Name = "BrowseDirectoryExportField";
            BrowseDirectoryExportField.PlaceholderText = "Укажите путь сохранения результата";
            BrowseDirectoryExportField.ReadOnly = true;
            BrowseDirectoryExportField.Size = new Size(630, 30);
            BrowseDirectoryExportField.TabIndex = 11;
            BrowseDirectoryExportField.Click += BrowseExportDirectory_Click;
            // 
            // FieldDataResetButton
            // 
            FieldDataResetButton.BackColor = Color.LightGray;
            FieldDataResetButton.ForeColor = SystemColors.ButtonFace;
            FieldDataResetButton.Image = (Image)resources.GetObject("FieldDataResetButton.Image");
            FieldDataResetButton.Location = new Point(887, 9);
            FieldDataResetButton.Name = "FieldDataResetButton";
            FieldDataResetButton.Size = new Size(36, 34);
            FieldDataResetButton.TabIndex = 12;
            FieldDataResetButton.UseVisualStyleBackColor = false;
            FieldDataResetButton.Click += ResetFormFieldsButton_Click;
            // 
            // DataLoadButton
            // 
            DataLoadButton.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            DataLoadButton.AutoSize = true;
            DataLoadButton.Location = new Point(33, 92);
            DataLoadButton.Name = "DataLoadButton";
            DataLoadButton.Size = new Size(164, 56);
            DataLoadButton.TabIndex = 13;
            DataLoadButton.Text = "Загрузить данные";
            DataLoadButton.UseVisualStyleBackColor = true;
            DataLoadButton.Click += DataLoadButton_Click;
            // 
            // panel1
            // 
            panel1.BackColor = Color.Transparent;
            panel1.BorderStyle = BorderStyle.Fixed3D;
            panel1.Controls.Add(FileSizeLabel);
            panel1.Controls.Add(DataLoadProgressBar);
            panel1.Controls.Add(FieldDataResetButton);
            panel1.Controls.Add(DataLoadButton);
            panel1.Controls.Add(BrowseDirectoryImportField);
            panel1.Controls.Add(BrowseFileText);
            panel1.Controls.Add(DirectoryImportButton);
            panel1.Location = new Point(23, 29);
            panel1.Name = "panel1";
            panel1.Size = new Size(940, 169);
            panel1.TabIndex = 14;
            // 
            // FileSizeLabel
            // 
            FileSizeLabel.AutoSize = true;
            FileSizeLabel.Location = new Point(224, 106);
            FileSizeLabel.Name = "FileSizeLabel";
            FileSizeLabel.Size = new Size(0, 23);
            FileSizeLabel.TabIndex = 15;
            // 
            // DataLoadProgressBar
            // 
            DataLoadProgressBar.Location = new Point(33, 148);
            DataLoadProgressBar.Name = "DataLoadProgressBar";
            DataLoadProgressBar.RightToLeft = RightToLeft.No;
            DataLoadProgressBar.Size = new Size(164, 5);
            DataLoadProgressBar.Style = ProgressBarStyle.Continuous;
            DataLoadProgressBar.TabIndex = 14;
            DataLoadProgressBar.Visible = false;
            // 
            // panel2
            // 
            panel2.BackColor = Color.Transparent;
            panel2.BorderStyle = BorderStyle.Fixed3D;
            panel2.Controls.Add(DirectoryExportButton);
            panel2.Controls.Add(BrowseDirectoryExportField);
            panel2.Controls.Add(SaveFileText);
            panel2.Location = new Point(969, 29);
            panel2.Name = "panel2";
            panel2.Size = new Size(926, 117);
            panel2.TabIndex = 14;
            // 
            // panel3
            // 
            panel3.BackColor = Color.Transparent;
            panel3.BorderStyle = BorderStyle.Fixed3D;
            panel3.Controls.Add(CategoryTree);
            panel3.Controls.Add(CheckAll);
            panel3.Controls.Add(CategoryListText);
            panel3.Controls.Add(label1);
            panel3.Location = new Point(23, 222);
            panel3.Name = "panel3";
            panel3.Size = new Size(940, 676);
            panel3.TabIndex = 15;
            // 
            // CategoryTree
            // 
            CategoryTree.BackColor = SystemColors.MenuBar;
            CategoryTree.FullRowSelect = true;
            CategoryTree.Location = new Point(33, 41);
            CategoryTree.Name = "CategoryTree";
            CategoryTree.Size = new Size(869, 600);
            CategoryTree.TabIndex = 17;
            CategoryTree.AfterCheck += CategoryTree_AfterCheck;
            // 
            // CheckAll
            // 
            CheckAll.AutoSize = true;
            CheckAll.Location = new Point(193, 8);
            CheckAll.Name = "CheckAll";
            CheckAll.Size = new Size(130, 27);
            CheckAll.TabIndex = 3;
            CheckAll.Text = "Выбрать все";
            CheckAll.UseVisualStyleBackColor = true;
            CheckAll.Click += CheckAll_Click;
            // 
            // CategoryListText
            // 
            CategoryListText.AutoSize = true;
            CategoryListText.Location = new Point(37, 9);
            CategoryListText.Name = "CategoryListText";
            CategoryListText.Size = new Size(129, 23);
            CategoryListText.TabIndex = 1;
            CategoryListText.Text = "Категории и ID";
            // 
            // label1
            // 
            label1.Location = new Point(0, 0);
            label1.Name = "label1";
            label1.Size = new Size(100, 23);
            label1.TabIndex = 0;
            // 
            // FeedCreatorProgressBar
            // 
            FeedCreatorProgressBar.Location = new Point(1007, 359);
            FeedCreatorProgressBar.Name = "FeedCreatorProgressBar";
            FeedCreatorProgressBar.Size = new Size(800, 17);
            FeedCreatorProgressBar.TabIndex = 16;
            FeedCreatorProgressBar.Visible = false;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(9F, 23F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.GradientInactiveCaption;
            ClientSize = new Size(1924, 923);
            Controls.Add(FeedCreatorProgressBar);
            Controls.Add(panel3);
            Controls.Add(VKFeedButton);
            Controls.Add(TwoGisFeedButton);
            Controls.Add(YandexFeedButton);
            Controls.Add(panel1);
            Controls.Add(panel2);
            Font = new Font("Segoe UI", 10.2F, FontStyle.Regular, GraphicsUnit.Point, 204);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "MainForm";
            Text = "Converser";
            WindowState = FormWindowState.Maximized;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            panel3.ResumeLayout(false);
            panel3.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Label DragDropElement;
        private Button YandexFeedButton;
        private Button TwoGisFeedButton;
        private Button VKFeedButton;
        private Label label1;
        private Button DirectoryImportButton;
        private Button DirectoryExportButton;
        private TextBox BrowseDirectoryImportField;
        private Label BrowseFileText;
        private Label SaveFileText;
        private TextBox BrowseDirectoryExportField;
        private Button FieldDataResetButton;
        private Button DataLoadButton;
        private Panel panel1;
        private Panel panel2;
        private Panel panel3;
        private Label CategoryListText;
        private CheckBox CheckAll;
        private ProgressBar DataLoadProgressBar;
        private ProgressBar FeedCreatorProgressBar;
        private TreeView CategoryTree;
        private Label FileSizeLabel;
    }
}
