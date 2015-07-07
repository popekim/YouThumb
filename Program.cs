//----------------------------------------------------------------------------
// Copyright (c) 2013 Pope Kim (www.popekim.com)
//
// See the file LICENSE for copying permission.
//-----------------------------------------------------------------------------

using System;
using System.Windows.Forms;

namespace YouThumb
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new frmMain());
        }
    }
}
