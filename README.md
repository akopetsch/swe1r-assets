# swe1r-assets

C#/.NET/Unity tools and libraries for the game Star Wars Episode 1 Racer.

This project offers a suite of tools and libraries designed for modding and researching the game's file formats. It features advanced functionalities such as bit-perfect reserialization of game assets, a level editor based on Unity, and lays the groundwork for extensive analysis of the file formats and internal mechanics. Dive in to explore, contribute, and bring new life to a beloved classic!

![Screenshot of SWE1R.Assets.Unity](screenshot.png)

## Features

* [ByteSerializer](#ByteSerializer)
* [SWE1R.Assets.Blocks](#SWE1R.Assets.Blocks)
* [SWE1R.Assets.Blocks.CommandLine](#SWE1R.Assets.Blocks.CommandLine)
* [SWE1R.Assets.Blocks.Unity](#SWE1R.Assets.Unity)

### ByteSerializer

A declarative .NET serialization library designed for bit-perfect control over binary representation. You can define C# classes like C structs by assigning attributes to classes and properties. Inspired by [BinarySerializer](https://github.com/jefffhaynes/BinarySerializer) by Jeff Haynes. 

This library was created from the ground up to support important features like references/pointers.

#### Example

Consider the following classes:

```csharp
public class Manufacturer
{
    [Order(0)]
    public byte NameLength { get; set; }
    [Order(1), Length(nameof(NameLength))]
    public char[] Name { get; set; }
}

public class Car
{
    [Order(0)]
    public byte NameLength { get; set; }
    [Order(1), Length(nameof(NameLength))]
    public char[] Name { get; set; }
    [Order(2), Reference]
    public Manufacturer Manufacturer { get; set; }
}
```

Create an object graph like this:

```csharp
var mf = new Manufacturer() { NameLength = 2, Name = "MF".ToArray() };
var car = new Car() { NameLength = 3, Name = "Car1".ToArray(), Manufacturer = mf };
using (var ms = new MemoryStream())
{
    new ByteSerializer().Serialize(ms, car, Endianness.BigEndian);
    File.WriteAllBytes("car.bin", ms.ToArray());
}
```

That gives you the following binary output:

```console
$ xxd car.bin
00000000: 0143 6172 3100 0000 0902 4d46            .Car1.....MF
```

| Bytes | Property | Property    | Value             |
|-------|----------|-------------|-------------------|
| 03          | car.NameLength   | 3                 |
| 43 61 72 31 | car.Name         | "Car1" in ASCII   |
| 00 00 00 09 | car.Manufacturer | (pointer to mf)   |
| 02          | mf.NameLength    | 2                 |
| 4d 46       | mf.Name          | "MF" in ASCII     |

### SWE1R.Assets.Blocks

A library used to deserialize, modify, and serialize block file assets. It allows for bit-perfect reserialization, meaning that if you deserialize assets into an object graph, make no changes, and serialize the graph again, the output binary data will be identical to the input data. Changing certain values will only alter the respective bytes in the output.

The following asset files can be manipulated:

* ``out_modelblock.bin``
* ``out_splineblock.bin``
* ``out_spriteblock.bin``
* ``out_textureblock.bin``

These files reside in the game's installation directory under ``data/lev01``.

### SWE1R.Assets.Blocks.Original.Tests

Unit testing involves reserializing every single game asset (block item) and asserting binary equality afterward.
This repository does not include the original asset files (obviously for copyright reasons).

### SWE1R.Assets.Blocks.CommandLine

A command-line application that allows you to work with the assets:

```console
$ SWE1R.Assets.Blocks.CommandLine.exe
SWE1R.Assets.Blocks.CommandLine 1.0.0
Copyright (C) 2023 SWE1R.Assets.Blocks.CommandLine

  list-models               List model block contents.

  list-splines              List spline block contents.

  list-sprites              List sprite block contents.

  list-textures             List texture block contents.

  export-sprites            Export sprites as PNG files.

  export-models-textures    Export models' textures as PNG files.

  mod-model-vertex-alpha    Modify a model by changing all vertices' alpha values to 128.

  help                      Display more information on a specific command.

  version                   Display version information.
```

### SWE1R.Assets.Blocks.Unity

A Unity project containing code to import, modify, and re-export assets. Like SWE1R.Assets.Blocks, it allows for bit-perfect re-exporting, meaning that if you import a block item (e.g., a model), make no changes in the Unity Editor, and export it again, the output binary data will be identical to the input data. Changing certain values will only alter the respective bytes in the output.

## Issues

This project is still heavily WIP. This is an early preview release, and I welcome your feedback or contributions (forks, pull-requests, issues) to improve it.

The following are some major domains to work on:

* Refactoring
* Documentation
* More Unit-Tests (other than re-serialization)
* Packaging (NuGet/Unity)
* Integrate existing findings of the community (where big credit goes):
  * [OpenSWE1R/swe1r-re](https://github.com/OpenSWE1R/swe1r-re)
  * [Olganix/Sw_Racer](https://github.com/Olganix/Sw_Racer)
  * [louriccia/SWE1R-Mods](https://github.com/louriccia/SWE1R-Mods)
* ...

Feel free to tell me what features you need the most or tell me about any bugs you find.
