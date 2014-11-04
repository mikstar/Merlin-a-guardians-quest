using UnityEngine;
using System.Collections;

public class CharacterTaskerRange : CharacterTasker {

	public GameObject projectile;
	private BasicProjectile projectileScript;

	protected override void Start()
	{
		base.Start();

		projectileScript = projectile.GetComponent<BasicProjectile>();
		projectileScript.damage = attackDmg;
	}

	protected override IEnumerator attackTarget()
	{
		targetPos = transform.position;
		busy++;
		animator.SetTrigger("Attacking");
	
		float xDiff = transform.position.x-targetEnemy.transform.position.x;
		transform.localScale = new Vector3(xDiff/Mathf.Abs(xDiff),1,1);
		
		GameObject tempTar = targetEnemy;
		
		yield return new WaitForSeconds(attackTimeTillDmg);
		projectileScript.assignNewTarget(tempTar);
		if(breakAttack)
		{
			breakAttack = false;
			animator.SetTrigger("BreakAttack");
			busy--;
		}
		else
		{
			attackWait = true;
			yield return new WaitForSeconds(attackTime);
			if(attackWait)
			{
				busy--;
				attackWait = false;
			}
		}

		
		
	}
}
