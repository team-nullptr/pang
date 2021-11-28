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
        transform.position = new Vector2(transform.position.x, transform.position.y + bulletSpeed * Time.deltaTime);
		// trail.transform.localScale = new Vector2(trail.transform.localScale.x, transform.position.y - transform.GetComponent<Renderer>().bounds.size.y / 2f - startingPoint);
		trail.transform.position = new Vector2(trail.transform.position.x, (startingPoint + transform.position.y) / 2f);
	}

	void OnTriggerEnter2D(Collider2D collider) {
		if(collider.tag != "Player") {
			Destroy(gameObject);
		}
	}
}
