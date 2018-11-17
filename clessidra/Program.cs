using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Blue_Screen_saver
{
    static class Program
    {
        /// <summary>
        
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            if (args.Length > 0)
            {
                if (args[0].ToLower().Trim().Substring(0, 2) == "/s") //show
                {
                    //Esegui  screen saver
                    Application.EnableVisualStyles();
                    Application.SetCompatibleTextRenderingDefault(false);
                    ShowScreensaver();
                    Application.Run();
                }
                else if (args[0].ToLower().Trim().Substring(0, 2) == "/p") //preview
                {
                    //screen saver anteprima
                    Application.EnableVisualStyles();
                    Application.SetCompatibleTextRenderingDefault(false);
                    Application.Run(new MainForm(new IntPtr(long.Parse(args[1])))); //args[1] is the handle to the preview window
                }
                else if (args[0].ToLower().Trim().Substring(0, 2) == "/c") //configure
                {
                    
                    MessageBox.Show("Questo screensaver non ha proprietà di configurazione","Clessidra",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }
                else 
                {
                    
                    Application.EnableVisualStyles();
                    Application.SetCompatibleTextRenderingDefault(false);
                    ShowScreensaver();
                    Application.Run();
                }
            }
            else 
            {
                //Esegui screen saver
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                ShowScreensaver();
                Application.Run();
            }
        }

        static void ShowScreensaver()
        {
            
            foreach (Screen screen in Screen.AllScreens)
            {
                
                MainForm screensaver = new MainForm(screen.Bounds);
                screensaver.Show();
            }
        }
    }
}
