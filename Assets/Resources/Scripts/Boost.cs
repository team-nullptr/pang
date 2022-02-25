using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class Boost : MonoBehaviour
{
	AudioSource boostSound;

	void Start() {
		// Get the boost sound
		GameObject speaker = GameObject.Find("BoostCollectionSpeaker");

		if(speaker != null)
			boostSound = speaker.GetComponent<AudioSource>();
	}

    virtual public void ExecuteBoost(PlayerManager player) {}

	// Check for collision with the player and execute the boost
	void OnTriggerEnter2D(Collider2D other)
	{
		if(other.gameObject.tag != "Player")
			return;

		PlayerManager playerManager = other.gameObject.GetComponent<PlayerManager>();
		if(playerManager == null)
			return;
		
		ExecuteBoost(playerManager);
		PickUpBoost();
	}

	void PickUpBoost() {
		// Play the boost sound
		if(boostSound != null)
			boostSound.Play();

		// Destroy the boost
		Destroy(transform.parent.gameObject);
	}
}
