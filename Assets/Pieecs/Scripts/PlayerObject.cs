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
        Tile().Object = null;
		
        GameObject.Destroy(this.gameObject);
    }
    


    public float Health { get; protected set; }



    public abstract Tile Tile();

    public Player Player { get; protected set; }




}
