OPTIPNG(1)                                                          OPTIPNG(1)

NAME
       optipng - optimize Portable Network Graphics files

SYNOPSIS
       optipng [-? | -h | -help]
       optipng [options...] files...

DESCRIPTION
       OptiPNG  shall attempt to optimize PNG files, i.e. reduce their size to
       a minimum, without losing semantic information. In addition, this  pro-
       gram  shall  perform  a  suite  of  auxiliary  functions like integrity
       checks, metadata recovery and pixmap-to-PNG conversion.

       The optimization attempts are not  guaranteed  to  succeed.  Valid  PNG
       files  that  cannot  be  optimized  by  this  program are normally left
       intact; their size will not grow. The user may request to override this
       default behavior.

FILES
       The  input  files  are  raster image files encoded either in PNG format
       (the native format), or in an external format. The currently  supported
       external formats are GIF, BMP, PNM and TIFF.

       OptiPNG processes each image file given in the command line as follows:

       - If the image is in PNG format:

              Attempts to optimize the given file in-place. If optimization is
              successful, or if the option -force  is  enabled,  replaces  the
              original file with its optimized version.

       - If the image is in an external format:

              Creates  an  optimized PNG version of the given file. The output
              file name is composed from the original file name and  the  .png
              extension.

OPTIONS
   General options
       -?, -h, -help
              Show a complete summary of options.

       -backup
              Back up the modified files.
              The backup file name is composed from the original file name and
              a backup suffix. The backup suffix is .bak.

       -clobber
              This option, which has no effect,  is  deprecated  and  will  be
              removed eventually.
              Starting with OptiPNG version 1.0, output files are clobbered by
              default.  Use -no-clobber to revert this setting.

       -dir directory
              Write the output files to directory.

       -fix   Enable error recovery. This option has no effect on valid  input
              files.
              The  program will spend a reasonable amount of effort to recover
              as much data as possible, without  increasing  the  output  file
              size,  but  the success cannot be generally guaranteed. The pro-
              gram may even increase the file size,  e.g.,  by  reconstructing
              missing  critical  data. Under this option, integrity shall take
              precedence over file size.
              When this option is not used, the invalid input files  are  left
              unprocessed.

       -force Enforce creation of a new output file.
              This  option overrides the program's decision not to create such
              file, e.g. when the PNG input is digitally signed (using  dSIG),
              or when the PNG output becomes larger than the PNG input.

       -keep  This  option  is  deprecated and will be removed eventually. Use
              -backup.

       -log file
              This option is no longer supported. Use standard shell redirect-
              ion.

       -no-clobber
              Do not overwrite existing output or backup files.

       -no-create
              Perform the encoding trials, but do not create any files.

       -stdout
              Write output to the standard console output.

       -out file
              Write  output  file  to file.  The command line must contain one
              input file.

       -preserve
              Preserve file attributes (time stamps, file access rights, etc.)
              where applicable.

       -quiet, -silent
              Run in quiet mode: do not show the informational messages on the
              console.

       -simulate
              This option is deprecated and will be  removed  eventually.  Use
              -no-create.

       -v     Enable the options -verbose and -version.

       -verbose
              Run in verbose mode.

       -version
              Show copyright, version and build info.

       --     Stop option switch parsing.

   PNG encoding and optimization options
       -o level
              Select the optimization level.
              The  optimization level 1 is an alias to the option -fast, which
              enables a single IDAT compression trial.
              The optimization levels 2 and higher enable multiple  IDAT  com-
              pression trials; the higher the level, the more trials.
              The  behavior  and  the  default value of this option may change
              across different program versions. Use the option -help  to  see
              the details pertaining to your specific version.

       -fast  Select the fast compression level.
              Only  one  IDAT compression trial is performed. The trial chosen
              is what OptiPNG thinks it's probably the most effective.
              At this level, OptiPNG is functionally similar to  other  common
              single-pass  PNG  encoders that run at their highest and slowest
              compression level (which, usually, is 9). Use the option -zc  to
              tailor this level.

       -fastest
              Select the fastest compression level.
              Only  one  IDAT  compression  trial  is  performed,  except when
              encountering existing IDAT datastreams,  which  are  not  recom-
              pressed.  The  trial chosen is what OptiPNG thinks it's probably
              the fastest that can give a decent compression ratio.

       -f filters
              Select the PNG delta filters.
              The filters argument is specified as a  rangeset  (e.g.  -f0-5),
              and  the default filters value depends on the optimization level
              set by the option -o.
              The filter values 0, 1, 2, 3 and 4  indicate  static  filtering,
              and correspond to the standard PNG filter codes (None, Left, Up,
              Average and Paeth, respectively). The filter value  5  indicates
              adaptive  filtering,  whose  effect  is defined by the libpng(3)
              library used by OptiPNG.

       -full  This option is deprecated and will be  removed  eventually.  Use
              -paranoid.

       -i type
              Select the interlace type (0-1).
              If  the  interlace type 0 is selected, the output image shall be
              non-interlaced (i.e. progressive-scanned). If the interlace type
              1  is  selected,  the output image shall be interlaced using the
              Adam7 method.
              By default, the output shall have the same interlace type as the
              input.

       -nb    Do not apply bit depth reduction.

       -nc    Do not apply color type reduction.

       -np    Do not apply palette reduction.

       -nx    Do  not  apply  any lossless image reduction: enable the options
              -nb, -nc and -np.

       -nz    Do not recode IDAT datastreams.
              The IDAT optimization operations that do  not  require  recoding
              (e.g. IDAT chunk concatenation) are still performed.
              This option has effect on PNG input files, as well as files that
              contain embedded PNG datastreams, like PNG-compressed BMP files.
              It is ignored otherwise.

       -paranoid
              Encode IDAT fully and show its size in the report.
              This  option  might  slow  down  the encoding trials, but has no
              effect on the final output.

       -zc levels
              Select the zlib compression levels used in IDAT compression.
              The levels argument is specified as a  rangeset  (e.g.  -zc6-9),
              and  the  default levels value depends on the optimization level
              set by the option -o.
              The effect of this option is defined by the zlib(3) library used
              by OptiPNG.

       -zm levels
              Select the zlib memory levels used in IDAT compression.
              The  levels  argument  is specified as a rangeset (e.g. -zm8-9),
              and the default levels value depends on the  optimization  level
              set by the option -o.
              The effect of this option is defined by the zlib(3) library used
              by OptiPNG.

       -zs strategies
              Select the zlib compression strategies used in IDAT compression.
              The  strategies  argument  is  specified  as  a  rangeset  (e.g.
              -zs0-3),  and  the default strategies value depends on the opti-
              mization level set by the option -o.
              The effect of this option is defined by the zlib(3) library used
              by OptiPNG.

       -zw size
              Select  the  zlib window size (32k,16k,8k,4k,2k,1k,512,256) used
              in IDAT compression.
              The size argument can be specified either in bytes (e.g.  16384)
              or  kilobytes  (e.g.  16k). The default size value is set to the
              lowest window size that yields an  IDAT  output  as  big  as  if
              yielded by the value 32768.
              The effect of this option is defined by the zlib(3) library used
              by OptiPNG.

   Editing options
       -set object=value
              Set an image data object in a PNG file.
              TODO: Explain -set image.alpha.precision=num, etc.

       -reset objects
              Reset image data objects in a PNG file.
              TODO: Explain -reset image.alpha.

       -strip objects
              Strip metadata objects from a PNG file.
              PNG metadata is the information stored in  any  ancillary  chunk
              except tRNS.  (tRNS represents the alpha channel, which, even if
              ignored in rendering, is still a proper  image  channel  in  the
              RGBA color space.)
              The  accepted  objects are either chunk names or the all object.
              Multiple objects can be comma-separated within a  single  -strip
              option, or split across multiple -strip options.

       -protect objects
              Prevent metadata objects from being stripped.
              This  option  has  priority  over  -strip.  For  example,  under
              -strip all -protect sRGB, everything except  sRGB  is  stripped;
              under -strip all -protect all, nothing is stripped.
              The  accepted  objects are either chunk names or the all object.
              Multiple objects can be comma-separated within a single -protect
              option, or split across multiple -protect options.

       -snip  Cut one image out of multi-image, animation or video files.
              Depending  on  the input format, this may be either the first or
              the most relevant (e.g. the largest) image.

   Notes
       Option names are case-sensitive and may be abbreviated to their  short-
       est  unique  prefix.  Option parsing stops at the first file name or at
       the option --, whichever comes first.

       Some options may have arguments that follow the option name,  separated
       by whitespace or the equal sign ('='). If the option argument is a num-
       ber or a rangeset, the separator may be omitted. For example:

              -out newfile.png  <=>  -out=newfile.png
              -o3  <=>  -o 3  <=>  -o=3
              -f0,3-5  <=>  -f 0,3-5  <=>  -f=0,3-5

       Rangeset arguments are cumulative; e.g.

              -f0 -f3-5  <=>  -f0,3-5
              -zs0 -zs1 -zs2-3  <=>  -zs0,1,2,3  <=>  -zs0-3

