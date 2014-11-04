using UnityEngine;
using System.Collections;

public class LevelScreenStartLvlButton : BaseButton {

	public override void whenPressed()
	{
		transform.parent.gameObject.GetComponent<LevelSelectMenu>().attemptLevelStart();
	}
}
