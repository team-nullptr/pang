using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementManager : MonoBehaviour
{
    public float speed = 100;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
<<<<<<< HEAD
        x = Input.GetAxis("Horizontal");
        x *= Time.deltaTime;
        x *= speed;
        //transform.position = transform.position + new Vector3(x, 0, 0);
        //rigidbody.AddForce(new Vector2(x, 0));
        rigidbody.MovePosition(transform.position + new Vector3(x, 0, 0));
        rigidbody.velocity = new Vector2(x, rigidbody.velocity.y);
        //characterController.Move(new Vector3(x, 0, 0));
=======
		transform.position = new Vector2(transform.position.x + Input.GetAxisRaw("Horizontal") * speed * Time.deltaTime * 10, transform.position.y);
>>>>>>> 18588949e09c03467059c769cedb2b574d8f2fb5
    }
}
