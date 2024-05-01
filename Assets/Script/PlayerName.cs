using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerName : MonoBehaviour
{
    public TMP_Text[] name_Text;
    public TMP_InputField enterName;

    public GameObject changeNameMenu;

    public SceneManagement sceneManagement;
    public LoadingScreen loadingScreen;
    // Start is called before the first frame update


    void Start()
    {
        for (int i = 0; i < name_Text.Length; i++)
		{
			name_Text[i].text = PlayerPrefs.GetString("PlayerName");            
        }
        PlayerPrefs.SetString("PlayerName", name_Text[0].text);

    }

    public void SubmitNameFP()
    {
        if (enterName.text != "")
        {
            for (int i = 0; i < name_Text.Length; i++)
            {
				name_Text[i].text = enterName.text;
            }
            
            PlayerPrefs.SetString("PlayerName", name_Text[0].text);
            PlayerPrefs.Save();
            loadingScreen.LoadSceneTargetWithLoadingScreen("MainMenu");
        }
        else
        {
            enterName.placeholder.color = Color.red;
        }

    }

    public void SubmitName(GameObject someThingOff)
	{
		if (enterName.text != "")
		{
            for (int i = 0; i < name_Text.Length; i++)
            {
                name_Text[i].text = enterName.text;
            }

            PlayerPrefs.SetString("PlayerName", name_Text[0].text);
            PlayerPrefs.Save();
            enterName.placeholder.color = Color.black;
            someThingOff.SetActive(false);

        }
		else
		{
            enterName.placeholder.color = Color.red;
		}
        
	}

    public void SubmitName2()
    {
        if (enterName.text != "")
        {           
            enterName.placeholder.color = Color.black;
            changeNameMenu.SetActive(false);

        }
        else
        {
            enterName.placeholder.color = Color.red;
        }

    }
}
