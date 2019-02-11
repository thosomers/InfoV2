using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{

	public Selection selectionBox;


	public bool IsEmpty
	{
		get { return Object == null; }
	}
	public PlayerObject Object;
	
	



	public int X
	{
		get { return (int) Position.x; }
	}
	public int Y
	{
		get { return (int) Position.y; }
	}

	public Vector2 Position { get; private set; }



	public void Setup(int x, int y)
	{
		this.Position = new Vector2(x,y);
		this.transform.localPosition = new Vector3(x,y,0);
		this.selectionBox = this.GetComponentInChildren<Selection>();
		
	}


	public void Start()
	{
		
	}
}
