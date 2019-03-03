using System;
using System.Collections;
using System.Collections.Generic;
using System.Resources;
using MoonSharp.Interpreter;
using Pieecs.Scripts.Utils;
using UnityEngine;

public class Player
{
	public static readonly Player Player1 = new Player();
	public static readonly Player Player2 = new Player();

	public static Player Active { get; set; }

	public static void DoTurn()
	{
		Player1.Turn();

		Player2.Turn();
	}

	protected Player()
	{

	}

	public HashSet<Robot> MyRobots = new HashSet<Robot>();
	public Base MyBase;
	public HashSet<CapturePoint> MyCapturePoints = new HashSet<CapturePoint>();

	private Script playerScript;

	public Script Script
	{
		get { return playerScript; }
	}

	public void Setup()
	{
		MyBase = Base.newBase(this);
		//MyCapturePoints.Add(CapturePoint.newCapturePoint(this));
		MyRobots.Add(Robot.newRobot(this, RobotClass.Guard));

		playerScript = LuaHandler.NewScript();
		playerScript.Globals["player"] = this;

		playerScript.Globals["enemy"] = this == Player1 ? Player2 : Player1;
	}



	private string ScriptText;
	private DynValue ScriptFunction;

	public void setText(string text)
	{
		ScriptText = text;
		ScriptFunction = Script.LoadString(text);
	}
	
	
	private void Turn()
	{
		Player.Active = this;
		
		foreach (var myRobot in MyRobots)
		{
			myRobot.ResetMove();
		}
		
		ScriptFunction.Function.Call();

		foreach (var myRobot in MyRobots)
		{
			myRobot.ResetMove();
		}
		
		Player.Active = null;

	}

	private Action<object> printAction;

	public void Execute(string text)
	{
		Player.Active = this;
		try
		{
			playerScript.DoString(text);
		}
		catch (InterpreterException e)
		{
			playerScript.LoadString("print(...)").Function.Call(e.DecoratedMessage);
		}
		catch (IndexOutOfRangeException e)
		{
			playerScript.LoadString("print(...)").Function.Call(e.Message);
		}
		
		Player.Active = null;
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


