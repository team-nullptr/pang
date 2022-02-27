using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

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
	/// <summary>
	/// The image of heart prefab.
	/// </summary>
	public GameObject heartImage;

	GameController gameController;
	float invulnerabilityTimer;
	public float InvulnerabilityTimer
	{
		get
		{
			return invulnerabilityTimer;
		}
		set {
			if(value > invulnerabilityTime)
			{
				invulnerabilityTimer = invulnerabilityTime;
			}
			else
			{
				invulnerabilityTimer = value;
			}
		}
	}
	AudioSource hitSound;
	GameObject hpUI;

	void Start()
	{
		// Get the necessary handles
		GameObject gameControllerObject = GameObject.FindWithTag("GameController");
		if(gameControllerObject != null)
			gameController = gameControllerObject.GetComponent<GameController>();

		GameObject speaker = GameObject.Find("HitSpeaker");

		if(speaker != null)
			hitSound = speaker.GetComponent<AudioSource>();
		
		hpUI = GameObject.Find("HpUI");

		// Update the HP UI
		UpdateHpUI();

		// Set the hit animation duration to the invulnerability time.
		if(hitAnimation != null) {
			foreach (AnimationState state in hitAnimation)
			{
				state.speed = hitAnimation.clip.length / invulnerabilityTime;
			}
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
		UpdateHpUI();

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

	/// <summary>
	/// Updates the HP UI.
	/// </summary>
	public void UpdateHpUI() {
		if(hpUI == null)
			return;
		
		// Destroy all the hearts
		foreach(Transform child in hpUI.transform)
		{
			Destroy(child.gameObject);
		}

		// Add hearts
		for(int i = 0; i < hp; i++)
		{
			GameObject heart = Instantiate(heartImage);
			heart.transform.SetParent(hpUI.transform);
		}
	}
}
