using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class SelectStage : MonoBehaviour
{
	public TMP_Text stageText;
	public LoadingScreen loadingScreen;
	private void Start()
	{
		if(!PlayerPrefs.HasKey("StageGame"))
		{
			PlayerPrefs.SetString("StageGame", "Stage01");
			PlayerPrefs.Save();			
		}

		stageText.text = PlayerPrefs.GetString("StageGame");
	}
	public void SelectWantStage(string stage)
	{
		PlayerPrefs.SetString("StageGame", stage);
		PlayerPrefs.Save();
		stageText.text = PlayerPrefs.GetString("StageGame");
	}

	public void StartGameWithStage()
	{
		string stageSelected;
		stageSelected = PlayerPrefs.GetString("StageGame");
		loadingScreen.LoadSceneTargetWithLoadingScreen(stageSelected);

	}
}
