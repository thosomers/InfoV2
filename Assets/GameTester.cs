using System;
using System.Collections;
using System.Collections.Generic;
using MoonSharp.Interpreter;
using Pieecs.Scripts.Utils;
using UnityEngine;
using UnityEngine.UI;

public class GameTester : MonoBehaviour
{
	private Script mscript;


	public ConsoleController console;

	private bool isSet = false;
	public Robot robot;
	
	
	
	public InputField PlayerEditorText;
	
	
	
	// Use this for initialization
	void Start ()
	{
		LuaHandler.RegisterProxies();
		mscript = LuaHandler.NewScript();

	}

	public void SetRobot(Robot robot)
	{
		mscript.Globals["robot"] = robot;
	}
	
	
	// Update is called once per frame
	void Update ()
	{
		if (!isSet)
		{
			foreach (var mrobot in Player.Player1.MyRobots)
			{
				this.robot = mrobot;
				SetRobot(robot);
				isSet = true;
				break;
			}
		}
	}


	void showMovement()
	{

	}
	
	
	


	private void OnGUI()
	{
		/*
		Event ev = Event.current;
		
		
		
		
		if (ev.type == EventType.KeyDown)
		{
			if (ev.keyCode == KeyCode.E)
			{
				robot.ResetMove();
				return;
			}
			}
			
			

			var command = "";
			
			if (ev.keyCode == KeyCode.W)
			{
				command = "local direction = Vector(0,1)\n";
				//robot.Move(0,1);
			}
			if (ev.keyCode == KeyCode.A)
			{
				command = "local direction = Vector(-1,0)\n";
				//robot.Move(-1,0);
			}
			if (ev.keyCode == KeyCode.S)
			{
				command = "local direction = Vector(0,-1)\n";
				//robot.Move(0,-1);
			}
			if (ev.keyCode == KeyCode.D)
			{
				command = "local direction = Vector(1,0)\n";
				//robot.Move(1,0);
			}


			if (command.Length == 0)
			{
				return;
			}
			
			if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
			{
				command += @"
							local robot = player.robots()[1]
							Tiles[robot.pos + direction].highlight = true
							Tiles[robot.pos + direction].color = Color(1,0,0)
							robot.attack(direction)
							";
			}
			else
			{
				command += @"
							local robot = player.robots()[1]
							Tiles[robot.pos].highlight = true
							Tiles[robot.pos].color = Color(0,1,0)
							robot.move(direction)
							";
			}
			Debug.Log(command);
			Player.Player1.Execute(command);
			
			
			*/
			
		}
	
		public void SetP1()
		{
			
			console.Setup(Player.Player1.Script);
			Player.Player1.setText(PlayerEditorText.text);
			
		}
		
		public void SetP2()
		{
			
			console.Setup(Player.Player2.Script);
			Player.Player2.setText(PlayerEditorText.text);
			
		}

		public void Run()
		{
			Player.DoTurn();
		}



}