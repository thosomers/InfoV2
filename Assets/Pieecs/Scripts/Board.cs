using System;
using System.Collections;
using System.Collections.Generic;
using MoonSharp.Interpreter;
using Pieecs.Scripts.Utils;
using UnityEditor.IMGUI.Controls;
using UnityEngine;

public class Board : MonoBehaviour
{
	public static Board Instance { get; private set; }




	public int WIDTH;
	public int HEIGHT;



	public Camera camera;
	
	
	
	public GameObject TilePrefab;
	public GameObject RobotPrefab;
	public GameObject BasePrefab;
	public GameObject CapturePointPrefab;
	
	
	public Tile[,] Tiles;
	
	
	
	
	
	
	
	
	
	// Use this for initialization
	void Start ()
	{
		Instance = this;
		
		//camera.transform.position = new Vector3((float)(WIDTH-1)/2,(float)(HEIGHT-1)/2f,-10);
		//camera.orthographicSize = HEIGHT / 2f;
		
		
		Tiles = new Tile[WIDTH,HEIGHT];
		for (int y = 0; y < HEIGHT; y++)
		{
			for (int x = 0; x < WIDTH; x++)
			{
				GameObject tileGO = GameObject.Instantiate<GameObject>(TilePrefab, this.transform);

				Tile tile = tileGO.GetComponent<Tile>();

				tile.Setup(x, y);

				this.Tiles[x, y] = tile;
			}
		}
		
		
		
		Player.Player1.Setup();
		Player.Player2.Setup();
		
		
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public static bool exists(int x, int y)
	{
		return x >= 0 && x < Instance.WIDTH && y >= 0 && y < Instance.HEIGHT;
	}

	public static PlayerObject getObject(int x, int y)
	{
		return !exists(x, y) ? null : Instance.Tiles[x, y].Object;
	}

	public static Tile getTile(int x, int y)
	{
		return !exists(x, y) ? null : Instance.Tiles[x, y];
	}
	
	public static Tile getTile(VectorProxy vec)
	{
		return getTile((int)vec.X,(int)vec.Y);
	}
	
	
	

	public static void Setup(Script scr)
	{
		scr.Globals["Tiles"] = (Func<VectorProxy, Tile>) getTile;
	}
}
