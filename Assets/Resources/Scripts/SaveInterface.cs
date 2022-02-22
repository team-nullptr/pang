using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveInterface : MonoBehaviour
{
    void Start()
    {
		StartCoroutine(SaveAndLoadAfterTime("test", 3));
    }

	IEnumerator SaveAfterTime(string filename, int time) {
		yield return new WaitForSeconds(time);
		SaveManager.Save(filename);
	}

	IEnumerator LoadAfterTime(string filename, int time) {
		yield return new WaitForSeconds(time);
		SaveManager.Load(filename);
	}

	IEnumerator SaveAndLoadAfterTime(string filename, int time) {
		yield return new WaitForSeconds(time);
		SaveManager.Save(filename);
		SaveManager.Load(filename);
	}

    void Update()
    {
        
    }
}
