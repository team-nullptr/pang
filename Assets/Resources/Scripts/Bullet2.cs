using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet2 : MonoBehaviour
{
    /// <summary>
	/// The velocity of the bullet.
	/// </summary>
	public float speed = 10f;

	void Update()
	{
		// Move the bullet up
		transform.Translate(Vector3.up * speed * Time.deltaTime);
	}
}
