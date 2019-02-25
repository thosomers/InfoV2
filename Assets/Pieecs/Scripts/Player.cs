using System.Collections;
using System.Collections.Generic;
using MoonSharp.Interpreter;
using UnityEngine;

public class Player
{
	public static readonly Player Player1 = new Player();
	public static readonly Player Player2 = new Player();
	
	protected Player()
	{
		
	}
	
	
	
	
	
	
	
	public HashSet<Robot> MyRobots = new HashSet<Robot>();
	public Base MyBase;
	public HashSet<CapturePoint> MyCapturePoints = new HashSet<CapturePoint>();

	public void Setup()
	{
		MyBase = Base.newBase(this);
		MyCapturePoints.Add(CapturePoint.newCapturePoint(this));
		MyRobots.Add(Robot.newRobot(this,RobotClass.Guard));


	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

[MoonSharpUserData]
public class PlayerProxy
{
	public Player Player;

	public PlayerProxy(Player player)
	{
		Player = player;
	}

	public Base Base()
	{
		return Player.MyBase;
	}

	public DynValue CapturePoints(Script script)
	{
		DynValue tab = DynValue.NewTable(script);

		foreach (var playerMyCapturePoint in Player.MyCapturePoints)
		{
			tab.Table.Append(DynValue.FromObject(script,playerMyCapturePoint));
		}

		return tab;
	}
	
	public DynValue Robots(Script script)
	{
		DynValue tab = DynValue.NewTable(script);

		foreach (var robot in Player.MyRobots)
		{
			tab.Table.Append(DynValue.FromObject(script,robot));
		}

		return tab;
	}
	
	
	
	
	
	
	
	
}


