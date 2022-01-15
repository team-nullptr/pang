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
	public const float defaultVerticalVelocity = 1f;
	/// <summary>
	/// The horizontal velocity of the ball for the highest layer.
	/// </summary>
	public const float defaultSpeed = 3f;
	/// <summary>
	/// The factor by which the speed is multiplied for the next layer.
	/// </summary>
	public const float speedFactor = 0.75f;
	/// <summary>
	/// The jump height of the ball for the highest layer.
	/// </summary>
	public const float defaultJumpHeight = 6f;
	/// <summary>
	/// The factor by which the jump height is multiplied for the next layer.
	/// </summary>
	public const float jumpHeightFactor = 0.5f;
	/// <summary>
	/// How high should the ball jump after it spawns.
	/// </summary>
	public const float spawnJump = 1f;
	public AudioSource ballShotSound;
	/// <summary>
	/// The default pitch of the sound of breaking the ball.
	/// </summary>
	public const float defaultSoundPitch = 0.5f;
	/// <summary>
	/// The factor by which the pitch of the sound of breaking the ball is multiplied for the next layer.
	/// </summary>
	public float breakSoundPitchFactor = 2f;

	private GameController gameController;

	void Start()
	{
		gameController = GameObject.Find("GameController").GetComponent<GameController>();

		// Scale according to the layer
		transform.localScale = scaleFactor * new Vector3(1f, 1f, 1f) * Mathf.Pow(scaleByLayerFactor, layer);

		// Set the horizontal velocity
		BallMovement ballMovement = GetComponent<BallMovement>();

		ballMovement.speed *= Mathf.Pow(speedFactor, layer);
		ballMovement.jumpAltitude = defaultJumpHeight * Mathf.Pow(jumpHeightFactor, layer);
	}

	public void GetShot()
	{
		if (ballShotSound != null)
		{
			ballShotSound.pitch = defaultSoundPitch * Mathf.Pow(breakSoundPitchFactor, layer);

			ballShotSound.Play();
		}

		BreakBall();
	}

	public void BreakBall()
	{
		// If the ball isn't the smallest size, create two new balls
		if (layer < maxLayer)
		{
			GameObject ball1 = Instantiate(ballPrefab, transform.position, Quaternion.identity);
			GameObject ball2 = Instantiate(ballPrefab, transform.position, Quaternion.identity);

			ball1.GetComponent<BallManager>().layer = layer + 1;
			ball2.GetComponent<BallManager>().layer = layer + 1;

			ball1.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, spawnJump);
			ball2.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, spawnJump);

			ball1.GetComponent<BallMovement>().speed = defaultSpeed;
			ball2.GetComponent<BallMovement>().speed = -defaultSpeed;
		}

		Destroy(gameObject);
	}

	// When a ball hits the bullet, destroy the ball and the bullet
	void OnTriggerEnter2D(Collider2D collider)
	{
		Debug.Log("Ball hit " + collider.gameObject.name);

		switch (collider.tag)
		{
			case "Bullet":
				GetShot();

				collider.gameObject.GetComponent<Bullet>().DestroyBullet();

				break;

			case "BulletTrail":
				GetShot();

				collider.gameObject.transform.parent.parent.GetComponent<Bullet>().DestroyBullet();

				break;

			case "Player":
				// Player hitbox hit
				if (collider.transform.parent.GetComponent<PlayerManager>().GetHurt())
				{
					BreakBall();
				}
				break;
		}
	}
}
