using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeightTile : MonoBehaviour
{

    public Sprite Above;
    public Sprite Below;

    public void SetSprite(bool above)
    {
        this.GetComponentInChildren<SpriteRenderer>().sprite = above ? Above : Below;
    }

}
