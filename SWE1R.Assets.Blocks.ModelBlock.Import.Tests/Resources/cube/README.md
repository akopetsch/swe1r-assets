# noonat-cube

The files [``cube.obj``](cube.obj) and [``cube.mtl``](cube.mtl) were downloaded from the GitHub gist ['noonat/cube.mtl'](https://gist.github.com/noonat/1131091#file-cube-obj)
[[archived]](https://web.archive.org/web/20231209210259/https://gist.github.com/noonat/1131091#file-cube-obj)
by Nathan Ostgard.

The file [``cube.png``](cube.png) was then generated using ['UV Checker Map Maker'](https://uvchecker.vinzi.xyz/) [[archived]](https://web.archive.org/web/20231210113302/https://uvchecker.vinzi.xyz/) by Valle Horge Hurtado. The downloaded image file was downscaled to a resolution of 64x64 and palettized using ImageMagick:

```console
$ convert CustomUVChecker_byValle_1K.png -resize 64x64 PNG8:cube.png
$ convert -version
Version: ImageMagick 6.9.11-60 Q16 x86_64 2021-01-25 https://imagemagick.org
Copyright: (C) 1999-2021 ImageMagick Studio LLC
License: https://imagemagick.org/script/license.php
Features: Cipher DPC Modules OpenMP(4.5)
Delegates (built-in): bzlib djvu fftw fontconfig freetype heic jbig jng jp2 jpeg lcms lqr ltdl lzma openexr pangocairo png tiff webp wmf x xml zlib
```

Permission to use the generated PNG file was kindly granted.
