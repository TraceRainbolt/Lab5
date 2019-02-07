using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereBackground : MonoBehaviour {

    public float rotSpeed = 2.0f;

    void Update () {
        rotSpeed += GameControl.instance.ticks / 600000000f;
        if (rotSpeed > 0.63)
            rotSpeed = 0.63f;
        transform.Rotate(new Vector3(rotSpeed, rotSpeed, 0));
	}
}
