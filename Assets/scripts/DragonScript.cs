using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonScript : MonoBehaviour
{
    // Start is called before the first frame update
    public float delay = 3f;
    public Animator animator;
    public GameObject firebal;
    private AudioManager audioManager;
    void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
        StartCoroutine(shotter());
    }

    IEnumerator shotter(){
        while(true){
            
            animator.ResetTrigger("fire");
            animator.SetTrigger("fire");
            audioManager.Play("firebal");
            Instantiate(firebal);
            yield return new WaitForSeconds(delay);
        }
    }
}
