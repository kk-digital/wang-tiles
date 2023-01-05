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

```./bin/Debug/net6.0/wang-tiles -- schonings-algo --version 1 --width 3 --height 3 --colors 3 --output-name "TestSchonings_V1"```

#### wang-tiles 
---
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
  weighted-probability
  test-algo
```

#### wang-tiles placement-algo
---
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

#### wang-tiles schonings-algo
---
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

#### wang-tiles weighted-probability
---
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

#### wang-tiles test-algo
---
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

#### wang-tiles tetris
---
Usage for ```wang-tiles tetris```
```
Usage:
  wang-tiles tetris [options]

Options:
  --version <version>                (int) The version of Tetris Algo to run (1,2,3,4).
  --width <width>                    (int) The width of the board to be made.
  --height <height>                  (int) The height of the board to be made.
  --output-name <output-name>        (string) The filename of the resulting picture (default directory is ./data).
  --color-matching <color-matching>  (int) The color matching option to be used(0 - CurrentBitmasking, 1 - SymmetricalMatching).
  --iterations <iterations>          (int) The number of iterations to do for the algo.
  --temperature <temperature>        (float) The initial temperature to be used.
  --lIteration <lIteration>          (int) For updating temperature every Lth iteration.
  --alpha <alpha>                    (float) The alpha value for updating the temperature.
  -?, -h, --help                     Show help and usage information
```
Sample Command
```
./bin/Debug/net6.0/wang-tiles -- tetris --version 1 --width 16 --height 16 --output-name "Tetris_V1_16x16" --color-matching 1
./bin/Debug/net6.0/wang-tiles -- tetris --version 3 --width 16 --height 16 --output-name "Tetris_V3_16x16" --color-matching 1 --iterations 500

./bin/Debug/net6.0/wang-tiles -- tetris --version 4 --width 16 --height 16 --output-name "Tetris_V4_16x16" --color-matching 1 --iterations 50000 --temperature 1000 --lIteration 50 --alpha 0.01
```

#### wang-tiles hemingway
---
Usage for ```wang-tiles hemingway```
```
Usage:
  wang-tiles hemingway [options]

Options:
  --version <version>                (int) The version of Tiled Algo to run (1).
  --width <width>                    (int) The width of the board to be made.
  --height <height>                  (int) The height of the board to be made.
  --output-name <output-name>        (string) The filename of the resulting picture (default directory is ./data).
  --color-matching <color-matching>  (int) The color matching option to be used(0 - CurrentBitmasking, 1 - SymmetricalMatching).
  --iterations <iterations>          (int) The number of iterations to do for the algo.
  --temperature <temperature>        (float) The initial temperature to be used.
  --lIteration <lIteration>          (int) For updating temperature every Lth iteration.
  --alpha <alpha>                    (float) The alpha value for updating the temperature.
  -?, -h, --help                     Show help and usage information
```
Sample Command
```
./bin/Debug/net6.0/wang-tiles -- hemingway --version 1 --width 8 --height 8 --output-name "Hemingway_V1_8x8" --color-matching 1 --iterations 50000 --temperature 1000 --lIteration 50 --alpha 0.01 
```

#### wang-tiles tiled
---
Usage for ```wang-tiles tiled```
```
Usage:
  wang-tiles tiled [options]

Options:
  --version <version>                (int) The version of Tiled Algo to run (1).
  --width <width>                    (int) The width of the board to be made.
  --height <height>                  (int) The height of the board to be made.
  --output-name <output-name>        (string) The filename of the resulting picture (default directory is ./data).
  --color-matching <color-matching>  (int) The color matching option to be used(0 - CurrentBitmasking, 1 - SymmetricalMatching).
  --iterations <iterations>          (int) The number of iterations to do for the algo.
  --temperature <temperature>        (float) The initial temperature to be used.
  --lIteration <lIteration>          (int) For updating temperature every Lth iteration.
  --alpha <alpha>                    (float) The alpha value for updating the temperature.
  -?, -h, --help                     Show help and usage information
```
Sample Command
```
./bin/Debug/net6.0/wang-tiles -- tiled --version 1 --width 16 --height 8 --output-name "Tiled_V1_8x16" --color-matching 1 --iterations 50000 --temperature 1000 --lIteration 50 --alpha 0.01
```
---
For more information, run

```./bin/Debug/net6.0/wang-tiles -- -h```

```./bin/Debug/net6.0/wang-tiles -- placement-algo -h```

```./bin/Debug/net6.0/wang-tiles -- schonings-algo -h```

```./bin/Debug/net6.0/wang-tiles -- weighted-probability -h```

```./bin/Debug/net6.0/wang-tiles -- test-algo -h```

```./bin/Debug/net6.0/wang-tiles -- tetris -h```