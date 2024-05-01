using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InpuManagerS : MonoBehaviour
{
    PlayerControll playerControls;
	PlayerLocoMotion playerLoco;
	AnimatorManage animatorManage;
	PlayerManager playerManager;
	

	public Vector2 moveMentInput;
	public Vector2 camInput;

	public float camInputX;
	public float camInputY;

	public float moveAmount;
	public float yInput;
	public float xInput;

	public bool b_Input;
	public bool jump_Input;
	public bool crouch_Input;

	private void Awake()
	{
		animatorManage = GetComponent<AnimatorManage>();
		playerLoco = GetComponent<PlayerLocoMotion>();
		playerManager = GetComponent<PlayerManager>();
		
	}

	private void OnEnable()
	{
		if(playerControls == null)
		{
			playerControls = new PlayerControll();

			playerControls.PlayerMovement.MoveMent.performed += i => moveMentInput = i.ReadValue<Vector2>();
			playerControls.PlayerMovement.Camera.performed += i => camInput = i.ReadValue<Vector2>();

			playerControls.PlayerAction.B.performed += i => b_Input = true;

			playerControls.PlayerAction.B.canceled += i => b_Input = false;

			playerControls.PlayerAction.Jump.performed += i => jump_Input = true;

			playerControls.PlayerAction.Crouch.performed += i => crouch_Input = true;

			playerControls.PlayerAction.Crouch.canceled += i => crouch_Input = false;

		}

		playerControls.Enable();
	}

	private void OnDisable()
	{
		playerControls.Disable();
	}

	public void HandleAllInputs()
	{
		HandleMovementInput();
		if (playerLoco.isCatch == false)
		{			
			HandleRunInput();
			HandleJumpingInput();
			HandleCrouchInput();
		}

	}

	private void HandleMovementInput()
	{
		yInput = moveMentInput.y;
		xInput = moveMentInput.x;

		camInputX = camInput.x;
		camInputY = camInput.y;

		moveAmount =Mathf.Clamp01(Mathf.Abs(xInput)+Mathf.Abs(yInput));
		animatorManage.UpdateAnimatorValues(0, moveAmount , playerLoco.isRun, playerLoco.isSneak , playerLoco.isCatch, playerLoco.isJumping);
	}

	private void HandleRunInput()
	{
		if (b_Input && moveAmount > 0.5f)
		{
			playerLoco.isRun = true;

		}
		else
		{
			playerLoco.isRun = false;
		}
	}

	private void HandleJumpingInput()
	{
		if (jump_Input)
		{
			jump_Input = false;
			playerLoco.HandleJumping();
		}
	}

	private void HandleCrouchInput()
	{
		if (crouch_Input|| isHide)
		{
			playerLoco.isSneak = true;
		}
		else
		{
			playerLoco.isSneak = false;
		}
	}

	public bool isHide;

	public void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Hide")
		{
			isHide = true;
			playerManager.boxCollider.enabled = false;
		}
		if (other.gameObject.tag == "Eixt")
		{
			playerManager.boxCollider.enabled = false;
		}
		if (other.gameObject.tag == "ExitHide")
		{
			isHide = false;
			playerManager.boxCollider.enabled = true;
		}

	}
	public void OnTriggerExit(Collider other)
	{
		if (other.gameObject.tag == "Hide")
		{
			isHide = false;
			playerManager.boxCollider.enabled = true;
		}
		if (other.gameObject.tag == "Eixt")
		{
			playerManager.boxCollider.enabled = true;
		}
	}
}