EXTENDED DESCRIPTION
       The PNG optimization algorithm consists of the following steps:

       1.  Reduce the bit depth, the color type and the color palette  of  the
           image.   This  step  may reduce the size of the uncompressed image,
           which, indirectly, may reduce the  size  of  the  compressed  image
           (i.e. the size of the output PNG file).

       2.  Run  a  suite  of compression methods and strategies and select the
           compression parameters that yield the smallest output file.

       3.  Store all IDAT contents into a single chunk, eliminating the  over-
           head incurred by repeated IDAT headers and CRCs.

       4.  Set  the  zlib  window  size inside IDAT to a mininum that does not
           affect the compression ratio, reducing the memory  requirements  of
           PNG decoders.

       Not all of the above steps need to be executed. The behavior depends on
       the actual input files and user options.

       Step 1 may be customized via the no-reduce options -nb,  -nc,  -np  and
       -nx.  Step 2 may be customized via the -o option, and may be fine-tuned
       via the options -zc, -zm, -zs and -zw. Step 3 is always executed.  Step
       4  is  executed  only  if a new IDAT is being created, and may be fine-
       tuned via the option -zw.

       Extremely exhaustive searches are not generally expected to yield  sig-
       nificant  improvements  in  compression  ratio,  and are recommended to
       advanced users only.

       The -o1 heuristic consists of picking the compression  parameters  that
       are  believed  to  produce  the  smallest  IDAT.  (Most  other good PNG
       encoders use a similar heuristic.) This heuristic works as follows:

              Select the zlib compression level 9  (i.e.  the  highest  avail-
              able).
              Select  the  filter value 0 (None) for images encoded in palette
              mode or with a bit depth less than 8; select the filter value  5
              (All) otherwise.
              Select  the  zlib  memory  level  8  and  the  zlib  strategy  0
              (Z_DEFAULT_STRATEGY) if the filter value is 0; select  the  zlib
              memory level 9 and the zlib strategy 1 (Z_FILTERED) otherwise.

