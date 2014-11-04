using UnityEngine;
using System.Collections;

public class UpGradeButton : BaseButton {

	public string upgradeItem;
	public int upgradeTo;
	public int price;
	public GameObject nextUpgrade;
	public GameObject lockIcon;
	public GameObject gemIcon;
	public GameObject gemText;

	// Use this for initialization
	void Start () {
		gemText.GetComponent<TextMesh>().text = "X" + price;
		checkIfUnlocked();
	}

	public void checkIfUnlocked()
	{
		if(PlayerPrefs.GetInt(upgradeItem) == upgradeTo-1)
		{
			gameObject.collider2D.enabled = true;
			gameObject.renderer.material.color = Color.white;
			lockIcon.SetActive(false);
			gemIcon.SetActive(true);
			gemText.SetActive(true);
		}
		else if(PlayerPrefs.GetInt(upgradeItem) >= upgradeTo)
		{
			gameObject.collider2D.enabled = false;
			gameObject.renderer.material.color = Color.grey;
			lockIcon.SetActive(false);
			gemIcon.SetActive(false);
			gemText.SetActive(false);
		}
		else if(PlayerPrefs.GetInt(upgradeItem) < upgradeTo-1)
		{
			gameObject.collider2D.enabled = false;
			gameObject.renderer.material.color = Color.grey;
			gemIcon.SetActive(false);
			gemText.SetActive(false);
			
		}
	}
	
	public override void whenPressed()
	{
		if(PlayerPrefs.GetInt("Gems") >= price)
		{
			PlayerPrefs.SetInt("Gems",PlayerPrefs.GetInt("Gems")-price);
			PlayerPrefs.SetInt(upgradeItem,upgradeTo);
			checkIfUnlocked();
			if(nextUpgrade)
			{
				nextUpgrade.GetComponent<UpGradeButton>().checkIfUnlocked();
			}
		}
	}
}
