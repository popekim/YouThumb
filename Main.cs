//----------------------------------------------------------------------------
// Copyright (c) 2013 Pope Kim (www.popekim.com)
//
// See the file LICENSE for copying permission.
//-----------------------------------------------------------------------------


using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Xml;

namespace YouThumb
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        // from: http://stackoverflow.com/questions/3652046/c-sharp-regex-to-get-video-id-from-youtube-and-vimeo-by-url
        static private readonly Regex YoutubeVideoRegex = new Regex(@"youtu(?:\.be|be\.com)/(?:.*v(?:/|=)|(?:.*/)?)([a-zA-Z0-9-_]+)", RegexOptions.IgnoreCase);
        private string currentVideoID = "";
        private Image cachedImage;
        private string cachedTitle;

        private string getVideoID(string url)
        {
            string id = string.Empty;

            Match youtubeMatch = YoutubeVideoRegex.Match(url);
            if (youtubeMatch.Success)
            {
                id = youtubeMatch.Groups[1].Value;
            }

            return id;
        }

        private void tryNewRender()
        {
            var videoID = getVideoID(tbURL.Text);
            if (videoID == string.Empty)
            {
                return;
            }

            // got valid youtube id - so let's load youtube image
            var shouldGenerateNewThumb = retrieveYoutubeVideoData(videoID);

            if (shouldGenerateNewThumb)
            {
                GenerateThumb();
            }
        }

        private void GenerateThumb()
        {
            if (cachedImage == null)
            {
                return;
            }

            //Bitmap myBitmap = new Bitmap(@"C:\Users\Scott\desktop\blank.bmp");
            var tmpImage = (Image)cachedImage.Clone();
            Graphics g = Graphics.FromImage(tmpImage);

            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;

            StringFormat stringFormat = new StringFormat();
            stringFormat.Alignment = StringAlignment.Center;
            stringFormat.LineAlignment = StringAlignment.Center;


            // TODO: configuable
            const float safeArea = 0.05f;
            float safeW = tmpImage.Width * safeArea;
            float safeH = tmpImage.Height * safeArea;

            var rect = new RectangleF(safeW, safeH, tmpImage.Width - safeW * 2, tmpImage.Height - safeH * 2);

            // TODO: configuable for 10
            var fontSize = Math.Min(tmpImage.Width, tmpImage.Height) / 10;

            var font = new Font(cbFonts.SelectedItem as string, fontSize, FontStyle.Bold);

            // draw drop shadow
            // TODO: configuable
            const int dropShadowWidth = 5;

            for (int y = -dropShadowWidth; y <= dropShadowWidth; ++y)
            {
                for (int x = -dropShadowWidth; x <= dropShadowWidth; ++x)
                {
                    var shadowRect = rect;
                    shadowRect.X = shadowRect.X - x;
                    shadowRect.Y = shadowRect.Y - y;

                    g.DrawString(cachedTitle, font, Brushes.Black, shadowRect, stringFormat);
                }
            }

            g.DrawString(cachedTitle, font, Brushes.White, rect, stringFormat);

            pbThumb.Image = tmpImage;
        }

        // always retrieves best definition image
        private bool retrieveYoutubeVideoData(string videoID)
        {
            if (videoID == currentVideoID)
            {
                return false;
            }

            var thumbURL = String.Format(@"https://img.youtube.com/vi/{0}/maxresdefault.jpg", videoID);

            Image image = null;
            try
            {
                image = Image.FromStream(new MemoryStream(new WebClient().DownloadData(thumbURL)));
            }
            catch (Exception)
            {
                // TODO: show error
            }

            if (image == null)
            {
                return false;
            }


            string title = null;
            try
            {
                var videoEntryUrl = new Uri(String.Format(@"http://gdata.youtube.com/feeds/api/videos/{0}", videoID));
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(new MemoryStream(new WebClient().DownloadData(videoEntryUrl)));

                title = xmlDoc.GetElementsByTagName("title")[0].InnerText;
            }
            catch (Exception)
            {
                // TODO: show error
            }

            currentVideoID = videoID;
            cachedImage = image;
            cachedTitle = title;

            return true;
        }

        private void tbURL_TextChanged(object sender, EventArgs e)
        {
            tryNewRender();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            var fontNameList = new List<string>(System.Drawing.FontFamily.Families.Length);
            foreach (var f in System.Drawing.FontFamily.Families)
            {
                fontNameList.Add(f.Name);
            }
            cbFonts.Items.AddRange(fontNameList.ToArray());

            // TODO: font name caching from last instance
            var index = fontNameList.FindIndex(f => f == "Verdana");
            cbFonts.SelectedIndex = (index >= 0) ? index : 0;
        }

        private void cbFonts_SelectedIndexChanged(object sender, EventArgs e)
        {
            GenerateThumb();
        }

        private void pbThumb_Click(object sender, EventArgs e)
        {
            if (pbThumb.Image == null)
            {
                return;
            }
            if (dlgSave.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    pbThumb.Image.Save(dlgSave.FileName);
                }
                catch (Exception)
                {
                    MessageBox.Show("failed to save " + dlgSave.FileName, "FAIL", MessageBoxButtons.OK);
                }
            }
        }
    }
}
