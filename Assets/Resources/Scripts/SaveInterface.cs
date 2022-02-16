using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveInterface : MonoBehaviour
{
    void Start()
    {
		StartCoroutine(SaveAfterTime("test", 1));
		StartCoroutine(LoadAfterTime("test", 2));
    }

	IEnumerator SaveAfterTime(string filename, int time) {
		yield return new WaitForSeconds(time);
		SaveManager.Save(filename);
	}

	IEnumerator LoadAfterTime(string filename, int time) {
		yield return new WaitForSeconds(time);
		SaveManager.Load(filename);
	}

    void Update()
    {
        
    }
}
