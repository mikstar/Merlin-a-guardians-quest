using UnityEngine;
using System.Collections;

public class ArcaneSpell : BaseSpell {

	public float aoERange;
	public int damage;

	public override void onAllys(GameObject tar)
	{
		GameObject[] enemys = GameObject.FindGameObjectsWithTag("Enemy");
		allyEffect.transform.position = tar.transform.position;
		allyEffect.SetActive(false);
		allyEffect.SetActive(true);
		allyEffect.transform.GetChild(0).gameObject.GetComponent<LayerSorter>().sortByY(1);
		for(int i = 0; i < enemys.Length ;i++)
		{
			if(Vector2.Distance(enemys[i].transform.position,tar.transform.position) < aoERange)
			{
				enemys[i].GetComponent<HealthSystem>().ChangeHealth(-(Mathf.RoundToInt(damage*0.33f)));
			}
		}
	}
	
	public override void onEnemys(GameObject tar)
	{
		tar.GetComponent<HealthSystem>().ChangeHealth(-damage);
		enemyEffect.transform.position = tar.transform.position;
		enemyEffect.SetActive(false);
		enemyEffect.SetActive(true);
		enemyEffect.transform.GetChild(0).gameObject.GetComponent<LayerSorter>().sortByY(1);
	}
}
