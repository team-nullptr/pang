using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementManager : MonoBehaviour
{
    public float speed = 100;
    public Rigidbody2D rigidbody;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		transform.position = new Vector2(transform.position.x + Input.GetAxisRaw("Horizontal") * speed * Time.deltaTime * 10, transform.position.y);
    }
}
