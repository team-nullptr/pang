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

	[UnityTest]
	public IEnumerator DoubleBulletBoostEffect() {
		// Create a mock player
		GameObject player = new GameObject();

		// Give the player componenets required by the weapon manager
		player.AddComponent<CapsuleCollider2D>();

		// Give the player a weapon manager
		WeaponManager weaponManager = player.AddComponent<WeaponManager>();

		weaponManager.collider = player.GetComponent<CapsuleCollider2D>();

		// Give the player a double bullet boost effect
		DoubleBulletBoostEffect doubleBulletBoostEffect = player.AddComponent<DoubleBulletBoostEffect>();
		doubleBulletBoostEffect.duration = 1f;

		// Save the original bullet count
		int originalBulletCount = weaponManager.maxBulletCount;

		yield return new WaitForSeconds(0.5f);

		// Check if the bullet count has been doubled
		Assert.AreEqual(originalBulletCount * 2, weaponManager.maxBulletCount);

		yield return new WaitForSeconds(0.75f);

		// Check if the bullet count has been returned to original
		Assert.AreEqual(originalBulletCount, weaponManager.maxBulletCount);
	}

	[UnityTest]
	public IEnumerator HealthBoost() {
		// Create a mock player
		GameObject player = new GameObject();

		PlayerManager playerManager = player.AddComponent<PlayerManager>();

		// Save the original health
		int originalHealth = playerManager.hp;

		// Create a health boost
		GameObject boost = new GameObject();
		HealthBoost healthBoost = boost.AddComponent<HealthBoost>();

		// Execute the boost
		healthBoost.ExecuteBoost(playerManager);

		// Check if the health has been increased
		Assert.AreEqual(originalHealth + 1, playerManager.hp);

		yield return null;
	}

	[UnityTest]
	public IEnumerator SpeedBoost() {
		// Create a mock player
		GameObject player = new GameObject();

		PlayerManager playerManager = player.AddComponent<PlayerManager>();
		player.AddComponent<CapsuleCollider2D>();

		// Create a speed boost
		GameObject boost = new GameObject();
		SpeedBoost speedBoost = boost.AddComponent<SpeedBoost>();

		// Execute the boost
		speedBoost.ExecuteBoost(playerManager);

		// Check if the player has a speed boost effect on him
		Assert.AreNotEqual(player.GetComponent<SpeedBoostEffect>(), null);

		yield return null;
	}

	[UnityTest]
	public IEnumerator DoubleBulletBoost() {
		// Create a mock player
		GameObject player = new GameObject();

		PlayerManager playerManager = player.AddComponent<PlayerManager>();
		player.AddComponent<CapsuleCollider2D>();

		// Create a double bullet boost
		GameObject boost = new GameObject();
		DoubleBulletBoost doubleBulletBoost = boost.AddComponent<DoubleBulletBoost>();

		// Execute the boost
		doubleBulletBoost.ExecuteBoost(playerManager);

		// Check if the player has a double bullet boost effect on him
		Assert.AreNotEqual(player.GetComponent<DoubleBulletBoostEffect>(), null);

		yield return null;
	}
}
