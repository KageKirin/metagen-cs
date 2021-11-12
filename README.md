# Metagen-cs

Unity `.meta` file generator written in C#.

Doesn't try to mimick Unity's GUID generator in favor of stable GUIDs.
Depends on xxHash for hashing.

## Installation

Metagen-cs is not on any NuGet registry, but you can install it from github:

### Using paket

This requires installing [paket](https://fsprojects.github.io/Paket/index.html) first.

```bash
dotnet tool install -g paket
```

And then setup a local `paket.dependencies` referring metagen

```bash
echo 'git https://github.com/kagekirin/metagen-cs.git main build: "dotnet publish -f net5.0 -r linux-x64 -c Release --self-contained true"' > paket.dependencies
```

See the build instructions below to change target framework and architecture.

Build the tool and the install it locally:

```bash
paket update
dotnet tool install -g --add-source paket-files/github.com/kagekirin/metagen-cs metagen
```

### After downloading this repo

Follow the build instructions below, and then install the tool with

```bash
dotnet tool install -g --add-source . metagen
```

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
