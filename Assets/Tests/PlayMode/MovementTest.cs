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

		// Move in the direction
		movementManager.Move(direction);

        yield return new WaitForSeconds(1f);

		// Check if the player has moved in the given direction
		if(direction.x > 0)
			Assert.Greater(player.transform.position.x, 0f);
		else
			Assert.Less(player.transform.position.x, 0f);
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

	[UnityTest]
	public IEnumerator Climb() {
		// Instantiate a mock player
		GameObject player = new GameObject();

		// Add components required by movement manager
		player.AddComponent<Rigidbody2D>();
		player.AddComponent<CapsuleCollider2D>();

		// Add the movement manager
		MovementManager movementManager = player.AddComponent<MovementManager>();

		// Create a mock ladder
		GameObject ladder = new GameObject();
		BoxCollider2D ladderCollider = ladder.AddComponent<BoxCollider2D>();
		ladderCollider.size = new Vector2(5f, 1f);
		ladder.tag = "Ladder";

		// Set the player position to 0, 0
		player.transform.position = new Vector3(0f, 0f, 0f);

		// Move in the direction
		movementManager.Move(Vector2.up);

		yield return new WaitForSeconds(1f);

		// Check if the player has climbed up
		Assert.Greater(player.transform.position.y, 0f);
	}
}
