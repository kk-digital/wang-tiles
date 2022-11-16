# wang-tiles

This repository is a generator based on wang-tile model in creating aperiodic and continuous representation of tile composition using only a few set of tiles that has different corner colors. 

### Usage
Compile using 
```dotnet build```

Run using the binary created from building

for windows
```./bin/Debug/net6.0/wang-tiles.exe```

for macos x
```./bin/Debug/net6.0/wang-tiles```

Sample usage
```./bin/Debug/net6.0/wang-tiles -- generate --placement-algo 2 --width 12 --height 12 --colors 5 --output-name "TestV2"```

```
Usage: wang-tiles generate [options]

Options:
  --placement-algo <placement-algo>  (int) The version of Placement Algo to run (1 or 2).
  --width <width>                    (int) The width of the board to be made.
  --height <height>                  (int) The height of the board to be made.
  --colors <colors>                  (int) The number of colors used to generate the tile set.
  --output-name <output-name>        (string) The filename of the resulting picture (default directory is ./data)
```

For more information, run
```./bin/Debug/net6.0/wang-tiles -- -h```
```./bin/Debug/net6.0/wang-tiles -- generate -h```