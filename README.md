# Metagen-cs

Unity `.meta` file generator written in C#.

Doesn't try to mimick Unity's GUID generator in favor of stable GUIDs.
Depends on xxHash for hashing.

## Build Instructions

See `.github/workflows/release.yml` for most cases. The ones built by this GitHub Action are:

```bash
dotnet publish -c Release -f net6.0 -r win10-x64 --self-contained
```

```bash
dotnet publish -c Release -f net6.0 -r osx.11.0-x64 --self-contained
```

```bash
dotnet publish -c Release -f net6.0 -r linux-x64 --self-contained
```

```bash
dotnet publish -c Release -f net6.0 -r linux-musl-x64 --self-contained
```

Please refer to [the RID catalog](https://docs.microsoft.com/en-us/dotnet/core/rid-catalog) for other runtimes.

The project supports `net6.0` and `netcoreapp3.1` as target frameworks.
This ought to be sufficient, but in case you need another one, feel free to add it (locally).
