using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class godCapsule : MonoBehaviour
{
    // Start is called before the first frame update
    private Renderer rend;
    public float Speed = 1;
    void Start()
    {
        rend = GetComponent<Renderer>();

    }

    // Update is called once per frame
    void Update()
    {
        rend.material.SetColor("_Color", Color.HSVToRGB( Mathf.PingPong(Time.time * Speed, 1), 1, 1));
    }
}
