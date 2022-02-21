using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TerrainSaveable : Savable
{
	[System.Serializable]
	struct BreakableTile {
		public int x, y;
	}

	[System.Serializable]
	struct TerrainData {
		public BreakableTile[] breakableTiles;
	}

	const string prefabPath = "Sprites/BreakableBricks";

    public override MemoryStream Save()
	{
		BinaryFormatter binaryFormatter = SaveManager.GetFormatter();
		MemoryStream memoryStream = new MemoryStream();

		// Look for breakable tiles and save them into an array.
		Tilemap tilemap = GetComponent<Tilemap>();
		List<BreakableTile> breakableTiles = new List<BreakableTile>();

		for(int x = tilemap.cellBounds.min.x; x < tilemap.cellBounds.max.x; x++)
		{
			for(int y = tilemap.cellBounds.min.y; y < tilemap.cellBounds.max.y; y++)
			{
				TileBase tile = tilemap.GetTile(new Vector3Int(x, y, 0));

				if(tile != null && tile.name == "BreakableBricks")
				{
					BreakableTile breakableTile = new BreakableTile();
					breakableTile.x = x;
					breakableTile.y = y;
					breakableTiles.Add(breakableTile);
				}
			}
		}

		// Save the breakable tiles into a TerrainData struct.
		TerrainData data = new TerrainData();
		data.breakableTiles = breakableTiles.ToArray();

		// Serialize the data.
		binaryFormatter.Serialize(memoryStream, data);
		return memoryStream;
	}

	public override void Load(MemoryStream stream)
	{
		// Find tilemap.
		GameObject terrain = GameObject.FindWithTag("Terrain");
		Tilemap tilemap = terrain.GetComponent<Tilemap>();

		// Deserialize the data.
		BinaryFormatter binaryFormatter = SaveManager.GetFormatter();
		stream.Position = 0;
		TerrainData data = (TerrainData)binaryFormatter.Deserialize(stream);

		// Load the breakable tiles.
		Tile prefab = Resources.Load<Tile>(prefabPath);

		foreach(BreakableTile breakableTile in data.breakableTiles)
		{
			tilemap.SetTile(new Vector3Int(breakableTile.x, breakableTile.y, 0), prefab);
		}
	}

	public override void OnLoad()
	{
		// Delete all the breakable tiles.
		Tilemap tilemap = GetComponent<Tilemap>();
		
		for(int x = tilemap.cellBounds.min.x; x < tilemap.cellBounds.max.x; x++)
		{
			for(int y = tilemap.cellBounds.min.y; y < tilemap.cellBounds.max.y; y++)
			{
				TileBase tile = tilemap.GetTile(new Vector3Int(x, y, 0));

				if(tile != null && tile.name == "BreakableBricks")
				{
					tilemap.SetTile(new Vector3Int(x, y, 0), null);
				}
			}
		}
	}
}
