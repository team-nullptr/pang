using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
	public GameController gameController;
	public GameObject deathParticles;
	/// <summary>
	/// The player's current health.
	/// </summary>
	public int hp = 3;
	/// <summary>
	/// HP counter UI.
	/// </summary>
	public Text hpText;
	/// <summary>
	/// How long should the player be invulnerable after getting hit.
	/// </summary>
	public const float invulnerabilityTime = 1.5f;
	public AudioSource hitSound;

	float invulnerabilityTimer;

	void Start()
	{
		if (hpText != null)
		{
			hpText.text = hp.ToString();
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
	/// <returns>True if the player loses health, false if he is invincible.</returns>
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

		invulnerabilityTimer = invulnerabilityTime;

		if (hp <= 0)
		{
			Die();
		}

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
