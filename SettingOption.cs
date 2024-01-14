using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingOption : MonoBehaviour
{
	public GameObject[] graphicText;
	int optionCount = 0;

	public void GraphicOption(int optionNum)
	{
		
		

		for (int i = 0; i < graphicText.Length; i++)
		{
			if(optionNum == i)
			{
				graphicText[i].SetActive(true);
				
			}else if(optionNum != i)
			{
				graphicText[i].SetActive(false);
			}
		}
		
	}
	public void SelectGraphicOption(bool isRight) 
	{
		
		if (isRight)
		{
			optionCount++;
		}
		else
		{
			optionCount--;
		}

		if (optionCount >= graphicText.Length)
		{
			optionCount = 0;
		}

		if (optionCount < 0)
		{
			optionCount = graphicText.Length-1;
		}

		GraphicOption(optionCount);
	}
}
