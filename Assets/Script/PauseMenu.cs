
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject gamePause;
    public GameObject gameSetting;
    public bool isPause = false;
    public PlayerManager playerManager;
    // Start is called before the first frame update
    void Start()
    {
        gamePause.gameObject.SetActive(false);

        MainGameController.instance.mouseOnOff(true);
        isPause = true;
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPause == true)
            {
                MainGameController.instance.mouseOnOff(false);
                isPause = false;

                MainGameController.instance.audioManager.SFXSource.PlayOneShot(MainGameController.instance.audioManager.arrowChange);

                gameSetting.SetActive(false);

                gamePause.SetActive(false);
                
            }
			else
			{
                MainGameController.instance.mouseOnOff(true);

                MainGameController.instance.audioManager.SFXSource.PlayOneShot(MainGameController.instance.audioManager.arrowChange);

                isPause = true;
                gamePause.SetActive(true);

                
            }
        }

    }

    public void OnResume()
	{
        playerManager = FindAnyObjectByType<PlayerManager>();

        isPause = false;
        MainGameController.instance.mouseOnOff(false);
        gamePause.SetActive(false);
        playerManager.gamePause = false;
        
    }

}
