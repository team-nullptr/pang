using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementManager : MonoBehaviour
{
	public float speed = 20;
	new public Rigidbody2D rigidbody;
	private float x;

	// Start is called before the first frame update
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{
		x = Input.GetAxis("Horizontal");
		//x *= Time.deltaTime;
		x *= speed;
		//transform.position = transform.position + new Vector3(x, 0, 0);
		//rigidbody.AddForce(new Vector2(x, 0));
		//rigidbody.MovePosition(transform.position + new Vector3(x, 0, 0));
		rigidbody.velocity = new Vector2(x, rigidbody.velocity.y);
		//characterController.Move(new Vector3(x, 0, 0));
	}
}
