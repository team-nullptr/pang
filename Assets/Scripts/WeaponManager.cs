using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
	public KeyCode fireKey = KeyCode.Space;
	public GameObject bulletPrefab;

    // Start is called before the first frame update
    void Start()
    {
        
    }

	void Shoot() {
		GameObject bullet = Instantiate(bulletPrefab, transform.position + new Vector3(0f, 0.1f, 0f), Quaternion.identity);
	}

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(fireKey)) {
			Shoot();
		}
    }
}
