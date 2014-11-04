using UnityEngine;
using System.Collections;

public class EnemyAI : MonoBehaviour {

	private GameObject[] allTargets;
	private CharacterTasker charTasker;
	private int indexMainTarget;
	public float aggrorange;

	// Use this for initialization
	void Start () {
		//save all characters that this enemy will want to attack
		allTargets = GameObject.FindGameObjectsWithTag("Character");
		//set a reference to the CharacterTasker of this enemy so commands can be set
		charTasker = GetComponent<CharacterTasker>();

		//choose closest target to attack
		chooseTarget();
	}
	
	// Update is called once per frame
	void Update () {
		chooseTarget();
	}

	private void chooseTarget()
	{
		
		//var to keep track of how close the closest target so far is
		float lowstDist;
		if(allTargets[indexMainTarget] && allTargets[indexMainTarget].GetComponent<PlayerDeathState>().isAlive)
		{
			lowstDist = Vector3.Distance(transform.position,allTargets[indexMainTarget].transform.position);
		}
		else 
		{
			lowstDist = 100;
		}


		float tempAggroRange = aggrorange;
		//Check what Character is closest
		for(int i= 0; i < allTargets.Length; i++)
		{
			if(allTargets[i].GetComponent<PlayerDeathState>().isAlive && Vector3.Distance(transform.position,allTargets[i].transform.position) < lowstDist-tempAggroRange)
			{
				indexMainTarget = i;
				tempAggroRange = 0;
			}
		}

		charTasker.setAttackCommand(allTargets[indexMainTarget]);
	}
}
