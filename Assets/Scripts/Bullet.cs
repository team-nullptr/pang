using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
	public float bulletSpeed = 10f;
	public GameObject trail;
	public WeaponManager shooter;
	private float startingPoint;

	// Start is called before the first frame update
	void Start()
	{
		startingPoint = transform.position.y;
	}

	// Update is called once per frame
	void Update()
	{
		transform.position = new Vector2(transform.position.x, transform.position.y + bulletSpeed * Time.deltaTime);
		trail.transform.localScale = new Vector2(trail.transform.localScale.x, (transform.position.y - startingPoint) / 20f);
	}

	void OnTriggerEnter2D(Collider2D collider)
	{
		if (collider.tag != "Player" && collider.tag != "Bullet" && collider.tag != "Ball")
		{
			DestroyBullet();
		}
	}

	public void DestroyBullet()
	{
		Destroy(gameObject);

		WeaponManager.bulletCount--;
	}
}
