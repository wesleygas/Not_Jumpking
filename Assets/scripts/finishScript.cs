﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class finishScript : MonoBehaviour
{
    // Start is called before the first frame update
    
    private void OnTriggerEnter2D(Collider2D collision){
        Debug.Log("Parabenizando");
        if(collision.tag == "Player"){
            SceneManager.LoadScene("parabains");
        }
    }
}
