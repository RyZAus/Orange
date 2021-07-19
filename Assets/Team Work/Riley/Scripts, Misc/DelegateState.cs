using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class DelegateState
{
    public Action Enter;
    public Action Update;
    public Action Exit;
    public bool isRunning;
}
