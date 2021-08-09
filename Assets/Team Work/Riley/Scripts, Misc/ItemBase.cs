using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBase : MonoBehaviour
{
    public itemType thisItemType;
    public bool isUsed = false;
    public enum itemType
    {
        medallion1,
        medallion2,
        medallion3
    }
}
