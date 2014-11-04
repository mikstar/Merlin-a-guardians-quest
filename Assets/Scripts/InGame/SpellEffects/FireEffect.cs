using UnityEngine;
using System.Collections;

public class FireEffect : MonoBehaviour {

	private int damage;
	private float duration;
	private float timeAlive = 0;
	private int charges;
	private int atCharge = 0;
	private bool onEnemy;
	private float aoERange;
	private GameObject effect;
	

	public void setEffect(int dmg,float drtn,int chrgs,bool isEnemy,float aoe, GameObject ffx)
	{
		damage = dmg;
		duration = drtn;
		charges = chrgs;
		onEnemy = isEnemy;
		aoERange = aoe;
		effect = ffx;
		effect.transform.position = transform.position;
		effect.SetActive(true);
		effect.transform.GetChild(0).gameObject.GetComponent<LayerSorter>().sortByY(1);
	}

	void Update()
	{
		timeAlive += Time.deltaTime;

		if(timeAlive > (atCharge+1)*(duration/charges))
		{
			atCharge++;
			
			effect.transform.position = transform.position;
			effect.transform.GetChild(0).gameObject.GetComponent<LayerSorter>().sortByY(1);
			//deal dmg here
			if(onEnemy)
			{
				GetComponent<HealthSystem>().ChangeHealth(-damage);
			}
			else
			{
				GameObject[] enemys = GameObject.FindGameObjectsWithTag("Enemy");
				for(int i = 0; i < enemys.Length ;i++)
				{
					if(Vector2.Distance(enemys[i].transform.position,transform.position) < aoERange)
					{
						enemys[i].GetComponent<HealthSystem>().ChangeHealth(-damage);
					}
				}
			}


			if(atCharge == charges)
			{
				//when all charger have passed
				effect.SetActive(false);
				Destroy(this);
			}
		}
	}
	void OnDestroy()
	{
		effect.SetActive(false);
	}
}
