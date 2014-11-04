using UnityEngine;
using System.Collections;

public class LeilaAbilty : BaseAbility {

	public int damage;
	public GameObject projectile;
	private BasicProjectile projectileScript;

	private void Start()
	{	
		projectileScript = projectile.GetComponent<BasicProjectile>();
		projectileScript.damage = damage;
	}
	public override IEnumerator useAbility()
	{
		StartCoroutine(startCooldown());
		StartCoroutine(abilityOwner.GetComponent<CharacterTasker>().UseAbility(abilityCastTime,abilityTimetTillDmg));
		yield return new WaitForSeconds(abilityTimetTillDmg);
		abilityFfct();
	}
	protected override void abilityFfct()
	{
		//base.useAbility();
		GameObject tar = null;
		float hghDist = 0;
		GameObject[] enemys = GameObject.FindGameObjectsWithTag("Enemy");
		for(int i = 0; i < enemys.Length ;i++)
		{
			float dist = Vector2.Distance(enemys[i].transform.position,abilityOwner.transform.position);
			if(dist > hghDist)
			{
				hghDist = dist;
				tar = enemys[i];
			}
		}
		if(tar)
		{
			float dist = abilityOwner.transform.position.x - tar.transform.position.x;
			abilityOwner.transform.localScale = new Vector2(dist/Mathf.Abs(dist),1);
			projectileScript.assignNewTarget(tar);
		}

	}
}
