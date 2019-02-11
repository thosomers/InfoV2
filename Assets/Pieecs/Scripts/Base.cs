using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Base : PlayerObject
{

	public Sprite P1Sprite;
	public Sprite P2Sprite;

	private SpriteRenderer renderer;
	
	
	
	
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
		renderer = this.GetComponent<SpriteRenderer>();
		renderer.sprite = P1 ? P1Sprite : P2Sprite;

		var posx = Xmin + (Width-1) / 2f;
		var posy = Ymin + (Height-1) / 2f;

		this.transform.position = new Vector3(posx,posy,-1);


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
