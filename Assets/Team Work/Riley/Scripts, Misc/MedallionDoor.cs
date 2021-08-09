using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MedallionDoor : MonoBehaviour
{
    //Private Vars
    private ItemBase[] medallionArray;
    private int medallionCounter;
    private bool isDoorOpen;

    //Public Vars
    public GameObject medallionSlot1;
    public GameObject medallionSlot2;
    public GameObject medallionSlot3;

    private void Start()
    {
        isDoorOpen = false;
        medallionCounter = 0;
    }

    private void FixedUpdate()
    {
        if (medallionCounter == 3)
        {
            isDoorOpen = true;
            OpenDoor();
        }
        if (medallionArray != null && isDoorOpen == false)
        {
            medallionCounter = 0;
            for (int i = 0; i < medallionArray.Length; i++)
            {
                if (medallionArray[i].thisItemType == ItemBase.itemType.medallion1)
                {
                    medallionCounter += 1;
                }
                else if(medallionArray[i].thisItemType == ItemBase.itemType.medallion2)
                {
                    medallionCounter += 1;
                }
                else if(medallionArray[i].thisItemType == ItemBase.itemType.medallion3)
                {
                    medallionCounter += 1;
                }
            }
        }
    }

    public void SlotMedallionIn(GameObject medallion)
    {
        if (medallion.GetComponent<ItemBase>() != null)
        {
            ItemBase medallionIB = medallion.GetComponent<ItemBase>();
            if (medallionIB.thisItemType == ItemBase.itemType.medallion1)
            {
                medallionIB.isUsed = true;
                medallionArray[medallionArray.Length] = medallionIB;
            }
            else if(medallion.GetComponent<ItemBase>().thisItemType == ItemBase.itemType.medallion2)
            {
                medallionIB.isUsed = true;
                medallionArray[medallionArray.Length] = medallionIB;
            }
            else if(medallion.GetComponent<ItemBase>().thisItemType == ItemBase.itemType.medallion3)
            {
                medallionIB.isUsed = true;
                medallionArray[medallionArray.Length] = medallionIB;
            }
        }
    }
    
    private void OpenDoor()
    {
        Vector3 thisPos = gameObject.transform.position;
        gameObject.transform.position = new Vector3(thisPos.x, thisPos.y - 10, thisPos.z);
    }
}
