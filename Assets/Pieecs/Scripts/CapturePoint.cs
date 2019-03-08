using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CapturePoint : PlayerObject {

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

		this.Tile().Object = this;


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

		
		
		capture.Setup(P1, !P1 ? 1 : Board.Instance.SIZE - capture.Width - 1, P1 ? 1 : Board.Instance.SIZE - capture.Height - 1);


		return capture;
	}

	public override Tile Tile()
	{
		return Board.Instance.Tiles[Xmin, Ymin];
	}
}
