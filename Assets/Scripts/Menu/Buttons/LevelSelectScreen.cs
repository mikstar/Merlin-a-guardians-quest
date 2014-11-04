using UnityEngine;
using System.Collections;

public class LevelSelectScreen : BaseButton {

	public int area;
	public int level;

	public override void whenPressed()
	{
		//clear scene
		Transform[] allTrans = GameObject.FindObjectsOfType(typeof(Transform)) as Transform[];
		foreach(Transform trns in allTrans)
		{
			Destroy(trns.gameObject);
		}

		//set up levelbuiler
		GameObject builder = new GameObject();
		builder.AddComponent<LevelBuilder>();
		builder.GetComponent<LevelBuilder>().buildLevel(0,1,area,level,new string[]{"arthur","Leila"},new int[]{1,1},new string[]{"fire","arcane","teleport"},new int[]{1,1,1});
	}
}
