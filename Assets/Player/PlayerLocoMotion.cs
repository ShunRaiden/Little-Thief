using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLocoMotion : MonoBehaviour
{
	PlayerManager playerManager;
	AnimatorManage animatorManage;
	InpuManagerS inpuManager;

    Vector3 moveDir;

	Transform cameraObj;
	Rigidbody playerRigid;

	public CapsuleCollider mainCap, sitCap; 
	

	[Header("Falling")]
	public float inAirTimer;
	public float leapingVelocity;
	public float fallingSpeed;
	public float rayCastHeightOffSet =0.5f;
	public LayerMask groundLayer;

	[Header("Movement Flags")]
	public bool isRun;
	public bool isGrounded;
	public bool isJumping;
	public bool isSneak;
	public bool isCatch = false;

	[Header("Movement Speed")]
	public float sneakSpeed = 1.5f;
	public float walkingSpeed =3.5f;
	public float moveSpeed = 5;
	public float runSpeed = 7;
	public float rotationSpeed = 15;

	[Header("Jump Speeds")]
	public float jumpHeight = 3;
	public float gravityIntenSity = -15;

	private void Awake()
	{
		animatorManage = GetComponent<AnimatorManage>();
		playerManager = GetComponent<PlayerManager>();
		inpuManager = GetComponent<InpuManagerS>();
		playerRigid = GetComponent<Rigidbody>();
		cameraObj = Camera.main.transform;
		isCatch = false;
	}


	public void HandleAllMovement()
	{
		if (!isCatch)
		{
			HandleFallingAndLanding();
		}

		if (playerManager.isInteracting)
		{
			return;
		}

		if (!isCatch)
		{
			HandleMovement();
		}

		HandleRotation();


	}

	private void HandleMovement()
	{
		if (isJumping)
		{
			return;
		}

			moveDir = cameraObj.forward * inpuManager.yInput;
			moveDir = moveDir + cameraObj.right * inpuManager.xInput;
			moveDir.Normalize();
			moveDir.y = 0;

		

		if (isRun)
		{
			moveDir = moveDir * runSpeed;
			
		}
		else
		{
			if (inpuManager.moveAmount >= 0.5f && isRun)
			{
				moveDir = moveDir * runSpeed;

				
			}
			else
			{
				moveDir = moveDir * walkingSpeed;
				
			}
		}

		if (isSneak)
		{
			moveDir = moveDir * sneakSpeed;
			mainCap.enabled = false;
			sitCap.enabled = true;
		}
		else
		{
			if (inpuManager.moveAmount >= 0 && isSneak)
			{
				moveDir = moveDir * sneakSpeed;
				mainCap.enabled = false;
				sitCap.enabled = true;
			}
			else
			{
				moveDir = moveDir * walkingSpeed;
				mainCap.enabled = true;
				sitCap.enabled = false;
			}
		}

		
		moveDir = moveDir * moveSpeed;
		
		Vector3 moveVelocity = moveDir;
		playerRigid.velocity = moveVelocity;
	}

	private void HandleRotation()
	{

		Vector3 targetDir = Vector3.zero;
		targetDir = cameraObj.forward * inpuManager.yInput;
		targetDir = targetDir + cameraObj.right * inpuManager.xInput;
		targetDir.Normalize();
		targetDir.y = 0;

		if(targetDir == Vector3.zero)
		{
			targetDir = transform.forward;
		}

		Quaternion targetRotation = Quaternion.LookRotation(targetDir);
		Quaternion playerRotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

		transform.rotation = playerRotation;
	}
	public Vector3 rayCastOrigin;
	private void HandleFallingAndLanding()
	{
		RaycastHit hit;
		rayCastOrigin = transform.position;
		Vector3 targetPos;
		rayCastOrigin.y = rayCastOrigin.y + rayCastHeightOffSet;
		targetPos = transform.position;

		if (!isGrounded && !isJumping)
		{
			

			animatorManage.animator.SetBool("isGround", false);
			if (!playerManager.isInteracting)
			{
				animatorManage.PlayTargetAnimation("Falling", true);
			}

			inAirTimer = inAirTimer + Time.deltaTime;
			playerRigid.AddForce(transform.forward * leapingVelocity);
			playerRigid.AddForce(-Vector3.up * fallingSpeed * inAirTimer);
		}

		Debug.Log(Physics.SphereCast(rayCastOrigin, 0.2f, -Vector3.up, out hit, rayCastHeightOffSet, groundLayer));
		if (Physics.SphereCast(rayCastOrigin, 0.2f, -Vector3.up, out hit, rayCastHeightOffSet, groundLayer))
		{			
			if (!isGrounded && !playerManager.isInteracting)
			{
				animatorManage.PlayTargetAnimation("Landing", true);
			}

			Vector3 rayCastHitPoint = hit.point;
			targetPos.y = rayCastHitPoint.y;
			inAirTimer = 0;
			Debug.Log("GroundTrue???");
			isGrounded = true;
			animatorManage.animator.SetBool("isGround", true);
		}
		else
		{
			Debug.Log("GroundFalse???");
			isGrounded = false;
			animatorManage.animator.SetBool("isGround", false);
		}

		if(isGrounded && !isJumping)
		{
			if(playerManager.isInteracting || inpuManager.moveAmount > 0)
			{
				transform.position = Vector3.Lerp(transform.position, targetPos, Time.deltaTime / 0.1f);
			}
			else
			{
				transform.position = targetPos;
			}
		}
	}

	public void HandleJumping()
	{
		if (isGrounded)
		{
			animatorManage.animator.SetBool("isJumping", true);
			animatorManage.PlayTargetAnimation("Jump", false);

			float jumpingVelocity = Mathf.Sqrt(-2 * gravityIntenSity * jumpHeight);
			Vector3 playerVelocity = moveDir;
			playerVelocity.y = jumpingVelocity;
			playerRigid.velocity = playerVelocity;

			
		}
	}

	private void OnDrawGizmos()
	{
		Gizmos.color = Color.green;
		Gizmos.DrawWireSphere(rayCastOrigin, rayCastHeightOffSet);


	}






}
