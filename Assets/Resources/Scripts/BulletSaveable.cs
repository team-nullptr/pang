using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class BulletSaveable : Saveable
{
	[System.Serializable]
	struct BulletData {
		public float x, y;
		public float startingPoint;
	}

    public override MemoryStream Save()
	{
		BinaryFormatter binaryFormatter = SaveManager.GetFormatter();
		MemoryStream memoryStream = new MemoryStream();

		// Read the data and serialize it.
		BulletData data = new BulletData();
		data.x = transform.position.x;
		data.y = transform.position.y;
		Bullet bullet = GetComponent<Bullet>();
		data.startingPoint = (float)bullet.startingPoint;
		binaryFormatter.Serialize(memoryStream, data);

		return memoryStream;
	}

	public override void Load(MemoryStream stream)
	{
		// Deserialize the data.
		BinaryFormatter binaryFormatter = SaveManager.GetFormatter();
		stream.Position = 0;
		BulletData data = (BulletData)binaryFormatter.Deserialize(stream);

		// Get the bullet prefab.
		WeaponManager weaponManager = GameObject.FindWithTag("Player").GetComponent<WeaponManager>();

		GameObject bulletPrefab = weaponManager.bulletPrefab.gameObject;

		// Reconstruct the object.
		GameObject bulletObject = Instantiate(bulletPrefab);
		bulletObject.transform.position = new Vector3(data.x, data.y, 0);
		Debug.Log("Bullet position: " + bulletObject.transform.position.x);
		Bullet bullet = bulletObject.GetComponent<Bullet>();
		bullet.startingPoint = data.startingPoint;

		// FIXME: A bullet sometimes moves a bit upwards or left when it is loaded.
	}

	public override void OnLoad() {
		// There shouldn't be any bullets, but who knows.
		Destroy(gameObject);
	}
}
