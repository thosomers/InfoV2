using System;
using System.Collections;
using System.Collections.Generic;
using MoonSharp.Interpreter;
using UnityEngine;

public class Base : PlayerObject
{

	public MeshRenderer Player1Mesh;
	public MeshRenderer Player2Mesh;
	
	
	
	
	public int Xmin { get; protected set; }
	public int Ymin { get; protected set; }
	public int Width;
	public int Height;
	public int StartHeath = 100;



	public void Setup(bool P1 ,int x, int y)
	{
		Setup(P1 ? Player.Player1 : Player.Player2,StartHeath);
		
		this.Xmin = x;
		this.Ymin = y;
		var renderer = P1 ? Player1Mesh : Player2Mesh;
		renderer.enabled = true;

		var posx = Xmin + (Width-1) / 2f;
		var posy = Ymin + (Height-1) / 2f;

		this.transform.position = new Vector3(posx,0,posy);


		foreach (var tile in Tiles())
		{
			tile.Object = this;
		}
		


	}
	
	
	
	
	
	
	
	

	public static Base newBase(Player player)
	{
		Debug.Log("New Base");
		GameObject BaseGO = GameObject.Instantiate(Board.Instance.BasePrefab, Board.Instance.transform);

		Base Base = BaseGO.GetComponent<Base>();


		var P1 = player == Player.Player1;

		Base.Setup(P1, P1 ? 0 : Board.Instance.WIDTH - Base.Width, P1 ? 0 : Board.Instance.HEIGHT - Base.Height);


		return Base;
	}

	public override HashSet<Tile> Tiles()
	{
		var tiles = new HashSet<Tile>();
		
		for (int x = 0; x < Width; x++)
		{
			for (int y = 0; y < Height; y++)
			{
				tiles.Add(Board.Instance.Tiles[Xmin + x, Ymin + y]);
			}
		}

		return tiles;
	}
}


[MoonSharpUserData]
public class BaseProxy
{
	public Base Base;

	public BaseProxy(Base @base)
	{
		Base = @base;
	}
}

