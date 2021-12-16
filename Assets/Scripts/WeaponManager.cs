using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
	public KeyCode fireKey = KeyCode.Space;
	public GameObject bulletPrefab;
	public float bulletSpeed = 10;
	public int maxBulletCount = 0;

	[SerializeField]
	const float bulletOffset = 0.1f;

	public static float bulletCount = 0f;

	void Shoot()
	{
		if (bulletCount < maxBulletCount)
		{
			GameObject bullet = Instantiate(bulletPrefab, transform.position + new Vector3(0f, bulletOffset, 0f), Quaternion.identity);
			Bullet bulletComponent = bullet.GetComponent<Bullet>();

			bulletComponent.shooter = this;
			bulletComponent.bulletSpeed = bulletSpeed;

			bulletCount++;
		}
	}

	void Update()
	{
		if (Input.GetKeyDown(fireKey))
		{
			Shoot();
		}
	}
}
