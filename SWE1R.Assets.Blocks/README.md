# SWE1R.Assets.Blocks

A library for handling game assets of Star Wars Episode 1 Racer.

## Overview

**SWE1R.Assets.Blocks** allows you to deserialize, modify, and serialize block file assets from the game. It supports bit-perfect reserialization, ensuring that if no changes are made to the deserialized object graph, the output binary data will be identical to the input data. This allows for precise modifications where only the intended bytes are altered.

## Features

* **Deserialize**: Convert binary block file assets into an object graph for easy manipulation.
* **Modify**: Make changes to the deserialized objects as needed.
* **Serialize**: Convert the modified object graph back into binary form with bit-perfect accuracy.

## Supported Asset Files

The library currently supports the following asset files:

* ``out_modelblock.bin``
* ``out_splineblock.bin``
* ``out_spriteblock.bin``
* ``out_textureblock.bin``

These files can be found in the game's installation directory under ``data/lev01``.
