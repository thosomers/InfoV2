using System;
using System.Collections;
using System.Collections.Generic;
using System.Resources;
using MoonSharp.Interpreter;
using Pieecs.Scripts.Utils;
using UnityEngine;
using Coroutine = MoonSharp.Interpreter.Coroutine;

public class Player
{
	public static Player Player1 { get; private set; }
	public static Player Player2 { get; private set; }

	public static Player Active { get; set; }


	public static bool IsRunning { get; private set; }

	public static void DoTurn()
	{
		if (IsRunning) return;

		Board.Instance.StartCoroutine(DoTurnEnumerable());
	}

	
	
	private static IEnumerator DoTurnEnumerable()
	{
		IsRunning = true;
		
		bool p1Running = true;

		bool p2Running = true;

		Board.ClearTiles();
		
		for(;;) 
		{
			if (p1Running)
			{
				p1Running = Player1.Turn();
				yield return null;
			} else if (p2Running)
			{
				p2Running = Player2.Turn();
				yield return null;
			}
			else
			{
				break;
			}
		}

		IsRunning = false;
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


	public static void Setup()
	{
		Player1 = new Player();
		Player2 = new Player();

		IsRunning = false;
		Active = null;
		
		
		Player1.SetupInstance();
		Player2.SetupInstance();
	}
	

	public void SetupInstance()
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


	private DynValue routine;

	private void StartTurn()
	{
		Player.Active = this;
		
		foreach (var myRobot in MyRobots)
		{
			myRobot.ResetMove();
		}

		routine = playerScript.CreateCoroutine(ScriptFunction);

		routine.Coroutine.AutoYieldCounter = 10000;
	}

	private bool ResumeTurn()
	{
		try
		{
			var ret = routine.Coroutine.Resume();
			
			return ret.Type == DataType.YieldRequest;
		}
		catch (InterpreterException e)
		{
			playerScript.LoadString("print(...)").Function.Call(e.DecoratedMessage);
			return false;
		}
	}

	private void EndTurn()
	{
		foreach (var myRobot in MyRobots)
		{
			myRobot.ResetMove();
		}
		
		Player.Active = null;
		routine = null;
	}
	
	
	private bool Turn()
	{
		if (routine == null)
		{
			StartTurn();
		}

		if (ResumeTurn())
		{
			return true;
		}
		else
		{
			EndTurn();
			return false;
		}
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


