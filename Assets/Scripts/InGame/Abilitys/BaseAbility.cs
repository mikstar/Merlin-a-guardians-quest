using UnityEngine;
using System.Collections;

public class BaseAbility : MonoBehaviour {

	public GameObject abilityOwner;
	public float coolDown;
	public bool onCooldown;
	private SpriteRenderer buttonGrafic;
	public float abilityTimetTillDmg;
	public float abilityCastTime;

	void Awake()
	{
		buttonGrafic = GetComponent<SpriteRenderer>();
	}
	
	public virtual IEnumerator startCooldown()
	{
		buttonGrafic.color = Color.red;
		onCooldown = true;
		yield return new WaitForSeconds(coolDown);
		onCooldown = false;
		buttonGrafic.color = Color.white;
	}

	public virtual IEnumerator useAbility()
	{
		StartCoroutine(startCooldown());
		StartCoroutine(abilityOwner.GetComponent<CharacterTasker>().UseAbility(abilityCastTime,abilityTimetTillDmg));
		yield return new WaitForSeconds(abilityTimetTillDmg);
		abilityFfct();
	}
	protected virtual void abilityFfct()
	{

	}
	public void setLock(bool isUnLocked)
	{
		gameObject.collider2D.enabled = isUnLocked;
	}
}
