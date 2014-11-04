using UnityEngine;
using System.Collections;

public class MenuScrolling : MonoBehaviour {


	private int screenLockIndex = 0;
	private int activeTouchIndex = -1;
	private float lastMovement;
	private float startPos;

	public GameObject[] menus;
	public float menuDistance;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {	
		if(activeTouchIndex==-1)
		{
			float distance = -(screenLockIndex*menuDistance) - transform.position.x;
			transform.Translate(new Vector2(distance/5,0));

			foreach(Touch touch in Input.touches)
			{
				//when no touch is being tracked yet
				if(touch.phase == TouchPhase.Began)
				{
					RaycastHit2D[] hits = Physics2D.RaycastAll(Camera.main.ScreenToWorldPoint(touch.position), Vector2.zero);
					if(hits.Length>1)
					{
						//activate button here
						foreach(RaycastHit2D hit in hits)
						{
							if(hit.collider.gameObject.tag == "MenuButton")
							{
								hit.collider.gameObject.GetComponent<BaseButton>().whenPressed();
								break;
							}
						}
					}
					else if(hits.Length == 1)
					{
						if(hits[0].collider.gameObject.tag == "Menu")
						{
							activeTouchIndex = touch.fingerId;
							startPos = Camera.main.ScreenToWorldPoint(touch.position).x;
						}
						else if(hits[0].collider.gameObject.tag == "MenuButton")
						{
							hits[0].collider.gameObject.GetComponent<BaseButton>().whenPressed();
						}
					}

					break;
				}
			}
		}
		else
		{
			foreach(Touch touch in Input.touches)
			{
				if(touch.fingerId == activeTouchIndex)
				{
					//if this is the touch that is being tracked

					//when the tracked touch has ended
					if(touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
					{
						activeTouchIndex = -1;
						float fullDist = startPos - Camera.main.ScreenToWorldPoint(touch.position).x;
						if(lastMovement != 0)
						{
							scrollMenu((int)-(lastMovement/Mathf.Abs(lastMovement)));
						}
						else if(Mathf.Abs(fullDist) > 9)
						{
							scrollMenu((int)(fullDist/Mathf.Abs(fullDist)));
						}
					}
					//when the tracked touch is still going
					else
					{
						Vector2 currentTouchPos = Camera.main.ScreenToWorldPoint(touch.position);

						//transform.Translate(new Vector2(Camera.main.ScreenToWorldPoint(touch.deltaPosition).x,0));
						float distance = (-(screenLockIndex*menuDistance) + (currentTouchPos.x - startPos)) - transform.position.x;
						transform.Translate(new Vector2(distance/2,0));

						lastMovement = touch.deltaPosition.x;
					}

					break;
				}
			}
		}
	}
	private void scrollMenu(int chnge)
	{
		int tempMove = ((2-(2*chnge))+screenLockIndex)%5;
		while(tempMove<0)
		{
			tempMove += 5;
		}

		menus[tempMove].transform.Translate(new Vector2((18*menus.Length)*chnge,0));
		screenLockIndex += chnge;
	}
}





