using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class BoostTest
{
	[UnityTest]
	public IEnumerator SpeedBoostEffect() {
		// Create a mock player
		GameObject player = new GameObject();

		// Give the player a movement manager
		player.AddComponent<CapsuleCollider2D>();
		MovementManager movementManager = player.AddComponent<MovementManager>();

		// Give the player a speed boost effect
		SpeedBoostEffect speedBoostEffect = player.AddComponent<SpeedBoostEffect>();
		speedBoostEffect.duration = 1f;

		// Save the original speed
		float originalSpeed = movementManager.speed;

		yield return new WaitForSeconds(0.5f);

		// Check if the speed has been increased
		Assert.AreEqual(originalSpeed * speedBoostEffect.boostFactor, movementManager.speed);

		yield return new WaitForSeconds(0.75f);

		// Check if the speed has been returned to original
		Assert.AreEqual(originalSpeed, movementManager.speed);
	}
}
