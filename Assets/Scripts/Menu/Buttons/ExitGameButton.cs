using UnityEngine;
using System.Collections;

public class ExitGameButton : BaseButton {

	public override void whenPressed()
	{
		Application.Quit();
	}
}
