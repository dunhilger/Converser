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
            saveFileDialog1 = new SaveFileDialog();
            DisplayFileNameElement = new Label();
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
            SuspendLayout();
            // 
            // DisplayFileNameElement
            // 
            DisplayFileNameElement.Font = new Font("Segoe UI", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 204);
            DisplayFileNameElement.Location = new Point(715, 430);
            DisplayFileNameElement.Name = "DisplayFileNameElement";
            DisplayFileNameElement.Size = new Size(311, 149);
            DisplayFileNameElement.TabIndex = 1;
            DisplayFileNameElement.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // YandexFeedButton
            // 
            YandexFeedButton.BackColor = SystemColors.GrayText;
            YandexFeedButton.BackgroundImage = Properties.Resources.original_yry2;
            YandexFeedButton.Location = new Point(65, 216);
            YandexFeedButton.Name = "YandexFeedButton";
            YandexFeedButton.Size = new Size(264, 117);
            YandexFeedButton.TabIndex = 2;
            YandexFeedButton.UseVisualStyleBackColor = false;
            YandexFeedButton.Click += YandexFeedButton_Click;
            // 
            // TwoGisFeedButton
            // 
            TwoGisFeedButton.BackgroundImage = Properties.Resources._2_GIS_Logo_Color_3afd0055671;
            TwoGisFeedButton.Location = new Point(65, 339);
            TwoGisFeedButton.Name = "TwoGisFeedButton";
            TwoGisFeedButton.Size = new Size(264, 117);
            TwoGisFeedButton.TabIndex = 3;
            TwoGisFeedButton.UseVisualStyleBackColor = true;
            TwoGisFeedButton.Click += TwoGisFeedButton_Click;
            // 
            // VKFeedButton
            // 
            VKFeedButton.BackgroundImage = Properties.Resources.vk_2;
            VKFeedButton.Location = new Point(65, 462);
            VKFeedButton.Name = "VKFeedButton";
            VKFeedButton.Size = new Size(264, 117);
            VKFeedButton.TabIndex = 4;
            VKFeedButton.UseVisualStyleBackColor = true;
            VKFeedButton.Click += VKFeedButton_Click;
            // 
            // DirectoryImportButton
            // 
            DirectoryImportButton.Location = new Point(703, 73);
            DirectoryImportButton.Name = "DirectoryImportButton";
            DirectoryImportButton.Size = new Size(36, 29);
            DirectoryImportButton.TabIndex = 6;
            DirectoryImportButton.Text = "...";
            DirectoryImportButton.UseVisualStyleBackColor = true;
            DirectoryImportButton.Click += BrowseFileImportDirectory_Click;
            // 
            // DirectoryExportButton
            // 
            DirectoryExportButton.Location = new Point(703, 158);
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
            BrowseDirectoryImportField.Location = new Point(65, 73);
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
            BrowseFileText.Location = new Point(66, 36);
            BrowseFileText.Name = "BrowseFileText";
            BrowseFileText.Size = new Size(135, 23);
            BrowseFileText.TabIndex = 9;
            BrowseFileText.Text = "Исходный файл";
            // 
            // SaveFileText
            // 
            SaveFileText.AutoSize = true;
            SaveFileText.Location = new Point(64, 119);
            SaveFileText.Name = "SaveFileText";
            SaveFileText.Size = new Size(157, 23);
            SaveFileText.TabIndex = 10;
            SaveFileText.Text = "Папка назначения";
            // 
            // BrowseDirectoryExportField
            // 
            BrowseDirectoryExportField.Location = new Point(67, 158);
            BrowseDirectoryExportField.Name = "BrowseDirectoryExportField";
            BrowseDirectoryExportField.PlaceholderText = "Укажите путь сохранения результата";
            BrowseDirectoryExportField.ReadOnly = true;
            BrowseDirectoryExportField.Size = new Size(630, 30);
            BrowseDirectoryExportField.TabIndex = 11;
            BrowseDirectoryExportField.Click += BrowseExportDirectory_Click;
            // 
            // FieldDataResetButton
            // 
            FieldDataResetButton.Image = Properties.Resources.icons8_кнопка_с_крестиком_48;
            FieldDataResetButton.Location = new Point(703, 113);
            FieldDataResetButton.Name = "FieldDataResetButton";
            FieldDataResetButton.Size = new Size(36, 34);
            FieldDataResetButton.TabIndex = 12;
            FieldDataResetButton.UseVisualStyleBackColor = true;
            FieldDataResetButton.Click += FieldDataResetButton_Click;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(9F, 23F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.GradientInactiveCaption;
            ClientSize = new Size(1071, 615);
            Controls.Add(FieldDataResetButton);
            Controls.Add(BrowseDirectoryExportField);
            Controls.Add(SaveFileText);
            Controls.Add(BrowseFileText);
            Controls.Add(BrowseDirectoryImportField);
            Controls.Add(DirectoryExportButton);
            Controls.Add(DirectoryImportButton);
            Controls.Add(VKFeedButton);
            Controls.Add(TwoGisFeedButton);
            Controls.Add(YandexFeedButton);
            Controls.Add(DisplayFileNameElement);
            Font = new Font("Segoe UI", 10.2F, FontStyle.Regular, GraphicsUnit.Point, 204);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "MainForm";
            Text = "Converser";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label DragDropElement;
        private SaveFileDialog saveFileDialog1;
        private Label DisplayFileNameElement;
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
    }
}
