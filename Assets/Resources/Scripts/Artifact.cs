using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Artifact : MonoBehaviour
{
	/// <summary>
	/// How many points should the player get for collecting the artifact.
	/// </summary>
	public int pointsForArtifact = 5000;
	public PointsAnimation pointsPrefab;

	AudioSource artifactSound;
	PointsManager pointsManager;

	bool collected = false;

	void Start() {
		pointsManager = FindObjectOfType<PointsManager>();

		GameObject artifactSoundObject = GameObject.Find("ArtifactCollectionSpeaker");
		if(artifactSoundObject != null)
			artifactSound = artifactSoundObject.GetComponent<AudioSource>();
	}

    void OnTriggerEnter2D(Collider2D collider) {
		if(collected)
			return;

		if(collider.gameObject.tag == "Player") {
			if(pointsManager != null)
				pointsManager.Score += pointsForArtifact;

			if(pointsPrefab != null) {
				PointsAnimation points = Instantiate(pointsPrefab, transform.position + (Vector3)Random.insideUnitCircle * transform.localScale.x * 2f, Quaternion.identity);

				points.text = "+" + pointsForArtifact;
			}

			if(artifactSound != null)
				artifactSound.Play();

			collected = true;
			Destroy(gameObject);
		}
	}
}
