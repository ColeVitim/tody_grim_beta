using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class CameraScript : MonoBehaviour
{
    [SerializeField]
    GameObject player;

    [SerializeField]
    GameObject cameraPos;

    [SerializeField]
    float timeOffset;

    [SerializeField]
    Vector2 posOffset;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        Vector3 StartPos = transform.position;
        Vector3 EndPos = cameraPos.transform.position;

        EndPos.x += posOffset.x;
        EndPos.y += posOffset.y;
        EndPos.z = -10;



        transform.position = Vector3.Lerp(StartPos, EndPos, timeOffset * Time.deltaTime);


    }
}
