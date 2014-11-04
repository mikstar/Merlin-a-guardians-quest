using UnityEngine;
using System.Collections;

public class FollowerScript : MonoBehaviour {

	private GameObject merlin;
	private GameObject[] followers;
	private bool following = false;

	// Use this for initialization
	void Start () {
		GameObject[] allChars = GameObject.FindGameObjectsWithTag("Character");

		followers = new GameObject[allChars.Length-1];
		int fc=0;
		foreach(GameObject tar in allChars)
		{
			if(tar.name == "Merlin")
			{
				merlin = tar;
			}
			else
			{
				followers[fc] = tar;
				fc++;
			}
		}
		startFollow();
	}
	
	public void assignTargets(GameObject[] fllwrs, GameObject merln)
	{
		followers = fllwrs;
		merlin = merln;
	}

	public void startFollow()
	{
		StartCoroutine(followCheck());
		following = true;

		foreach(GameObject fllwr in followers)
		{
			fllwr.collider2D.enabled = false;
		}
	}

	public void endFollow()
	{
		following = false;

		foreach(GameObject fllwr in followers)
		{
			fllwr.collider2D.enabled = true;
		}
	}

	private IEnumerator followCheck()
	{
		yield return new WaitForSeconds(0.3f);
		int i=0;
		foreach(GameObject fllwr in followers)
		{

			Vector2 tarPos = (Vector2)merlin.transform.position + new Vector2(1 - (i*2) ,1 - (i*2));
			if(Vector2.Distance(fllwr.transform.position,tarPos) > 1f)
			{
				fllwr.GetComponent<CharacterTasker>().setMoveCommand(tarPos);
			}
			i++;
		}
		if(following)
		{
			StartCoroutine(followCheck());
		}
	}
}





