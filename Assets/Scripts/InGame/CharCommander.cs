using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CharCommander : MonoBehaviour {

	public GameObject focusIndicator;
	public GameObject targetIndicator;
	public GameObject commandArrow;

	private GameObject focusChar;
	private int touchindex = -1;


	public void followCommandStart(int tchIndx,GameObject targt)
	{
		if(touchindex == -1)
		{
			//save index of the touch that is now being tracked as a command
			touchindex = tchIndx;
			//save what character is given a command to
			focusChar = targt;
			//make indicator visible
			focusIndicator.SetActive(true);
			commandArrow.SetActive(true);
			
			//set indicator as child of the target, so it will follow without using extra script
			focusIndicator.transform.parent = focusChar.transform;
			focusIndicator.transform.localPosition = Vector2.zero;
			focusIndicator.transform.localScale = Vector3.one;

			Color colo = targt.GetComponent<CharacterTasker>().playerColor;
			commandArrow.GetComponent<SpriteRenderer>().color = colo;
			focusIndicator.GetComponent<SpriteRenderer>().color = colo;
			
			trackCommand();
		}
	}

	public void cancelCommand()
	{
		focusChar = null;
		touchindex = -1;

		focusIndicator.SetActive(false);
		commandArrow.SetActive(false);

	}
	
	// Update is called once per frame
	void Update () {
		//see if a There's currently a command being tracked(when not -1)
		if(touchindex >= -1)
		{
			trackCommand();
		}

	}

	private void trackCommand()
	{
		//loop along touches
		for(int i = 0;i<Input.touches.Length;i++)
		{
			//check if this touch is the one thats being tracked
			if(Input.touches[i].fingerId == touchindex)
			{


				GameObject targetChar = null;
				Vector2 targetPos = Vector2.zero;
				
				Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.touches[i].position);
				mousePos.y = Mathf.Clamp(mousePos.y,-5,-0.9f);
				mousePos.x = Mathf.Clamp(mousePos.x,-6,2000);

				//check if touch is above a targiteble character
				RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);
				if(hit.collider != null && hit.transform.gameObject.tag == "Enemy")
				{
					targetChar = hit.collider.gameObject;
				}
				else
				{
					targetPos = Camera.main.ScreenToWorldPoint(Input.touches[i].position);
				}
				
				//check if the tracked command has ended
				if(Input.touches[i].phase == TouchPhase.Ended)
				{

					if(targetChar && hit.transform.gameObject != focusChar)
					{
						
						targetIndicator.transform.parent = null;
						targetIndicator.SetActive(false);
						//send command to the commandedChar with target
						focusChar.GetComponent<CharacterTasker>().setAttackCommand(targetChar);
						
					}
					else
					{
						focusChar.GetComponent<CharacterTasker>().setMoveCommand(targetPos);
					}
					focusChar = null;
					touchindex = -1;
					focusIndicator.SetActive(false);
					commandArrow.SetActive(false);
				}
				else
				{
					////visuals for targiting here
					
					//if an enemy is targeted
					if(targetChar)
					{
						targetIndicator.SetActive(true);
						targetIndicator.transform.parent = targetChar.transform;
						targetIndicator.transform.localPosition = Vector2.zero;
					}
					else
					{
						targetIndicator.transform.parent = null;
						targetIndicator.SetActive(false);
					}
					Vector2 diffVector = mousePos-(Vector2)focusChar.transform.position;
					commandArrow.transform.position = (Vector2)focusChar.transform.position+((diffVector)/2);
					commandArrow.transform.localScale = new Vector2(Vector2.Distance((Vector2)focusChar.transform.position,mousePos),0.3f);
					
					
					
					Vector3 rotationVector = commandArrow.transform.rotation.eulerAngles;
					rotationVector.z = Mathf.Rad2Deg*Mathf.Atan(diffVector.y/diffVector.x);
					
					commandArrow.transform.rotation = Quaternion.Euler(rotationVector);
					
					
				}
				
				//change i to end for loop because the tracked command has already been found
				i = Input.touches.Length;
			}
		}
	}
}
