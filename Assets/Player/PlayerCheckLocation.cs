using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCheckLocation : MonoBehaviour
{
    public bool isInShop2nd;


    void Start()
    {
        isInShop2nd = false;

    }

	private void OnTriggerEnter(Collider other)
	{
        if(other.gameObject.tag == "Shop2nd")
		{
            Debug.Log("t");
            isInShop2nd = true;
        }
        
	}
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Shop2nd")
        {
            Debug.Log("f");
            isInShop2nd = false;
        }

    }

}
