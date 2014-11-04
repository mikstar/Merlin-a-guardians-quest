using UnityEngine;
using System.Collections;

public class SpellSelectButton : BaseButton {
	
	public GameObject indicator;
	public string spellName;
	private int selectIndex = -1;
	private LevelSelectMenu lvlMenu;
	
	void Start()
	{
		lvlMenu = transform.parent.gameObject.GetComponent<LevelSelectMenu>();
	}

	public void resetButton()
	{
		indicator.SetActive(false);
		selectIndex = -1;
	}
	
	public override void whenPressed()
	{
		//when button is pressed
		
		if(selectIndex == -1)
		{
			//when button is not already active
			selectIndex = lvlMenu.addSpell(spellName);
			if(selectIndex > -1)
			{
				indicator.SetActive(true);
			}
		}
		else
		{
			//when buttin is active
			lvlMenu.removeSpell(selectIndex);
			selectIndex = -1;
			indicator.SetActive(false);
		}
	}
}