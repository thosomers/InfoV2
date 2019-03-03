using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class TerrainGen : MonoBehaviour
{

    public float[,] HeightMap;

    public int Size;
    

    private void Start()
    {
       
        HeightMap = new float[Size,Size];

        HeightMap[0, 0] = 0;
        HeightMap[Size - 1, Size - 1] = 0;
        HeightMap[0, 0] = 0;
        HeightMap[Size - 1, Size - 1] = 0;

    }
}
