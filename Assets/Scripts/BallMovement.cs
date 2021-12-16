using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMovement : MonoBehaviour
{
	public float speed = 10f;

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
		// Reverse the ball's direction
		Vector2 directionalVector = (collision.contacts[0].point - new Vector2(transform.position.x, transform.position.y)).normalized;
		float direction = directionalVector.x;

		if (Mathf.Abs(direction) > 0.5)
		{
			speed *= -1;
		}
	}

	public void Jump(float jumpHeight)
	{
		rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, jumpHeight);
	}
}
