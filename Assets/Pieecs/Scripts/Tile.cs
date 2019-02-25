using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoonSharp.Interpreter;
using Pieecs.Scripts.Utils;

public class Tile : MonoBehaviour
{

	public Selection selectionBox;


	public bool IsEmpty
	{
		get { return Object == null; }
	}
	public PlayerObject Object;
	
	



	public int X
	{
		get { return (int) Position.x; }
	}
	public int Y
	{
		get { return (int) Position.y; }
	}

	public Vector2 Position { get; private set; }



	public void Setup(int x, int y)
	{
		this.Position = new Vector2(x,y);
		this.transform.localPosition = new Vector3(x,0,y);
		this.selectionBox = this.GetComponentInChildren<Selection>();
		
	}


	public void Start()
	{
		
	}
}


[MoonSharpUserData]
public class TileProxy
{

	public Tile tile;

	public TileProxy(Tile tile)
	{
		this.tile = tile;
	}
	
	
	//Position of this tile

	public int X
	{
		get { return tile.X; }
	}

	public int Y
	{
		get { return tile.Y; }
	}

	public PlayerObject Object
	{
		get
		{
			return tile.Object;
		}
	}

	public bool highlight
	{
		get { return tile.selectionBox.enabled; }
		set { tile.selectionBox.setEnabled(value); }

	}

	public BetterColor color
	{
		get { return tile.selectionBox.getColor(); }
		set { tile.selectionBox.setColor(value);}
	}
}