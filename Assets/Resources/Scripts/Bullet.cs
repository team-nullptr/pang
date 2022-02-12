using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
	/// <summary>
	/// The vertical velocity of a bullet.
	/// </summary>
	public float bulletSpeed = 5f;
	public SpriteRenderer trailSpriteRenderer;
	public BoxCollider2D trailBoxCollider2D;
	
	float startingPoint;
	float trailColiderWidth;

	// Start is called before the first frame update
	void Start()
	{
		// Save the starting point of the bullet
		startingPoint = transform.position.y;

		// Get the width of the trail collider
		trailColiderWidth = trailBoxCollider2D.bounds.size.x;
	}

	// Update is called once per frame
	void Update()
	{
		// Update bullet head position
		transform.position = new Vector2(transform.position.x, transform.position.y + bulletSpeed * Time.deltaTime);

		float scaleY = (transform.position.y - startingPoint) / trailColiderWidth;

		// Make trail longer
		trailSpriteRenderer.size = new Vector2(
			trailSpriteRenderer.size.x,
			scaleY
		);

		trailBoxCollider2D.size = new Vector2(
			trailColiderWidth,
			scaleY
		);

		// Fix box collider positioning
		trailBoxCollider2D.offset = new Vector2(trailBoxCollider2D.offset.x, -trailBoxCollider2D.size.y / 2f);
	}
}
