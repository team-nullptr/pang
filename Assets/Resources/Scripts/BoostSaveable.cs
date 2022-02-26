using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class BoostSaveable : Saveable
{
	public string prefabPath = "Prefabs/HealthBoost";

	[System.Serializable]
	struct BoostData {
		public float x, y;
		public string prefabPath;
	}

    override public MemoryStream Save()
	{
		BinaryFormatter binaryFormatter = SaveManager.GetFormatter();
		MemoryStream memoryStream = new MemoryStream();

		// Get necessary data.
		BoostData data = new BoostData();
		data.x = transform.position.x;
		data.y = transform.position.y;
		data.prefabPath = prefabPath;

		// Serialize the data.
		binaryFormatter.Serialize(memoryStream, data);
		return memoryStream;
	}

	override public void Load(MemoryStream memoryStream)
	{
		BinaryFormatter binaryFormatter = SaveManager.GetFormatter();
		BoostData data = (BoostData)binaryFormatter.Deserialize(memoryStream);

		// Load the data.
		GameObject boost = Instantiate(Resources.Load(data.prefabPath)) as GameObject;
		boost.transform.position = new Vector3(data.x, data.y, 0);
	}

	override public void OnLoad()
	{
		// There shouldn't be any boosts in the scene, but just in case.
		Destroy(gameObject);
	}
}
