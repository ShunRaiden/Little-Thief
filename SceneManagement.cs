using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneManagement : MonoBehaviour
{
	public void NextScene()
	{
		Time.timeScale = 1;
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
	}

	public void ReStart()
	{
		Time.timeScale = 1;
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}

	public void BackToMenu()
	{
		Time.timeScale = 1;
		SceneManager.LoadScene("MainMenu");
	}

	public void GoToRoom()
	{
		Time.timeScale = 1;
		SceneManager.LoadScene("PartyRoom");
	}

	public void ExitGame()
	{
		Application.Quit();
	}

	public void FristPage()
	{
		
		if (!PlayerPrefs.HasKey("PlayerName"))
		{
			PlayerPrefs.SetString("PlayerName", "0");
			PlayerPrefs.SetInt("PlayerIcon", 0);
			PlayerPrefs.Save();
			SceneManager.LoadScene("KeyUserName1stTIme");
		}
		else
		{
			SceneManager.LoadScene("MainMenu");
		}
		
	}

	public void ClearAll()
	{
		PlayerPrefs.DeleteAll();
		PlayerPrefs.Save();
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}

	
}
