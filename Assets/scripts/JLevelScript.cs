using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JLevelScript : MonoBehaviour
{
    // Start is called before the first frame update
    
    Text texto;
    public bool godMode = false;
    public float Speed = 1;
    void Start()
    {
        texto = gameObject.GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if(godMode) texto.color = Color.HSVToRGB(Mathf.PingPong(Time.time * Speed, 1), 1, 1);
    }

    void activate(){
        godMode = true;
        texto.text ="INF";
    }
    
    void change(float amount){
        if(!godMode) texto.text = $"+{amount}";
    }

}
