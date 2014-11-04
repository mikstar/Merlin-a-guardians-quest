using UnityEngine;
using System.Collections;

public class BaseButton : MonoBehaviour {

	public virtual void whenPressed()
	{

	}

	protected void scrollOnOff(bool onOff)
	{
		GameObject[] menus = GameObject.FindGameObjectsWithTag("Menu");
		foreach(GameObject menu in menus)
		{
			menu.collider2D.enabled = onOff;
		}
	}
	protected void buttonsOnOff(bool onOff)
	{
		foreach(Transform chld in transform.parent)
		{
			if(chld.gameObject.collider2D)
			{
				chld.gameObject.collider2D.enabled = onOff;
			}
		}
	}
}
