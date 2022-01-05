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

	private float startingPoint;

	// Start is called before the first frame update
	void Start()
	{
		startingPoint = transform.position.y;
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
	}

	void OnTriggerEnter2D(Collider2D collider)
	{
		if (collider.tag != "Player" && collider.tag != "Bullet" && collider.tag != "Ball")
		{
			DestroyBullet();
		}
	}

	public void DestroyBullet()
	{
		Destroy(gameObject);

		WeaponManager.bulletCount--;
	}
}
