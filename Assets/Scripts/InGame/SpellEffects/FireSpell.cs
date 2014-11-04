using UnityEngine;
using System.Collections;

public class FireSpell : BaseSpell {

	public int damage;

	public override void onAllys(GameObject tar)
	{
		//GameObject ffx = Instantiate(Resources.Load("SpellEffects/" + effectNameAlly),tar.transform.position, Quaternion.identity) as GameObject;
		//ffx.transform.parent = tar.transform;
		//ffx.transform.localPosition = Vector2.zero;

		tar.AddComponent<FireEffect>();
		tar.GetComponent<FireEffect>().setEffect(2,5,damage,false,2,allyEffect);
	}
	
	public override void onEnemys(GameObject tar)
	{
		//GameObject ffx = Instantiate(Resources.Load("SpellEffects/" + effectNameEnemy),tar.transform.position, Quaternion.identity) as GameObject;
		//ffx.transform.parent = tar.transform;
		//ffx.transform.localPosition = Vector2.zero;

		tar.AddComponent<FireEffect>();
		tar.GetComponent<FireEffect>().setEffect(2,5,Mathf.RoundToInt(damage*1.3f),true,0,enemyEffect);
	}
}
