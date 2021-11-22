using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
	public float bulletSpeed = 10;
	public GameObject trail;
	private float startingPoint;

    // Start is called before the first frame update
    void Start()
    {
        startingPoint = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
		float step = bulletSpeed * Time.deltaTime;
        transform.position = new Vector2(transform.position.x, transform.position.y + step);
		trail.transform.localScale = new Vector2(trail.transform.localScale.x, trail.transform.localScale.y + step / 10f);
		trail.transform.position = new Vector2(trail.transform.position.x, (startingPoint + transform.position.y - 1f) / 2f);
	}

	void OnTriggerEnter2D(Collider2D collider) {
		if(collider.tag != "Player") {
			Destroy(gameObject);
		}
	}
}
