using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMovement : MonoBehaviour
{
	/// <summary>
	/// Horizontal speed of the ball.
	/// </summary>
	public float speed;
	/// <summary>
	/// Jump height of the ball.
	/// </summary>
	public float jumpAltitude;

	public const float groundLevel = -2.5f;

	new Rigidbody2D rigidbody2D;
	new CircleCollider2D collider;

	void Start()
	{
		rigidbody2D = GetComponent<Rigidbody2D>();
		collider = GetComponent<CircleCollider2D>();
	}

	void FixedUpdate()
	{
		rigidbody2D.velocity = new Vector2(speed, rigidbody2D.velocity.y);
	}

	void Update()
	{
		// If the ball goes out of the left screen border, it bounces back.
		if (Camera.main.WorldToScreenPoint(transform.position - collider.bounds.extents).x < 0f)
		{
			speed = -speed;

			transform.position = new Vector3(Camera.main.ScreenToWorldPoint(new Vector3(0f, 0f, 0f)).x + collider.bounds.extents.x, transform.position.y, transform.position.z);
		}

		// If the ball goes out of the right screen border, it bounces back.
		if (Camera.main.WorldToScreenPoint(transform.position + collider.bounds.extents).x > Screen.width)
		{
			speed = -speed;

			transform.position = new Vector3(Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0f, 0f)).x - collider.bounds.extents.x, transform.position.y, transform.position.z);
		}
	}

	void OnCollisionStay2D(Collision2D collision)
	{
		// Calculate hit direction
		Vector2 directionalVector = (collision.contacts[0].point - new Vector2(transform.position.x, transform.position.y)).normalized;
		float direction = directionalVector.x;

		// If the ball hits the wall, reverse the ball's direction
		if (direction > 0.25f && speed > 0f || direction < -0.25f && speed < 0f)
		{
			speed = -speed;
		}

		// If the ball hits the ground, bounce the ball so it achieves a constant height

		if (directionalVector.y < -0.5)
		{
			float force = 2 * -Physics2D.gravity.y * (jumpAltitude + groundLevel - transform.position.y);
			rigidbody2D.velocity = new Vector2(
				speed, force > 0 ?
				Mathf.Sqrt(Mathf.Abs(force)) :
				0f);
		}

		// If the ball hits the ceiling, bounce the ball back

		if (directionalVector.y > 0.5)
		{
			float force = 2 * -Physics2D.gravity.y * (jumpAltitude + groundLevel - transform.position.y);
			rigidbody2D.velocity = new Vector2(speed, -Mathf.Sqrt(Mathf.Abs(force)));
		}
	}

	public void Jump(float _jumpHeight)
	{
		if (rigidbody2D != null)
			rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, _jumpHeight);
		else
			GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, _jumpHeight);
	}
}
