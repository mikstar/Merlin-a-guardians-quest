using UnityEngine;
using System.Collections;

public class MapScreen : MonoBehaviour {

	public GameObject[] areaLockIndicators;



	// Use this for initialization
	void Start () {
		foreach(Transform obj in transform)
		{
			if(obj.gameObject.tag == "MenuButton")
			{
				LevelSelectButton btnScript = obj.gameObject.GetComponent<LevelSelectButton>();

				if(PlayerPrefs.GetInt("Level" + btnScript.area + "-" + btnScript.level) < 0)
				{
					obj.gameObject.SetActive(false);
				}
			}
		}

		
		for(int i=0;i<6;i++)
		{
			if(PlayerPrefs.GetInt("Level" + (i+2) + "-1") < 0)
			{
				areaLockIndicators[i].SetActive(true);
			}
		}
	}

}
