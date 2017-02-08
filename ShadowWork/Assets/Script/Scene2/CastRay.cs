using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastRay : MonoBehaviour {
	private Ray ray;
	private RaycastHit rayHit;
	[SerializeField] private GameObject hitObject;
	[SerializeField] private LayerMask layer;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetButton("Fire1"))
		{
			ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			if(Physics.Raycast(ray, out rayHit, 100.0f, layer))
			{
				ObjectHit(rayHit.collider);
				MoveObject();
			}
			else
			{
				ClearObject();
			}
		}
		else
		{
			ClearObject();
		}
	}
	
	void ObjectHit(Collider colliderObject)
	{
		hitObject = rayHit.collider.gameObject;

		if(hitObject.GetComponent<MovingPlat>())
		{
			hitObject.GetComponent<MovingPlat>().SetMove(true);
		}
	}

	void ClearObject()
	{
		if(hitObject && hitObject.GetComponent<MovingPlat>())
		{
			hitObject.GetComponent<MovingPlat>().SetMove(false);
		}
		hitObject = null;
	}

	void MoveObject()
	{
		Vector3 tempPos = hitObject.transform.position;
		tempPos.y = rayHit.point.y;
		hitObject.transform.position = tempPos;
	}
}
