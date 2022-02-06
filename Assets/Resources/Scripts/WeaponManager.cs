using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
	public KeyCode fireKey = KeyCode.Space;
	public GameObject bulletPrefab;
	/// <summary>
	/// The maximum number of bullets that can be on the screen at once.
	/// </summary>
	public int maxBulletCount = 0;
	/// <summary>
	/// How high above the ground should the bullet spawn.
	/// </summary>
	public const float bulletOffset = 0.1f;
	/// <summary>
	/// How many bullets are currently on the screen.
	/// </summary>
	public int bulletCount = 0;

	AudioSource shotSound;
	new CapsuleCollider2D collider;
	PlayerControls controls;

	// INPUT

	void Awake()
	{
		controls = new PlayerControls();

		// Shoot the bullet when the fire key is pressed.
		controls.Gameplay.Shoot.performed += ctx => Shoot();
	}

	void OnEnable()
	{
		controls.Gameplay.Enable();
	}

	void OnDisable()
	{
		controls.Gameplay.Disable();
	}

	// LOGIC

	void Start()
	{
		shotSound = GameObject.Find("ShotSpeaker").GetComponent<AudioSource>();
		collider = GetComponent<CapsuleCollider2D>();
	}

	//FIXME: Don't let the player shoot when the game is paused.

	void Shoot()
	{
		// If the bullet count is at the maximum, do nothing.
		if (bulletCount >= maxBulletCount)
			return;

		// Create a new bullet.
		GameObject bullet = Instantiate(bulletPrefab, transform.position + new Vector3(0f, bulletOffset - collider.bounds.extents.y, 0f), Quaternion.identity);

		// Get the neccessary components.
		Bullet bulletComponent = bullet.GetComponent<Bullet>();
		BoxCollider2D bulletCollider = bullet.GetComponent<BoxCollider2D>();

		// Set the bullet's shooter.
		bulletComponent.shooter = this;
		// Put the bullet a bit above the ground so it doesn't collide with it and automatically destroy it.
		bullet.transform.position += new Vector3(0f, bulletCollider.bounds.extents.y, 0f);

		// Add one to the bullet count.
		bulletCount++;

		// Play the shot sound.
		if (shotSound != null)
			shotSound.Play();
	}
}
