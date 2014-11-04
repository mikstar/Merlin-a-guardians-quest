using UnityEngine;
using System.Collections;

public class QuickTest : MonoBehaviour {

	public bool test;

	// Update is called once per frame
	void Update () {
		if(test)
		{
			gameObject.SetActive(false);
			gameObject.SetActive(true);
			test = false;
		}
	}
}
