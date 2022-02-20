using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerSavable : Savable
{
	[System.Serializable]
	struct PlayerData {
		public float x, y;
		public int hp;
		public float invulnerabilityTimer;
	}

    public override MemoryStream Save() {
		BinaryFormatter binaryFormatter = SaveManager.GetFormatter();
		MemoryStream memoryStream = new MemoryStream();

		// Get necessary data.
		PlayerData data = new PlayerData();
		data.x = transform.position.x;
		data.y = transform.position.y;
		PlayerManager playerManager = GetComponent<PlayerManager>();
		data.hp = playerManager.hp;
		data.invulnerabilityTimer = playerManager.InvulnerabilityTimer;

		// Serialize the data.
		binaryFormatter.Serialize(memoryStream, data);
		return memoryStream;
	}

	public override void Load(MemoryStream saveData) {
		// Find player's saveable component.
		GameObject player = GameObject.FindWithTag("Player");
		PlayerSavable currentSavable = player.GetComponent<PlayerSavable>();

		// Deserialize the data.
		BinaryFormatter binaryFormatter = SaveManager.GetFormatter();
		saveData.Position = 0;
		PlayerData data = (PlayerData)binaryFormatter.Deserialize(saveData);

		// Set the player data
		currentSavable.Set(data);
	}

	/// <summary>
	/// Loads the save data to a player gameObject.
	/// </summary>
	void Set(PlayerData data) {
		transform.position = new Vector2(data.x, data.y);

		PlayerManager playerManager = GetComponent<PlayerManager>();
		playerManager.hp = data.hp;
		playerManager.InvulnerabilityTimer = data.invulnerabilityTimer;
	}

	public override void OnLoad() {}
}
