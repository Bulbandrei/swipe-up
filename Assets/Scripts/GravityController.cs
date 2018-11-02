using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityController : MonoBehaviour {

    public static void NoGravity()
    {
        Physics.gravity = Vector3.zero;
    }

    public static void NormalGravity()
    {
        Physics.gravity = new Vector3(0,-9.8f,0);
    }

    public static void SetGravity(Vector3 gravity)
    {
        Physics.gravity = gravity;
    }
}
