******
Base
******

Accessor
########

The **Base** object can accessed only using *Player.Base()*

.. code-block:: lua
    :linenos:

    local me = player.base() # The Current player base
    local them = enemy.base # The Current enemy base

Methods
########

====================================================  ====================================================
Name                                                  usage
====================================================  ====================================================
**Robot.Pos : Vector**                                Returns a **Vector** containing the position of the **Base**.
                                                      Vector(x,y)
**Robot.Tile : Tile**                                 Returns the **Tile**  object the **Base** is on.
                                                      Equivalent to *Tiles.getTile(Base.Pos)*
**Base.Health : Float**                               Returns the current **Base** health.
====================================================  ====================================================
