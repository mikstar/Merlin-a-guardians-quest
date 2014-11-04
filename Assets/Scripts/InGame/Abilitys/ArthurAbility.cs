using UnityEngine;
using System.Collections;

public class ArthurAbility : BaseAbility {

	public int damage;
	public float aoERange;

	protected override void abilityFfct()
	{
		//base.useAbility();

		GameObject[] enemys = GameObject.FindGameObjectsWithTag("Enemy");
		for(int i = 0; i < enemys.Length ;i++)
		{
			if(Vector2.Distance(enemys[i].transform.position,abilityOwner.transform.position) < aoERange)
			{
				enemys[i].GetComponent<HealthSystem>().ChangeHealth(-damage);
			}
		}
	}
}
