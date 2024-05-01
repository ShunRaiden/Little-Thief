using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillPlayer : MonoBehaviour
{
    public GameObject pivotPlayer;
    public GameObject skillPref;
    public GameObject currentSkill;


	// Update is called once per frame
	void Update()
    {
        
		if (Input.GetKeyDown(KeyCode.Q))
		{
            if (currentSkill == null)
            {
                StartCoroutine(SkillCoolDown());
                
            }
            
        }
    }

    public IEnumerator SkillCoolDown()
	{
        currentSkill = Instantiate(skillPref, pivotPlayer.transform.position, pivotPlayer.transform.rotation);
        MainGameController.instance.skillOffUI.SetActive(true);
        yield return new WaitForSeconds(10);
        MainGameController.instance.skillOffUI.SetActive(false);
        Destroy(currentSkill);
    }
}
