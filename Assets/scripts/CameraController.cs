using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform playerTransform;
    

    // Update is called once per frame
    void Update()
    {
        Vector3 playPos = playerTransform.position;
        Vector3 newPos = gameObject.transform.position;
        if(playPos.x > -18.34f && playPos.x < 4.16f){
            newPos.x = playPos.x;
        }
        if(playPos.y > -10.7f && playPos.y < 30f){
            newPos.y = playPos.y;
        }
        newPos.z = -10f;
        gameObject.transform.position = newPos;
    }
}
