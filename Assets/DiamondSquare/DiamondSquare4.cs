// Original header: "Adapted for Processing from "Plasma Fractal" by Giles Whitaker - Written January, 2002 by Justin Seyster (thanks for releasing into public domain, Justin)."
// Unity C# version - 4.4.2012 - mgear - http://unitycoder.com/blog

using UnityEngine;
using System.Collections;
using System.Xml.Linq;
using PathFind;
using UnityEngine.Serialization;
using Grid = PathFind.Grid;

public class DiamondSquare4 {
	
	float[,] HeightMap;
	float[,] HeightMap2;
	bool[,] HeightMap3;
	
	
	public int Size=50;
	float pwidthpheight;
	public int GRAIN=8;

	public GameObject Prefab;

	public Transform TerrainParent;

	private GameObject[,] Children;
	
	
	
	
	// Use this for initialization
	public DiamondSquare4 (GameObject prefab, Transform terrainParent,int size = 50, int gain = 8)
	{
		this.Size = size;
		this.GRAIN = gain;
		this.Prefab = prefab;
		this.TerrainParent = terrainParent;
		
		
		pwidthpheight= (float)Size+Size;
//		renderer.material.mainTexture.filterMode = FilterMode.Point;
		
		HeightMap = new float[Size,Size];
		HeightMap2 = new float[Size,Size];
		HeightMap3 = new bool[Size, Size];
		
		Children = new GameObject[Size,Size];

		for (var x = 0; x < Size; x++)
		{
			for (var y = 0; y < Size; y++)
			{
				Children[x, y] = GameObject.Instantiate<GameObject>(Prefab, new Vector3(x, 0, y),Quaternion.identity,TerrainParent);
			}
		}
		

		// draw one here already..
		drawPlasma(Size, Size);
	}
	
	// Update is called once per frame
	void Update () 
	{
		// hold button down to generate new
		if (Input.GetMouseButton(0))
		{
			drawPlasma(Size, Size);
		}
	}


	//	--- actual code starts ----
	float displace(float num)
	{
//			if (dummy<300) {print(num); dummy++;}
//			Debug.Break();
		float max = num / pwidthpheight * GRAIN;
		return Random.Range(-0.5f, 0.5f)* max;
	}

	//This is something of a "helper function" to create an initial grid
	//before the recursive function is called. 
	public Tile[,] drawPlasma(float w, float h)
	{
		var ret = new Tile[Size,Size];
		while (true)
		{
			float c1, c2, c3, c4;

			//Assign the four corners of the intial grid random color values
			//These will end up being the colors of the four corners of the applet.     
			c1 = 0.7f;
			c2 = 0.5f;
			c3 = 0.7f;
			c4 = 0.5f;

			divideGrid(0.0f, 0.0f, w, h, c1, c2, c3, c4);

			for (var x = 0; x < Size; x++)
			{
				for (var y = 0; y < Size; y++)
				{
					HeightMap2[x, y] = (HeightMap[Size - (x + 1), Size - (y + 1)] + HeightMap[x, y]) * 3;
					HeightMap3[x, y] = HeightMap2[x, y] > 3.5f;
				}
			}

			PathFind.Grid grid = new PathFind.Grid(Size, Size, HeightMap3);

			PathFind.Point _from = new PathFind.Point(2, 2);
			PathFind.Point _to = new PathFind.Point(Size - 3, Size - 3);

			if (PathFind.Pathfinding.FindPath(grid, _from, _to).Count == 0)
			{
				continue;
			}
			else
			{
				for (var x = 0; x < Size; x++)
				{
					for (var y = 0; y < Size; y++)
					{
						var height = HeightMap2[x, y];
						//Children[x, y].transform.localPosition = new Vector3(x, height, y);
						Children[x, y].GetComponent<HeightTile>().SetSprite(HeightMap3[x, y]);
						ret[x, y] = Children[x, y].GetComponent<Tile>();
						ret[x, y].Walkable = HeightMap3[x, y];
						ret[x,y].Setup(x, y);
					}
				}
				
				
			}

			break;
		}

		return ret;
	}

	public static Tile[,] generateMap(GameObject prefab, Transform terrainParent,int size = 50, int gain = 8)
	{
		var generator = new DiamondSquare4(prefab,terrainParent,size,gain);

		return generator.drawPlasma(size, size);
	}
	
	
	

	//This is the recursive function that implements the random midpoint
    //displacement algorithm.  It will call itself until the grid pieces
    //become smaller than one pixel.   
	void divideGrid(float x, float y, float w, float h, float c1, float c2, float c3, float c4)
	{
	 
	   float newWidth = w * 0.5f;
	   float newHeight = h * 0.5f;
	 
	   if (w < 1.0f || h < 1.0f)
	   {
		 //The four corners of the grid piece will be averaged and drawn as a single pixel.
		 float c = (c1 + c2 + c3 + c4) * 0.25f;
			HeightMap[(int)x,(int)y] = c;
	   }
	   else
	   {
		 float middle =(c1 + c2 + c3 + c4) * 0.25f + displace(newWidth + newHeight);      //Randomly displace the midpoint!
		 float edge1 = (c1 + c2) * 0.5f; //Calculate the edges by averaging the two corners of each edge.
		 float edge2 = (c2 + c3) * 0.5f;
		 float edge3 = (c3 + c4) * 0.5f;
		 float edge4 = (c4 + c1) * 0.5f;
	 
		 //Make sure that the midpoint doesn't accidentally "randomly displaced" past the boundaries!
		 if (middle <= 0)
		 {
		   middle = 0;
		 }
		 else if (middle > 1.0f)
		 {
		   middle = 1.0f;
		 }
	 
		 //Do the operation over again for each of the four new grids.                 
		 divideGrid(x, y, newWidth, newHeight, c1, edge1, middle, edge4);
		 divideGrid(x + newWidth, y, newWidth, newHeight, edge1, c2, edge2, middle);
		 divideGrid(x + newWidth, y + newHeight, newWidth, newHeight, middle, edge2, c3, edge3);
		 divideGrid(x, y + newHeight, newWidth, newHeight, edge4, middle, edge3, c4);
	   }
	}

	
}
