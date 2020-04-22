using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Prime31;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    public CharacterController2D.CharacterCollisionState2D flags;
 	public float walkSpeed = 4.0f;     // Depois de incluido, alterar no Unity Editor
 	public float maxjumpSpeed = 8.0f;     // Depois de incluido, alterar no Unity Editor
 	public float gravity = 9.8f;       // Depois de incluido, alterar no Unity Editor
	public float chargeSpeed = .5f;
	public float jumpXModifier = 1f;
	public float jumpXBoost = 0f;
	float jumpSpeed = 0f;
 	private bool isGrounded;		// Se está no chão
	private bool wasGroundedLastFrame;
 	private bool isJumping;		// Se está pulando
	private bool isCharging;
	private bool isWalking;
	private bool hitHead;
	private bool hitLeft;
	private bool hitRight;

	private bool isDead = false;
	public bool gotFlower = false;
	public bool jumpPower = false;
	float x_direction = .01f;

	float flying_speed;
	float horizontal_input;
 	public Vector3 moveDirection = Vector3.zero; // direção que o personagem se move
 	private CharacterController2D characterController;	//Componente do Char. Controller

	public Animator animator;
	private GameObject jumpNo;
	private GameObject jumpUI;

	private AudioManager audioManager;
		

    void Start()
    {
		audioManager = FindObjectOfType<AudioManager>();
    	characterController = GetComponent<CharacterController2D>(); //identif. o componente
		jumpUI = GameObject.Find("JStrenght");
		jumpUI.SetActive(false);
		//StartCoroutine(walk());
    }

    void Update()
    {
		if(isDead){
			return;
		}
		if(jumpPower){
			if(Input.GetKeyDown("page up")){
				Debug.Log("Got +");
				if(jumpXBoost < 2f){
					jumpXBoost+= .5f;
					jumpNo.SendMessage("change", jumpXBoost);
				}
			}
			if(Input.GetKeyDown("page down")){
				Debug.Log("Got -");
				if(jumpXBoost > 0f){
					jumpXBoost-= .5f;
					jumpNo.SendMessage("change", jumpXBoost);
				}
			}
		}
		

		horizontal_input = Input.GetAxis("Horizontal");
        if(horizontal_input != 0){
			if(horizontal_input > 0){
				x_direction = 1f;
			}else{
				x_direction = -1f;
			}
			
		}

		if(x_direction < 0) {
			transform.eulerAngles = new Vector3(0,180,0);
		}
		else
		{
			transform.eulerAngles = new Vector3(0,0,0);
		}
		
		if(isGrounded) {			// caso esteja no chão
			if(isJumping){
				flying_speed = 0.0f;
				jumpSpeed = 0f;	
				audioManager.Play("fall");
				isJumping = false;
			}else{
				if(!isCharging){
					moveDirection.x = horizontal_input; // recupera valor dos controles
					moveDirection.x *= walkSpeed;
				}
				
			}
			
			  
			if(Input.GetButtonDown("Jump")){
				audioManager.Play("charge");
				isCharging = true;
				jumpSpeed = 1f;
				moveDirection.x = 0;
				
			}
			if(isCharging){
				if(Input.GetButton("Jump")){
					moveDirection.x = 0;
					jumpSpeed *= 1f + chargeSpeed;
				}else{
					isCharging = false;
				}
			}
			if(Input.GetButtonUp("Jump"))
			{	
				audioManager.Play("jump");
				if(jumpSpeed > maxjumpSpeed) jumpSpeed = maxjumpSpeed;
				moveDirection.y = jumpSpeed;
				flying_speed = x_direction*(jumpSpeed*jumpXModifier + jumpXBoost);
				moveDirection.x = flying_speed;
				isJumping = true;
				isCharging = false;
			}
		//Se comecou a cair mas nao tava pulando 
		}else if(wasGroundedLastFrame && !isJumping){
			moveDirection.y = 0f; //
		}else if(isJumping){
			moveDirection.x = flying_speed;
		}
		if(gotFlower){
			moveDirection.x = horizontal_input; // recupera valor dos controles
			moveDirection.x *= walkSpeed;
		}
		if(hitHead){
			if(isJumping){
				moveDirection.y = 0f;
			}
		}
		
		isWalking = (Mathf.Abs(moveDirection.x) > .2) && !isJumping;

		animator.SetBool("charging", isCharging);
		animator.SetBool("jumping", isJumping);
		animator.SetBool("walking", isWalking);
		animator.SetFloat("y_speed", moveDirection.y);

		moveDirection.y -= gravity * Time.deltaTime;	// aplica a gravidade
		if(Mathf.Abs(moveDirection.y) > gravity) moveDirection.y = -gravity;
		flags = characterController.collisionState; 	// recupera flags
		hitLeft = flags.left;
		hitRight = flags.right;
		if(hitLeft || hitRight){ //provavelmente n ta fazendo nada, mas talvez esteja e ta parecendo bom :)
			flying_speed=-flying_speed/2;
		}
		characterController.move(moveDirection * Time.deltaTime);	// move personagem	
		
		flags = characterController.collisionState; 	// recupera flags
		isGrounded = flags.below;				// define flag de chão
		wasGroundedLastFrame = flags.wasGroundedLastFrame;
		hitHead = flags.above;
    }


	void JumpUnlock(){
		
		audioManager.Play("win");
		jumpUI.SetActive(true);
		jumpNo = GameObject.Find("JLevel");
		jumpPower = true;
	}

	void EnableGodMode(){
		gotFlower = true;
		GameObject.Find("JLevel").SendMessage("activate");
		//set ui accordingly
	}

	void GotHit(){
		audioManager.Play("hit");
		StartCoroutine(death());

	}

	IEnumerator death(){
		animator.SetTrigger("die");
		isDead = true;
		yield return new WaitForSeconds(1f);
		gameObject.transform.position = new Vector3(-7f,-3.5f,0f);
		yield return new WaitForSeconds(4f);
		isDead = false;
		animator.ResetTrigger("die");
		yield return null;
	}
}
