using UnityEngine;
using System.Collections;

public class BackGroundSound : MonoBehaviour {

	// Use this for initialization
	void Start () {
		GetComponent<AudioSource>().clip = Resources.Load("Sounds/"+ PlayerPrefs.GetInt("currentArea"),typeof(AudioClip)) as AudioClip;
		GetComponent<AudioSource>().Play();
	}
}
