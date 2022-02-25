using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO: Make saveable
public class SpeedBoost : Boost
{
	/// <summary>
	/// How faster should the player move under the effect of the speed boost.
	/// </summary>
	public float boostFactor = 1.25f;
	/// <summary>
	/// How long should the boost last.
	/// </summary>
	public float duration = 10f;

    override public void ExecuteBoost(PlayerManager playerManager) {
		SpeedBoostEffect speedEffectBoost = playerManager.gameObject.AddComponent<SpeedBoostEffect>();

		speedEffectBoost.boostFactor = boostFactor;
		speedEffectBoost.duration = duration;
	}
}
