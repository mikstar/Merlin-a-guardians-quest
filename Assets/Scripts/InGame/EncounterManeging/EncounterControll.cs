using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EncounterControll : MonoBehaviour {

	private Encounter[] encounters;
	private int encounterIndex = 0;
	private int waveIndex = 0;
	private int enemyCount = 0;
	private Transform playerTrans;
	private CameraControl camScript;
	private FollowerScript followScript;
	private GameObject[] HealFfxs;

	public int gemReward = 3;

	public string[] unlockNames;


	// Use this for initialization
	void Start () {
		//temp loop to link Merlin to Camera
		GameObject[] allChars = GameObject.FindGameObjectsWithTag("Character");
		HealFfxs = new GameObject[allChars.Length];
		int g = 0;
		foreach(GameObject obj in allChars)
		{
			HealFfxs[g] = Instantiate(Resources.Load("HealEffect"),Vector2.zero, Quaternion.identity) as GameObject;

			if(obj.name == "Merlin")
			{
				playerTrans = obj.transform;
			}
			g++;
		}


		followScript = GetComponent<FollowerScript>();
		//sets the array to the ammount of encounters
		encounters = new Encounter[transform.childCount];
		//loops along encounters
		int i = 0;
		foreach(Transform encount in transform)
		{
			encounters[i] = new Encounter();
			encounters[i].xPos = encount.position.x;
			encounters[i].backGrounds = encount.GetComponent<EncounterBckGrndHold>().backGrounds;
			//set the array in the encounter to the ammount of waves
			encounters[i].waves = new Wave[encount.childCount];
			//loops along waves in this encounter
			int j = 0;
			foreach(Transform wav in encount)
			{
				encounters[i].waves[j] = new Wave();
				//set the array in the wave to the ammount of enemys
				encounters[i].waves[j].enemySpawns = new EnemySpwInfo[wav.childCount];
				//loops along enemy spawn info in this wave
				int k = 0;
				foreach(Transform swn in wav)
				{
					encounters[i].waves[j].enemySpawns[k] = new EnemySpwInfo();
					encounters[i].waves[j].enemySpawns[k].enemyID = swn.gameObject.name;
					encounters[i].waves[j].enemySpawns[k].spawnPos = swn.position;
					//Debug.Log(encounters[i].waves[j].enemySpawns[k].spawnPos);

					k++;
				}
				j++;
			}
			i++;
			Destroy(encount.gameObject);


			camScript = Camera.main.gameObject.GetComponent<CameraControl>();
		}

		prepareNextEncounter();
	}

	private void prepareNextEncounter()
	{
		transform.position = new Vector2(encounters[encounterIndex].xPos,0);
		foreach(GameObject bkGrnd in encounters[encounterIndex].backGrounds)
		{
			bkGrnd.SetActive(true);
		}
		
		//Set camera to follow player
		camScript.setFollowTarget(playerTrans);
		//keep track of when the player has reached the encounter
		StartCoroutine(checkForEncounter());
	}

	private IEnumerator checkForEncounter()
	{
		bool loop = true;
		while(loop)
		{
			yield return new WaitForSeconds(0.3f);
			if(encounters[encounterIndex].xPos < playerTrans.position.x)
			{
				followScript.endFollow();
				loop = false;
				//Lock the camera on the encounter
				camScript.setFollowTarget(transform);
				//spawn first wave
				spawnWave();
				if(encounterIndex > 0)
				{
					foreach(GameObject bkGrnd in encounters[encounterIndex-1].backGrounds)
					{
						bkGrnd.SetActive(false);
					}
				}
					foreach(GameObject bkGrnd in encounters[encounterIndex].backGrounds)
				{
					bkGrnd.SetActive(true);
				}
			}
		}

	}
	private void spawnWave()
	{
		for(int i = 0;i < encounters[encounterIndex].waves[waveIndex].enemySpawns.Length; i++)
		{
			EnemySpwInfo inf = encounters[encounterIndex].waves[waveIndex].enemySpawns[i];
			enemyCount++;
			Instantiate(Resources.Load("Enemys/" + inf.enemyID),inf.spawnPos, Quaternion.identity);

		}
	}
	public void enemyDown()
	{
		enemyCount--;
		if(enemyCount == 0)
		{
			waveIndex++;
			if(waveIndex == encounters[encounterIndex].waves.Length)
			{
				encounterIndex++;

				if(encounterIndex == encounters.Length)
				{



					if(gemReward < 1)
					{
						gemReward=1;
					}
					PlayerPrefs.SetInt("GemReward",gemReward);

					Instantiate(Resources.Load("winScreen"),(Vector2)Camera.main.gameObject.transform.position, Quaternion.identity);

					Destroy(GameObject.FindGameObjectWithTag("GameController"));
					Destroy(this);



				}
				else
				{
					waveIndex = 0;
					//heal all player characters for 20% of there hs
					GameObject[] plyrs = GameObject.FindGameObjectsWithTag("Character");
					int g=0;
					foreach(GameObject plyr in plyrs)
					{
						//heal the players after an encounter
						if(plyr.GetComponent<PlayerDeathState>().isAlive)
						{
							HealthSystem hs = plyr.GetComponent<HealthSystem>();
							hs.ChangeHealth((int)Mathf.Round(hs.maxHealth/4));
						}
						else
						{
							plyr.GetComponent<PlayerDeathState>().revive();
						}
						HealFfxs[g].transform.position = plyr.transform.position;
						HealFfxs[g].SetActive(false);
						HealFfxs[g].SetActive(true);
						HealFfxs[g].transform.GetChild(0).gameObject.GetComponent<LayerSorter>().sortByY(1);

						g++;
					}
					followScript.startFollow();
					prepareNextEncounter();
				}
			}
			else
			{
				spawnWave();
			}
		}
	}



}
