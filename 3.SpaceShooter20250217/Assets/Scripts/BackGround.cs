using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGround : MonoBehaviour
{
    private Transform transPlayer;
    public float rate = 0.2f;
    void Start()
    {
        transPlayer = GameObject.Find("Player").transform;
        if (transPlayer == null) Debug.Log("BackGround.cs/Start() : transPlayer is Not Found");
    }
    void Update()
    {
        transform.position = transPlayer.position * -rate;
    }
}