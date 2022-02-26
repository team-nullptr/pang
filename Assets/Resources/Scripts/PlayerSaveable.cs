using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class PlayerSaveable : Saveable
{
	[System.Serializable]
	struct SpeedBoostData {
		public float boostFactor;
		public float duration;
	}

	[System.Serializable]
	struct PlayerData {
		public float x, y;
		public int hp;
		public float invulnerabilityTimer;
		public int iceDirection;
		public SpeedBoostData[] speedBoosts;
		public float DoubleBulletBoostEffectDuration;
	}

    public override MemoryStream Save() {
		BinaryFormatter binaryFormatter = SaveManager.GetFormatter();
		MemoryStream memoryStream = new MemoryStream();

		// Get the necessary data.
		PlayerData data = new PlayerData();
		data.x = transform.position.x;
		data.y = transform.position.y;
		PlayerManager playerManager = GetComponent<PlayerManager>();
		data.hp = playerManager.hp;
		data.invulnerabilityTimer = playerManager.InvulnerabilityTimer;
		MovementManager playerMovement = GetComponent<MovementManager>();
		data.iceDirection = playerMovement.iceDirection;

		SpeedBoostEffect[] speedBoostEffects = GetComponents<SpeedBoostEffect>();
		data.speedBoosts = new SpeedBoostData[speedBoostEffects.Length];
		for(int i = 0; i < speedBoostEffects.Length; i++) {
			SpeedBoostEffect speedBoostEffect = speedBoostEffects[i];
			data.speedBoosts[i] = new SpeedBoostData();
			data.speedBoosts[i].boostFactor = speedBoostEffect.boostFactor;
			data.speedBoosts[i].duration = speedBoostEffect.duration;
		}

		DoubleBulletBoostEffect doubleBulletBoostEffect = GetComponent<DoubleBulletBoostEffect>();
		if(doubleBulletBoostEffect != null) {
			data.DoubleBulletBoostEffectDuration = doubleBulletBoostEffect.duration;
		} else {
			data.DoubleBulletBoostEffectDuration = 0;
		}

		// Serialize the data.
		binaryFormatter.Serialize(memoryStream, data);
		return memoryStream;
	}

	public override void Load(MemoryStream saveData) {
		// Find player's saveable component.
		GameObject player = GameObject.FindWithTag("Player");
		PlayerSaveable currentSavable = player.GetComponent<PlayerSaveable>();

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

		MovementManager playerMovement = GetComponent<MovementManager>();
		playerMovement.iceDirection = data.iceDirection;
		playerMovement.SetOnIce(data.iceDirection != 0);

		foreach(SpeedBoostData speedBoostData in data.speedBoosts) {
			SpeedBoostEffect speedBoostEffect = gameObject.AddComponent<SpeedBoostEffect>();
			speedBoostEffect.boostFactor = speedBoostData.boostFactor;
			speedBoostEffect.duration = speedBoostData.duration;
		}

		if(data.DoubleBulletBoostEffectDuration > 0) {
			DoubleBulletBoostEffect doubleBulletBoostEffect = gameObject.AddComponent<DoubleBulletBoostEffect>();
			doubleBulletBoostEffect.duration = data.DoubleBulletBoostEffectDuration;
		}
	}

	public override void OnLoad() {}
}
