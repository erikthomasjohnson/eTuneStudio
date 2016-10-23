using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;

namespace eTuneStudio.eTuneConsole
{
    public class ScreenCapture
    {
        Bitmap printscreen = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
        List<string> filenames = new List<string>();
        public async void PrintScreen()
        {
            try
            {
                using (Graphics graphics = Graphics.FromImage(printscreen as Image))
                {
                    graphics.CopyFromScreen(0, 0, 0, 0, printscreen.Size);
                    printscreen.Tag = "GetText_" + DateTime.Now.ToString("yyyy-MM-dd@hh.mm.ss.fff");
                    string filename = Path.Combine(Environment.GetEnvironmentVariable("TEMP") ?? @"C:\Temp", "GetText_" + DateTime.Now.ToString("yyyy-MM-dd_ss") + ".png");
                    filenames.Add(filename);
                    printscreen.Save(filename, ImageFormat.Png);
                    Console.WriteLine(filename);
                    await Task.Delay(1000).ConfigureAwait(false);
                    PrintScreen();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                await Task.Delay(1000).ConfigureAwait(false);
                if (Console.ReadKey().Key != ConsoleKey.Escape)
                {
                    PrintScreen();
                }
            }
        }
    }
}