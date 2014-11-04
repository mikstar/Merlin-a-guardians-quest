using UnityEngine;
using System.Collections;

public class GemText : MonoBehaviour {

	TextMesh mesh;
	// Use this for initialization
	void Start () {
		mesh = GetComponent<TextMesh>();
	}
	
	// Update is called once per frame
	void Update () {
		mesh.text = "X" + PlayerPrefs.GetInt("Gems");
	}
}
