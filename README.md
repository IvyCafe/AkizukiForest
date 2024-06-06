# Akizuki Forest

Akizuki and the forest.

## Summary
This game was created as the first game of Ivy Cafeteria. This game support only Japanese.

## How to use
### Windows
#### On your file viewer
1. Open `.exe` file.

#### On your terminal
1. Move to the directory.
1. Input this command: `./AkizukiForest.exe`

### MacOS
Not Yet Available

### Linux
1. Open your terminal.
1. Move to the directory.
1. Add permission to `AkizukiForest` file  
e.g. `sudo chmod a+x ./AkizukiForest`
1. Input this command: `./AkizukiForest`

> [!WARNING]
> Your terminal must support Japanese output.

## Dev
Build command for linux: 
```shell
dotnet publish -r linux-x64 -c Release --self-contained true /p:PublishTrimmde=true /p:Publish
```
