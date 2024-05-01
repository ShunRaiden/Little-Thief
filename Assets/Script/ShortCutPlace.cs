using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShortCutPlace : MonoBehaviour
{
	public GameObject blockShortCut;

	Rigidbody rb;


	private void Start()
	{
		rb = GetComponent<Rigidbody>();	
	}

	public void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.tag == "Player")
		{
			rb.isKinematic = false;
			Destroy(blockShortCut);
		}
	}
}
