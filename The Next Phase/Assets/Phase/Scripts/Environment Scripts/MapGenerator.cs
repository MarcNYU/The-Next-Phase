using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour {

	public Hashtable levelMap = new Hashtable();
	List<FractionalHex> levelMapData = new List<FractionalHex>();

	public Transform tilePrefab;
	public Vector2 mapSize;

	[Range(0,1)]
	public float outlinePercent;

	private void Awake()
	{
		float q, r;
		for (q = mapSize.x * -1 + 1; q < mapSize.x; ++q)
		{
			for (r = mapSize.y * -1 + 1; r < mapSize.y; ++r)
			{
				if (q + r + (q + r) * -1 == 0)
				{
					FractionalHex fractionalHex = new FractionalHex(q, r, (q + r) * -1);
					levelMapData.Add(fractionalHex);
				}
			}
		}
	}

	private void Start()
	{
		//AdjustCoords();
		GenerateHexMap();
	}
    
    void AdjustCoords()
	{
		for (int i = 0; i < levelMapData.Count; i++)
        {
            Layout layout = new Layout();
			Point point = new Point(levelMapData[i].q, levelMapData[i].r);
            levelMapData[i] = layout.PixelToHex(point);
        }
	}

	public void GenerateHexMap()
	{
		for (int i = 0; i < levelMapData.Count; i++)
        {
			Vector3 tilePostion = new Vector3(-mapSize.x / 2 + (float)levelMapData[i].q, 0, -mapSize.y / 2 + (float)levelMapData[i].r);
            Transform newTile = Instantiate(tilePrefab, tilePostion, Quaternion.Euler(Vector3.right * -90)) as Transform;
            newTile.localScale = Vector3.one * (1 - outlinePercent);
        }
	}

}

