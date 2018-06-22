using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SearchField : FieldOfView {

	public float searchRadius;
	public float searchAngle = 360;

	public LayerMask entityMask;

	[HideInInspector]
	public List<Transform> pointsOfInterest = new List<Transform>();

	private void Start()
	{
		StartCoroutine("FindPOI");
	}

	IEnumerator FindPOI()
	{
		while (true)
		{
			yield return null;
			FindTargets();
		}
	}

	void FindTargets() 
	{
		
	}
}
