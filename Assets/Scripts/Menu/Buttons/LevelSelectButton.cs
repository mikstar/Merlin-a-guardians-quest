using UnityEngine;
using System.Collections;

public class LevelSelectButton : BaseButton {

	public int area;
	public int level;
	public int spellCount;
	public int allyCount;
	public GameObject lvlSelectMenu;
	public string upgradeName;
	public int upgradeLvl;
	public bool upgradeYesNo;

	public override void whenPressed()
	{
		scrollOnOff(false);
		buttonsOnOff(false);

		if(upgradeYesNo)
		{
			PlayerPrefs.SetString("upgrade",upgradeName);
			PlayerPrefs.SetInt("upgradeNum",upgradeLvl);
			PlayerPrefs.SetInt("upgradeBool",1);
		}
		else
		{
			PlayerPrefs.SetInt("upgradeBool",0);
		}
		lvlSelectMenu.SetActive(true);
		lvlSelectMenu.GetComponent<LevelSelectMenu>().setLevel(area,level,allyCount,spellCount);
	}
}
