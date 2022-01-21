using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
	public GameObject deathParticles;
	/// <summary>
	/// The player's current health.
	/// </summary>
	public int hp = 3;
	/// <summary>
	/// How long should the player be invulnerable after getting hit.
	/// </summary>
	public const float invulnerabilityTime = 1.5f;
	/// <summary>
	/// The animation to be played after the player gets hit.
	/// </summary>
	public Animation hitAnimation;

	GameController gameController;
	float invulnerabilityTimer;
	AudioSource hitSound;
	Text hpText;

	void Start()
	{
		gameController = GameObject.Find("GameController").GetComponent<GameController>();
		hitSound = GameObject.Find("HitSpeaker").GetComponent<AudioSource>();
		hpText = GameObject.Find("HpText").GetComponent<Text>();

		// Set the HP text to the current hp.
		hpText.text = hp.ToString();

		// Set the hit animation duration to the invulnerability time.
		foreach (AnimationState state in hitAnimation)
		{
			state.speed = hitAnimation.clip.length / invulnerabilityTime;
		}
	}

	void Update()
	{
		if (invulnerabilityTimer > 0)
		{
			invulnerabilityTimer -= Time.deltaTime;
		}
	}

	/// <summary>
	/// Called when the player gets hit. If the player is invulnerable, this method does nothing. If the player is not invulnerable, the player's health is decreased and the player is invulnerable for a short time.
	/// </summary>
	/// <returns>
	/// True if the player loses health, false if he is invincible.
	///</returns>
	public bool GetHurt()
	{
		if (invulnerabilityTimer > 0)
			return false;

		hp--;
		if (hpText != null)
		{
			hpText.text = hp.ToString();
		}

		if (hitSound != null)
		{
			hitSound.Play();
		}

		hitAnimation.Play();

		invulnerabilityTimer = invulnerabilityTime;

		if (hp <= 0)
			Die();

		return true;
	}

	/// <summary>
	/// Called when the player dies.
	/// </summary>
	public void Die()
	{
		Instantiate(deathParticles, transform.position, Quaternion.identity);

		Destroy(gameObject);

		gameController.Lose();
	}
}
