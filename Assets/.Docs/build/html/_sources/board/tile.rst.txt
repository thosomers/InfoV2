*****
Tile
*****

Accessor
########

The **Tile** object is only accessible through functions. See **Board**

.. code-block:: lua
    :linenos:

    local tile = Tiles.getTile(Vector(1,1)) # Gets the tile at position (1,1)

Methods
########

====================================================  ====================================================
Name                                                  usage
====================================================  ====================================================
**Tile.x : Int**                                      Returns the **Tile** object X coordinate
**Tile.y : Int**                                      Returns the **Tile** object Y coordinate
**Tile.pos : Vector**                                 Returns the **Vector**: (**Tile.X**,**Tile.y**)
**Tile.Object : Object**                              Returns the **Object** currently on this tile. For example a **Base** objects
**Tile.highlight = Boolean**                          A **Boolean** checking if this tile is highlighted. Can be set to enable/disable the highlighting of a **Tile**.
**Tile.color = Color**                                A **Color** object representing the **Tile** highlight color. Can be set to change the color.
====================================================  ====================================================
