using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
	/// <summary>
	/// The speed of the platform.
	/// </summary>
	public float speed = 2f;
	/// <summary>
	/// The points to which the platform will move.
	/// </summary>
	public GameObject[] waypoints;
	/// <summary>
	/// The current waypoint.
	/// </summary>
	public int currentWaypoint = 0;

	void Start() {
		// Check if the platform has waypoints.
		if (waypoints.Length == 0) {
			Debug.LogError("The platform has no waypoints.");
			this.enabled = false;
		}
	}

	void Update()
	{
		// Move the platform to the next waypoint.
		Vector3 target = waypoints[currentWaypoint].transform.position;
		transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);

		// If the platform has reached the current waypoint, move to the next one.
		if(transform.position == target)
		{
			currentWaypoint = (currentWaypoint + 1) % waypoints.Length;
		}
	}
}
