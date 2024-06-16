# F3DEX2

**TODO**: move (parts of) this document to other GitHub repos

The microcode signature ``RSP Gfx ucode F3DEX.NoN   fifo 2.08  Yoshitaka Yasumoto 1999 Nintendo.``
can only be found in the specific microcode variant ``gspF3DEX2.NoN.fifo.o``:

```console
$ grep -B 1 "RSP Gfx ucode F3DEX.NoN   fifo 2.08  Yoshitaka Yasumoto 1999 Nintendo." *.txt
2.0J.txt-gspF3DEX2.NoN.fifo.o
2.0J.txt:RSP Gfx ucode F3DEX.NoN   fifo 2.08  Yoshitaka Yasumoto 1999 Nintendo.
--
2.0K.txt-gspF3DEX2.NoN.fifo.o
2.0K.txt:RSP Gfx ucode F3DEX.NoN   fifo 2.08  Yoshitaka Yasumoto 1999 Nintendo.
--
2.0L.txt-gspF3DEX2.NoN.fifo.o
2.0L.txt:RSP Gfx ucode F3DEX.NoN   fifo 2.08  Yoshitaka Yasumoto 1999 Nintendo.
```

The specific microcode variant ``gspF3DEX2.NoN.fifo.o`` was introduced with ``OS v2.0J`` 
and did not change with further versions of the N64 OS:

```console
$ grep -i gspF3DEX2.NoN.fifo.o *.md5
2.0J.md5:3def4a18188eec8113679d65c3a40314  gspF3DEX2.NoN.fifo.o
2.0K.md5:3def4a18188eec8113679d65c3a40314  gspF3DEX2.NoN.fifo.o
2.0L.md5:3def4a18188eec8113679d65c3a40314  gspF3DEX2.NoN.fifo.o
```

The microcode ``gspF3DEX2`` (in all of its variants) was introduced with ``OS v2.0J``:

https://ultra64.ca/files/documentation/online-manuals/man/n64man/relnote_j/index.htm

> The major change made this time is the change from the old micro code (Fast3D) to the latest micro code (F3DEX2).

## F3DEX_GBI_2

[ultra64.ca - 'Online Manuals (OS 2.0J)' - 'N64 Programming Manual' - '25. Microcode' - '25.4 F3DEX2 Microcode'](https://ultra64.ca/files/documentation/online-manuals/man/pro-man/pro25/index25.4.html)

> GBI Compatibility
> The GBI used by the F3DEX2 series are compatible at the source level with those used by the Fast3D/F3DEX series. Code created with Fast3D/F3DEX can be recompiled for use with F3DEX2.
>   
> **Note:** The F3DEX2 AND Fast3D/F3DEX series microcodes are NOT compatible at the binary level.
>  
> Specifically, the F3DEX_GBI_2 macro must be defined at compile time.

Note:

An odd finding is that the ``F3DEX_GBI_2`` macro is already available in OS 2.0I:

```console
$ sed -n '89p' include/PR/gbi.h
```

```c
#ifdef   F3DEX_GBI_2
```

But also note that the binary format for gSPVertex was introduced with OS 2.0J:

```console
$ sed -n '1763,1785p' include/PR/gbi.h
```

```c
#if     defined(F3DEX_GBI_2)
/*
 * F3DEX_GBI_2: G_VTX GBI format was changed.
 *
 *        +--------+----+---+---+----+------+-+
 *  G_VTX |  cmd:8 |0000|  n:8  |0000|v0+n:7|0|
 *        +-+---+--+----+---+---+----+------+-+
 *        | |seg|         address             |
 *        +-+---+-----------------------------+
 */
# define        gSPVertex(pkt, v, n, v0)                                \
{                                                                       \
        Gfx *_g = (Gfx *)(pkt);                                         \
        _g->words.w0 =                                                  \
          _SHIFTL(G_VTX,24,8)|_SHIFTL((n),12,8)|_SHIFTL((v0)+(n),1,7);  \
        _g->words.w1 = (unsigned int)(v);                               \
}
# define        gsSPVertex(v, n, v0)                                    \
{                                                                       \
        (_SHIFTL(G_VTX,24,8)|_SHIFTL((n),12,8)|_SHIFTL((v0)+(n),1,7)),  \
        (unsigned int)(v)                                               \
}
#elif   (defined(F3DEX_GBI)||defined(F3DLP_GBI))
```
