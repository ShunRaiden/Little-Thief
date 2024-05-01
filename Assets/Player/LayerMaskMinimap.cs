using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayerMaskMinimap : MonoBehaviour
{

    public Camera miniMap;
    public LayerMask nomaL;
    public LayerMask _2rdF;
    public PlayerCheckLocation checkLocation;

    // Start is called before the first frame update
    void Start()
    {

        
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if (checkLocation.isInShop2nd == false)
        {
            Debug.Log("1");
            miniMap.cullingMask = nomaL;
        }

        if (checkLocation.isInShop2nd == true)
		{
            Debug.Log("2");
            miniMap.cullingMask = _2rdF;
		}
		
        
    }
}
