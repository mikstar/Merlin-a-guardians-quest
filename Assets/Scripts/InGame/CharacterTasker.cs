using UnityEngine;
using System.Collections;

public class CharacterTasker : MonoBehaviour {


	public float attackdistance;
	public float moveSpeed;
	public float attackTime;
	public float attackTimeTillDmg;
	public int attackDmg;

	private bool hasCommand;
	protected Vector2 targetPos;
	protected GameObject targetEnemy;
	public int busy;
	protected Animator animator;
	public GameObject spriteObject;
	private LayerSorter layerSort;
	public bool isAlive = true;
	protected bool attackWait;

	protected bool breakAttack = false;

	public Color playerColor;

	protected virtual void Start()
	{
		animator = spriteObject.GetComponent<Animator>();
		layerSort = spriteObject.GetComponent<LayerSorter>();
		layerSort.sortByY(0);
	}
	// Update is called once per frame
	void Update () {

		if(hasCommand && busy==0 )
		{
			if(targetEnemy)
			{
				if(targetEnemy.collider2D.enabled == true)
				{
					if(Vector2.Distance(transform.position,targetEnemy.transform.position) > attackdistance)
					{
						moveTowards(targetEnemy.transform.position);

					}
					else
					{
						//attack here
						StartCoroutine(attackTarget());
					}
				}
				else
				{
					bool cng = false;
					GameObject[] ems = GameObject.FindGameObjectsWithTag("Enemy");
					foreach(GameObject em in ems)
					{
						if(em.collider2D.enabled && Vector2.Distance(transform.position, em.transform.position) < attackdistance)
						{
							targetEnemy = em;
							cng = true;
							StartCoroutine(attackTarget());
						}
					}
					if(!cng)
					{
						hasCommand = false;
						targetEnemy = null;
					}
				}
			}
			else
			{
				if(Vector2.Distance(transform.position,targetPos) > (moveSpeed*Time.deltaTime))
				{
					moveTowards(targetPos);
				}
				else
				{
					hasCommand = false;
				}
			}
		}
		else
		{
			animator.SetBool("Moving",false);
		}
	}

	protected virtual IEnumerator attackTarget()
	{
		targetPos = transform.position;
		busy++;
		animator.SetTrigger("Attacking");
		//turn character towards enemy
		float xDiff = transform.position.x-targetEnemy.transform.position.x;
		transform.localScale = new Vector3(xDiff/Mathf.Abs(xDiff),1,1);
			
		GameObject tempTar = targetEnemy;
		yield return new WaitForSeconds(attackTimeTillDmg);
		tempTar.GetComponent<HealthSystem>().ChangeHealth(-attackDmg);
		if(breakAttack)
		{
			breakAttack = false;
			animator.SetTrigger("BreakAttack");
			busy--;
		}
		else
		{
			attackWait = true;
			yield return new WaitForSeconds(attackTime-attackTimeTillDmg);
			if(attackWait)
			{
				busy--;
				attackWait = false;
			}
		}

	}
	public virtual IEnumerator UseAbility(float castTime, float timeTillFfx)
	{
		busy ++;
		
		animator.SetTrigger("UseAbility");

		
		yield return new WaitForSeconds(timeTillFfx);
		//spawn spell effx here
		if(breakAttack)
		{
			breakAttack = false;
			animator.SetTrigger("BreakAttack");
		}
		else
		{
			yield return new WaitForSeconds(castTime-timeTillFfx);
		}
		
		busy--;
	}
	
	private void moveTowards(Vector2 tarPos)
	{
		//make a vector2 of its position
		Vector2 vTwoPos = new Vector2(transform.position.x,transform.position.y);
		//move in the direction of the target, with speed 'moveSpeed'
		Vector2 moveVec = ((tarPos - vTwoPos)*(moveSpeed/(Vector2.Distance(vTwoPos,tarPos))))*Time.deltaTime;
		transform.Translate(moveVec);
		animator.SetBool("Moving",true);

		layerSort.sortByY(0);

		float xDir = -(moveVec.x/Mathf.Abs(moveVec.x));
		transform.localScale = new Vector3(xDir,1,1);
	}


	public void setMoveCommand(Vector2 moveTar)
	{
		
		moveTar.y = Mathf.Clamp(moveTar.y,-5,-0.8f);
		moveTar.x = Mathf.Clamp(moveTar.x,-6,2000);


		if(attackWait)
		{
			attackWait = false;
			animator.SetTrigger("BreakAttack");
			busy--;
		}
		else if(isAlive && busy > 0)
		{
			breakAttack = true;
		}
		targetPos = moveTar;
		//remove and target that may me assigned
		targetEnemy = null;
		hasCommand = true;
	}
	public void setAttackCommand(GameObject attackTar)
	{
		targetEnemy = attackTar;
		targetPos = attackTar.transform.position;
		hasCommand = true;
	}
}
