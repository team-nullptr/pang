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

	void Start()
	{
		rigidbody2D = GetComponent<Rigidbody2D>();
	}

	void FixedUpdate()
	{
		rigidbody2D.velocity = new Vector2(speed, rigidbody2D.velocity.y);
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		// Calculate hit direction
		Vector2 directionalVector = (collision.contacts[0].point - new Vector2(transform.position.x, transform.position.y)).normalized;
		float direction = directionalVector.x;

		// Reverse the ball's direction
		if (Mathf.Abs(direction) > 0.25)
		{
			speed *= -1;
		}

		// If the ball hits the ground, bounce the ball so it achieves a constant height

		if (directionalVector.y < -0.5)
		{
			float force = 2 * -Physics2D.gravity.y * (jumpAltitude + groundLevel - transform.position.y);
			rigidbody2D.velocity = new Vector2(
				rigidbody2D.velocity.x, force > 0 ?
				Mathf.Sqrt(Mathf.Abs(force)) :
				-Mathf.Sqrt(Mathf.Abs(force)));
		}

		// If the ball hits the ceiling, bounce the ball

		if (directionalVector.y > 0.5)
		{
			rigidbody2D.velocity = new Vector2(
				rigidbody2D.velocity.x,
				collision.relativeVelocity.y);
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
