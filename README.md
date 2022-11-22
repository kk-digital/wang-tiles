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

```./bin/Debug/net6.0/wang-tiles -- schonings-algo --version 1 --width 3 --height 3 --colors 3 --output-name "TestSchonings_V1```

Usage for ```wang-tiles```
```
Description:
  Wang Tile Generator

Usage:
  wang-tiles [command] [options]

Options:
  --version       Show version information
  -?, -h, --help  Show help and usage information

Commands:
  placement-algo
  schonings-algo
```


Usage for ```wang-tiles placement-algo```
```
Usage:
  wang-tiles placement-algo [options]

Options:
  --version <version>          (int) The version of Placement Algo to run (1 or 2).
  --width <width>              (int) The width of the board to be made.
  --height <height>            (int) The height of the board to be made.
  --colors <colors>            (int) The number of colors used to generate the tile set.
  --output-name <output-name>  (string) The filename of the resulting picture (default directory is ./data).
  -?, -h, --help               Show help and usage information
```


Usage for ```wang-tiles schonings-algo```
```
Usage:
  wang-tiles schonings-algo [options]

Options:
  --version <version>          (int) The version of Schonings Algo to run (1).
  --width <width>              (int) The width of the board to be made.
  --height <height>            (int) The height of the board to be made.
  --colors <colors>            (int) The number of colors used to generate the tile set.
  --output-name <output-name>  (string) The filename of the resulting picture (default directory is ./data).
  -?, -h, --help               Show help and usage information
```


Usage for ```wang-tiles weighted-probability```
```
Usage:
  wang-tiles weighted-probability [options]

Options:
  --version <version>          (int) The version of Weighted Probability Algo to run (1).
  --width <width>              (int) The width of the board to be made.
  --height <height>            (int) The height of the board to be made.
  --colors <colors>            (int) The number of colors used to generate the tile set.
  --output-name <output-name>  (string) The filename of the resulting picture (default directory is ./data).
  -?, -h, --help               Show help and usage information
```


Usage for ```wang-tiles test-algo```
```
Usage:
  wang-tiles test-algo [options]

Options:
  --version <version>                                  (int) The version of Test Algo to run (1).
  --width <width>                                      (int) The width of the board to be made.
  --height <height>                                    (int) The height of the board to be made.
  --colors <colors>                                    (int) The number of colors used to generate the tile set.
  --output-name <output-name>                          (string) The filename of the resulting picture (default directory is ./data).
  --tile-selection-rule <tile-selection-rule>          (int) The tile selection rule to use.
  --energy-calculation-mode <energy-calculation-mode>  (int) The mode for energy calculation.
  --skip-unassigned-tile-without-adjacent              (int) Set true to skip unassigned tile that doesn't have any adjacent tiles.
  --select-lowest-energy                               (int) Set true if we want to select the tile with lowest energy.
  -?, -h, --help                                       Show help and usage information
```

For more information, run

```./bin/Debug/net6.0/wang-tiles -- -h```

```./bin/Debug/net6.0/wang-tiles placement-algo -- -h```

```./bin/Debug/net6.0/wang-tiles schonings-algo -- -h```