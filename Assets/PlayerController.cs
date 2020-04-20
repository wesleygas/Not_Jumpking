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
	float jumpSpeed = 0f;
 	public bool isGrounded;		// Se está no chão
	public bool wasGroundedLastFrame;
	public bool movingSlope;
 	public bool isJumping;		// Se está pulando
	public bool isCharging;
	public bool hitHead;
	public bool hitLeft;
	public bool hitRight;
	float x_direction = .01f;

	float flying_speed;
	float horizontal_input;
 	public Vector3 moveDirection = Vector3.zero; // direção que o personagem se move
 	private CharacterController2D characterController;	//Componente do Char. Controller

	public Animator animator;

    void Start()
    {
    	characterController = GetComponent<CharacterController2D>(); //identif. o componente
    }

    void Update()
    {
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
				isJumping = false;
			}else{
				if(!isCharging){
					moveDirection.x = horizontal_input; // recupera valor dos controles
					moveDirection.x *= walkSpeed;
				}
				
			}
			
			  
			if(Input.GetButtonDown("Jump")){
				isCharging = true;
				jumpSpeed = 1f;
				moveDirection.x = 0;
			}
			if(isCharging && Input.GetButton("Jump")){
				moveDirection.x = 0;
				jumpSpeed *= 1f + chargeSpeed;
			}
			if(Input.GetButtonUp("Jump"))
			{
				if(jumpSpeed > maxjumpSpeed) jumpSpeed = maxjumpSpeed;
				moveDirection.y = jumpSpeed;
				flying_speed = x_direction*jumpSpeed*jumpXModifier;
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
		if(hitHead){
			if(isJumping){
				moveDirection.y = 0f;
			}
		}
		
		animator.SetBool("charging", isCharging);
		animator.SetBool("jumping", isJumping);
		animator.SetBool("walking", Mathf.Abs(moveDirection.x) > .2);
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
		movingSlope = flags.movingDownSlope;
		hitHead = flags.above;
    }

}
