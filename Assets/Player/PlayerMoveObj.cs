using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveObj : MonoBehaviour
{
    [SerializeField] private Transform playerCamTran;
    [SerializeField] private Transform objGarbPointTrans;
    [SerializeField] private LayerMask pickUpLayerMask;

    private ItemCanMove itemCanMove;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
		{ 
            if(itemCanMove == null)
			{
                // not carry any obj and want to grab
                float pickUpDistance = 2f;
                if (Physics.Raycast(playerCamTran.position, playerCamTran.forward, out RaycastHit raycastHit, pickUpDistance))
                {
                    if (raycastHit.transform.TryGetComponent(out itemCanMove))
                    {
                        itemCanMove.Grap(objGarbPointTrans);
                    }
				}
				
            }
            else
            {
                itemCanMove.Drop();
                itemCanMove = null;
            }

        }
    }
}
