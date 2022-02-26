using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleBulletBoost : Boost
{
	/// <summary>
	/// How long should the boost last.
	/// </summary>
	public float duration;

    override public void ExecuteBoost(PlayerManager playerManager) {
		GameObject player = playerManager.gameObject;
		DoubleBulletBoostEffect boostEffect = player.GetComponent<DoubleBulletBoostEffect>();

		// If the effect isn't active, add it to the player
		if(boostEffect == null) {
			boostEffect = player.AddComponent<DoubleBulletBoostEffect>();
		} else {
			// If the effect is active, reset the duration
			if(boostEffect.duration < duration)
				boostEffect.duration = duration;
		}
	}
}
