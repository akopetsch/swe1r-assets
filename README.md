# swe1r-assets

[![NuGet](https://img.shields.io/nuget/vpre/SWE1R.Assets.Blocks)](https://nuget.org/packages/SWE1R.Assets.Blocks)

A C#/.NET library for handling game assets of Star Wars Episode 1 Racer.

Notably, this libary is used by [swe1r-assets-unity](https://github.com/akopetsch/swe1r-assets-unity).

https://github.com/akopetsch/swe1r-assets/assets/8048046/511646d4-738c-41cc-b7c0-9e67920ac07e

Imported OBJ model is by [Leadphalanx](https://forums.tigsource.com/index.php?topic=68973.0).

## Overview

**SWE1R.Assets.Blocks** allows you to deserialize, modify, and serialize block file assets from the game. It supports bit-perfect reserialization, ensuring that if no changes are made to the deserialized object graph, the output binary data will be identical to the input data. This allows for precise modifications where only the intended bytes are altered.

## Features

* **Deserialize**: Convert binary block file assets into an object graph for easy manipulation.
* **Modify**: Make changes to the deserialized objects as needed.
* **Serialize**: Convert the modified object graph back into binary form with bit-perfect accuracy.

The library currently supports the following asset files:

* ``out_modelblock.bin``
* ``out_splineblock.bin``
* ``out_spriteblock.bin``
* ``out_textureblock.bin``

These files can be found in the game's installation directory under ``data/lev01``.

## Community

Join the ``# modding`` channel of the discord server ['Star Wars Episode I: Racer'](https://discord.gg/xfvYpCma) for general discussions about hacking and modding of the game.
