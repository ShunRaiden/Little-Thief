using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowAI : MonoBehaviour
{
	public void OnTriggerStay(Collider other)
	{
		if(other.gameObject.tag == "Enemy")
		{
			other.GetComponent<EnemyNav>().ChaseSpeed = 2;
		}
	}

	public void OnTriggerExit(Collider other)
	{
		if (other.gameObject.tag == "Enemy")
		{
			other.GetComponent<EnemyNav>().ChaseSpeed = 4;
		}
	}
}
