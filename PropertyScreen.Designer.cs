namespace TeaShoot_3
{
    partial class PropertyScreen
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
            components = new System.ComponentModel.Container();
            propertyGrid1 = new System.Windows.Forms.PropertyGrid();
            listView1 = new System.Windows.Forms.ListView();
            columnHeader1 = new System.Windows.Forms.ColumnHeader();
            columnHeader2 = new System.Windows.Forms.ColumnHeader();
            contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(components);
            追加ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            削除ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            保存ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            再読み込みToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            toolStripMenuItem4 = new System.Windows.Forms.ToolStripSeparator();
            テキストを設定ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            テキストフィットToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            コードを編集ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            codeRemoveを編集ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            codeInitを編集ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
            devFileNameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            menuStrip1 = new System.Windows.Forms.MenuStrip();
            errorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ログを削除ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            contextMenuStrip1.SuspendLayout();
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // propertyGrid1
            // 
            propertyGrid1.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 128);
            propertyGrid1.Location = new System.Drawing.Point(0, 34);
            propertyGrid1.Margin = new System.Windows.Forms.Padding(4);
            propertyGrid1.Name = "propertyGrid1";
            propertyGrid1.Size = new System.Drawing.Size(366, 522);
            propertyGrid1.TabIndex = 0;
            propertyGrid1.Resize += propertyGrid1_Resize;
            // 
            // listView1
            // 
            listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] { columnHeader1, columnHeader2 });
            listView1.ContextMenuStrip = contextMenuStrip1;
            listView1.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 128);
            listView1.FullRowSelect = true;
            listView1.Location = new System.Drawing.Point(374, 27);
            listView1.Margin = new System.Windows.Forms.Padding(4);
            listView1.Name = "listView1";
            listView1.Size = new System.Drawing.Size(366, 522);
            listView1.TabIndex = 2;
            listView1.UseCompatibleStateImageBehavior = false;
            listView1.View = System.Windows.Forms.View.Details;
            listView1.DoubleClick += listView1_DoubleClick;
            // 
            // columnHeader1
            // 
            columnHeader1.Text = "Number";
            // 
            // columnHeader2
            // 
            columnHeader2.Text = "Text";
            // 
            // contextMenuStrip1
            // 
            contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { 追加ToolStripMenuItem, 削除ToolStripMenuItem, toolStripMenuItem2, 保存ToolStripMenuItem, 再読み込みToolStripMenuItem, toolStripMenuItem4, テキストを設定ToolStripMenuItem, テキストフィットToolStripMenuItem, toolStripSeparator1, コードを編集ToolStripMenuItem, codeRemoveを編集ToolStripMenuItem, codeInitを編集ToolStripMenuItem });
            contextMenuStrip1.Name = "contextMenuStrip1";
            contextMenuStrip1.Size = new System.Drawing.Size(205, 220);
            // 
            // 追加ToolStripMenuItem
            // 
            追加ToolStripMenuItem.Name = "追加ToolStripMenuItem";
            追加ToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.A;
            追加ToolStripMenuItem.Size = new System.Drawing.Size(204, 22);
            追加ToolStripMenuItem.Text = "追加";
            追加ToolStripMenuItem.Click += 追加ToolStripMenuItem_Click;
            // 
            // 削除ToolStripMenuItem
            // 
            削除ToolStripMenuItem.Name = "削除ToolStripMenuItem";
            削除ToolStripMenuItem.Size = new System.Drawing.Size(204, 22);
            削除ToolStripMenuItem.Text = "削除";
            削除ToolStripMenuItem.Click += 削除ToolStripMenuItem_Click_1;
            // 
            // toolStripMenuItem2
            // 
            toolStripMenuItem2.Name = "toolStripMenuItem2";
            toolStripMenuItem2.Size = new System.Drawing.Size(201, 6);
            // 
            // 保存ToolStripMenuItem
            // 
            保存ToolStripMenuItem.Name = "保存ToolStripMenuItem";
            保存ToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.S;
            保存ToolStripMenuItem.Size = new System.Drawing.Size(204, 22);
            保存ToolStripMenuItem.Text = "保存";
            保存ToolStripMenuItem.Click += 保存ToolStripMenuItem_Click;
            // 
            // 再読み込みToolStripMenuItem
            // 
            再読み込みToolStripMenuItem.Name = "再読み込みToolStripMenuItem";
            再読み込みToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.R;
            再読み込みToolStripMenuItem.Size = new System.Drawing.Size(204, 22);
            再読み込みToolStripMenuItem.Text = "再読み込み";
            再読み込みToolStripMenuItem.Click += 再読み込みToolStripMenuItem_Click;
            // 
            // toolStripMenuItem4
            // 
            toolStripMenuItem4.Name = "toolStripMenuItem4";
            toolStripMenuItem4.Size = new System.Drawing.Size(201, 6);
            // 
            // テキストを設定ToolStripMenuItem
            // 
            テキストを設定ToolStripMenuItem.Name = "テキストを設定ToolStripMenuItem";
            テキストを設定ToolStripMenuItem.Size = new System.Drawing.Size(204, 22);
            テキストを設定ToolStripMenuItem.Text = "テキストを設定";
            テキストを設定ToolStripMenuItem.Click += テキストを設定ToolStripMenuItem_Click;
            // 
            // テキストフィットToolStripMenuItem
            // 
            テキストフィットToolStripMenuItem.Name = "テキストフィットToolStripMenuItem";
            テキストフィットToolStripMenuItem.Size = new System.Drawing.Size(204, 22);
            テキストフィットToolStripMenuItem.Text = "テキストフィット";
            テキストフィットToolStripMenuItem.Click += テキストフィットToolStripMenuItem_Click;
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new System.Drawing.Size(201, 6);
            // 
            // コードを編集ToolStripMenuItem
            // 
            コードを編集ToolStripMenuItem.Name = "コードを編集ToolStripMenuItem";
            コードを編集ToolStripMenuItem.Size = new System.Drawing.Size(204, 22);
            コードを編集ToolStripMenuItem.Text = "コードを編集";
            コードを編集ToolStripMenuItem.Click += コードを編集ToolStripMenuItem_Click;
            // 
            // codeRemoveを編集ToolStripMenuItem
            // 
            codeRemoveを編集ToolStripMenuItem.Name = "codeRemoveを編集ToolStripMenuItem";
            codeRemoveを編集ToolStripMenuItem.Size = new System.Drawing.Size(204, 22);
            codeRemoveを編集ToolStripMenuItem.Text = "CodeRemoveを編集";
            codeRemoveを編集ToolStripMenuItem.Click += codeRemoveを編集ToolStripMenuItem_Click;
            // 
            // codeInitを編集ToolStripMenuItem
            // 
            codeInitを編集ToolStripMenuItem.Name = "codeInitを編集ToolStripMenuItem";
            codeInitを編集ToolStripMenuItem.Size = new System.Drawing.Size(204, 22);
            codeInitを編集ToolStripMenuItem.Text = "CodeInitを編集";
            codeInitを編集ToolStripMenuItem.Click += codeInitを編集ToolStripMenuItem_Click;
            // 
            // openFileDialog1
            // 
            openFileDialog1.FileName = "openFileDialog1";
            // 
            // fileToolStripMenuItem
            // 
            fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { saveToolStripMenuItem, openToolStripMenuItem, toolStripMenuItem3, devFileNameToolStripMenuItem, toolStripMenuItem1, exitToolStripMenuItem });
            fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            fileToolStripMenuItem.Size = new System.Drawing.Size(51, 20);
            fileToolStripMenuItem.Text = "File(&F)";
            // 
            // saveToolStripMenuItem
            // 
            saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            saveToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S;
            saveToolStripMenuItem.Size = new System.Drawing.Size(145, 22);
            saveToolStripMenuItem.Text = "Save";
            saveToolStripMenuItem.Click += saveToolStripMenuItem_Click;
            // 
            // openToolStripMenuItem
            // 
            openToolStripMenuItem.Name = "openToolStripMenuItem";
            openToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O;
            openToolStripMenuItem.Size = new System.Drawing.Size(145, 22);
            openToolStripMenuItem.Text = "Open";
            openToolStripMenuItem.Click += openToolStripMenuItem_Click;
            // 
            // toolStripMenuItem3
            // 
            toolStripMenuItem3.Name = "toolStripMenuItem3";
            toolStripMenuItem3.Size = new System.Drawing.Size(142, 6);
            // 
            // devFileNameToolStripMenuItem
            // 
            devFileNameToolStripMenuItem.Name = "devFileNameToolStripMenuItem";
            devFileNameToolStripMenuItem.Size = new System.Drawing.Size(145, 22);
            devFileNameToolStripMenuItem.Text = "DevFileName";
            devFileNameToolStripMenuItem.Click += devFileNameToolStripMenuItem_Click;
            // 
            // toolStripMenuItem1
            // 
            toolStripMenuItem1.Name = "toolStripMenuItem1";
            toolStripMenuItem1.Size = new System.Drawing.Size(142, 6);
            // 
            // exitToolStripMenuItem
            // 
            exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            exitToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F6;
            exitToolStripMenuItem.Size = new System.Drawing.Size(145, 22);
            exitToolStripMenuItem.Text = "Exit";
            exitToolStripMenuItem.Click += exitToolStripMenuItem_Click;
            // 
            // menuStrip1
            // 
            menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { fileToolStripMenuItem, errorToolStripMenuItem });
            menuStrip1.Location = new System.Drawing.Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Padding = new System.Windows.Forms.Padding(7, 2, 0, 2);
            menuStrip1.Size = new System.Drawing.Size(752, 24);
            menuStrip1.TabIndex = 1;
            menuStrip1.Text = "menuStrip1";
            // 
            // errorToolStripMenuItem
            // 
            errorToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { ログを削除ToolStripMenuItem });
            errorToolStripMenuItem.Name = "errorToolStripMenuItem";
            errorToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            errorToolStripMenuItem.Text = "Error";
            errorToolStripMenuItem.Click += errorToolStripMenuItem_Click;
            // 
            // ログを削除ToolStripMenuItem
            // 
            ログを削除ToolStripMenuItem.Name = "ログを削除ToolStripMenuItem";
            ログを削除ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            ログを削除ToolStripMenuItem.Text = "ログを削除";
            ログを削除ToolStripMenuItem.Click += ログを削除ToolStripMenuItem_Click;
            // 
            // PropertyScreen
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(752, 562);
            Controls.Add(menuStrip1);
            Controls.Add(listView1);
            Controls.Add(propertyGrid1);
            ImeMode = System.Windows.Forms.ImeMode.On;
            MainMenuStrip = menuStrip1;
            Margin = new System.Windows.Forms.Padding(4);
            Name = "PropertyScreen";
            Text = "PropertyScreen";
            Load += PropertyScreen_Load;
            Resize += PropertyScreen_Resize;
            contextMenuStrip1.ResumeLayout(false);
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.PropertyGrid propertyGrid1;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 追加ToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem 保存ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 再読み込みToolStripMenuItem;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem4;
        private System.Windows.Forms.ToolStripMenuItem テキストを設定ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem テキストフィットToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem devFileNameToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem コードを編集ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem codeRemoveを編集ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem codeInitを編集ToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem 削除ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem errorToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ログを削除ToolStripMenuItem;
    }
}