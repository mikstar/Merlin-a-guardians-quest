using UnityEngine;
using System.Collections;

public class TeleportSpell : BaseSpell {


	public override void spellEffect(GameObject[] tar)
	{
		StartCoroutine(startCooldown());

		Vector3 pos = tar[0].transform.position;

		tar[0].transform.position = tar[1].transform.position;
		tar[1].transform.position = pos;

		tar[0].GetComponent<CharacterTasker>().spriteObject.GetComponent<LayerSorter>().sortByY(0);
		tar[1].GetComponent<CharacterTasker>().spriteObject.GetComponent<LayerSorter>().sortByY(0);

		allyEffect.transform.position = tar[1].transform.position;
		allyEffect.SetActive(false);
		allyEffect.SetActive(true);
		allyEffect.transform.GetChild(0).gameObject.GetComponent<LayerSorter>().sortByY(1);

		
		enemyEffect.transform.position = tar[0].transform.position;
		enemyEffect.SetActive(false);
		enemyEffect.SetActive(true);
		enemyEffect.transform.GetChild(0).gameObject.GetComponent<LayerSorter>().sortByY(1);

	}
}
