# SWE1R.Assets.Blocks

A library used to deserialize, modify, and serialize block file assets. It allows for bit-perfect reserialization, meaning that if you deserialize assets into an object graph, make no changes, and serialize the graph again, the output binary data will be identical to the input data. Changing certain values will only alter the respective bytes in the output.

The following asset files can be manipulated:

* ``out_modelblock.bin``
* ``out_splineblock.bin``
* ``out_spriteblock.bin``
* ``out_textureblock.bin``

These files reside in the game's installation directory under ``data/lev01``.
