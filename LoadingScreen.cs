using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingScreen : MonoBehaviour
{
	public GameObject loaderUI;
	public Image loadSlider;

	public void LoadSceneTargetWithLoadingScreen(string index)
	{
		StartCoroutine(LoadingScreenTimer(index));
	}

	public IEnumerator LoadingScreenTimer(string index)
	{
		loadSlider.fillAmount = 0;
		loaderUI.SetActive(true);

		AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(index);
		asyncOperation.allowSceneActivation = false;
		float progress = 0;
		while (!asyncOperation.isDone)
		{
			progress = Mathf.MoveTowards(progress, asyncOperation.progress, Time.deltaTime);
			loadSlider.fillAmount = progress;
			if (progress >= 0.9f)
			{
				loadSlider.fillAmount = 1;
				asyncOperation.allowSceneActivation = true;
			}
			yield return null;
		}
	}
}
