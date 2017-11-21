# RealSmart F# Challenge

## Build Instructions

The code has been tested on macOS. You will need F#, .NET Core _and_ Mono installed. 

### Fetch Dependencies

```bash=
mono .paket/paket.bootstrapper.exe
mono .paket/paket.exe install
```

### Build

```bash=
./build.sh
```

### Run

#### CLI 

```bash=
dotnet ./Conway/bin/Debug/netcoreapp2.0/Conway.dll 
```

#### Server

```bash=
dotnet ./ConwayServer/bin/Debug/netcoreapp2.0/ConwayServer.dll 
```

The API is described in [`ConwayServer/API.md`](/ConwayServer/API.md). 

## Dev Tools

The recommended tooling is [Visual Studio Code](https://code.visualstudio.com/) with the [Ionide](http://ionide.io/) extenstion. 
