using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBoost : Boost
{
    override public void ExecuteBoost(PlayerManager playerManager) {
		if(playerManager == null) {
			Debug.LogError("PlayerManager is null");
			return;
		}

		playerManager.hp += 1;
		playerManager.UpdateHpUI();
	}
}
