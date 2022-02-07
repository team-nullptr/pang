using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementManager : MonoBehaviour
{
	/// <summary>
	/// Player's velocity.
	/// </summary>
	public float speed = 7f;

	new Rigidbody2D rigidbody;
	new CapsuleCollider2D collider;
	PlayerControls controls;
	Vector2 movement;

	void Awake()
	{
		controls = new PlayerControls();

		controls.Gameplay.Move.performed += ctx => movement = ctx.ReadValue<Vector2>();
		controls.Gameplay.Move.canceled += ctx => movement = Vector2.zero;
	}

	void OnEnable()
	{
		controls.Gameplay.Enable();
	}

	void OnDisable()
	{
		controls.Gameplay.Disable();
	}

	void Start()
	{
		rigidbody = GetComponent<Rigidbody2D>();
		collider = GetComponent<CapsuleCollider2D>();
	}

	void Update()
	{
		// Movement
		float x = movement.x * speed;

		rigidbody.velocity = new Vector2(x, rigidbody.velocity.y);

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
