using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
	/// <summary>
	/// The vertical velocity of a bullet.
	/// </summary>
	public float bulletSpeed = 10f;
	public SpriteRenderer trailSpriteRenderer;
	public BoxCollider2D trailBoxCollider2D;
	public WeaponManager shooter;
	public AudioSource bulletBreakingSound;

	float startingPoint;
	new BoxCollider2D collider;

	// Start is called before the first frame update
	void Start()
	{
		// Save the starting point of the bullet
		startingPoint = transform.position.y;

		// Get the bullet breaking sound
		GameObject bulletBreakingSoundObject = GameObject.Find("BulletBreakingSpeaker");

		if (bulletBreakingSoundObject != null)
		{
			bulletBreakingSound = bulletBreakingSoundObject.GetComponent<AudioSource>();
		}

		// Get the bullet collider
		collider = GetComponent<BoxCollider2D>();
	}

	// Update is called once per frame
	void Update()
	{
		// Update bullet head position
		transform.position = new Vector2(transform.position.x, transform.position.y + bulletSpeed * Time.deltaTime);

		// Make trail longer
		trailSpriteRenderer.size = new Vector2(trailSpriteRenderer.size.x, transform.position.y - startingPoint);
		trailBoxCollider2D.size = new Vector2(trailBoxCollider2D.size.x, transform.position.y - startingPoint);

		// Fix box collider positioning
		trailBoxCollider2D.offset = new Vector2(trailBoxCollider2D.offset.x, -(trailBoxCollider2D.gameObject.transform.position.y - startingPoint) / 2 - 0.5f);

		// Destroy the bullet when it goes off screen
		if (transform.position.y + collider.bounds.extents.y > Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height, 0)).y)
		{
			BreakBullet();
		}
	}

	void OnTriggerEnter2D(Collider2D collider)
	{
		if (collider.tag != "Player" && collider.tag != "Bullet" && collider.tag != "Ball")
		{
			BreakBullet();
		}
	}

	public void BreakBullet()
	{
		if (bulletBreakingSound != null)
		{
			bulletBreakingSound.Play();
		}

		DestroyBullet();
	}

	public void DestroyBullet()
	{
		Destroy(gameObject);

		WeaponManager.bulletCount--;
	}
}
