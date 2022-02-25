using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveInterface : MonoBehaviour
{
	public void Save(string filename)
	{
		SaveManager.Save(filename);
	}

	public void Load(string filename)
	{
		SaveManager.Load(filename);
	}

	public void Delete(string filename)
	{
		SaveManager.Delete(filename);
	}
}
