using UnityEngine;
using System.Collections;

public class CameraControl : MonoBehaviour
{
	private Transform target;
	public int followDrag;

	public void setFollowTarget(Transform tar)
	{
		target = tar;
	}

	void Update()
	{
		float distance = target.position.x - transform.position.x;
		
		transform.Translate(new Vector2(distance/followDrag,0));
	}
}