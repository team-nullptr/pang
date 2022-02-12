using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BulletDestruction : MonoBehaviour
{
	AudioSource bulletBreakingSound, brickBreakingSound;
	new BoxCollider2D collider;

	void Start() {
		// Get the bullet breaking sound
		GameObject bulletBreakingSoundObject = GameObject.Find("BulletBreakingSpeaker");

		if (bulletBreakingSoundObject != null)
			bulletBreakingSound = bulletBreakingSoundObject.GetComponent<AudioSource>();

		// Get the brick breaking sound
		GameObject brickBreakingSoundObject = GameObject.Find("BrickBreakingSpeaker");

		if (brickBreakingSoundObject != null)
			brickBreakingSound = brickBreakingSoundObject.GetComponent<AudioSource>();

		// Get the bullet collider
		collider = GetComponent<BoxCollider2D>();
	}

    void FixedUpdate()
    {
		// Destroy the bullet when it goes off screen
		if (transform.position.y + collider.bounds.extents.y > Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height, 0)).y)
		{
			BreakBullet();
		}
    }

	void OnCollisionEnter2D(Collision2D collision)
	{
		// If the bullet hits a player or another bullet, do nothing
		// If it hits a ball, the BallManager will handle it
		if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "Bullet" || collision.gameObject.tag == "Ball")
			return;

		foreach(ContactPoint2D contact in collision.contacts) {
			Vector3 collisionPoint = contact.point;

			// If the bullet collides from the bottom, ignore it
			if(collisionPoint.y < transform.position.y)
				return;

			// If the bullet collides with a breakable brick, break it
			Tilemap tilemap = collision.gameObject.GetComponentInParent<Tilemap>();

			if(tilemap != null) {
				// Find the tile at the collision point
				Vector3Int cell = tilemap.WorldToCell(collisionPoint);

				TileBase tile = tilemap.GetTile(cell);

				// Check a bit around to make up for imperfect rounding (we know that the bullet hit a tile)
				if(tile == null) {
					cell = tilemap.WorldToCell(collisionPoint + new Vector3(0, 0.1f, 0));
					tile = tilemap.GetTile(cell);
				}

				if(tile == null) {
					cell = tilemap.WorldToCell(collisionPoint + new Vector3((collisionPoint.x % 1 > 0.5 ? 0.1f : -0.1f), 0, 0));
					tile = tilemap.GetTile(cell);
				}

				// Check if the tile is breakable
				if(tile != null && tile.name == "BreakableBricks") {
					// Destroy the bricks
					tilemap.SetTile(cell, null);

					// Play the brick breaking sound
					brickBreakingSound.Play();

					// Destroy the bullet without the bullet breaking sound
					DestroyBullet();

					// To always destroy at most one brick
					return;
				}
			}
			
		}

		// Destroy the bullet
		BreakBullet();
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
	}
}
