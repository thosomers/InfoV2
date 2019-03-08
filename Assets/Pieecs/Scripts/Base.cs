using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace.Pieecs.Scripts;
using MoonSharp.Interpreter;
using Pieecs.Scripts.Utils;
using UnityEngine;

public class Base : PlayerObject
{

	public MeshRenderer Player1Mesh;
	public MeshRenderer Player2Mesh;
	
	
	
	
	public int X { get; protected set; }
	public int Y { get; protected set; }
	public int StartHeath = 100;



	public void Setup(bool P1 ,int x, int y)
	{
		Setup(P1 ? Player.Player1 : Player.Player2,StartHeath);
		
		this.X = x;
		this.Y = y;
		var renderer = P1 ? Player1Mesh : Player2Mesh;
		renderer.enabled = true;

		var posx = X;
		var posy = Y;

		this.transform.position = new Vector3(posx,0,posy);


		Tile().Object = this;
		


	}
	
	
	
	
	
	
	
	

	public static Base newBase(Player player)
	{
		Debug.Log("New Base");
		GameObject BaseGO = GameObject.Instantiate(Board.Instance.BasePrefab, Board.Instance.transform);

		Base Base = BaseGO.GetComponent<Base>();


		var P1 = player == Player.Player1;

		Base.Setup(P1, P1 ? 1 : Board.Instance.SIZE - 2, P1 ? 1 : Board.Instance.SIZE - 2);


		return Base;
	}

	public override Tile Tile()
	{

		return Board.getTile(X,Y);
	}


	protected override void Destroy()
	{
		base.Destroy();

		Game.End(Player == Player.Player1 ? Player.Player2 : Player.Player1);
	}
}
[MoonSharpUserData]
public class BaseProxy : PlayerObjectProxy
{
	public Base Base;

	public BaseProxy(Base Base) : base(Base)
	{
		this.Base = Base;
	}
	
	public VectorProxy Pos {
		get { return new VectorProxy(Base.X,Base.Y);}
	}
	
	public Tile Tile
	{
		get { return Base.Tile(); }
	}

	public int Range = 0;

}

