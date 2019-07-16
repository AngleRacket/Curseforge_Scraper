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
                var imageNodes = page.DocumentNode.SelectNodes("/html/body/div[1]/main/div[1]/div[3]/ul/div/div/div/div[1]/div[1]/div/a/img");
                var linkNodes = page.DocumentNode.SelectNodes("/html/body/div[1]/main/div[1]/div[3]/ul/div/div/div/div[2]/div[1]/a[1]");

                // check if there are any search results
                if (imageNodes != null)
                {

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
                                    var linkNode = linkNodes[i];
                                    var listViewItem = new ListViewItem(WebUtility.HtmlDecode(linkNode.InnerText), i.ToString());
                                    listViewItem.ToolTipText = linkNode.Attributes["href"].Value;
                                    listView_Results.Items.Add(listViewItem);
                                }
                            }
                        }
                    }

                } else
                {
                    MessageBox.Show("No search results for query: \"" + searchQuery + "\"", "Curseforge Scraper", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void ListView_Results_DoubleClick(object sender, EventArgs e)
        {
            if (listView_Results.SelectedItems.Count == 1)
            {
                var listViewItem = listView_Results.SelectedItems[0];
                var url = listViewItem.ToolTipText;

                DownloadMod(url, listViewItem.Text);
            }
        }

        private async void DownloadMod(string url, string modName)
        {
            // get minecraft version
            string humanReadableMcVersion =
                radioButton_McVersion_1_12.Checked ? "1.12" :
                radioButton_McVersion_1_11.Checked ? "1.11" :
                radioButton_McVersion_1_10.Checked ? "1.10" :
                radioButton_McVersion_1_9.Checked ? "1.9" :
                radioButton_McVersion_1_8.Checked ? "1.8" :
                radioButton_McVersion_1_7.Checked ? "1.7" : null; // will never be null
            string mcVersionString =
                radioButton_McVersion_1_12.Checked ? "1738749986%3a628" :
                radioButton_McVersion_1_11.Checked ? "1738749986%3a599" :
                radioButton_McVersion_1_10.Checked ? "1738749986%3a572" :
                radioButton_McVersion_1_9.Checked ? "1738749986%3a552" :
                radioButton_McVersion_1_8.Checked ? "1738749986%3a4" :
                radioButton_McVersion_1_7.Checked ? "1738749986%3a5" : null; // will never be null

            // get the download page
            var page = await htmlWeb.LoadFromWebAsync("https://www.curseforge.com" + url + "/files/all?filter-game-version=" + mcVersionString);
            var downloadLink = page.DocumentNode.SelectSingleNode("/html/body/div[1]/main/div[1]/div[2]/section/div/div/div/section/div[2]/div/table/tbody/tr[1]/td[2]/a");
            if (downloadLink != null)
            {
                var downloadUrl = downloadLink.Attributes["href"].Value;

                // ask for file location
                var sfd = new SaveFileDialog();
                sfd.Filter = "JAR file|*.jar";
                sfd.Title = "Download mod";
                sfd.FileName = new Regex("[^a-zA-Z0-9-]").Replace(modName, "") + "_" + humanReadableMcVersion;
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    using (var wc = new WebClient())
                    {
                        wc.DownloadFileAsync(new Uri("https://www.curseforge.com" + downloadUrl), sfd.FileName);
                    }

                    if (checkBox_DownloadDependencies.Checked)
                    {
                        // download additional dependencies
                        var dependencyPage = await htmlWeb.LoadFromWebAsync("https://www.curseforge.com" + url + "/relations/dependencies");
                        var dependencies = dependencyPage.DocumentNode.SelectNodes("/html/body/div[1]/main/div[1]/div[2]/div/div/div[3]/ul/li/div[2]/div[1]/a[1]");

                        if (dependencies != null)
                        {
                            foreach (var dependencyLink in dependencies)
                            {
                                DownloadMod(dependencyLink.Attributes["href"].Value, dependencyLink.InnerText);
                            }
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("The specified Minecraft version is not available for this mod.", "Curseforge Scraper", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
