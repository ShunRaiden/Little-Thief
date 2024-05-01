using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
	Animator animator;
	InpuManagerS inpuManager;
	CameraManager cameraManager;
	PlayerLocoMotion playerLocoMotion;
	public BoxCollider boxCollider;

	public bool isInteracting;

	public bool gamePause;


	private void Awake()
	{
		animator = GetComponent<Animator>();
		inpuManager = GetComponent<InpuManagerS>();
		cameraManager = FindObjectOfType<CameraManager>();
		playerLocoMotion = GetComponent<PlayerLocoMotion>();
		boxCollider = GetComponent<BoxCollider>();

	}

	private void Update()
	{
		inpuManager.HandleAllInputs();
		
	}

	private void FixedUpdate()
	{
		playerLocoMotion.HandleAllMovement();
	}

	private void LateUpdate()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			if(gamePause == false)
			{
				gamePause = true;
			}
			else
			{
				gamePause = false;
			}			
		}

		if (!gamePause)
		{
			cameraManager.RotateCamera();
		}

		cameraManager.HandleAllCameraMovement();
	

		isInteracting = animator.GetBool("isInteracting");
		playerLocoMotion.isJumping = animator.GetBool("isJumping");
		animator.SetBool("isGround", playerLocoMotion.isGrounded);
	}

}
