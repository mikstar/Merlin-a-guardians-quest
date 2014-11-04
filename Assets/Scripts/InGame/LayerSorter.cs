using UnityEngine;
using System.Collections;

public class LayerSorter : MonoBehaviour {

	private SpriteRenderer rend;
	private Transform trans;
	public int parentNum;

	void Awake()
	{
		
		trans = transform.parent;

		rend = GetComponent<SpriteRenderer>();
	}


	public void sortByY(int offset)
	{
		rend.sortingOrder = offset+(int)Mathf.Round((-trans.position.y*8f));
	}
}
