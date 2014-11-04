using UnityEngine;
using System.Collections;

public class TouchDetect : MonoBehaviour {

	public GameObject spellTargeter;
	public GameObject[] spellIndicators;
	private CharCommander commandScript;
	private bool nowCasting = false;
	private BaseSpell currentSpell;
	private GameObject[] targets;
	private int targetIndx = 0;

	// Use this for initialization
	void Start ()
	{
		commandScript = GetComponent<CharCommander>();
	}

	void Update () {
		//loop along all current touches
		for(int i = 0;i<Input.touches.Length;i++)
		{
			//check if this touch has just began
			if(Input.touches[i].phase == TouchPhase.Began)
			{
				//for multiple stacked hitboxes//////
				RaycastHit2D[] hit = Physics2D.RaycastAll(Camera.main.ScreenToWorldPoint(Input.touches[i].position), Vector2.zero);
				for(int j = 0; j < hit.Length ;j++)
				{
					if(hit[j].collider != null)
					{
						if(hit[j].transform.gameObject.tag == "SpellButton")
						{
							//when hitting a spell button
							if(!hit[j].transform.gameObject.GetComponent<BaseSpell>().onCooldown)
							{
								//if the spell is not on cooldown
								if(!nowCasting)
								{
									//when another spell is not already selected
									//select this spell
									commandScript.cancelCommand();

									nowCasting = true;

									currentSpell = hit[j].transform.gameObject.GetComponent<BaseSpell>();
									targets = new GameObject[currentSpell.targetCount];

									spellTargeter.transform.position = hit[j].transform.gameObject.transform.position;
									spellTargeter.SetActive(true);

									
								}
								else
								{
									//when a spell is already selected
									if(hit[j].transform.gameObject.GetComponent<BaseSpell>() == currentSpell)
									{
										//if this same spell was already selected
										//deselect spell/cancel casting
										for(int k=0;k<targetIndx;k++)
										{
											spellIndicators[k].SetActive(false);
										}

										nowCasting = false;
										currentSpell = null;
										targets = null;
										targetIndx = 0;

										spellTargeter.SetActive(false);



									}
									else
									{
										//when a different spell is selected
										//change to this spell/cancel former spell
										for(int k=0;k<targetIndx;k++)
										{
											spellIndicators[k].SetActive(false);
										}

										currentSpell = hit[j].transform.gameObject.GetComponent<BaseSpell>();
										targets = new GameObject[currentSpell.targetCount];
										targetIndx = 0;

										spellTargeter.transform.position = hit[j].transform.gameObject.transform.position;

									}
								}
							}
						}
						else if(hit[j].transform.gameObject.tag == "AbilityButton")
						{
							//when hitting a ability button
							if(!hit[j].transform.gameObject.GetComponent<BaseAbility>().onCooldown)
							{
								StartCoroutine(hit[j].transform.gameObject.GetComponent<BaseAbility>().useAbility());
							}

						}
						else if(hit[j].transform.gameObject.tag == "Menu")
						{
							Application.LoadLevel(0);
							
						}
						else if(hit[j].transform.gameObject.tag == "Character" || hit[j].transform.gameObject.tag == "Enemy")
						{
							if(nowCasting)
							{

								targets[targetIndx] = hit[j].transform.gameObject;
								targetIndx++;

								if(targetIndx == currentSpell.targetCount)
								{
									currentSpell.spellEffect(targets);
									spellTargeter.SetActive(false);

									for(int k=0;k<currentSpell.targetCount-1;k++)
									{
										spellIndicators[k].SetActive(false);
									}

									nowCasting = false;
									targets = null;
									currentSpell = null;
									targetIndx = 0;
								}
								else
								{



									spellIndicators[targetIndx-1].transform.parent = targets[targetIndx-1].transform;
									spellIndicators[targetIndx-1].transform.localPosition = Vector2.zero;
									spellIndicators[targetIndx-1].transform.localScale = new Vector2(1,1);

									spellIndicators[targetIndx-1].SetActive(true);
								}
								
								j = hit.Length;
							}
							else
							{
								if(hit[j].transform.gameObject.tag == "Character")
								{
									commandScript.followCommandStart(Input.touches[i].fingerId,hit[j].transform.gameObject);
									j = hit.Length;
								}
							}

						}

					}
				}
			}
		}
	}




}
