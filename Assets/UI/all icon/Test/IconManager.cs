using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IconManager : MonoBehaviour
{
    public Sprite[] iconAll;
    public Image[] iconPlayer;
	public int numberIcon;

	private void Start()
	{
		numberIcon = PlayerPrefs.GetInt("PlayerIcon");
		for(int i = 0; i < iconPlayer.Length; i++)
		{
			iconPlayer[i].sprite = iconAll[numberIcon];
		}
		

	}

	public void IconChange(int spriteNum)
	{
		for (int i = 0; i < iconPlayer.Length; i++)
		{
			iconPlayer[i].sprite = iconAll[spriteNum];
		}
		
		PlayerPrefs.SetInt("PlayerIcon", spriteNum);
		PlayerPrefs.Save();
	}


}
