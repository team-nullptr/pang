using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementManager : MonoBehaviour
{
	public float speed = 20f;
	new Rigidbody2D rigidbody;
	new CapsuleCollider2D collider;

	void Start()
	{
		rigidbody = GetComponent<Rigidbody2D>();
		collider = GetComponent<CapsuleCollider2D>();
	}

	void Update()
	{
		// Movement
		float x = Input.GetAxisRaw("Horizontal") * Time.deltaTime * speed;

		transform.position = new Vector2(transform.position.x + x, transform.position.y);

		// Limit player movenent to the screen borders
		if (Camera.main.WorldToScreenPoint(transform.position - collider.bounds.extents).x < 0f)
		{
			transform.position = new Vector3(Camera.main.ScreenToWorldPoint(new Vector3(0f, 0f, 0f)).x + collider.bounds.extents.x, transform.position.y, transform.position.z);
		}

		if (Camera.main.WorldToScreenPoint(transform.position + collider.bounds.extents).x > Screen.width)
		{
			transform.position = new Vector3(Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0f, 0f)).x - collider.bounds.extents.x, transform.position.y, transform.position.z);
		}
	}
}
