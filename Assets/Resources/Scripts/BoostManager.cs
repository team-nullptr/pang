using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoostManager : MonoBehaviour
{
	/// <summary>
	/// The boosts that can drop from the ball after destruction.
	/// </summary>
	public GameObject[] boosts;

	/// <summary>
	/// The probability of each boost to drop.
	/// </summary>
	public float[] boostProbabilities;

	void Start() {
		// Check if the boost probabilities sum up to 1.
		float sum = 0;
		foreach(float probability in boostProbabilities)
			sum += probability;
		
		if(sum != 1)
			Debug.LogError("Boost probabilities do not sum up to 1.");
	}

	void OnBreak() {
		// Instantiate a random boost according to their probabilities.
		float random = UnityEngine.Random.Range(0f, 1f);
		float sum = 0f;

		for (int i = 0; i < boostProbabilities.Length; i++) {
			sum += boostProbabilities[i];
			if (random < sum) {
				if(boosts[i] != null)
					Instantiate(boosts[i], transform.position, Quaternion.identity);

				break;
			}
		}
	}
}
