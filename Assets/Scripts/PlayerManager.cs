using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
	public GameObject DeathParticles;

	public void Die()
	{
		Instantiate(DeathParticles, transform.position, Quaternion.identity);

		Destroy(gameObject);
	}
}
