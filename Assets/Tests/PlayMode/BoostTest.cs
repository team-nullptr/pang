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
}
