using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallManager : MonoBehaviour
{
    public Rigidbody2D rigidbody;
    public float speed=10;
    public float direction = 1;
    public GameController gameController;
    public float minRelativeHeight = 11;
    // Start is called before the first frame update
    void Start()
    {
        //rigidbody.AddForce(new Vector2(1500, 0));
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.name == "Player") gameController.KillPlayer();
        else if (collision.gameObject.name == "right")
        {
            direction = -direction;
        }
        else if(collision.gameObject.name == "left")
        {
            direction = -direction;
        }
        else if(collision.gameObject.name == "floor")
        {
            if(collision.relativeVelocity.y< minRelativeHeight)
            {
                rigidbody.velocity = new Vector2(rigidbody.velocity.x, minRelativeHeight);
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        rigidbody.velocity = new Vector2(speed * direction, rigidbody.velocity.y);
        //RaycastHit2D hit = Physics2D.CircleCast(transform.position, transform.localScale.x/2, transform.position + new Vector3(transform.localScale.x * direction, 0));
        //if (hit.transform.gameObject.name == "Player")
        //{
        //    gameController.KillPlayer();
        //}

        ////rigidbody.MovePosition(transform.position + new Vector3(speed * direction * Time.deltaTime, 0,0));
        //rigidbody.AddForce(new Vector2(speed * direction * Time.deltaTime, 0));
    }
}
