using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class ArtifactSaveable : Saveable
{
	[System.Serializable]
	struct ArtifactData {
		public float x, y;
		public int points;
		public float animationNormalizedTime;
	}

	const string prefabPath = "Prefabs/Artifact";

    public override MemoryStream Save()
	{
		BinaryFormatter binaryFormatter = SaveManager.GetFormatter();
		MemoryStream memoryStream = new MemoryStream();

		// Serialize the data.
		ArtifactData data = new ArtifactData();
		data.x = transform.position.x;
		data.y = transform.position.y;

		Artifact artifact = GetComponent<Artifact>();
		data.points = artifact.pointsForArtifact;

		Animator animator = GetComponent<Animator>();
		data.animationNormalizedTime = animator.GetCurrentAnimatorStateInfo(0).normalizedTime;

		binaryFormatter.Serialize(memoryStream, data);
		return memoryStream;
	}

	public override void Load(MemoryStream stream)
	{
		// Deserialize the data.
		BinaryFormatter binaryFormatter = SaveManager.GetFormatter();
		stream.Position = 0;
		ArtifactData data = (ArtifactData)binaryFormatter.Deserialize(stream);

		// Reconstruct the object.
		GameObject artifactObject = Instantiate(Resources.Load(prefabPath)) as GameObject;
		artifactObject.transform.position = new Vector3(data.x, data.y, 0);
		Artifact artifact = artifactObject.GetComponent<Artifact>();
		artifact.pointsForArtifact = data.points;
		Animator animator = artifactObject.GetComponent<Animator>();
		animator.Play("artifactAnimation", 0, data.animationNormalizedTime);
	}

	public override void OnLoad()
	{
		// Destroy the existing artifacts to prevent duplicates.
		Destroy(gameObject);
	}
}
