using UnityEngine;
using System.Collections;

public class BasicProjectile : MonoBehaviour {

	private Transform target;
	public Transform ownerPos;
	public int damage;
	public float speed;
	private LayerSorter sortScript;
	private Vector2 emitOffset;

	void Start()
	{
		sortScript = GetComponent<LayerSorter>();
		emitOffset = transform.position-ownerPos.position;
	}

	public void assignNewTarget(GameObject tar)
	{
		if(target)
		{
			doDmg();
		}
		else
		{
			renderer.enabled = true;
		}

		target = tar.transform;
		transform.position = ownerPos.position + new Vector3(emitOffset.x*ownerPos.transform.localScale.x,emitOffset.y,0);
		sortScript.sortByY(0);
	}

	private void doDmg()
	{
		target.gameObject.GetComponent<HealthSystem>().ChangeHealth(-damage);
	}

	void Update()
	{
		if(target)
		{
			//check if it is at target
			if(Vector3.Distance(transform.position,target.position+new Vector3(0,0.5f,0)) < speed)
			{
				doDmg();
				target = null;
				renderer.enabled = false;
			}
			else
			{
				//point to target
				Vector2 relative = transform.InverseTransformPoint(target.position+new Vector3(0,0.5f,0));
				float angle = Mathf.Atan2(relative.y, relative.x) * Mathf.Rad2Deg;
				transform.Rotate( 0, 0,angle);
				transform.Translate(new Vector2(speed,0));
				sortScript.sortByY(0);
			}
		}
	}
}
