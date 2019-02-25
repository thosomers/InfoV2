using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Robot : PlayerObject {

	public MeshRenderer Player1Mesh;
	public MeshRenderer Player2Mesh;

	private SpriteRenderer renderer;
	public int X { get; protected set; }
	public int Y { get; protected set; }

	
	private int stepsLeft = 0;
	private bool attackLeft = false;	
	
	public RobotClass RClass;



	public void Setup(bool P1, RobotClass mClass ,int x, int y)
	{
		Setup(P1 ? Player.Player1 : Player.Player2,mClass.MaxHealth);

		RClass = mClass;
		
		this.X = x;
		this.Y = y;
		
		var renderer = P1 ? Player1Mesh : Player2Mesh;
		renderer.enabled = true;

		this.transform.position = new Vector3(X,0,Y);


		foreach (var tile in Tiles())
		{
			tile.Object = this;
		}


	}

	public static Robot newRobot(Player player,RobotClass mClass)
	{
		GameObject RobotGO = GameObject.Instantiate(Board.Instance.RobotPrefab, Board.Instance.transform);

		Robot robot = RobotGO.GetComponent<Robot>();


		var p1 = player == Player.Player1;

		robot.Setup(p1,mClass, p1 ? 0 : Board.Instance.WIDTH-1, p1 ? 2 : Board.Instance.HEIGHT - 3);


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
		if (!attackLeft)
		{
			Debug.Log("No attack left.");
			return;
		}

		if (Mathf.Abs(dx) + Mathf.Abs(dy) > RClass.AttackRange+0.1)
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
		
		
		attackLeft = false;
		Debug.Log("Attacked.");
		stepsLeft = 0;
		Debug.Log("Steps left: " + stepsLeft);
		obj.RemoveHealth(RClass.AttackDamage);
		
	}






	public void ResetMove()
	{
		stepsLeft = RClass.MoveRange;
		attackLeft = true;
		Debug.Log("RESET");
		Debug.Log("Steps left: " + stepsLeft);
	}
	
	public void Move(int dx, int dy)
	{
		if (stepsLeft <= 0)
		{
			Debug.Log("No steps left.");
			return;
		}
		
		if (Mathf.Abs(dx) + Mathf.Abs(dy) > 1+0.1)
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
			tile.selectionBox.setColor(Color.red);
			
		}

		X = nx;
		Y = ny;
		
		foreach (var tile in Tiles())
		{
			tile.Object = this;
			tile.selectionBox.setColor(Color.blue);
			tile.selectionBox.setEnabled(!tile.selectionBox.getEnabled());
		}

		stepsLeft -= 1;
		
		Debug.Log("Steps left: " + stepsLeft);
		this.transform.position = new Vector3(X,0,Y);
	}

	protected override void Destroy()
	{
		this.Player.MyRobots.Remove(this);
		base.Destroy();
	}
}

public class RobotClass
{
	public float MaxHealth;
	public float AttackDamage;
	public int MoveRange;
	public int AttackRange;

	public static RobotClass Soldier = new RobotClass(40f,20f,3,1);
	public static RobotClass Guard = new RobotClass(70f,8f,4,1);
	public static RobotClass Archer = new RobotClass(30f,15f,2,2);
	
	
	
	
	private RobotClass(float Health, float Damage, int MRange, int ARange)
	{
		MaxHealth = Health;
		AttackDamage = Damage;
		MoveRange = MRange;
		AttackRange = ARange;
	}
}
