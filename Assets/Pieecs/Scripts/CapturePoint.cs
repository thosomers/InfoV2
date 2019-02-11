using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CapturePoint : PlayerObject {

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

	protected override void Destroy()
	{
		this.Player.MyCapturePoints.Remove(this);
		var x = this.Xmin;
		var y = this.Ymin;
		var newP = this.Player == Player.Player1 ? Player.Player2 : Player.Player1;
		base.Destroy();

		newP.MyCapturePoints.Add(newCapturePoint(newP, x, y));




	}

	public static CapturePoint newCapturePoint(Player player, int x, int y)
	{
		GameObject CaptureGO = GameObject.Instantiate(Board.Instance.CapturePointPrefab, Board.Instance.transform);

		CapturePoint capture = CaptureGO.GetComponent<CapturePoint>();


		var P1 = player == Player.Player1;
		
		capture.Setup(P1,x,y);
		
		return capture;
		
	}


	public static CapturePoint newCapturePoint(Player player)
	{
		GameObject CaptureGO = GameObject.Instantiate(Board.Instance.CapturePointPrefab, Board.Instance.transform);

		CapturePoint capture = CaptureGO.GetComponent<CapturePoint>();


		var P1 = player == Player.Player1;

		
		
		capture.Setup(P1, !P1 ? 1 : Board.Instance.WIDTH - capture.Width - 1, P1 ? 1 : Board.Instance.HEIGHT - capture.Height - 1);


		return capture;
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
