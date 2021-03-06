#region License Information (GPL v3)

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
using System.Collections.Generic;
using Mono.Options;

namespace YogMinify
{
    internal static class HandleArgs
    {
        // Set variables.
        public static string output = "";                // TODO: Output path.
        public static string format = "";                // If not null specifies a format to convert to.
        public static int quality = 90;                  // Quality setting for compression.
        public static string prefix = "";                // Prefix added to output filename.
        public static string suffix = ".min";            // Suffix added to output filename.
        public static string priority = "BelowNormal";   // Priority of Minifier processes.
        public static int lossy = 0;                     // Allow lossy compression.
        public static int overwrite = 0;                 // Overwrite output file without asking.
        public static int pause = 0;                     // Show additional info on console.
        public static int verbosity = 0;                 // Show additional info on console.
        public static int test = 0;                      // Test mode, no changes done to files.
        public static int skipwarnings = 0;              // Skip warnings and continue operation.
        public static bool showLibraries = false;        // TODO: Show minify library information.
        public static bool showHelp = false;             // TODO: Show usage and arguments help.
        private static List<string> inputFiles;          // List containing input files found in command line.

        public static OptionSet GetOptionSet(string[] args)
        {
            // Get arguments.
            var p = new OptionSet
            {
                { "o|output=", "Set output file or directory.\n" +
                  "If not provided, 'min' will be appended to the input filename.\n" +
                  "If the input provided is a directory, this will be the folder where new files will be saved, preserving original directory structure.",
                   (string v) => output = v },
                { "f|format=", "Convert to specified format before minifying.",
                   (string v) => format = v },
                { "q|quality=", "Set quality of the JPEG compression from 0 to 100.\n(default value: 95)",
                   (int v) => quality = v },
                { "l|libraries",  "Show a message with info about the minify libraries used by YogMinify and exit.",
                   v => showLibraries = v != null },
                { "m|lossy", "Allow lossy compression on PNG.",
                   v => { if (v != null) ++lossy; } },
                { "p|prefix=", "Set prefix pattern used for output filename.\n" +
                  "By default no prefix is added.\n" +
                  "Example: 'min_' will result in 'min_image.jpg'.",
                   (string v) => prefix = v },
                { "s|suffix=", "Set suffix pattern used for output filename.\n" +
                  "By default '.min' is appended to the filename.\nExample: 'image.min.jpg'.",
                   (string v) => suffix = v },
                { "r|priority=", "Set CPU priority for minifier processes.\n" +
                  "By default Below Normal is used.\n" +
                  "Options are: Idle, BelowNormal, Normal, AboveNormal, High, Realtime.\n",
                   (string v) => priority = v },
                { "w|overwrite", "Overwrite output file if it already exists.",
                   v => { if (v != null) ++overwrite; } },
                { "a|pause", "Pause when finished.",
                   v => { if (v != null) ++pause; } },
                { "v|verbose", "Increase debug message verbosity. Will look messy.",
                   v => { if (v != null) ++verbosity; } },
                { "t|test", "Preview mode, outputs list of files that will be output without making any actual changes.",
                   v => { if (v != null) ++test; } },
                { "y|skipwarnings", "Skips warnings (like when you're about to minify lots of files) and continues operation.",
                   v => { if (v != null) ++skipwarnings; } },
                { "h|help",  "Show this message and exit.",
                   v => showHelp = v != null },
            };
            return p;
        }

        public static List<string> GetRawArgs(string[] args)
        {
            var p = GetOptionSet(args);

            // Parse input files from command line arguments.
            try
            {
                inputFiles = p.Parse(args);
                Utils.Debug("Parse inputs: ");
                Utils.Debug(string.Join(",\n", inputFiles.ToArray()));
                Utils.Debug("");
            }
            catch (OptionException e)
            {
                Console.Write("Syntax error: ");
                Console.WriteLine(e.Message);
                Console.WriteLine();
                Console.WriteLine("Try `YogMinify --help' for more information.");
                Console.WriteLine("If you think this is a bug feel free to report on GitHub: https://github.com/yogensia/YogMinify/issues");
                Utils.PressAnyKey(overwrite);
                return null;
            }

            return inputFiles;
        }
    }
}
