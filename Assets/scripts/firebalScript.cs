using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class firebalScript : MonoBehaviour
{


    // Update is called once per frame
    public float speed = 1f;
    void Update()
    {
        Vector3 pos = gameObject.transform.position;
        if(pos.x > 10f) Destroy(gameObject);
        pos.x+= speed;
        gameObject.transform.position = pos;

    }

    private void OnTriggerEnter2D(Collider2D collision){
        if(collision.tag == "Player"){
            //FindObjectOfType<AudioManager>().Play("alienDeath");
            collision.gameObject.SendMessage("GotHit");
            Destroy(gameObject);
        }
    }
}
