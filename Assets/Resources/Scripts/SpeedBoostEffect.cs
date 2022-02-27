using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MovementManager))]
public class SpeedBoostEffect : MonoBehaviour
{
	/// <summary>
	/// How faster should the player move under the effect of the speed boost.
	/// </summary>
	public float boostFactor = 1.5f;
	/// <summary>
	/// How long should the boost last.
	/// </summary>
	public float duration = 10f;

	const string prefabPath = "Prefabs/SpeedBoostImage";
	GameObject boostImage;
	MovementManager movementManager;

    void Start()
    {
		// Execute the boost
        movementManager = GetComponent<MovementManager>();
		movementManager.speed *= boostFactor;

		// Update UI
		GameObject boostUI = GameObject.Find("BoostUI");

		if(boostUI != null) {
			boostImage = Instantiate(Resources.Load(prefabPath)) as GameObject;
			boostImage.transform.SetParent(boostUI.transform);
		}
    }

    void Update()
    {
        if(duration > 0)
		{
			duration -= Time.deltaTime;
		}
		else
		{
			movementManager.speed /= boostFactor;
			Destroy(boostImage);
			Destroy(this);
		}
    }
}
