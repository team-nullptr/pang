using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointsAnimation : MonoBehaviour
{
	/// <summary>
	/// Frame rate of the points animation.
	/// </summary>
	public const float frameRate = 30f;
	/// <summary>
	/// How high is the text supposed to go before it disappears.
	/// </summary>
	public const float distance = 1f;
	public string text {
		get {
			return _text;
		}
		set {
			_text = value;
			textMesh.text = _text;
		}
	}
	public TextMesh textMesh;

	string _text;

    void Start()
    {
        StartCoroutine(FadeOut());
    }

	IEnumerator FadeOut() {
		for (float alpha = 1f; alpha >= 0; alpha -= 1 / frameRate) {
			transform.position += new Vector3(0, distance / frameRate, 0);

			Color newTextMeshColor = textMesh.color;

			newTextMeshColor.a = alpha;

			textMesh.color = newTextMeshColor;

			yield return new WaitForSeconds(1 / frameRate);
		}

		Destroy(gameObject);
	}
}
