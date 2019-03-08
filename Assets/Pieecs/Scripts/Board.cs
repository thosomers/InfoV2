using System;
using System.Collections;
using System.Collections.Generic;
using MoonSharp.Interpreter;
using PathFind;
using Pieecs.Scripts.Utils;
using UnityEngine;
using UnityEngine.UI;



public class Board : MonoBehaviour
{
	public static Board Instance { get; private set; }




	public int SIZE;



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
		
		
		/*
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
		}*/
	}

	public static void Setup()
	{
		Instance.SetupInstance();
	}

	private void SetupInstance()
	{
		for (int i = 0; i < transform.childCount; i++)
		{
			GameObject.Destroy(transform.GetChild(i).gameObject);
		}
		
		Tiles = DiamondSquare4.generateMap(TilePrefab,this.transform,size: SIZE);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public static bool exists(int x, int y)
	{
		return x >= 0 && x < Instance.SIZE && y >= 0 && y < Instance.SIZE;
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
	
	
	

	public static void SetupScript(Script scr)
	{
		scr.Globals["Tiles"] = Instance;
	}

	public static void ClearTiles()
	{
		foreach (var boardTile in Instance.Tiles)
		{
			boardTile.selectionBox.setEnabled(false);
		}
	}
	
}



[MoonSharpUserData]
public class BoardProxy
{
	public Board board;

	public BoardProxy(Board board)
	{
		this.board = board;
	}
	
	
	public Tile getTile(VectorProxy vec)
	{
		if (vec == null) return null;
		return Board.getTile((int)vec.X,(int)vec.Y);
	}

	public Tile this[VectorProxy vec]
	{
		get { return getTile(vec); }
	}
	

	public List<Tile> enabled
	{
		get
		{
			var lst = new List<Tile>();
			foreach (var boardTile in board.Tiles)
			{
				if (boardTile.selectionBox.enabled)
				{
					lst.Add(boardTile);
				}
			}

			return lst;
		}
	}


	private PathFind.Grid grid = null;


	private Tile PointToTile(Point point)
	{
		return Board.Instance.Tiles[point.x, point.y];
	}
	
	public List<Tile> pathBetween2(Tile t1, Tile t2)
	{
		if (grid == null)
		{
			grid = new PathFind.Grid(Board.Instance.SIZE,Board.Instance.SIZE,Board.Instance.Tiles);
		}

		var path = Pathfinding.FindPath(grid, new Point(t1.X, t1.Y), new Point(t2.X, t2.Y));

		if (path == null) return null;
		return path.ConvertAll(PointToTile);
	}
	
	public DynValue pathBetween(Script script,Tile t1, Tile t2)
	{
		if (grid == null)
		{
			grid = new PathFind.Grid(Board.Instance.SIZE,Board.Instance.SIZE,Board.Instance.Tiles);
		}

		var path = Pathfinding.FindPath(grid, new Point(t1.X, t1.Y), new Point(t2.X, t2.Y));

		if (path == null || path.Count == 0)
			return DynValue.Nil;

		return DynValue.FromObject(script,path.ConvertAll(PointToTile));

	}

	public List<Tile> withRange(Tile t1, int range)
	{
		return PathFind.Pathfinding.withDistance(t1, range);
	}
	
	
	
}
