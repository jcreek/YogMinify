﻿#region License Information (GPL v3)

/*

    YogMinify - Simple command line image minifier wrapper.
    Copyright (c) 2018 Yogensia.

    This program is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with this program. If not, see <http://www.gnu.org/licenses/>.

*/

#endregion License Information (GPL v3)

using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Threading;

namespace YogMinify
{
    class Minifier
    {
        // Properties.
        public string minifier;      // Name of the minifier.
        public string arguments;     // Arguments for the minifier execution.
        public string minifyFormat;  // Format supported by this minifier.
        public string fileFormat;    // Format of the image supplied.
        public FileInfo file;        // Absolute path to file we are working on.

        // Constructor.
        public Minifier(string _minifier, string _arguments, string _minifyFormat, string _fileFormat, string _file)
        {
            minifier     = _minifier;
            arguments    = _arguments;
            minifyFormat = _minifyFormat;
            fileFormat   = _fileFormat;
            file         = new FileInfo(_file);

            // Check the image format matches this minifier, if so run it.
            if (minifyFormat == fileFormat)
                Minify();
        }

        // Methods.
        public void Minify()
        {
            // Variables for process console output redirection.
            bool output = HandleArgs.verbosity <= 0;
            bool outputError = HandleArgs.verbosity <= 0;

            // Create and run minifier process.
            Console.WriteLine("Running " + minifier + "...");
            var process = new Process();
            process.StartInfo.FileName = @"Minifiers\" + minifier + ".exe";
            process.StartInfo.Arguments = arguments;
            Utils.Debug("with args: {0}", process.StartInfo.Arguments);
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.RedirectStandardOutput = output;
            process.StartInfo.RedirectStandardError = outputError;

            // Try to start minifier process.
            try
            {
                process.Start();
            }
            catch (Win32Exception)
            {
                Console.WriteLine("Could not find minifier executable '" + minifier + ".exe'.");
                Utils.PressAnyKey(HandleArgs.pause);
                Environment.Exit(0);
            }
            catch (Exception e)
            {
                Console.WriteLine("An error ocurred while trying to execute '" + minifier + ".exe':");
                Console.WriteLine(e.Message);
                Utils.PressAnyKey(HandleArgs.pause);
                Environment.Exit(0);
            }

            // Set priority & wait for process to end.
            Utils.ChangePriority(process);
            process.WaitForExit();

            // Compare file sizes after compressing.
            CheckSize();
        }

        private void CheckSize()
        {
            FileInfo smallestFile = new FileInfo(file.ToString() + "_smallest");

            if (smallestFile.Exists)
            {
                // Smallest file already exists, so compare sizes.
                if (file.Length < smallestFile.Length)
                {
                    // Smaller, save as new smallest
                    Console.WriteLine("Smaller size: {0}", Utils.SizeSuffix(file.Length));

                    smallestFile.Delete();
                    File.Copy(file.ToString(), smallestFile.ToString());
                }
                else if (file.Length == smallestFile.Length)
                {
                    // Same size, ignore.
                    Console.WriteLine("Same size: {0}", Utils.SizeSuffix(file.Length));
                }
                else if (file.Length > smallestFile.Length)
                {
                    // Bigger, revert the changes.
                    Console.WriteLine("Bigger size: {0} (ignoring)", Utils.SizeSuffix(file.Length));

                    file.Delete();
                    File.Copy(smallestFile.ToString(), file.ToString());
                }
            }
            else
            {
                // If no smallest size recorded, create it for the first time.
                File.Copy(file.ToString(), smallestFile.ToString());

                Console.WriteLine("New size: {0}", Utils.SizeSuffix(file.Length));
            }
        }
    }
}