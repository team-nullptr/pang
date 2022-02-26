using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleBulletBoostEffect : MonoBehaviour
{
	/// <summary>
	/// How long should the boost last.
	/// </summary>
	public float duration = 10f;

	const string prefabPath = "Prefabs/DoubleBulletBoostImage";
	GameObject boostImage;
	WeaponManager weaponManager;

    void Start()
    {
		// Execute the boost
        weaponManager = GetComponent<WeaponManager>();
		weaponManager.maxBulletCount *= 2;

		// Update UI
		GameObject boostUI = GameObject.Find("BoostUI");
		boostImage = Instantiate(Resources.Load(prefabPath)) as GameObject;
		boostImage.transform.SetParent(boostUI.transform);
    }

    void Update()
    {
        if(duration > 0)
		{
			duration -= Time.deltaTime;
		}
		else
		{
			weaponManager.maxBulletCount /= 2;
			Destroy(boostImage);
			Destroy(this);
		}
    }
}
