using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTester : MonoBehaviour
{
	// Use this for initialization
	void Start ()
	{

	}
	
	// Update is called once per frame
	void Update ()
	{
	}


	private void OnGUI()
	{
		Event ev = Event.current;
		if (ev.type == EventType.KeyDown)
		{
			if (ev.keyCode == KeyCode.Space)
			{
				foreach (var robot in Player.Player1.MyRobots)
				{
					robot.Attack(0,1);
				}
			}

			if (ev.keyCode == KeyCode.W)
			{
				foreach (var robot in Player.Player1.MyRobots)
				{
					robot.Move(0,1);
				}
			}
			if (ev.keyCode == KeyCode.A)
			{
				foreach (var robot in Player.Player1.MyRobots)
				{
					robot.Move(-1,0);
				}
			}
			if (ev.keyCode == KeyCode.S)
			{
				foreach (var robot in Player.Player1.MyRobots)
				{
					robot.Move(0,-1);
				}
			}
			if (ev.keyCode == KeyCode.D)
			{
				foreach (var robot in Player.Player1.MyRobots)
				{
					robot.Move(1,0);
				}
			}

			if (ev.keyCode == KeyCode.E)
			{
				foreach (var robot in Player.Player1.MyRobots)
				{
					robot.ResetMove();
				}
			}
			
		}


	}
}
