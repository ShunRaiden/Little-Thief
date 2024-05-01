using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorManage : MonoBehaviour
{
	public Animator animator;

	SFXBYPlayer sfxPlayer;

	int x;
	int y;

	private void Awake()
	{
		animator = GetComponent<Animator>();
		x = Animator.StringToHash("Horizontal");
		y = Animator.StringToHash("Vertical");
	}

	private void Start()
	{
		sfxPlayer = GetComponent<SFXBYPlayer>();
	}

	public void PlayTargetAnimation(string targetAnim, bool isInteracting)
	{
		animator.SetBool("isInteracting", isInteracting);
		animator.CrossFade(targetAnim, 0.2f);
	}

	public void UpdateAnimatorValues(float xMovement, float yMovement , bool isRun, bool isSneak ,bool isCatch,bool isJump)
	{
		float snappedX;
		float snappedY;

		#region snappedX
		if (xMovement > 0 && xMovement > 0.55f)
		{
			snappedX = 1f;

		}
		else if(xMovement < 0 && xMovement > -0.55f)
		{
			snappedX = -1;
		}
		else
		{
			snappedX = 0;
		}
		#endregion
		#region snappedY
		if (yMovement > 0 && yMovement > 0.55f)
		{
			snappedY = 1f;
		}
		else if (yMovement < 0 && yMovement > -0.55f)
		{
			snappedY = -1;
		}
		else
		{
			snappedY = 0;
		}
		#endregion


		if (isRun)
		{
			snappedX = xMovement;
			snappedY = 2;
		}

		if (isSneak)
		{
			snappedX = xMovement;
			if(snappedY > 0)
			{
				snappedY = -1f;
			}
			else
			{
				snappedY = -0.5f;
			}
			
		}
		if (isCatch)
		{
			snappedX = xMovement;
			snappedY = -2;
		}


		sfxPlayer.sfxPlayerInput(snappedY, isJump);

		animator.SetFloat(x, snappedX, 0.1f, Time.deltaTime);
		animator.SetFloat(y, snappedY, 0.1f, Time.deltaTime);

	}
}
