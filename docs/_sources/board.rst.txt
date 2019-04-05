*****
Board
*****

Accessor
########

The **Board** object is accesssable from lua code using the **Tiles** name. The **Tiles** object is in the global environment.

.. code-block:: lua
    :linenos:

    local board = _G.Tiles # saves this object to the variable board

    local board = Tiles # Does the exact same thing

Methods
########

====================================================  ====================================================
Name                                                  usage
====================================================  ====================================================
**Tiles.getTile(Vector vector) : Tile**               Returns the **Tile** at position (vector.x,vector.y)
                                                      Returns nil if vector does not fit a tile
**Tiles[Vector vec] : Tile**                          A shorthand for **Tiles.getTile**
**Tiles.enabled : List<Tile>**                        Returns a list of all highlighted **Tile** objects
**Tiles.pathBetween[Tile t1,Tile t2] : List<Tile>**   Returns a list of **Tile** objects matching the shortest path between t1 and t2.
                                                      Returns an empty list if no path is found
**Tiles.withRange(Tile t1, Int range)**               Returns a list of all accessible tiles around Tile at a distance of at most range.
====================================================  ====================================================
