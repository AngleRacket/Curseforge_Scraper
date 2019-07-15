using HtmlAgilityPack;

using System;
using System.Drawing;
using System.Windows.Forms;
using System.Net;
using System.IO;
using System.Text.RegularExpressions;

namespace Curseforge_Scraper
{
    public partial class MainForm : Form
    {
        HtmlWeb htmlWeb = new HtmlWeb();

        public MainForm()
        {
            InitializeComponent();
        }

        private async void Button_Search_Click(object sender, EventArgs e)
        {
            string searchQuery = textbox_SearchQuery.Text;

            if (!string.IsNullOrEmpty(searchQuery))
            {
                // unpopulate listview
                imageList_Results.Images.Clear();
                listView_Results.Items.Clear();

                // get data from web
                var page = await htmlWeb.LoadFromWebAsync("https://www.curseforge.com/minecraft/mc-mods/search?search=" + Uri.EscapeDataString(searchQuery));
                var textNodes = page.DocumentNode.SelectNodes("/html/body/div[1]/main/div[1]/div[3]/ul/div/div/div/div[2]/div[1]/a[1]/h3");
                var imageNodes = page.DocumentNode.SelectNodes("/html/body/div[1]/main/div[1]/div[3]/ul/div/div/div/div[1]/div[1]/div/a/img");
                var linkNodes = page.DocumentNode.SelectNodes("/html/body/div[1]/main/div[1]/div[3]/ul/div/div/div/div[2]/div[1]/a[1]");

                // populate listview
                for (int i = 0; i < imageNodes.Count; i++)
                {
                    var imageNode = imageNodes[i];
                    var url = imageNode.Attributes["src"].Value;
                    // download associated image
                    using (var wc = new WebClient())
                    {
                        using (var ms = new MemoryStream(wc.DownloadData(url)))
                        {
                            using (var img = Image.FromStream(ms))
                            {
                                // add items
                                imageList_Results.Images.Add(i.ToString(), img);
                                var textNode = textNodes[i];
                                var listViewItem = new ListViewItem(WebUtility.HtmlDecode(textNode.InnerText), i.ToString());
                                var linkNode = linkNodes[i];
                                listViewItem.ToolTipText = linkNode.Attributes["href"].Value;
                                listView_Results.Items.Add(listViewItem);
                            }
                        }
                    }
                }
            }
        }

        private async void ListView_Results_DoubleClick(object sender, EventArgs e)
        {
            if (listView_Results.SelectedItems.Count == 1)
            {
                var listViewItem = listView_Results.SelectedItems[0];
                var url = listViewItem.ToolTipText;

                // get the download page
                var page = await htmlWeb.LoadFromWebAsync("https://www.curseforge.com" + url + "/download");
                var downloadLink = page.DocumentNode.SelectSingleNode("/html/body/div[1]/main/div[1]/div/div[2]/div/div[1]/p[2]/a");
                var downloadUrl = downloadLink.Attributes["href"].Value;

                // ask for file location
                var sfd = new SaveFileDialog();
                sfd.Filter = "JAR file|*.jar";
                sfd.Title = "Download mod";
                sfd.FileName = new Regex("[^a-zA-Z0-9-]").Replace(listViewItem.Text, "");
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    using (var wc = new WebClient())
                    {
                        wc.DownloadFileAsync(new Uri("https://www.curseforge.com" + downloadUrl), sfd.FileName);
                    }
                }
            }
        }
    }
}
