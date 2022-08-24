            wang-tiles

#### usage

*wang_tiles help

#### list tilesets

*wang_tiles list tilesets 

#### list boards

*wang_tiles list boards 

#### list  scenes

*wang_tiles list scenes 

#### list pixelassignments

*wang_tiles list pixelassignments


#### create new tileset

*wang_tiles new tileset <tilesize> <vertical_color_count> <horizontal_color_count> <variant_count> <tilesPerRow> ///// <tilesize> ---> 8x8 16x16 32x32
new tileset 16x16 2 2 1 6

#### example

*wang_tiles new tileset 8x8 4 4 2

#### Board Generation

*wang_tiles Board Generate <Board Type> <sizeX> <<sizeY>>  ///// <Board Type> ---> Radial flat FloatingIsland

### random scene
test-scene-output-random -ts <tileset_1> -ts <tileset_2> -width <sizeX> -height <sizeY> -out <outfilename>
example: test-scene-output-random -ts tileset_7135374712056073458.json -out abc
example: test-scene-output-random -ts tileset_7135374712056073458.json -width 4 -height 4 -out abc
