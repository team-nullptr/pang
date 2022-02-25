using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedBoostEffect : MonoBehaviour
{
	/// <summary>
	/// How faster should the player move under the effect of the speed boost.
	/// </summary>
	public float boostFactor = 1.5f;
	/// <summary>
	/// How long should the boost last.
	/// </summary>
	public float duration = 2f;

	MovementManager movementManager;

    void Start()
    {
        movementManager = GetComponent<MovementManager>();
		movementManager.speed *= boostFactor;
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
			Destroy(this);
		}
    }
}
