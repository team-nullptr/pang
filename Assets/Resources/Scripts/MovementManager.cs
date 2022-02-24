using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementManager : MonoBehaviour
{
	/// <summary>
	/// Player's velocity.
	/// </summary>
	public float speed = 7f;
	/// <summary>
	/// Player's velocity for climbing on ladder.
	/// </summary>
	public float climbingSpeed = 5f;

	new Rigidbody2D rigidbody;
	new CapsuleCollider2D collider;
	PlayerControls controls;
	Animator animator;
	SpriteRenderer spriteRenderer;
	Vector2 movement;
	bool isOnLadder = false;

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
		animator = GetComponent<Animator>();
		spriteRenderer = GetComponent<SpriteRenderer>();
	}

	void Update()
	{
		// Movement
		float x = movement.x * speed;

		// If the player is on a ladder and presses the up button, climb up
		rigidbody.velocity = new Vector2(
			x,
			isOnLadder ? movement.y * climbingSpeed : rigidbody.velocity.y
		);

		// Block the horizontal movement if you would run into a wall
		if(x != 0) {
			List<ContactPoint2D> contacts = new List<ContactPoint2D>();
			collider.GetContacts(contacts);

			foreach(ContactPoint2D contact in contacts)
			{
				if(contact.normal.x == -Mathf.Sign(x))
				{
					rigidbody.velocity = new Vector2(0, rigidbody.velocity.y);
				}
			}
		}

		// Limit player movenent to the screen borders
		if (Camera.main.WorldToScreenPoint(transform.position - collider.bounds.extents).x < 0f)
		{
			transform.position = new Vector3(Camera.main.ScreenToWorldPoint(new Vector3(0f, 0f, 0f)).x + collider.bounds.extents.x, transform.position.y, transform.position.z);
		}

		if (Camera.main.WorldToScreenPoint(transform.position + collider.bounds.extents).x > Screen.width)
		{
			transform.position = new Vector3(Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0f, 0f)).x - collider.bounds.extents.x, transform.position.y, transform.position.z);
		}

		// Set animator values
		if(movement.x != 0) {
			animator.SetBool("walking", true);

			// Make player face the direction of movement
			spriteRenderer.flipX = movement.x < 0;
		}
		else
			animator.SetBool("walking", false);

		if(isOnLadder) {
			animator.SetBool("climbing", true);

			animator.SetFloat("climbingPerformed", movement.y);
		}
		else
			animator.SetBool("climbing", false);
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.CompareTag("Ladder"))
		{
			isOnLadder = true;

			// Disable gravity for player so he doesn't fall down
			rigidbody.gravityScale = 0f;
		}
	}

	void OnTriggerExit2D(Collider2D other)
	{
		if (other.CompareTag("Ladder"))
		{
			isOnLadder = false;

			// Enable gravity for player
			rigidbody.gravityScale = 1f;

			// Reset vertical velocity so that the player doesn't jump when he leaves the ladder
			rigidbody.velocity = new Vector2(rigidbody.velocity.x, 0f);

			if(transform.position.y > other.transform.position.y + other.bounds.extents.y + collider.bounds.extents.y) {
				transform.position = new Vector3(
					transform.position.x,
					other.transform.position.y + other.bounds.extents.y + collider.bounds.extents.y,
					transform.position.z
				);
			}
		}
	}

	public bool IsOnLadder()
	{
		return isOnLadder;
	}
}
