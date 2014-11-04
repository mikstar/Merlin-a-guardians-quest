using UnityEngine;
using System.Collections;

public class PlayerDeathState : MonoBehaviour {

	public bool isAlive = true;

	public void kill()
	{
		CharacterTasker chartsc = GetComponent<CharacterTasker>();
		chartsc.busy++;
		chartsc.isAlive = false;
		isAlive = false;

		if(gameObject.name == "Merlin")
		{
			//lose state
			Instantiate(Resources.Load("loseScreen"),(Vector2)Camera.main.gameObject.transform.position, Quaternion.identity);

			GameObject[] hur = GameObject.FindGameObjectsWithTag("Enemy");
			foreach(GameObject obj in hur)
			{
				Destroy(obj);
			}
			
			Destroy(GameObject.FindGameObjectWithTag("GameController"));
			Destroy(GameObject.FindGameObjectWithTag("EncounterControll"));
		}
		GameObject.FindGameObjectWithTag("EncounterControll").GetComponent<EncounterControll>().gemReward--;
	}

	public void revive()
	{
		collider2D.enabled = true;
		CharacterTasker chartsc = GetComponent<CharacterTasker>();
		chartsc.spriteObject.GetComponent<Animator>().SetBool("IsDead",false);
		chartsc.busy--;
		chartsc.isAlive = true;
		isAlive = true;
		HealthSystem hs = GetComponent<HealthSystem>();
		hs.ChangeHealth((int)Mathf.Round(hs.maxHealth/4));
	}
}
