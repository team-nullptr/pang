using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class MovementTest
{
	public IEnumerator Move(Vector2 direction) {
		// Instantiate a mock player
        GameObject player = new GameObject();

		// Add components required by movement manager
		player.AddComponent<Rigidbody2D>();
		player.AddComponent<CapsuleCollider2D>();

		// Add the movement manager
		MovementManager movementManager = player.AddComponent<MovementManager>();

		// Move right
		movementManager.Move(Vector2.right);

        yield return new WaitForSeconds(1f);

		// Check if the player has moved right
		Assert.Greater(player.transform.position.x, 0f);
	}

    [UnityTest]
    public IEnumerator MoveRight()
    {
		yield return Move(Vector2.right);
    }

    [UnityTest]
    public IEnumerator MoveLeft()
    {
		yield return Move(Vector2.left);
    }
}