EXIT STATUS
       Upon program termination, the following exit codes shall be returned:

       0      The execution terminated normally. The input files (if any) were
              either successfully optimized or left intact.

       1      One or more input files had errors, all of which  were  success-
              fully  fixed.   This  can  only  happen  if  the  -fix option is
              enabled.

       2      One or more input files had errors that were  not  fixed.   This
              can  happen  when  the  errors are too severe to recover, or the
              -fix option is not enabled.

       64 (EX_USAGE)
              The command line was incorrect.

       66 (EX_NOINPUT)
              A file or directory did not exist or was not readable.

       69 (EX_UNAVAILABLE)
              An unavailable or unimplemented program feature or  service  was
              requested.

       70 (EX_SOFTWARE)
              An unrecoverable internal software error (i.e. a severe bug) was
              detected.

       71 (EX_OSERR)
              A system error (e.g. a memory allocation failure) has occurred.

       73 (EX_CANTCREAT)
              A file or directory could not be created.

       Other sysexits may be added in the future.

EXAMPLES
       optipng file.png      # default speed
       optipng -o4 file.png  # slow
       optipng -o6 file.png  # very slow

CAVEAT
       Lossless image reductions are not completely implemented.   (This  does
       not  affect  the  integrity of the output files.)  Here are the missing
       pieces:

              - The color palette reductions are implemented only partially.
              - The bit depth reductions below 8, for  grayscale  images,  are
              not implemented yet.

       Encoding of images whose total IDAT size exceeds 2GB is not supported.

       TIFF support is limited to uncompressed, PNG-compatible (grayscale, RGB
       and RGBA) images.

       Metadata is not imported from the external image formats.

       There is no support for pipes or streams.

SEE ALSO
       png(5), libpng(3), zlib(3), pngcrush(1), pngrewrite(1).

STANDARDS
       The files produced by OptiPNG are compliant with PNG-2003:
       Glenn Randers-Pehrson et al.  Portable Network Graphics (PNG)  Specifi-
       cation, Second Edition.
       W3C Recommendation 10 November 2003; ISO/IEC IS 15948:2003 (E).
       http://www.w3.org/TR/PNG/

AUTHOR
       OptiPNG is written and maintained by Cosmin Truta.

       This  manual  page  was originally written by Nelson A. de Oliveira for
       the Debian Project. It was later updated by Cosmin Truta,  and  is  now
       part of the OptiPNG distribution.

OptiPNG version Hg                  @DATE@                          OPTIPNG(1)
