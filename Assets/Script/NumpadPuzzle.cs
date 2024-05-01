using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class NumpadPuzzle : MonoBehaviour
{
    [SerializeField] private TMP_Text Ans;
	[SerializeField] private Rigidbody doorRigid;
	[SerializeField] private string Answer;
	[SerializeField] private GameObject numPadUi;
	[SerializeField] private string[] textForNumBTN;

	public GameObject pressE;
	public PauseMenu pauseMenu;

	AudioManager audioManager;

	private void Start()
	{
		audioManager = FindObjectOfType<AudioManager>();
		numPadUi.SetActive(false);

	}

	public void Number(int numPad)
	{
		audioManager.SFXSource.PlayOneShot(audioManager.keyNum);

		for (int i = 0; i < textForNumBTN.Length; i++)
		{
			Debug.Log(Ans.text.Length);
			Debug.Log(Answer.Length);
			if (Ans.text.Length == Answer.Length)
			{
				break;
			}
			if(numPad == i)
			{
				Ans.text += textForNumBTN[i];
			}
		}
		
	}
	public void ResetNumPad()
	{
		audioManager.SFXSource.PlayOneShot(audioManager.keyNum);
		Ans.text = "";
	}

	public void ExitNumPad()
	{
		numPadUi.SetActive(false);
	}

	public void EnterPass()
	{
		audioManager.SFXSource.PlayOneShot(audioManager.keyNum);

		if (Ans.text == Answer)
		{
			
			StartCoroutine(Correct());
		}
		else
		{
			StartCoroutine(Invalid());
		}
	}

	IEnumerator Invalid()
	{
		Ans.color = Color.red;
		Ans.text = "Invalid";
		yield return new WaitForSeconds(1);
		Ans.color = Color.white;
		Ans.text = "";
	}

	IEnumerator Correct()
	{
		PlayerManager playerManager = FindObjectOfType<PlayerManager>();

		Ans.color = Color.green;
		Ans.text = "Correct";
		doorRigid.isKinematic = false;
		MainGameController.instance.isAlert = true;
		MainGameController.instance.eventNum--;
		yield return new WaitForSeconds(1);
		Ans.text = "";
		Ans.color = Color.white;
		numPadUi.SetActive(false);
		Cursor.visible = false;
		playerManager.gamePause = false;

	}

	public void OnTriggerStay(Collider other)
	{
		if(other.gameObject.tag == "Player")
		{
			PlayerManager playerManager = other.GetComponent<PlayerManager>();

			pressE.SetActive(true);

			if (Input.GetKey(KeyCode.E))
			{
				if (playerManager.gamePause == false)
				{
					MainGameController.instance.audioManager.SFXSource.PlayOneShot(MainGameController.instance.audioManager.openNumPad);
				}

				Ans.text = "";
				numPadUi.SetActive(true);
				MainGameController.instance.mouseOnOff(true);
				pauseMenu.isPause = true;
				playerManager.gamePause = true;
			}
			if (Input.GetKey(KeyCode.Escape))
			{
				Ans.text = "";
				numPadUi.SetActive(false);
				MainGameController.instance.mouseOnOff(false);
				playerManager.gamePause = false;
			}
		}
	}
	public void OnTriggerExit(Collider other)
	{
		if (other.gameObject.tag == "Player")
		{
			PlayerManager playerManager = other.GetComponent<PlayerManager>();

			if (playerManager.gamePause == true)
			{
				MainGameController.instance.audioManager.SFXSource.PlayOneShot(MainGameController.instance.audioManager.openPaper);
			}

			Ans.text = "";
			pressE.SetActive(false);
			numPadUi.SetActive(false);
			MainGameController.instance.mouseOnOff(false);
			pauseMenu.isPause = false;
			playerManager.gamePause = false;

		}
	}
}
