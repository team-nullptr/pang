using UnityEngine;

public class BallManager : MonoBehaviour
{
    public Rigidbody2D rigidbody;
    public float speed=3;
    public float direction = 1;
    public GameController gameController;
    public float minRelativeHeight = 11;
    public Vector2 startPos;


    public bool useTime = true;
    public float time = 10;
    // Start is called before the first frame update
    void Start()
    {
        if (startPos.x!=0&&startPos.y!=0)
        {
            gameObject.transform.position = new Vector3(startPos.x, startPos.y, 0);
        }
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
        //rigidbody.AddForce(new Vector2(1500, 0));
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Player") gameController.KillPlayer();
        else if (collision.gameObject.name == "right")
        {
            direction = -direction;
        }
        else if (collision.gameObject.name == "left")
        {
            direction = -direction;
        }
        else if (collision.gameObject.name == "floor")
        {
            rigidbody.velocity = new Vector2(rigidbody.velocity.x, minRelativeHeight);
        }
        //Debug.Log(collision.contacts.Length);
        //Debug.Log(point);
        Vector2 point = collision.contacts[0].point;
        float diffrence = gameObject.transform.position.x - point.x;
        if (diffrence > gameObject.transform.localScale.x * 0.45)
        {
        direction = (gameObject.transform.position.x - point.x) / Mathf.Abs((gameObject.transform.position.x - point.x));
        Debug.Log("New direction:" + direction.ToString());
        }
    }
    public void destroyBall()
    {
        GameObject dp = Resources.Load<GameObject>("Objects/BallDeathParticles");
        print(dp);
        dp.transform.position = gameObject.transform.position;
        GameObject child = null;
        string childFile = "Objects/Ball3";
        float height= 11;
        switch (gameObject.transform.localScale.x)
        {
            case 3:
                childFile = "Objects/Ball2";
                height = 10;
                //child = Resources.Load<GameObject>("Objects/Ball2");
                //child.GetComponent<BallManager>().minRelativeHeight = 10;
                break;
            case 1.5f:
                childFile = "Objects/Ball1";
                height = 8;
                //child = Resources.Load<GameObject>("Objects/Ball1");
                //child.GetComponent<BallManager>().minRelativeHeight = 8;
                break;
        }   
        if(gameObject.transform.localScale.x != 0.7f)
        {
        float distance = gameObject.transform.localScale.x / 2;
        Debug.Log("position: " + gameObject.transform.position+", distance: "+distance);
            Debug.Log(new Vector2(gameObject.transform.position.x - distance, gameObject.transform.position.y) + "New position");
            Debug.Log(new Vector2(gameObject.transform.position.x + distance, gameObject.transform.position.y) + "New position 2");
            child = Resources.Load<GameObject>(childFile);
        child.GetComponent<BallManager>().minRelativeHeight = height;
            child.GetComponent<BallManager>().direction = 1;
            //child.GetComponent<BallManager>().useTime = true;
            child.GetComponent<BallManager>().startPos = new Vector2(gameObject.transform.position.x + distance, gameObject.transform.position.y);
            Instantiate(child);
            child.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 30000));
            //child.transform.position = new Vector2(gameObject.transform.position.x + distance, gameObject.transform.position.y);
            Debug.Log("End pos 1:"+child.transform.position);

        child = Resources.Load<GameObject>(childFile);
        child.GetComponent<BallManager>().minRelativeHeight = height;
            child.GetComponent<BallManager>().direction = -1;
        child.GetComponent<BallManager>().time -= 1;

            //child.GetComponent<BallManager>().useTime = true;
            child.GetComponent<BallManager>().startPos = new Vector2(gameObject.transform.position.x - distance, gameObject.transform.position.y);

            Instantiate(child);
            child.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 30000));
        //child.transform.position = new Vector2(gameObject.transform.position.x - distance, gameObject.transform.position.y);
        }
        Destroy(gameObject);
        Instantiate(dp);


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
