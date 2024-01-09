using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrintUpRotation : MonoBehaviour
{
    private void OnEnable()
    {
        print(this.name + ": " + this.transform.up);
    }
}
