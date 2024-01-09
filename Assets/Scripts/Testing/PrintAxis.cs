using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrintAxis : MonoBehaviour
{
    private void Update()
    {
        var transform1 = this.transform;
        print(this.name + ": (UP, FORWARD, RIGHT)" + transform1.up + " " + transform1.forward + " " + transform1.right);
    }
}
