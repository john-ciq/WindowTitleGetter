using System;
using System.Collections.Generic;
using System.Linq;

namespace WindowTitleGetter
{

    /// <summary>
    /// Prints the window titles of running apps
    /// </summary>
    class Program
    {
        // List of titles already seen
        private static HashSet<String> seenTitles = new HashSet<String>();

        /// <summary>
        /// Log that a window title has been seen; also outputs the title.
        /// </summary>
        /// <param name="title">the title to mark as seen</param>
        /// <param name="prefix">optional log message prefix</param>
        private static void seeTitle(String title, String prefix = "\t")
        {
            Console.WriteLine(prefix + title);
            seenTitles.Add(title);
        }

        /// <summary>
        /// Gets a list of all window titles of currently running processes.
        /// </summary>
        /// <returns>a list of all window titles of currently running processes</returns>
        static List<String> getWindowCurrentWindowTitles()
        {
            // A list of all window titles which have been seen already
            List<String> windowTitles = new List<String>();

            // Get a list of all current processes
            System.Diagnostics.Process[] processlist = System.Diagnostics.Process.GetProcesses();
            //
            // If the process has a main window title, add it to the return set
            processlist.ToList().ForEach(process =>
            {
                if (!String.IsNullOrEmpty(process.MainWindowTitle))
                {
                    windowTitles.Add(process.MainWindowTitle);
                }
            });

            return windowTitles;
        }

        /// <summary>
        /// The main entry point.
        /// </summary>
        /// <param name="args">command line arguments</param>
        static void Main(string[] args)
        {

            // Output a list of window titles already open when this program starts
            Console.WriteLine("Existing window titles:");
            getWindowCurrentWindowTitles().ForEach(title =>
            {
                seeTitle(title);
            });

            // Start watching
            Console.WriteLine("Looking for new window titles:");
            while(true)
            {
                // See any new titles
                getWindowCurrentWindowTitles().ForEach(title => {
                    if (!seenTitles.Contains(title))
                    {
                        seeTitle(title);
                    }
                });
                System.Threading.Thread.Sleep(50);
            }
        }
    }
}
