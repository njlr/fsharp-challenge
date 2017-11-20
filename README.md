# RealSmart F# Challenge

## Build Instructions

Fetch dependencies: 

```bash=
mono .paket/paket.bootstrapper.exe
mono .paket/paket.exe install
```

Build: 

```bash=
./build.sh
```

Run: 

```bash=
dotnet ./Conway/bin/Debug/netcoreapp2.0/Conway.dll 
```

## Dev Tools

The recommended tooling is [Visual Studio Code](https://code.visualstudio.com/) with the [Ionide](http://ionide.io/) extenstion. 
