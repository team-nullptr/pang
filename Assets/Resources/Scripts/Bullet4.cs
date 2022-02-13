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
	BulletDestruction bulletDestruction;

	float lifeTimer = 0f;

    void Start()
    {
		// Get the rigidbody
        rigidbody = GetComponent<Rigidbody2D>();

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
	}
}
