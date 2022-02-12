using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallManager : MonoBehaviour
{
	/// <summary>
	/// The bigger the number, the smaller the ball.
	/// </summary>
	public int layer = 0;
	/// <summary>
	/// The biggest layer a ball could get before getting destroyed (the smallest ball size).
	/// </summary>
	public const int maxLayer = 2;
	/// <summary>
	/// The ball prefab.
	/// </summary>
	public BallManager ballPrefab;
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
	public const float spawnJump = 5f;
	/// <summary>
	/// The points player gets for the ball of the highest layer.
	/// </summary>
	public const int defaultPoints = 3000;
	/// <summary>
	/// The factor by which the points are multiplied for the next layer.
	/// </summary>
	public const float pointsFactor = 0.5f;
	/// <summary>
	/// The prefab of the points text to be shown after the ball is destroyed.
	/// </summary>
	public PointsAnimation pointsPrefab;
	/// <summary>
	/// The default pitch of the sound of breaking the ball.
	/// </summary>
	public const float defaultSoundPitch = 0.5f;
	/// <summary>
	/// The factor by which the pitch of the sound of breaking the ball is multiplied for the next layer.
	/// </summary>
	public float breakSoundPitchFactor = 1f;
	/// <summary>
	/// The sprites of the balls.
	/// </summary>
	public Sprite[] sprites;
	public BallMovement ballMovement;

	AudioSource ballShotSound;
	GameController gameController;
	PointsManager pointsManager;
	bool destroyed = false;

	void Start()
	{
		GameObject gameControllerObject = GameObject.Find("GameController");
		gameController = gameControllerObject.GetComponent<GameController>();
		pointsManager = gameControllerObject.GetComponent<PointsManager>();
		ballShotSound = GameObject.Find("BallBreakingSpeaker").GetComponent<AudioSource>();

		// Scale according to the layer
		transform.localScale = scaleFactor * new Vector3(1f, 1f, 1f) * Mathf.Pow(scaleByLayerFactor, layer);

		// Set the horizontal velocity
		ballMovement.speed *= Mathf.Pow(speedFactor, layer);
		ballMovement.jumpAltitude = defaultJumpHeight * Mathf.Pow(jumpHeightFactor, layer);

		// Set the ball sprite
		if(layer < sprites.Length)
			GetComponent<SpriteRenderer>().sprite = sprites[layer];
		else
			GetComponent<SpriteRenderer>().sprite = sprites[sprites.Length - 1];
	}

	public void GetShot()
	{
		// Play the ball breaking sound
		if (ballShotSound != null)
		{
			ballShotSound.pitch = defaultSoundPitch * Mathf.Pow(breakSoundPitchFactor, layer);

			ballShotSound.Play();
		}

		// Give player points
		if(pointsManager != null) {
			int points = (int)(defaultPoints * Mathf.Pow(pointsFactor, layer));

			pointsManager.Points += points;

			PointsAnimation pointsAnimation = Instantiate(
				pointsPrefab,
				transform.position + (Vector3)Random.insideUnitCircle * transform.localScale.x * 2f, // To add a little variety
				Quaternion.identity
			);

			pointsAnimation.text = "+" + points;
		}

		// Destroy the ball
		BreakBall();
	}

	public void BreakBall()
	{
		if(destroyed)
			return;

		// If the ball isn't the smallest size, create two new balls
		if (layer < maxLayer)
		{
			BallManager ball1 = Instantiate(ballPrefab, transform.position, Quaternion.identity);
			BallManager ball2 = Instantiate(ballPrefab, transform.position, Quaternion.identity);

			BallMovement ballMovement1 = ball1.GetBallMovement();
			BallMovement ballMovement2 = ball2.GetBallMovement();

			ball1.layer = layer + 1;
			ball2.layer = layer + 1;

			ballMovement1.speed = ballMovement.speed;
			ballMovement2.speed = -ballMovement.speed;

			ballMovement1.Jump(spawnJump);
			ballMovement2.Jump(spawnJump);
		}

		// Destroy the ball
		Destroy(gameObject);
		destroyed = true;
	}

	public BallMovement GetBallMovement()
	{
		return ballMovement;
	}
	void OnTriggerEnter2D(Collider2D collider)
	{
		if(collider.tag != "Player")
			return;
		
		// Player hitbox hit
		if (collider.transform.parent.GetComponent<PlayerManager>().GetHurt())
		{
			BreakBall();
		}
	}

	// When a ball hits the bullet, destroy the ball and the bullet
	void OnCollisionEnter2D(Collision2D collision) {
		switch (collision.gameObject.tag)
		{
			case "Bullet":
				GetShot();

				collision.gameObject.GetComponent<BulletDestruction>().DestroyBullet();

				break;

			case "BulletTrail":
				GetShot();

				collision.gameObject.transform.parent.parent.GetComponent<BulletDestruction>().DestroyBullet();

				break;
		}
	}
}
