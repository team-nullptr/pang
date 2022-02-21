using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public abstract class Saveable : MonoBehaviour
{
    public abstract MemoryStream Save();
	public abstract void Load(MemoryStream saveData);

	/// <summary>
	/// Called on all savables when the scene is loaded.
	/// </summary>
	public abstract void OnLoad();
}
