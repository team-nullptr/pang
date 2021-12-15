using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
	public void Die()
	{
		GameObject DeathParticles = Resources.Load<GameObject>("Objects/DeathParticles");
		print(DeathParticles);
		DeathParticles.transform.position = gameObject.transform.position;
		Instantiate(DeathParticles);

		Destroy(gameObject);
	}
}
