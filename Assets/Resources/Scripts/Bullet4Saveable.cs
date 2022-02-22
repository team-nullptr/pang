using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class Bullet4Saveable : Saveable
{
	[System.Serializable]
	struct BulletData {
		public float x, y;
		public float xVelocity, yVelocity;
		public float lifeTime;
	}

    public override MemoryStream Save()
	{
		BinaryFormatter binaryFormatter = SaveManager.GetFormatter();
		MemoryStream memoryStream = new MemoryStream();

		// Read the data and serialize it.
		BulletData data = new BulletData();
		data.x = transform.position.x;
		data.y = transform.position.y;
		Rigidbody2D rigidbody = GetComponent<Rigidbody2D>();
		data.xVelocity = rigidbody.velocity.x;
		data.yVelocity = rigidbody.velocity.y;
		data.lifeTime = GetComponent<Bullet4>().lifeTime;

		binaryFormatter.Serialize(memoryStream, data);
		return memoryStream;
	}

	public override void Load(MemoryStream stream)
	{
		// Read the data and deserialize it.
		BinaryFormatter binaryFormatter = SaveManager.GetFormatter();
		BulletData data = (BulletData)binaryFormatter.Deserialize(stream);

		// Get the bullet prefab.
		WeaponManager weaponManager = GameObject.FindWithTag("Player").GetComponent<WeaponManager>();

		GameObject bulletPrefab = weaponManager.bulletPrefab.gameObject;

		// Instantiate the bullet.
		GameObject bullet = Instantiate(bulletPrefab) as GameObject;

		// Set the position and velocity.
		bullet.transform.position = new Vector3(data.x, data.y, 0);
		bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(data.xVelocity, data.yVelocity);
		bullet.GetComponent<Bullet4>().lifeTime = data.lifeTime;
	}

	public override void OnLoad() {
		// There shouldn't be any bullets, but who knows.
		Destroy(gameObject);
	}
}
