pngrewrite
version 1.4.0  - 8 Jun 2010

A utility to reduce unnecessarily large palettes and bit depths in PNG image
files.

Web site: <http://entropymine.com/jason/pngrewrite/>

-----------------

Copyright (c) 2010 Jason Summers

This software is provided 'as-is', without any express or implied
warranty. In no event will the authors be held liable for any damages
arising from the use of this software.

Permission is granted to anyone to use this software for any purpose,
including commercial applications, and to alter it and redistribute it
freely, subject to the following restrictions:

    1. The origin of this software must not be misrepresented; you must not
    claim that you wrote the original software. If you use this software
    in a product, an acknowledgment in the product documentation would be
    appreciated but is not required.

    2. Altered source versions must be plainly marked as such, and must not be
    misrepresented as being the original software.

    3. This notice may not be removed or altered from any source
    distribution.

-----------------

Primary author: Jason Summers  <jason1@pobox.com>

Contributing authors:

  Wayne Schlitt
   - Write grayscale images when possible
   - Ability to sort palette by color usage
   - Improved find_pal_entry performance

  Soren Anderson
   - Changes to allow use in a unix-style pipeline
   - Improved compatibility with MinGW and Cygwin
   - Maintain tIME chunks

-----------------

Pngrewrite will:

 * Remove any unused palette entries, and write a palette that is only as
   large as needed.

 * Remove (collapse) any duplicate palette entries.

 * Convert non-palette image to palette images, provided they contain no
   more than 256 different colors.

 * Move any colors with transparency to the beginning of the palette, and
   write a tRNS chunk that is a small as possible.

 * Reduce the bit-depth (bits per pixel) as much as possible.

 * Write images as grayscale when possible, if that is compatible with
   the goal of using the minimum possible bit depth.

Under no circumstances does pngrewrite change the actual pixel colors, or
background color, or transparency of the image. If it ever does, that's a
bug.

--WARNING--
pngrewrite removes most extra (ancillary) information from the PNG file,
such as text comments. Although this does make the file size smaller, the
removed information may sometimes be important.

The only ancillary chunks that are NOT removed are:
   gAMA  - Image gamma setting
   sRGB  - srgb color space indicator
   tIME  - creation time
   pHYs  - physical pixel size
   bKGD and tRNS - Background color and transparency are maintained. The
      actual chunk may be modified according to the new color structure.

If the original image was interlaced, the new one will also be interlaced.

Pngrewrite will not work at all on images that have more than 256 colors.
Colors with the same RGB values but a different level of transparency
count as different colors. The background color counts as an extra color
if it does not occur in the image.
  
It will also not work at all on images that have a color depth of 16 bits,
since they cannot have a palette.

-----------------

This is a very inefficient program. It is (relatively) slow, and may use a
lot of memory. To be specific, it uses about 5 bytes per pixel, no matter
what the bit depth of the image is.

This program is (hopefully) reasonably portable, and should compile
without too much effort on most C compilers. It requires the libpng and
zlib libraries.

The pngrewrite code is structured as a library that could be used in other
applications, but I have not documented the interface.

-----------------

Files in this package:
 readme.txt = This file.
 pngrewrite.exe = Command-line executable for Windows. Should work on
         Windows 2000 and above (XP, Vista, 7, etc.).
 pngrewrite.c = Command-line utility.
 libpngrewrite.c = Core pngrewrite code.
 libpngrewrite.h = Header file for libpngrewrite.c.
 Makefile = A Makefile for Unix-like platforms. Tested on Linux and Cygwin
         platforms. May require GNU make.
 pngrewrite.sln, pngrewrite.vcproj = Project files for Visual C++ 2008.

-----------------

How to use:

From a (Windows) command-line, run 

  pngrewrite.exe <input-file.png> <output-file.png>

To read from standard-input, or write to standard-output, use "-" for the
filename.

-----------------