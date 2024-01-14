using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemyNav : MonoBehaviour
{
	
	public NavMeshAgent aiNav;
	public List<Transform> destination;
	public Animator aiAnim;

	public float walkSpeed, ChaseSpeed,minIdleTime,maxIdleTime,idleTime, sightCastDistance;
	public float catchDistance, chaseTime, minChaseTime, maxChaseTime;
	public float teBeBoxTime;
	public bool walking, Chasing;

	public Transform player;
	Transform currentDest;

	Vector3 dest;
	public Vector3 rayCastOffset;
	public Vector3 dir;

	int randNum;
	public int destinationAmount;

	public Vector3 forCatch;
	public float nearToCatchX, nearToCatchZ;

	public int i = 0;
	//public GameObject toBeBoxScene;

	private void Start()
	{
		walking = true;
		randNum = Random.Range(0, destinationAmount);
		currentDest = destination[randNum];
		
	}

	private void Update()
	{
		player = FindNearestObjectByTag("Player");
		if(player != null && MainGameController.instance.isAlert == true)
		{
			dir = (player.position - transform.position).normalized;
			RaycastHit hit;
			if (Physics.Raycast(transform.position + rayCastOffset, dir, out hit, sightCastDistance))
			{
				if (hit.collider.gameObject.tag == "Player")
				{
					Debug.Log("RunToPlayer");
					walking = false;
					StopCoroutine(StayIdle());
					Chasing = true;

				}
			}
			else
			{
				Debug.Log("BackToWalk");
				walking = true;
				Chasing = false;
			}
		}
		
		if (Chasing)
		{
			if(i == 0)
			{
				MainGameController.instance.audioManager.SFXSource.PlayOneShot(MainGameController.instance.audioManager.enemyDetect);
				i++;
			}

			forCatch = new Vector3(Mathf.Abs(player.transform.position.x - transform.position.x), 0, Mathf.Abs(player.transform.position.z - transform.position.z));
			nearToCatchX = forCatch.x;
			nearToCatchZ = forCatch.z;

			dest = player.position;
			aiNav.destination = dest;
			aiNav.speed = ChaseSpeed;
			aiAnim.ResetTrigger("Walk");
			aiAnim.ResetTrigger("Idle");
			aiAnim.SetTrigger("Run");

			if (player.gameObject.GetComponent<BoxCollider>().enabled == false)
			{
				if(i == 1)
				{
					i = 0;
				}

				aiAnim.ResetTrigger("Catch");
				aiAnim.ResetTrigger("Walk");
				aiAnim.SetTrigger("Idle");
				aiAnim.ResetTrigger("Run");
				randNum = Random.Range(0, destinationAmount);
				currentDest = destination[randNum];
				StartCoroutine(StayIdle());
				Chasing = false;
				
			}
			Debug.Log(nearToCatchX);
			if (nearToCatchX <= catchDistance && nearToCatchZ <= catchDistance)
			{
				Debug.Log(nearToCatchX);
				Debug.Log("in");
				aiAnim.SetTrigger("Catch");
				aiAnim.ResetTrigger("Walk");
				aiAnim.ResetTrigger("Idle");
				aiAnim.ResetTrigger("Run");				
				StartCoroutine(TobeBox());				
				player.gameObject.GetComponent<BoxCollider>().enabled = false;
				player.gameObject.GetComponent<PlayerLocoMotion>().isCatch = true;
				Chasing = false;
				//aiAnim.ResetTrigger("Catch");
				//aiAnim.SetTrigger("Idle");
				if (MainGameController.instance.playerCount != 0)
				{
					MainGameController.instance.playerCount--;
				}
				StartCoroutine(NoPlayerAlive());
				StartCoroutine(StayIdle());
				Debug.Log("Pass");
			}
		}
		if (walking)
		{
			dest = currentDest.position;
			aiNav.destination = dest;
			aiNav.speed = walkSpeed;
			aiAnim.ResetTrigger("Run");
			aiAnim.ResetTrigger("Idle");
			aiAnim.SetTrigger("Walk");

			if (aiNav.remainingDistance <= aiNav.stoppingDistance)
			{
				aiAnim.ResetTrigger("Run");
				aiAnim.ResetTrigger("Walk");
				aiAnim.SetTrigger("Idle");
				aiNav.speed = 0;
				StopCoroutine(StayIdle());
				StartCoroutine(StayIdle());
				walking = false;
			}
		}		

	}

	//FindNearestObjectByTag
	Transform FindNearestObjectByTag(string tag)
	{
		GameObject[] objectsWithTag = GameObject.FindGameObjectsWithTag(tag);
		Transform nearestObject = null;
		float shortestDistance = Mathf.Infinity;

		foreach (GameObject obj in objectsWithTag)
		{
			float distance = Vector3.Distance(transform.position, obj.transform.position);
			if (distance < shortestDistance)
			{
				shortestDistance = distance;
				nearestObject = obj.transform;
			}
		}

		return nearestObject;
	}


	IEnumerator StayIdle()
	{
		idleTime = Random.Range(minIdleTime, maxIdleTime);
		yield return new WaitForSeconds(idleTime);
		walking = true;
		randNum = Random.Range(0, destinationAmount);
		currentDest = destination[randNum];

	}

	public void stopChase()
	{
		walking = true;
		Chasing = false;
		randNum = Random.Range(0, destinationAmount);
		currentDest = destination[randNum];
	}

	IEnumerator TobeBox()
	{
		yield return new WaitForSeconds(teBeBoxTime);		
	}

	IEnumerator NoPlayerAlive()
	{
		yield return new WaitForSeconds(3);
		if (MainGameController.instance.playerCount == 0)
		{
			MainGameController.instance.mouseOnOff(true);
			MainGameController.instance.gameLose = true;
		}
		
	}

	private void OnDrawGizmos() //ระยะการตรวจจัง Enemy 
	{
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(transform.position, catchDistance);
		Gizmos.DrawWireSphere(transform.position, sightCastDistance);
		Gizmos.DrawLine(transform.position, dir);
	}

}
