using System.Collections;
using System.Collections.Generic;
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
