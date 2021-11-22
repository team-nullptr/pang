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


    public bool useTime = true;
    public float time = 10;
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
            rigidbody.velocity = new Vector2(rigidbody.velocity.x, minRelativeHeight);
        }
    }

    public void destroyBall()
    {
        GameObject dp = Resources.Load<GameObject>("Objects/BallDeathParticles");
        print(dp);
        dp.transform.position = gameObject.transform.position;
        Instantiate(dp);
        GameObject child = null;
        switch (transform.localScale.x)
        {
            case 3:
                child = Resources.Load<GameObject>("Objects/Ball2");
                break;
            case 1.5f:
                child = Resources.Load<GameObject>("Objects/Ball1");
                break;
        }
        if(child!= null)
        {
            float distance = transform.localScale.x / 2;
            child.transform.position = new Vector2(transform.position.x + distance, transform.position.y);
            child.GetComponent<BallManager>().direction = 1;
            Instantiate(child);
            child.transform.position = new Vector2(transform.position.x - distance, transform.position.y);
            child.GetComponent<BallManager>().direction = -1;
            Instantiate(child);
        }
        Destroy(gameObject);
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
        if (useTime)
        {
        time -= Time.deltaTime;
            if (time < 0)
                destroyBall();
        }
        ////rigidbody.MovePosition(transform.position + new Vector3(speed * direction * Time.deltaTime, 0,0));
        //rigidbody.AddForce(new Vector2(speed * direction * Time.deltaTime, 0));
    }
}
