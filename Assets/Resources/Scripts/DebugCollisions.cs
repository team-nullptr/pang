using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugCollisions : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D collision)
	{
		Debug.Log(collision.gameObject.tag);
	}
}
