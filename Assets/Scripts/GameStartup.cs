using UnityEngine;
using System.Collections;

public class GameStartup : MonoBehaviour {

	public GameObject fadeScreen;

	// Use this for initialization
	void Start () {

		//cheat for full unlock
		//PlayerPrefs.DeleteAll();
		/*
		PlayerPrefs.SetInt("firstStartUp",1);
		PlayerPrefs.SetInt("Gems",99);

		PlayerPrefs.SetInt("arthur",1);
		PlayerPrefs.SetInt("leila",1);
		PlayerPrefs.SetInt("lance",1);
		PlayerPrefs.SetInt("shen",1);
		PlayerPrefs.SetInt("mimi",1);

		PlayerPrefs.SetInt("fire",3);
		PlayerPrefs.SetInt("arcane",3);
		PlayerPrefs.SetInt("teleport",1);

		PlayerPrefs.SetInt("Level1-1",3);
		PlayerPrefs.SetInt("Level1-2",3);
		PlayerPrefs.SetInt("Level1-3",3);
		PlayerPrefs.SetInt("Level2-1",2);
		PlayerPrefs.SetInt("Level2-2",2);
		PlayerPrefs.SetInt("Level2-3",1);
		PlayerPrefs.SetInt("Level3-1",1);
		PlayerPrefs.SetInt("Level3-2",1);
		PlayerPrefs.SetInt("Level3-3",1);
		PlayerPrefs.SetInt("Level4-1",1);
		PlayerPrefs.SetInt("Level4-2",1);
		PlayerPrefs.SetInt("Level4-3",1);
		PlayerPrefs.SetInt("Level5-1",1);
		PlayerPrefs.SetInt("Level5-2",1);
		PlayerPrefs.SetInt("Level5-3",1);
		PlayerPrefs.SetInt("Level6-1",1);
		PlayerPrefs.SetInt("Level6-2",1);
		PlayerPrefs.SetInt("Level6-3",1);
		PlayerPrefs.SetInt("Level7-1",1);
		PlayerPrefs.SetInt("Level7-2",0);
		*/

		if(PlayerPrefs.GetInt("firstStartUp2") != 1)
		{
			PlayerPrefs.DeleteAll();
			PlayerPrefs.SetInt("firstStartUp2",1);

			PlayerPrefs.SetInt("arthur",-1);
			PlayerPrefs.SetInt("leila",-1);
			PlayerPrefs.SetInt("lance",-1);
			PlayerPrefs.SetInt("mimi",-1);
			PlayerPrefs.SetInt("shen",-1);

			PlayerPrefs.SetInt("merlin0",1);
			PlayerPrefs.SetInt("merlin1",0);
			PlayerPrefs.SetInt("merlin2",0);
			
			PlayerPrefs.SetInt("fire",1);
			PlayerPrefs.SetInt("arcane",-1);
			PlayerPrefs.SetInt("teleport",-1);
			
			PlayerPrefs.SetInt("Level1-1",0);
			PlayerPrefs.SetInt("Level1-2",-1);
			PlayerPrefs.SetInt("Level1-3",-1);
			PlayerPrefs.SetInt("Level2-1",-1);
			PlayerPrefs.SetInt("Level2-2",-1);
			PlayerPrefs.SetInt("Level2-3",-1);
			PlayerPrefs.SetInt("Level3-1",-1);
			PlayerPrefs.SetInt("Level3-2",-1);
			PlayerPrefs.SetInt("Level3-3",-1);
			PlayerPrefs.SetInt("Level4-1",-1);
			PlayerPrefs.SetInt("Level4-2",-1);
			PlayerPrefs.SetInt("Level4-3",-1);
			PlayerPrefs.SetInt("Level5-1",-1);
			PlayerPrefs.SetInt("Level5-2",-1);
			PlayerPrefs.SetInt("Level5-3",-1);
			PlayerPrefs.SetInt("Level6-1",-1);
			PlayerPrefs.SetInt("Level6-2",-1);
			PlayerPrefs.SetInt("Level6-3",-1);
			PlayerPrefs.SetInt("Level7-1",-1);
			PlayerPrefs.SetInt("Level7-2",-1);
		}

		StartCoroutine(fadeout());
	}

	private IEnumerator fadeout()
	{
		SpriteRenderer temp = fadeScreen.GetComponent<SpriteRenderer>();
		Color tempcol = Color.white;
		tempcol.a = 0;
		temp.color = tempcol;

		yield return new WaitForSeconds(0.1f);
		tempcol.a = 0.1f;
		temp.color = tempcol;
		yield return new WaitForSeconds(0.1f);
		tempcol.a = 0.2f;
		temp.color = tempcol;
		yield return new WaitForSeconds(0.1f);
		tempcol.a = 0.3f;
		temp.color = tempcol;
		yield return new WaitForSeconds(0.1f);
		tempcol.a = 0.4f;
		temp.color = tempcol;
		yield return new WaitForSeconds(0.1f);
		tempcol.a = 0.5f;
		temp.color = tempcol;
		yield return new WaitForSeconds(0.1f);
		tempcol.a = 0.6f;
		temp.color = tempcol;
		yield return new WaitForSeconds(0.1f);
		tempcol.a = 0.7f;
		temp.color = tempcol;
		yield return new WaitForSeconds(0.1f);
		tempcol.a = 0.8f;
		temp.color = tempcol;
		yield return new WaitForSeconds(0.1f);
		tempcol.a = 0.9f;
		temp.color = tempcol;
		yield return new WaitForSeconds(0.1f);
		tempcol.a = 1;
		temp.color = tempcol;
		yield return new WaitForSeconds(2);
		tempcol.a = 0.9f;
		temp.color = tempcol;
		yield return new WaitForSeconds(0.1f);
		tempcol.a = 0.8f;
		temp.color = tempcol;
		yield return new WaitForSeconds(0.1f);
		tempcol.a = 0.7f;
		temp.color = tempcol;
		yield return new WaitForSeconds(0.1f);
		tempcol.a = 0.6f;
		temp.color = tempcol;
		yield return new WaitForSeconds(0.1f);
		tempcol.a = 0.5f;
		temp.color = tempcol;
		yield return new WaitForSeconds(0.1f);
		tempcol.a = 0.4f;
		temp.color = tempcol;
		yield return new WaitForSeconds(0.1f);
		tempcol.a = 0.3f;
		temp.color = tempcol;
		yield return new WaitForSeconds(0.1f);
		tempcol.a = 0.2f;
		temp.color = tempcol;
		yield return new WaitForSeconds(0.1f);
		tempcol.a = 0.1f;
		temp.color = tempcol;
		yield return new WaitForSeconds(0.1f);
		tempcol.a = 0;
		temp.color = tempcol;


		Instantiate(Resources.Load("hub"),Vector2.zero, Quaternion.identity);
		Destroy(gameObject);
	}
}
