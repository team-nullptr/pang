using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallManagerAlternative : MonoBehaviour
{
	int layer = 0;
	public const int maxLayer = 2;
	public GameObject ballPrefab;
	public const float scaleFactor = 3f;
	private GameController gameController;

	void Start()
	{
		gameController = GameObject.Find("GameController").GetComponent<GameController>();

		// Scale according to the layer
		transform.localScale = scaleFactor * new Vector3(1f, 1f, 1f) / Mathf.Pow(2, layer);
	}

	public void DestroyBall()
	{
		if (layer < maxLayer)
		{
			GameObject ball1 = Instantiate(ballPrefab, transform.position, Quaternion.identity);
			GameObject ball2 = Instantiate(ballPrefab, transform.position, Quaternion.identity);
			ball1.GetComponent<BallManagerAlternative>().layer = layer + 1;
			ball2.GetComponent<BallManagerAlternative>().layer = layer + 1;
			ball2.GetComponent<BallMovement>().speed *= -1;
		}

		Destroy(gameObject);
	}

	void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.collider.tag == "Player")
		{
			collision.collider.GetComponent<PlayerManager>().Die();

			DestroyBall();

			gameController.Lose();
		}
	}
}
