using UnityEngine;
using System.Collections;

public class LevelSelectMenu : MonoBehaviour {

	private int spellCount;
	private int allyCount;
	private string[] currentSpells;
	private string[] currentAllys;

	private const string empty = " ";
	public GameObject indicationText;
	public GameObject spellText;
	public GameObject allyText;

	public GameObject[] gemIndicators;

	public GameObject[] charButons;
	public GameObject[] spellButtons;
	private int area;
	private int level;
	// Use this for initialization
	void Start () {





		//look which chars are avalible
		isCharAvalible("arthur",0);
		isCharAvalible("leila",1);
		isCharAvalible("lance",2);
		isCharAvalible("shen",3);
		isCharAvalible("mimi",4);

		//look what spell are avalible
		isSpellAvalible("fire",0);
		isSpellAvalible("arcane",1);
		isSpellAvalible("teleport",2);

		gameObject.SetActive(false);
	}

	private void isCharAvalible(string name,int inx)
	{
		if(PlayerPrefs.GetInt(name)>0)
		{
			charButons[inx].SetActive(true);
		}

	}

	private void isSpellAvalible(string name,int inx)
	{
		if(PlayerPrefs.GetInt(name)>0)
		{
			spellButtons[inx].SetActive(true);
		}
	}

	public int addSpell(string nm)
	{
		for(int i=0;i<currentSpells.Length;i++)
		{
			if(currentSpells[i] == empty)
			{
				currentSpells[i] = nm;
				spellCount++;
				spellText.GetComponent<TextMesh>().text = spellCount + "/" + currentSpells.Length;
				return i;
			}
		}
		return -1;
	}
	public int addAlly(string nm)
	{

		for(int i=0;i<currentAllys.Length;i++)
		{
			if(currentAllys[i] == empty)
			{
				currentAllys[i] = nm;
				allyCount++;
				allyText.GetComponent<TextMesh>().text = allyCount + "/" + currentAllys.Length;
				return i;
			}
		}
		return -1;
	}
	public void removeSpell(int inx)
	{
		currentSpells[inx] = empty;
		spellCount--;
		spellText.GetComponent<TextMesh>().text = spellCount + "/" + currentSpells.Length;
	}
	public void removeAlly(int inx)
	{
		currentAllys[inx] = empty;
		allyCount--;
		allyText.GetComponent<TextMesh>().text = allyCount + "/" + currentAllys.Length;
	}

	public void setLevel(int ara,int lvl,int allyCnt,int spllCnt)
	{
		//look which chars are avalible
		isCharAvalible("arthur",0);
		isCharAvalible("leila",1);
		isCharAvalible("lance",2);
		isCharAvalible("shen",3);
		isCharAvalible("mimi",4);
		
		//look what spell are avalible
		isSpellAvalible("fire",0);
		isSpellAvalible("arcane",1);
		isSpellAvalible("teleport",2);


		indicationText.GetComponent<TextMesh>().text = "Level " + ara + "-" + lvl; 
		
		
		foreach(GameObject obj in gemIndicators)
		{
			obj.SetActive(false);
		}
		int gmNum = PlayerPrefs.GetInt("Level" + ara + "-" + lvl);
		for(int i=0;i<gmNum;i++)
		{
			gemIndicators[i].SetActive(true);
		}

		allyCount = 0;
		spellCount = 0;

		GetComponent<SpriteRenderer>().sprite = Resources.Load("Grafics/LevelMenu/"+ ara,typeof(Sprite)) as Sprite;

		foreach(GameObject obj in spellButtons)
		{
			obj.transform.GetComponent<SpellSelectButton>().resetButton();
		}
		foreach(GameObject obj in charButons)
		{
			obj.transform.GetComponent<CharSelectButton>().resetButton();
		}

		level = lvl;
		area = ara;
		currentAllys = new string[allyCnt];
		for(int i=0;i<currentAllys.Length;i++)
		{
			currentAllys[i] = empty;
		}
		currentSpells = new string[spllCnt];
		for(int i=0;i<currentSpells.Length;i++)
		{
			currentSpells[i] = empty;
		}

		
		spellText.GetComponent<TextMesh>().text = "0/" + spllCnt;
		allyText.GetComponent<TextMesh>().text = "0/" + allyCnt;
	}

	public void attemptLevelStart()
	{

		if(allyCount == currentAllys.Length && spellCount == currentSpells.Length)
		{
			GameObject loadScrn = new GameObject();
			loadScrn.AddComponent<SpriteRenderer>();
			loadScrn.GetComponent<SpriteRenderer>().sortingLayerName="GUI";
			loadScrn.GetComponent<SpriteRenderer>().sprite = Resources.Load("Grafics/LoadScreens/"+ area,typeof(Sprite)) as Sprite;
			StartCoroutine(lvlStart());
		}
	}
	private IEnumerator lvlStart()
	{
		yield return new WaitForSeconds(0.1f);
		//clear scene
		Transform[] allTrans = GameObject.FindObjectsOfType(typeof(Transform)) as Transform[];
		foreach(Transform trns in allTrans)
		{
			Destroy(trns.gameObject);
		}
		
		PlayerPrefs.SetInt("currentArea", area);
		PlayerPrefs.SetInt("currentLevel", level);

		int[] spellLvls = new int[currentSpells.Length];
		for(int i=0;i<currentSpells.Length;i++)
		{
			spellLvls[i] = PlayerPrefs.GetInt(currentSpells[i]);
		}
		
		//set up levelbuiler
		GameObject builder = new GameObject();
		builder.AddComponent<LevelBuilder>();
		builder.GetComponent<LevelBuilder>().buildLevel(0,1,area,level,currentAllys,spellLvls,currentSpells,new int[]{1,1,1});
	}
	
}
