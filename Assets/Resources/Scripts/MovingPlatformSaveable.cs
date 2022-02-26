using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class MovingPlatformSaveable : Saveable
{
	[System.Serializable]
	struct MovingPlatformData {
		public string name;
		public float x, y;
		public int currentWaypoint;
	}

	override public MemoryStream Save() {
		BinaryFormatter binaryFormatter = SaveManager.GetFormatter();
		MemoryStream memoryStream = new MemoryStream();

		// Get the necessary data.
		MovingPlatformData data = new MovingPlatformData();
		data.name = gameObject.name;
		data.x = transform.position.x;
		data.y = transform.position.y;
		MovingPlatform movingPlatform = GetComponent<MovingPlatform>();
		data.currentWaypoint = movingPlatform.currentWaypoint;

		// Serialize the data.
		binaryFormatter.Serialize(memoryStream, data);
		return memoryStream;
	}

	override public void Load(MemoryStream saveData) {
		BinaryFormatter binaryFormatter = SaveManager.GetFormatter();
		MovingPlatformData data = (MovingPlatformData)binaryFormatter.Deserialize(saveData);


		// Set the necessary data.
		GameObject platform = GameObject.Find(data.name);

		platform.transform.position = new Vector3(data.x, data.y, 0);
		MovingPlatform movingPlatform = platform.GetComponent<MovingPlatform>();
		movingPlatform.currentWaypoint = data.currentWaypoint;
	}

    override public void OnLoad() {}
}
