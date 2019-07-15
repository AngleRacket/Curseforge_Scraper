namespace Curseforge_Scraper
{
    partial class MainForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.textbox_SearchQuery = new System.Windows.Forms.TextBox();
            this.labelInfo_Search = new System.Windows.Forms.Label();
            this.button_Search = new System.Windows.Forms.Button();
            this.listView_Results = new System.Windows.Forms.ListView();
            this.imageList_Results = new System.Windows.Forms.ImageList(this.components);
            this.SuspendLayout();
            // 
            // textbox_SearchQuery
            // 
            this.textbox_SearchQuery.Location = new System.Drawing.Point(15, 25);
            this.textbox_SearchQuery.Name = "textbox_SearchQuery";
            this.textbox_SearchQuery.Size = new System.Drawing.Size(317, 20);
            this.textbox_SearchQuery.TabIndex = 0;
            // 
            // labelInfo_Search
            // 
            this.labelInfo_Search.AutoSize = true;
            this.labelInfo_Search.Location = new System.Drawing.Point(12, 9);
            this.labelInfo_Search.Name = "labelInfo_Search";
            this.labelInfo_Search.Size = new System.Drawing.Size(41, 13);
            this.labelInfo_Search.TabIndex = 1;
            this.labelInfo_Search.Text = "Search";
            // 
            // button_Search
            // 
            this.button_Search.Location = new System.Drawing.Point(338, 23);
            this.button_Search.Name = "button_Search";
            this.button_Search.Size = new System.Drawing.Size(89, 23);
            this.button_Search.TabIndex = 2;
            this.button_Search.Text = "Search";
            this.button_Search.UseVisualStyleBackColor = true;
            this.button_Search.Click += new System.EventHandler(this.Button_Search_Click);
            // 
            // listView_Results
            // 
            this.listView_Results.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listView_Results.LargeImageList = this.imageList_Results;
            this.listView_Results.Location = new System.Drawing.Point(12, 51);
            this.listView_Results.MultiSelect = false;
            this.listView_Results.Name = "listView_Results";
            this.listView_Results.Size = new System.Drawing.Size(415, 425);
            this.listView_Results.TabIndex = 3;
            this.listView_Results.UseCompatibleStateImageBehavior = false;
            this.listView_Results.View = System.Windows.Forms.View.Tile;
            this.listView_Results.DoubleClick += new System.EventHandler(this.ListView_Results_DoubleClick);
            // 
            // imageList_Results
            // 
            this.imageList_Results.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imageList_Results.ImageSize = new System.Drawing.Size(64, 64);
            this.imageList_Results.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // MainForm
            // 
            this.AcceptButton = this.button_Search;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(444, 488);
            this.Controls.Add(this.listView_Results);
            this.Controls.Add(this.button_Search);
            this.Controls.Add(this.labelInfo_Search);
            this.Controls.Add(this.textbox_SearchQuery);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.Text = "Cureforge Scraper";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textbox_SearchQuery;
        private System.Windows.Forms.Label labelInfo_Search;
        private System.Windows.Forms.Button button_Search;
        private System.Windows.Forms.ListView listView_Results;
        private System.Windows.Forms.ImageList imageList_Results;
    }
}

