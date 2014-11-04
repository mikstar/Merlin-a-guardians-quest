using UnityEngine;
using System.Collections;

public class LevelBuilder : MonoBehaviour {

	
	public void buildLevel(int merType,int merLvl,int areaNr, int lvlNr,string[] chrNms, int[] chrLvls, string[] splNms, int[] splLvls)
	{
		//add game controll
		Instantiate(Resources.Load("GameControll"),Vector2.zero, Quaternion.identity);
		//add merlin
		GameObject mer = Instantiate(Resources.Load("Characters/Merlin/Set"+ merType + "/Lvl" + merLvl),new Vector2(-2,-2), Quaternion.identity) as GameObject;
		if(mer.tag == "Character")
		{
			mer.name = "Merlin";
		}

		//add allys
		for(int i=0;i<chrNms.Length;i++)
		{
			GameObject ally =  Instantiate(Resources.Load("Characters/" + chrNms[i] + "/Lvl" + PlayerPrefs.GetInt(chrNms[i])),new Vector2(0,-2), Quaternion.identity) as GameObject;
			ally.name = chrNms[i] + "_Lvl" + PlayerPrefs.GetInt(chrNms[i]);
		}
		//add UI/Camera
		GameObject cam = Instantiate(Resources.Load("GameCamera"),new Vector3(0,0,-10), Quaternion.identity) as GameObject;
		//add merlin as target to the camera
		cam.GetComponent<CameraControl> ().setFollowTarget(mer.transform);
		//add main spells
		for(int i=0;i<splNms.Length;i++)
		{
			GameObject spl = Instantiate(Resources.Load("Spells/" + splNms[i] + "/Lvl" + PlayerPrefs.GetInt(splNms[i])),Vector2.zero, Quaternion.identity) as GameObject;
			spl.name = splNms[i] + "_Lvl" + splLvls[i];
			spl.transform.parent = cam.transform;
			spl.transform.position = new Vector2(1.6f + (i*2.9f),3.8f);
			spl.transform.DetachChildren();
		}
		//position ally spells
		GameObject[] allyButtons = GameObject.FindGameObjectsWithTag("AbilityButton");
		for(int i=0;i<allyButtons.Length;i++)
		{
			GameObject hld = Instantiate(Resources.Load("AbilityHold"),Vector2.zero, Quaternion.identity) as GameObject;
			hld.GetComponent<SpriteRenderer>().color = allyButtons[i].GetComponent<BaseAbility>().abilityOwner.GetComponent<CharacterTasker>().playerColor;

			hld.transform.parent = cam.transform;
			allyButtons[i].transform.parent = cam.transform;

			Vector2 pos = new Vector2(-4.93f + (i*2.72f),3.72f);
			allyButtons[i].transform.position = pos + new Vector2(-0.04f,0.15f);
			hld.transform.position = pos;
		}
		//add stage
		Instantiate(Resources.Load("Stages/Area_" + areaNr + "/Stage" + lvlNr),Vector2.zero, Quaternion.identity);
		//remove itself
		Destroy(gameObject);

	}
}
