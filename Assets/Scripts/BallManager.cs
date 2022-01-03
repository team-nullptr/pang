using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallManager : MonoBehaviour
{
	/// <summary>
	/// The bigger the number, the smaller the ball.
	/// </summary>
	int layer = 0;
	/// <summary>
	/// The biggest layer a ball could get before getting destroyed (the smallest ball size).
	/// </summary>
	public const int maxLayer = 2;
	/// <summary>
	/// The ball prefab.
	/// </summary>
	public GameObject ballPrefab;
	/// <summary>
	/// The size of the biggest ball.
	/// </summary>
	public const float scaleFactor = 3f;
	/// <summary>
	/// How smaller is the ball in the next layer.
	/// </summary>
	public const float scaleByLayerFactor = 0.5f;
	/// <summary>
	/// The vertical velocity of the ball for the highest layer.
	/// </summary>
	public const float defaultHorizontalVelocity = 1f;
	/// <summary>
	/// The jump height of the ball for the highest layer.
	/// </summary>
	public const float defaultJumpHeight = 6f;
	/// <summary>
	/// The factor by which the jump height is multiplied for the next layer.
	/// </summary>
	public const float jumpHeightFactor = 0.5f;

	private GameController gameController;

	void Start()
	{
		gameController = GameObject.Find("GameController").GetComponent<GameController>();

		// Scale according to the layer
		transform.localScale = scaleFactor * new Vector3(1f, 1f, 1f) * Mathf.Pow(scaleByLayerFactor, layer);

		// Set the horizontal velocity
		GetComponent<BallMovement>().jumpAltitude = defaultJumpHeight * Mathf.Pow(jumpHeightFactor, layer);
	}

	public void DestroyBall()
	{
		if (layer < maxLayer)
		{
			GameObject ball1 = Instantiate(ballPrefab, transform.position, Quaternion.identity);
			GameObject ball2 = Instantiate(ballPrefab, transform.position, Quaternion.identity);

			ball1.GetComponent<BallManager>().layer = layer + 1;
			ball2.GetComponent<BallManager>().layer = layer + 1;

			ball1.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, 1f);
			ball2.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, 1f);

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

	void OnTriggerEnter2D(Collider2D collider)
	{
		switch (collider.tag)
		{
			case "Bullet":
				DestroyBall();

				collider.gameObject.GetComponent<Bullet>().DestroyBullet();

				break;

			case "BulletTrail":
				DestroyBall();

				collider.gameObject.transform.parent.GetComponent<Bullet>().DestroyBullet();

				break;
		}
	}
}
