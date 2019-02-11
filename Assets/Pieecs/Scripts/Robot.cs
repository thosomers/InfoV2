using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Robot : PlayerObject {

	public Sprite P1Sprite;
	public Sprite P2Sprite;

	private SpriteRenderer renderer;
	
	
	
	
	
	public int X { get; protected set; }
	public int Y { get; protected set; }

	public int MoveSteps = 1;
	public int AttackSteps = 1;

	public int AttackStrength = 20;
	public int StartHeath = 100;



	public void Setup(bool P1 ,int x, int y)
	{
		Setup(P1 ? Player.Player1 : Player.Player2,StartHeath);
		
		this.X = x;
		this.Y = y;
		renderer = this.GetComponent<SpriteRenderer>();
		renderer.sprite = P1 ? P1Sprite : P2Sprite;

		this.transform.position = new Vector3(X,Y,-1);


		foreach (var tile in Tiles())
		{
			tile.Object = this;
		}


	}

	public static Robot newRobot(Player player)
	{
		GameObject RobotGO = GameObject.Instantiate(Board.Instance.RobotPrefab, Board.Instance.transform);

		Robot robot = RobotGO.GetComponent<Robot>();


		var P1 = player == Player.Player1;

		robot.Setup(P1, P1 ? 0 : Board.Instance.WIDTH-1, P1 ? 2 : Board.Instance.HEIGHT - 3);


		return robot;
	}

	public override HashSet<Tile> Tiles()
	{
		var tiles = new HashSet<Tile>();

		tiles.Add(Board.Instance.Tiles[X, Y]);

		return tiles;
	}

	public void Attack(int dx, int dy)
	{
		if (Mathf.Abs(dx) + Mathf.Abs(dy) > AttackSteps+0.1)
		{
			Debug.Log("Too far");
			return;
		}
		
		
		var nx = X + dx;
		var ny = Y + dy;

		
		var obj = Board.getObject(nx, ny);
		if (Board.getObject(nx, ny) == null)
		{
			Debug.Log("Nothing to attack");
			return;
		}

		
		if (obj.Player == this.Player)
		{
			Debug.Log("No Self-Harm please");
			return;
		}
		
		obj.RemoveHealth(AttackStrength);
		
		
		
		
		
		
	}
	
	
	
	
	
	
	
	public void Move(int dx, int dy)
	{
		if (Mathf.Abs(dx) + Mathf.Abs(dy) > MoveSteps+0.1)
		{
			Debug.Log("Too far");
			return;
		}

		var nx = X + dx;
		var ny = Y + dy;

		if (!Board.exists(nx, ny) || Board.getObject(nx, ny) != null)
		{
			Debug.Log("Too full :(");
			return;
		}

		foreach (var tile in Tiles())
		{
			tile.Object = null;
		}

		X = nx;
		Y = ny;
		
		foreach (var tile in Tiles())
		{
			tile.Object = this;
		}
		
		this.transform.position = new Vector3(X,Y,-1);
	}

	protected override void Destroy()
	{
		this.Player.MyRobots.Remove(this);
		base.Destroy();
	}
}
