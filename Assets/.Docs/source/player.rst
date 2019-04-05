******
Player
******

Accessor
########

The **Player** object can accessed as either **player** or **enemy** in the global environment.
These  are obviously the player and enemy player objects respectively.

.. code-block:: lua
    :linenos:

    local me = player # The Current player
    local them = enemy # The Current enemy

Methods
########

====================================================  ====================================================
Name                                                  usage
====================================================  ====================================================
**Player.Base() : Base**                              Returns the **Player** object's Base.
**Player.Robots() : List<Robot>**                     Returns the **Player** object's Robots as a List.
**Player.Turn : Int**                                 The current Turn count.
====================================================  ====================================================
