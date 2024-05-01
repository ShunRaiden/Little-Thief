using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
	InpuManagerS inpuManager;

	public Transform targetTran; //The obj cam follow
	public Transform camPivot;   //The obj the cam uses to pivot
	public Transform camTransform;//The transform of the actual cam obj in the scene
	public LayerMask collisionLayer;//The layer we want out cam to collide with
	private float defaultPos;
	private Vector3 camFollowVelocity = Vector3.zero;
	private Vector3 camVectorPos;

	public float camCollisionOffSet = 0.2f;// How much the cam will jump off of obj its colliding with
	public float minCollisionOffSet = 0.2f;
	public float camCollisionRadius = 2;
	public float camFollowSpd = 0.2f;
	public float camLookSpd = 2;
	public float camPivotSpd = 2;

	public float lookAngle; //Cam looking up/down
	public float pivotAngle; //Cam looking left/right

	public float minPivotAngle = -35;
	public float maxPivotAngle = 35;

	

	private void Awake()
	{
		inpuManager = FindObjectOfType<InpuManagerS>();
		targetTran = FindObjectOfType<PlayerManager>().transform;
		camTransform = Camera.main.transform;
		defaultPos = camTransform.localPosition.z;
	}

	public void HandleAllCameraMovement()
	{
		FollowTarget();		

		HandleCameraCollision();
	}

	private void FollowTarget()
	{
		Vector3 targetPos = Vector3.SmoothDamp(transform.position, targetTran.position, ref camFollowVelocity, camFollowSpd);

		transform.position = targetPos;
	}

	public void RotateCamera()
	{
			Vector3 rotation;
			Quaternion targerRotation;

			lookAngle = lookAngle + (inpuManager.camInputX * camLookSpd);
			pivotAngle = pivotAngle - (inpuManager.camInputY * camPivotSpd);
			pivotAngle = Mathf.Clamp(pivotAngle, minPivotAngle, maxPivotAngle);

			rotation = Vector3.zero;
			rotation.y = lookAngle;
			targerRotation = Quaternion.Euler(rotation);
			transform.rotation = targerRotation;

			rotation = Vector3.zero;
			rotation.x = pivotAngle;
			targerRotation = Quaternion.Euler(rotation);
			camPivot.localRotation = targerRotation;

	}

	private void HandleCameraCollision()
	{
		float targetPos = defaultPos;
		RaycastHit hit;
		Vector3 dir = camTransform.position - camPivot.position;
		dir.Normalize();

		if(Physics.SphereCast(camPivot.transform.position, camCollisionRadius, dir, out hit, Mathf.Abs(targetPos), collisionLayer))
		{
			float distance = Vector3.Distance(camPivot.position, hit.point);
			targetPos = targetPos - (distance - camCollisionOffSet);
		}

		if(Mathf.Abs(targetPos) < minCollisionOffSet)
		{
			targetPos = targetPos - minCollisionOffSet;
		}

		camVectorPos.z = Mathf.Lerp(camTransform.localPosition.z, targetPos, 0.2f);
		camTransform.localPosition = camVectorPos;
	}

}
