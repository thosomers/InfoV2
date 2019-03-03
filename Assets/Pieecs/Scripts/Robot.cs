using System;
using System.Collections;
using System.Collections.Generic;
using MoonSharp.Interpreter;
using Pieecs.Scripts.Utils;
using UnityEngine;

public class Robot : PlayerObject {

	public MeshRenderer Player1Mesh;
	public MeshRenderer Player2Mesh;

	private SpriteRenderer renderer;
	public int X { get; protected set; }
	public int Y { get; protected set; }


	public int stepsLeft { get; private set; }

	public bool attackLeft { get; private set; }
	
	public RobotClass RClass;

	public void Rotate(int x, int y)
	{
		if (Math.Abs(x) > Math.Abs(y))
		{
			this.GetComponentInChildren<Transform>().eulerAngles = new Vector3(0,Math.Sign(-x)*90,0);
		}
		else
		{
			this.GetComponentInChildren<Transform>().eulerAngles = new Vector3(0,(Math.Sign(y)+1)*90,0);
		}
		
		
		
		
		
	}
	



	public void Setup(bool P1, RobotClass mClass ,int x, int y)
	{
		Setup(P1 ? Player.Player1 : Player.Player2,mClass.MaxHealth);

		RClass = mClass;
		
		this.X = x;
		this.Y = y;
		
		var renderer = P1 ? Player1Mesh : Player2Mesh;
		renderer.enabled = true;

		this.transform.position = new Vector3(X,0,Y);

		Tile().Object = this;


	}

	public static Robot newRobot(Player player,RobotClass mClass)
	{
		GameObject RobotGO = GameObject.Instantiate(Board.Instance.RobotPrefab, Board.Instance.transform);

		Robot robot = RobotGO.GetComponent<Robot>();


		var p1 = player == Player.Player1;

		robot.Setup(p1,mClass, p1 ? 0 : Board.Instance.WIDTH-1, p1 ? 2 : Board.Instance.HEIGHT - 3);


		return robot;
	}

	public override Tile Tile()
	{

		return Board.getTile(X,Y);
	}

	public bool Attack(int dx, int dy)
	{
		if (!attackLeft)
		{
			Debug.Log("No attack left.");
			return false;
		}

		if (Mathf.Abs(dx) + Mathf.Abs(dy) > RClass.AttackRange+0.1)
		{
			Debug.Log("Too far");
			return false;
		}
		
		var nx = X + dx;
		var ny = Y + dy;

		
		var obj = Board.getObject(nx, ny);
		if (Board.getObject(nx, ny) == null)
		{
			Debug.Log("Nothing to attack");
			return false;
		}

		
		if (obj.Player == this.Player)
		{
			Debug.Log("No Self-Harm please");
			return false;
		}


		Rotate(dx, dy);
		
		attackLeft = false;
		Debug.Log("Attacked.");
		stepsLeft = 0;
		Debug.Log("Steps left: " + stepsLeft);
		obj.RemoveHealth(RClass.AttackDamage);
		return true;
	}






	public void ResetMove()
	{
		stepsLeft = RClass.MoveRange;
		attackLeft = true;
		Debug.Log("RESET");
		Debug.Log("Steps left: " + stepsLeft);
	}
	
	public bool Move(int dx, int dy)
	{
		if (stepsLeft <= 0)
		{
			Debug.Log("No steps left.");
			return false;
		}
		
		if (Mathf.Abs(dx) + Mathf.Abs(dy) > 1+0.1)
		{
			Debug.Log("Too far");
			return false;
		}

		var nx = X + dx;
		var ny = Y + dy;

		if (!Board.exists(nx, ny) || Board.getObject(nx, ny) != null)
		{
			Debug.Log("Too full :(");
			return false;
		}

		Rotate(dx, dy);
		
		Tile().Object = null;
		//tile.selectionBox.setColor(new BetterColor(Color.red));
			

		X = nx;
		Y = ny;
		
		Tile().Object = this;

		stepsLeft -= 1;
		
		Debug.Log("Steps left: " + stepsLeft);
		this.transform.position = new Vector3(X,0,Y);
		return true;
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

[MoonSharpUserData]
public class RobotProxy : PlayerObjectProxy
{
	public Robot Robot;

	public RobotProxy(Robot robot) : base(robot)
	{
		Robot = robot;
	}


	public VectorProxy Pos {
		get { return new VectorProxy(Robot.X,Robot.Y);}
	}

	public int Steps
	{
		get { return Robot.stepsLeft; }
	}

	public int Attacks
	{
		get
		{
			return Robot.attackLeft ?  Robot.RClass.AttackRange : 0;
		}
	}

	public Player Player {
		get { return Robot.Player;}
	}


	public bool Attack(VectorProxy vec)
	{
		if (!ActionAllowed()) return false;
		
		return Robot.Attack((int) vec.X, (int) vec.Y);
	}

	public int Move(VectorProxy vec)
	{
		if (!ActionAllowed()) return -1;
		
		var ret = Robot.Move((int) vec.X,(int) vec.Y);
		
		return ret ? Robot.stepsLeft : -1;
	}





}


