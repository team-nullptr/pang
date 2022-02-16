using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public abstract class Savable : MonoBehaviour
{
    public abstract MemoryStream Save();
	public abstract void Load(MemoryStream saveData);
}
