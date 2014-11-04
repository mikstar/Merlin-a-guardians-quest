using UnityEngine;
using System.Collections;

public class ExitScreenButton : BaseButton {

	public override void whenPressed()
	{
		//turn buttons back on
		foreach(Transform chld in transform.parent.parent)
		{
			if(chld.gameObject.collider2D)
			{
				chld.gameObject.collider2D.enabled = true;
			}
		}

		scrollOnOff(true);

		//turn menu screen off
		transform.parent.gameObject.SetActive(false);
	}
}
