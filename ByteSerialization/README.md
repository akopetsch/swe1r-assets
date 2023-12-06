# ByteSerialization

A declarative .NET serialization library designed for bit-perfect control over binary representation. You can define C# classes like C structs by assigning attributes to classes and properties. Inspired by [BinarySerializer](https://github.com/jefffhaynes/BinarySerializer) by Jeff Haynes. 

This library was created from the ground up to support important features like references/pointers.

## Example

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
var car = new Car() { NameLength = 4, Name = "Car1".ToArray(), Manufacturer = mf };
using (var ms = new MemoryStream())
{
    new ByteSerializer().Serialize(ms, car, Endianness.BigEndian);
    File.WriteAllBytes("car.bin", ms.ToArray());
}
```

That gives you the following binary output:

```console
$ xxd car.bin
00000000: 0443 6172 3100 0000 0902 4d46            .Car1.....MF
```

| Bytes | Property | Property    | Value             |
|-------|----------|-------------|-------------------|
| 04          | car.NameLength   | 4                 |
| 43 61 72 31 | car.Name         | "Car1" in ASCII   |
| 00 00 00 09 | car.Manufacturer | (pointer to mf)   |
| 02          | mf.NameLength    | 2                 |
| 4d 46       | mf.Name          | "MF" in ASCII     |
