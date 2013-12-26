//----------------------------------------------------------------------------
// Copyright (c) 2013 Pope Kim (www.popekim.com)
//
// See the file LICENSE for copying permission.
//-----------------------------------------------------------------------------

//#define DO_PROFILE

using System;
using System.Collections.Generic;
using System.Diagnostics;
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

        private static readonly int MaxWordWrapLines = 3;
        private List<string> GetWordWrappedText(ref int fontSize, StringFormat stringFormat, RectangleF rect, Graphics graphics)
        {
            const int MinFontSize = 10;

            var lines = new List<string>(MaxWordWrapLines);

            string[] tokens = cachedTitle.Split(' ');

            var origin = new PointF(0, 0);
            bool keepTrying = false;
            var fontName = cbFonts.SelectedItem as string;
            do
            {
                keepTrying = false;
                --fontSize;

                // reytry with a smaller font size
                lines.Clear();
                lines.Add("");

                var tmpFont = new Font(fontName, fontSize, FontStyle.Bold);
                foreach (var w in tokens)
                {
                    var lineToTest = new string(lines[lines.Count - 1].ToCharArray());

                    // if it's not first word pad a space
                    if (lineToTest.Length != 0)
                    {
                        lineToTest += " ";
                    }
                    lineToTest += w;

                    var renderedSize = graphics.MeasureString(lineToTest, tmpFont, origin, stringFormat);
                    if (renderedSize.Width >= rect.Width)
                    {
                        if (lines.Count == MaxWordWrapLines ||                      // last line
                            rect.Height / (lines.Count + 1) <= renderedSize.Height) // make sure it fits into each line's height
                        {
                            keepTrying = true;
                            break;
                        }

                        lines.Add("");
                    }

                    if (lines[lines.Count - 1].Length != 0)
                    {
                        lines[lines.Count - 1] += " ";
                    }
                    lines[lines.Count - 1] += w;
                }
            }
            while (keepTrying && fontSize > MinFontSize);

            return lines;
        }

        private void DrawText(Graphics g, string text, Font font, int dropShadowWidth, RectangleF rect, StringFormat stringFormat)
        {
            // 1) draw drop shadow
            for (int y = -dropShadowWidth; y <= dropShadowWidth; ++y)
            {
                for (int x = -dropShadowWidth; x <= dropShadowWidth; ++x)
                {
                    var shadowRect = rect;
                    shadowRect.X = shadowRect.X - x;
                    shadowRect.Y = shadowRect.Y - y;

                    g.DrawString(text, font, Brushes.Black, shadowRect, stringFormat);
                }
            }

            // 2) draw text
            g.DrawString(text, font, Brushes.White, rect, stringFormat);
        }

        private void GenerateThumb()
        {
            if (cachedImage == null)
            {
                return;
            }

            // 1) some setup
            var tmpImage = (Image)cachedImage.Clone();

            Graphics graphics = Graphics.FromImage(tmpImage);
            graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;

            StringFormat stringFormat = new StringFormat();
            stringFormat.Alignment = StringAlignment.Center;
            stringFormat.LineAlignment = StringAlignment.Center;

            // 2) calculate proper draw region based on ratio
            // some youtube thumbs are shown in 4:3 ratio. so let's make it 4:3 ratio
            float marginPercentW = 0;
            if (tmpImage.Height * 16 == tmpImage.Width * 9)
            {
                marginPercentW = 4 * 0.5F / 16F;
            }

            var rect = new RectangleF(marginPercentW * tmpImage.Width,
                0,
                tmpImage.Width - marginPercentW * tmpImage.Width * 2,
                tmpImage.Height);

            // 3) properly word wrap. (.NET function text-wraps at character level, but we want word-level wrap
            // find some biggest fontsize we will begin with. GetWordWrappedText will find the proper smaller font size
            // that makes everything fit into the draw region
            var fontSize = Math.Min(tmpImage.Width, tmpImage.Height) / 2;
#if DO_PROFILE
            Stopwatch profiler = Stopwatch.StartNew();
#endif
            var lines = GetWordWrappedText(ref fontSize, stringFormat, rect, graphics);
#if DO_PROFILE
            profiler.Stop();
            Console.WriteLine(String.Format("GetWorldWrappedText() took {0} ms", profiler.ElapsedMilliseconds));
#endif
            // TODO: configuable
            var dropShadowWidth = Math.Max(5, fontSize / 12);

            // 4) divide draw rect into N regions and draw each line.
            var font = new Font(cbFonts.SelectedItem as string, fontSize, FontStyle.Bold);

            var numLines = lines.Count;
            float heightPerRow = rect.Height / numLines;
            rect.Height = heightPerRow;
            rect.X = 0;
            rect.Width = tmpImage.Width;        // now set the width to be max. it'll be centered aligned anyways

            for (int i = 0; i < numLines; ++i)
            {
                DrawText(graphics, lines[i], font, dropShadowWidth, rect, stringFormat);
                rect.Y += heightPerRow;
            }

            // 5) finally set the image to the picture box
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
