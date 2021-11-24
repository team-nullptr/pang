using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject player;
    public GameObject[] balls;

    private void Start()
    {
        balls = GameObject.FindGameObjectsWithTag("Ball");
    }
    public void KillPlayer()
    {
        GameObject dp = Resources.Load<GameObject>("Objects/DeathParticles");
        print(dp);
        //GameObject deathParticles = GameObject.Find("DeathParticles");
        dp.transform.position = player.transform.position;
        Instantiate(dp);
        //deathParticles.transform.position = player.transform.position;
        //deathParticles.SetActive(true);
        foreach(GameObject ball in balls)
        {
            //ball.GetComponent<Rigidbody2D>().simulated = false;
        }
        Destroy(player);

    }
}
