using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerObject : MonoBehaviour
{


    public void Setup(Player player, float Health)
    {
        this.Player = player;
        this.Health = Health;
    }

    public void RemoveHealth(float damage)
    {
        this.Health -= damage;
        if (this.Health <= 0)
        {
            this.Destroy();
        }
    }

    protected virtual void Destroy()
    {
        foreach (var tile in Tiles())
        {
            tile.Object = null;
        }
		
        GameObject.Destroy(this.gameObject);
    }
    


    public float Health { get; protected set; }



    public abstract HashSet<Tile> Tiles();

    public Player Player { get; protected set; }




}
