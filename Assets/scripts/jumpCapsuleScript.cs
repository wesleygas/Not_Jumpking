using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jumpCapsuleScript : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision){
        if(collision.tag == "Player"){
            //FindObjectOfType<AudioManager>().Play("alienDeath");
            collision.gameObject.SendMessage("JumpUnlock");
            Destroy(gameObject);
        }
    }
}
