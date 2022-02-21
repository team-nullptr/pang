using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class BallSaveable : Saveable
{
	[System.Serializable]
	struct BallData {
		public float x, y;
		public int layer;
		public bool direction;
		public float jumpAltitude;
		public float xVelocity, yVelocity;
	}

	const string prefabPath = "Prefabs/Ball";

    public override MemoryStream Save() {
		BinaryFormatter binaryFormatter = SaveManager.GetFormatter();
		MemoryStream memoryStream = new MemoryStream();

		// Get necessary data.
		BallData data = new BallData();
		data.x = transform.position.x;
		data.y = transform.position.y;
		BallManager ballManager = GetComponent<BallManager>();
		data.layer = ballManager.layer;
		BallMovement ballMovement = GetComponent<BallMovement>();
		data.direction = ballMovement.speed > 0;
		data.jumpAltitude = ballMovement.jumpAltitude;
		Rigidbody2D rigidbody2D = GetComponent<Rigidbody2D>();
		data.xVelocity = rigidbody2D.velocity.x;
		data.yVelocity = rigidbody2D.velocity.y;

		// Serialize the data.
		binaryFormatter.Serialize(memoryStream, data);
		return memoryStream;
	}

	public override void Load(MemoryStream saveData) {
		// Deserialize the data.
		BinaryFormatter binaryFormatter = SaveManager.GetFormatter();
		saveData.Position = 0;
		BallData data = (BallData)binaryFormatter.Deserialize(saveData);

		// Instantiate the ball and fill in the data.
		GameObject ball = Instantiate(Resources.Load(prefabPath)) as GameObject;
		ball.transform.position = new Vector2(data.x, data.y);
		BallManager ballManager = ball.GetComponent<BallManager>();
		ballManager.layer = data.layer;
		BallMovement ballMovement = ball.GetComponent<BallMovement>();
		ballMovement.speed *= data.direction ? 1 : -1;
		ballMovement.jumpAltitude = data.jumpAltitude;
		Rigidbody2D rigidbody2D = ball.GetComponent<Rigidbody2D>();
		rigidbody2D.velocity = new Vector2(data.xVelocity, data.yVelocity);
	}

	public override void OnLoad()
	{
		// Destroy all the original balls.
		Destroy(gameObject);
	}
}
