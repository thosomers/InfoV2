******
Robot
******

Accessor
########

The **Robot** object can only be obtained via *Player.Robots()*

.. code-block:: lua
    :linenos:

    local robots = player.Robots() #b Get a list of my Robots

    for index,robot in pairs(robots) do # Loops for every index and respective robot in robots
      # Do Something
    end


Methods
########

====================================================  ====================================================
Name                                                  usage
====================================================  ====================================================
**Robot.Pos : Vector**                                Returns a **Vector** containing the position of the **Robot**.
                                                      Vector(x,y)
**Robot.Tile : Tile**                                 Returns the **Tile**  object the **Robot** is on.
                                                      Equivalent to *Tiles.getTile(Robot.Pos)*
**Robot.Health : Float**                              Returns the current **Robot** health.
**Robot.Steps : Int**                                 Returns the number of steps this **Robot** can take in this turn.
**Robot.Range : Int**                                 Returns the number of Tiles this **Robot** can attack this turn.
**Robot.Player : Player**                             Returns the Player that owns this **Robot**.
                                                      This means: *Robot.Player.Robots()* contains **Robot**
**Robot.Alive : Boolean**                             Returns **True** is this robot is still alive.
                                                      Returns **False** otherwise
**Robot.Move(Vector delta)**                          Tries to move the **Robot** according to delta
                                                      In essence: *Robot.Pos += delta*.
                                                      Only possible if delta.length is 1, the tile is accessible (So has no object and is land), and Robot.steps > 0
**Robot.Attack(Vector delta)**                        Tries to attack the Object on tile with position: *Robot.pos + delta*
                                                      Possible only if robot.range <= delta.length and the tile contains an enemy target.
====================================================  ====================================================
