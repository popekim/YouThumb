///----------------------------------------------------------------------------
// Copyright (c) 2013 Pope Kim (www.popekim.com)
//
// See the file LICENSE for copying permission.
//-----------------------------------------------------------------------------

namespace YouThumb
{
    partial class frmMain
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
            this.dlgSave = new System.Windows.Forms.SaveFileDialog();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabAuto = new System.Windows.Forms.TabPage();
            this.webBrowser = new System.Windows.Forms.WebBrowser();
            this.buttonLogin = new System.Windows.Forms.Button();
            this.tabManual = new System.Windows.Forms.TabPage();
            this.label2 = new System.Windows.Forms.Label();
            this.pbThumb = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cbFonts = new System.Windows.Forms.ComboBox();
            this.tbURL = new System.Windows.Forms.TextBox();
            this.textboxClientID = new System.Windows.Forms.TextBox();
            this.labelClientID = new System.Windows.Forms.Label();
            this.labelClientSecret = new System.Windows.Forms.Label();
            this.textboxClientSecret = new System.Windows.Forms.TextBox();
            this.tabControl.SuspendLayout();
            this.tabAuto.SuspendLayout();
            this.tabManual.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbThumb)).BeginInit();
            this.SuspendLayout();
            // 
            // dlgSave
            // 
            this.dlgSave.Filter = "PNG Files|*.png";
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabAuto);
            this.tabControl.Controls.Add(this.tabManual);
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Location = new System.Drawing.Point(0, 0);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(986, 812);
            this.tabControl.TabIndex = 4;
            // 
            // tabAuto
            // 
            this.tabAuto.Controls.Add(this.textboxClientSecret);
            this.tabAuto.Controls.Add(this.labelClientSecret);
            this.tabAuto.Controls.Add(this.labelClientID);
            this.tabAuto.Controls.Add(this.textboxClientID);
            this.tabAuto.Controls.Add(this.webBrowser);
            this.tabAuto.Controls.Add(this.buttonLogin);
            this.tabAuto.Location = new System.Drawing.Point(4, 22);
            this.tabAuto.Name = "tabAuto";
            this.tabAuto.Padding = new System.Windows.Forms.Padding(3);
            this.tabAuto.Size = new System.Drawing.Size(978, 786);
            this.tabAuto.TabIndex = 1;
            this.tabAuto.Text = "Auto";
            this.tabAuto.UseVisualStyleBackColor = true;
            // 
            // webBrowser
            // 
            this.webBrowser.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.webBrowser.Location = new System.Drawing.Point(8, 35);
            this.webBrowser.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser.Name = "webBrowser";
            this.webBrowser.Size = new System.Drawing.Size(967, 743);
            this.webBrowser.TabIndex = 1;
            // 
            // buttonLogin
            // 
            this.buttonLogin.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonLogin.Location = new System.Drawing.Point(829, 6);
            this.buttonLogin.Name = "buttonLogin";
            this.buttonLogin.Size = new System.Drawing.Size(141, 23);
            this.buttonLogin.TabIndex = 0;
            this.buttonLogin.Text = "Youtube Login";
            this.buttonLogin.UseVisualStyleBackColor = true;
            this.buttonLogin.Click += new System.EventHandler(this.buttonLogin_Click);
            // 
            // tabManual
            // 
            this.tabManual.Controls.Add(this.label2);
            this.tabManual.Controls.Add(this.pbThumb);
            this.tabManual.Controls.Add(this.label1);
            this.tabManual.Controls.Add(this.cbFonts);
            this.tabManual.Controls.Add(this.tbURL);
            this.tabManual.Location = new System.Drawing.Point(4, 22);
            this.tabManual.Name = "tabManual";
            this.tabManual.Padding = new System.Windows.Forms.Padding(3);
            this.tabManual.Size = new System.Drawing.Size(978, 786);
            this.tabManual.TabIndex = 0;
            this.tabManual.Text = "Manual";
            this.tabManual.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 35);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(78, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Font to Render";
            // 
            // pbThumb
            // 
            this.pbThumb.Location = new System.Drawing.Point(9, 65);
            this.pbThumb.Name = "pbThumb";
            this.pbThumb.Size = new System.Drawing.Size(1280, 720);
            this.pbThumb.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pbThumb.TabIndex = 3;
            this.pbThumb.TabStop = false;
            this.pbThumb.Click += new System.EventHandler(this.pbThumb_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(106, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "YouTube Video URL";
            // 
            // cbFonts
            // 
            this.cbFonts.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbFonts.FormattingEnabled = true;
            this.cbFonts.Location = new System.Drawing.Point(118, 32);
            this.cbFonts.Name = "cbFonts";
            this.cbFonts.Size = new System.Drawing.Size(162, 21);
            this.cbFonts.TabIndex = 2;
            this.cbFonts.SelectedIndexChanged += new System.EventHandler(this.cbFonts_SelectedIndexChanged);
            // 
            // tbURL
            // 
            this.tbURL.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbURL.Location = new System.Drawing.Point(118, 6);
            this.tbURL.Name = "tbURL";
            this.tbURL.Size = new System.Drawing.Size(1640, 20);
            this.tbURL.TabIndex = 1;
            this.tbURL.TextChanged += new System.EventHandler(this.tbURL_TextChanged);
            // 
            // textboxClientID
            // 
            this.textboxClientID.Location = new System.Drawing.Point(60, 9);
            this.textboxClientID.Name = "textboxClientID";
            this.textboxClientID.Size = new System.Drawing.Size(148, 20);
            this.textboxClientID.TabIndex = 2;
            this.textboxClientID.UseSystemPasswordChar = true;
            this.textboxClientID.TextChanged += new System.EventHandler(this.textboxClientID_TextChanged);
            // 
            // labelClientID
            // 
            this.labelClientID.AutoSize = true;
            this.labelClientID.Location = new System.Drawing.Point(8, 11);
            this.labelClientID.Name = "labelClientID";
            this.labelClientID.Size = new System.Drawing.Size(47, 13);
            this.labelClientID.TabIndex = 3;
            this.labelClientID.Text = "Client ID";
            // 
            // labelClientSecret
            // 
            this.labelClientSecret.AutoSize = true;
            this.labelClientSecret.Location = new System.Drawing.Point(214, 11);
            this.labelClientSecret.Name = "labelClientSecret";
            this.labelClientSecret.Size = new System.Drawing.Size(67, 13);
            this.labelClientSecret.TabIndex = 4;
            this.labelClientSecret.Text = "Client Secret";
            // 
            // textboxClientSecret
            // 
            this.textboxClientSecret.Location = new System.Drawing.Point(287, 9);
            this.textboxClientSecret.Name = "textboxClientSecret";
            this.textboxClientSecret.Size = new System.Drawing.Size(148, 20);
            this.textboxClientSecret.TabIndex = 5;
            this.textboxClientSecret.UseSystemPasswordChar = true;
            this.textboxClientSecret.WordWrap = false;
            this.textboxClientSecret.TextChanged += new System.EventHandler(this.textboxClientSecret_TextChanged);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(986, 812);
            this.Controls.Add(this.tabControl);
            this.Name = "frmMain";
            this.Text = "YouTube Thumbnail Generator by Pope Kim (www.popekim.com)";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMain_FormClosing);
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.tabControl.ResumeLayout(false);
            this.tabAuto.ResumeLayout(false);
            this.tabAuto.PerformLayout();
            this.tabManual.ResumeLayout(false);
            this.tabManual.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbThumb)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.SaveFileDialog dlgSave;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabAuto;
        private System.Windows.Forms.WebBrowser webBrowser;
        private System.Windows.Forms.Button buttonLogin;
        private System.Windows.Forms.TabPage tabManual;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.PictureBox pbThumb;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbFonts;
        private System.Windows.Forms.TextBox tbURL;
        private System.Windows.Forms.Label labelClientSecret;
        private System.Windows.Forms.Label labelClientID;
        private System.Windows.Forms.TextBox textboxClientID;
        private System.Windows.Forms.TextBox textboxClientSecret;
    }
}

