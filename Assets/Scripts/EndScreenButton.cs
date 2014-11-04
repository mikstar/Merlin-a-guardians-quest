using UnityEngine;
using System.Collections;

public class EndScreenButton : MonoBehaviour {

	public bool win;
	public GameObject[] gems;
	public GameObject resetButton;
	public GameObject quitButton;
	public GameObject lossFade;
	public GameObject extra;
	public GameObject unlockText;
	// Use this for initialization
	void Start () {
		if(win)
		{
			for(int i=0;i<PlayerPrefs.GetInt("GemReward");i++)
			{
				gems[i].SetActive(true);
			}
			//unlock any upgrade that this lvl has
			if(PlayerPrefs.GetInt("upgradeBool") == 1 && PlayerPrefs.GetInt(PlayerPrefs.GetString("upgrade")) < PlayerPrefs.GetInt("upgradeNum"))
			{
				PlayerPrefs.SetInt(PlayerPrefs.GetString("upgrade"),PlayerPrefs.GetInt("upgradeNum"));
				unlockText.SetActive(true);
				unlockText.GetComponent<TextMesh>().text = "You've unlocked " + PlayerPrefs.GetString("upgrade") + "!";
			}

			//change gem total and for this lvl
			if(PlayerPrefs.GetInt("GemReward") > PlayerPrefs.GetInt("Level"+(PlayerPrefs.GetInt("currentArea"))+"-"+(PlayerPrefs.GetInt("currentLevel"))))
			{
				int diff = PlayerPrefs.GetInt("GemReward") - PlayerPrefs.GetInt("Level"+(PlayerPrefs.GetInt("currentArea"))+"-"+(PlayerPrefs.GetInt("currentLevel")));
				PlayerPrefs.SetInt("Gems",PlayerPrefs.GetInt("Gems")+diff);
				
				PlayerPrefs.SetInt("Level"+(PlayerPrefs.GetInt("currentArea"))+"-"+(PlayerPrefs.GetInt("currentLevel")),PlayerPrefs.GetInt("Level"+(PlayerPrefs.GetInt("currentArea"))+"-"+(PlayerPrefs.GetInt("currentLevel")))+diff);
			}
			//unlock next level
			int tempArea = PlayerPrefs.GetInt("currentArea");
			int tempLvl = PlayerPrefs.GetInt("currentLevel");
			if(tempLvl==3)
			{
				tempArea = tempArea+1;
				tempLvl = 0;
			}
			if(PlayerPrefs.GetInt("Level"+tempArea+"-"+(tempLvl+1)) == -1)
			{
				PlayerPrefs.SetInt("Level"+tempArea+"-"+(tempLvl+1),0);
			}
		}
		else
		{
			StartCoroutine(lossFader());
		}
	}

	private IEnumerator lossFader()
	{
		renderer.enabled = false;
		SpriteRenderer temp = lossFade.GetComponent<SpriteRenderer>();
		Color tempcol = Color.black;
		tempcol.a = 0;
		temp.color = tempcol;
		
		yield return new WaitForSeconds(0.1f);
		tempcol.a = 0.1f;
		temp.color = tempcol;
		yield return new WaitForSeconds(0.1f);
		tempcol.a = 0.2f;
		temp.color = tempcol;
		yield return new WaitForSeconds(0.1f);
		tempcol.a = 0.3f;
		temp.color = tempcol;
		yield return new WaitForSeconds(0.1f);
		tempcol.a = 0.4f;
		temp.color = tempcol;
		yield return new WaitForSeconds(0.1f);
		tempcol.a = 0.5f;
		temp.color = tempcol;
		yield return new WaitForSeconds(0.1f);
		tempcol.a = 0.6f;
		temp.color = tempcol;
		yield return new WaitForSeconds(0.1f);
		tempcol.a = 0.7f;
		temp.color = tempcol;
		yield return new WaitForSeconds(0.1f);
		tempcol.a = 0.8f;
		temp.color = tempcol;
		yield return new WaitForSeconds(0.1f);
		tempcol.a = 0.9f;
		temp.color = tempcol;
		yield return new WaitForSeconds(0.1f);
		tempcol.a = 1;
		temp.color = tempcol;

		
		renderer.enabled = true;
		extra.SetActive(true);

	}
	
	// Update is called once per frame
	void Update () {
		//loop along all current touches
		for(int i = 0;i<Input.touches.Length;i++)
		{
			//check if this touch has just began
			if(Input.touches[i].phase == TouchPhase.Began)
			{
				//for multiple stacked hitboxes//////
				RaycastHit2D[] hit = Physics2D.RaycastAll(Camera.main.ScreenToWorldPoint(Input.touches[i].position), Vector2.zero);
				for(int j = 0; j < hit.Length ;j++)
				{
					if(hit[j].collider.gameObject == quitButton)
					{
						//clear scene
						Transform[] allTrans = GameObject.FindObjectsOfType(typeof(Transform)) as Transform[];
						foreach(Transform trns in allTrans)
						{
							Destroy(trns.gameObject);
						}
						
						Instantiate(Resources.Load("hub"),Vector2.zero, Quaternion.identity);
					}
					else if(hit[j].collider.gameObject == resetButton)
					{
						
						StartCoroutine(lvlReset());
					}
				}
			}
		}
	}

	private IEnumerator lvlReset()
	{
		//retrieve names ao all ally's
		GameObject[] allChars = GameObject.FindGameObjectsWithTag("Character");
		string[] charNames = new string[allChars.Length - 1];
		int[] charLvls = new int[allChars.Length - 1];
		int y = 0;
		foreach(GameObject charec in allChars)
		{
			Debug.Log(charec.name);
			if(charec.name != "Merlin")
			{
				charNames[y] = charec.name;
				charLvls[y] = PlayerPrefs.GetInt(charec.name);
				y++;
			}
		}

		//retrieve names of all spells
		GameObject[] allSplss = GameObject.FindGameObjectsWithTag("SpellButton");
		string[] splNames = new string[allSplss.Length];
		int[] splLvls = new int[allSplss.Length];
		int q = 0;
		foreach(GameObject spl in allSplss)
		{
			string[] mnSplit = spl.name.Split('_');
			Debug.Log(mnSplit[0]);
			splNames[q] = mnSplit[0];
			splLvls[q] = PlayerPrefs.GetInt(mnSplit[0]);
			q++;
		}
		
		//clear scene
		Transform[] allTrans = GameObject.FindObjectsOfType(typeof(Transform)) as Transform[];
		foreach(Transform trns in allTrans)
		{
			if(trns.gameObject != gameObject)
			{
				Destroy(trns.gameObject);
			}
		}

		GameObject loadScrn = new GameObject();
		loadScrn.AddComponent<SpriteRenderer>();
		loadScrn.GetComponent<SpriteRenderer>().sortingLayerName="GUI";
		loadScrn.GetComponent<SpriteRenderer>().sprite = Resources.Load("Grafics/LoadScreens/"+ PlayerPrefs.GetInt("currentArea"),typeof(Sprite)) as Sprite;

		yield return new WaitForSeconds(0.1f);
		
		//set up levelbuiler
		GameObject builder = new GameObject();
		builder.AddComponent<LevelBuilder>();
		builder.GetComponent<LevelBuilder>().buildLevel(0,1,PlayerPrefs.GetInt("currentArea"),PlayerPrefs.GetInt("currentLevel"),charNames,charLvls,splNames,splLvls);
		Destroy(gameObject);
		Destroy(loadScrn);
	}
}
