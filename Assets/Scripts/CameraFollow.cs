﻿using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour
{


    public Transform Player;
    float cameraZ;


    // Use this for initialization
    void Start()
    {
        cameraZ = transform.position.z;
    }

    void Update()
    {
        transform.position = new Vector3(Player.position.x + 0.5f, 0, cameraZ);
    }

}
