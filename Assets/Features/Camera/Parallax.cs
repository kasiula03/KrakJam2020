using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class Parallax : MonoBehaviour
{

    [SerializeField] private Transform followCamer;
    [SerializeField] private float multiplier;

    [SerializeField] private float lastFrameX;

    void Awake()
    {
        followCamer = Camera.main.transform;
    }

    void Start()
    {
        lastFrameX = followCamer.transform.position.x;
    }

    void LateUpdate()
    {
        float currFrameX = followCamer.transform.position.x;

        float difference = currFrameX - lastFrameX;
        
        this.transform.Translate(new Vector3(difference * multiplier, 0f));
        lastFrameX = currFrameX;
    }
}
