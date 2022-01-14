using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementManager : MonoBehaviour
{
	public float speed = 20f;
	new public Rigidbody2D rigidbody;

	void Update()
	{
		float x = Input.GetAxisRaw("Horizontal") * Time.deltaTime * speed;

		transform.position = new Vector2(transform.position.x + x, transform.position.y);
	}
}
