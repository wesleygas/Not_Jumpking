using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform playerTransform;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(gameObject.transform.ToString());
        Debug.Log(playerTransform.ToString());
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 playPos = playerTransform.position;
        playPos.z = -10f;
        gameObject.transform.position = playPos;
    }
}
