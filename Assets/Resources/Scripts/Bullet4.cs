using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet4 : MonoBehaviour
{
	/// <summary>
	/// The upward force applied to the bullet when it is fired.
	/// </summary>
	public float shotForce = 5f;
	/// <summary>
	/// How long should the bullet live before it is destroyed.
	/// </summary>
	public float lifeTime = 5f;

	new Rigidbody2D rigidbody;
	new Collider2D collider;
	BulletDestruction bulletDestruction;

	float lifeTimer = 0f;

    void Start()
    {
		// Get the rigidbody
        rigidbody = GetComponent<Rigidbody2D>();

		// Get the collider
		collider = GetComponent<Collider2D>();

		// Get the bullet destruction script
		bulletDestruction = GetComponent<BulletDestruction>();

		// Throw the bullet in the air
		rigidbody.AddForce(transform.up * shotForce, ForceMode2D.Impulse);
    }

	void Update() {
		// Destroy the bullet after some time
		lifeTimer += Time.deltaTime;

		if(lifeTimer > lifeTime)
			bulletDestruction.BreakBullet();

		// If the bullet goes off-screen, bounce it back
		if (
			Camera.main.WorldToScreenPoint(transform.position - collider.bounds.extents).x < 0f ||
			Camera.main.WorldToScreenPoint(transform.position + collider.bounds.extents).x > Screen.width
		) {
			rigidbody.velocity = new Vector2(-rigidbody.velocity.x, rigidbody.velocity.y);
			transform.position = new Vector3(
				Mathf.Clamp(
					transform.position.x,
					Camera.main.ScreenToWorldPoint(new Vector3(0f, 0f, 0f)).x + collider.bounds.extents.x,
					Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0f, 0f)).x - collider.bounds.extents.x
				),
				transform.position.y,
				transform.position.z
			);
		}
	}
}
