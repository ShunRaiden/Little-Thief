using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCanMove : MonoBehaviour
{
	private Rigidbody _Rigidbody;
	private Transform objGrapPointTrans;
	private void Awake()
	{
		_Rigidbody = GetComponent<Rigidbody>();
	}
	public void Grap(Transform objGarbPointTrans)
	{
		this.objGrapPointTrans = objGarbPointTrans;
		_Rigidbody.useGravity = false;
	}
	public void Drop()
	{
		this.objGrapPointTrans = null;
		_Rigidbody.useGravity = true;
	}

	private void FixedUpdate()
	{
		if(objGrapPointTrans != null)
		{
			float lerpSpeed = 10f;
			Vector3 newPosition = Vector3.Lerp(transform.position ,objGrapPointTrans.position,Time.deltaTime*lerpSpeed);
			_Rigidbody.MovePosition(newPosition);
		}
	}
}
