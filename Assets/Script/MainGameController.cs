using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainGameController : MonoBehaviour
{
    public static MainGameController instance;
    public bool isAlert = false;
    public GameObject[] mainQuest;
    public GameObject[] arrowAll;
    
    public GameObject loseUi;
    public bool gameLose = false;
    public bool canExit = false;
    public int questDone = 0;
    public int playerCount = 1;
    public int eventNum;

    public GameObject spawnPoint;
    public GameObject[] playerPref;
    public int chaNum;

    public AudioManager audioManager;

    public GameObject skillOffUI;

    PlayerManager playerManager;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(this);
        }
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {   
        eventNum = arrowAll.Length - 1;
        isAlert = false;

        chaNum = PlayerPrefs.GetInt("CharacterSelection");        
        Instantiate(playerPref[chaNum], spawnPoint.transform.position, spawnPoint.transform.rotation);

        audioManager = FindObjectOfType<AudioManager>();

        audioManager.musicSource.clip = audioManager.backGroundIngame;
        audioManager.musicSource.Play();

    }
    public int i = 0;
    // Update is called once per frame
    void Update()
    {
		if (isAlert)
		{
            if(i == 0)
			{
                audioManager.musicSource.clip = audioManager.backGroundAlert;
                audioManager.musicSource.Play();
                i++;
            }
            
        }

        if(mainQuest.Length == questDone)
		{
            canExit = true;
		}
        if(gameLose == true)
		{
            playerManager = FindAnyObjectByType<PlayerManager>();
            playerManager.gamePause = true;

            
            if(i == 1)
			{
                audioManager.musicSource.Stop();
                audioManager.SFXSource.PlayOneShot(audioManager.loseGame);               
                i++;
            }            

            loseUi.SetActive(true);

            
		}

        EventArrow();



    }

    public void mouseOnOff(bool OnOff)
    {
        if (OnOff)
        {
            Cursor.visible = true;

        }
        
        if(!OnOff)
        {
            Cursor.visible = false;

        }
    }

    public void EventArrow()
	{
        for(int i = 0; i < arrowAll.Length; i++)
		{
			if (i == eventNum)
			{
                arrowAll[eventNum].SetActive(true);
            }

            if(i != eventNum)
			{
                arrowAll[i].SetActive(false);
            }
            
        }
		
        
    }

	public KeyHolder keyHolder;
    public ItemCollect itemCollect;
}
