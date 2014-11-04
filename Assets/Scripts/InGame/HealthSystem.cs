using UnityEngine;
using System.Collections;

public class HealthSystem : MonoBehaviour {

	public int maxHealth;
	public GameObject healthBar;

	private int health;

	void Start()
	{
		health = maxHealth;
		healthBar.renderer.material.color = Color.green;
	}

	public void ChangeHealth(int hpChange)
	{
		if(!(health==0 && hpChange<0))
		{
			health += hpChange;

			if(health > maxHealth)
			{
				health = maxHealth;
			}
			else if(health <= 0)
			{
				GetComponent<CharacterTasker>().spriteObject.GetComponent<Animator>().SetBool("IsDead",true);
				collider2D.enabled = false;
				health = 0;
				if(tag == "Character")
				{
					//Destroy(gameObject);
					GetComponent<PlayerDeathState>().kill();
				}
				else
				{
					Destroy(gameObject,3);
					GetComponent<CharacterTasker>().enabled = false;
					GameObject.FindGameObjectWithTag("EncounterControll").GetComponent<EncounterControll>().enemyDown();
				}
			}

			Vector3 hScale = healthBar.transform.localScale;
			hScale.x = (float)health/(float)maxHealth;
			healthBar.transform.localScale = hScale;
			healthBar.renderer.material.color = Color.Lerp(Color.red,Color.green,(float)health/(float)maxHealth);
		}

	}

}
