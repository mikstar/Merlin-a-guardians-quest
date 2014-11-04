using UnityEngine;
using System.Collections;

public class BaseSpell : MonoBehaviour {

	public GameObject allyEffect;
	public GameObject enemyEffect;
	public int targetCount;
	public float coolDown;
	public bool onCooldown;
	private SpriteRenderer buttonGrafic;
		
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

	public virtual void spellEffect(GameObject[] tar)
	{
		StartCoroutine(startCooldown());

		for(int i=0;i<targetCount;i++)
		{
			if(tar[i].tag == "Enemy")
			{
				//when casting a spell on a enemy
				onEnemys(tar[i]);
			}
			else
			{
				//when casting a spell on a ally
				onAllys(tar[i]);
			}

		}
	}

	public virtual void onAllys(GameObject tar)
	{
		
	}

	public virtual void onEnemys(GameObject tar)
	{
		
	}
}
