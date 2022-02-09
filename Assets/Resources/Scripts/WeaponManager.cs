using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
	public KeyCode fireKey = KeyCode.Space;
	public BoxCollider2D bulletPrefab;
	/// <summary>
	/// The maximum number of bullets that can be on the screen at once.
	/// </summary>
	public int maxBulletCount = 0;
	/// <summary>
	/// How high above the ground should the bullet spawn.
	/// </summary>
	public const float bulletOffset = 0f;
	/// <summary>
	/// Player's collider.
	/// </summary>
	public new CapsuleCollider2D collider;

	AudioSource shotSound;
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
		// Get the bullet shot sound
		shotSound = GameObject.Find("ShotSpeaker").GetComponent<AudioSource>();
	}

	void Shoot()
	{
		// If the game is over, don't shoot.
		if (GameState.paused)
			return;

		// Count the bullets on scene
		int bulletCount = 0;

		foreach(GameObject _bullet in GameObject.FindGameObjectsWithTag("Bullet"))
		{
			bulletCount++;
		}

		// If the bullet count is at the maximum, do nothing.
		if (bulletCount >= maxBulletCount)
			return;

		// Create a new bullet.
		BoxCollider2D bulletCollider = Instantiate(
			bulletPrefab,
			transform.position - new Vector3(0f, collider.bounds.extents.y, 0f),
			Quaternion.identity
		);

		// Fix the bullet position.
		bulletCollider.gameObject.transform.position += new Vector3(0f, bulletCollider.bounds.extents.y, 0f);

		// Play the shot sound.
		if (shotSound != null)
			shotSound.Play();
	}
}
