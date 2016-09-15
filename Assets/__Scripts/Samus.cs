using UnityEngine;
using System.Collections;

//Controls Samus movement and shooting
//Move Samus left and right
//Allow Samus to jump
//Allow Samus to fire

public class Samus : MonoBehaviour {
	
	//  These are variables set in the Inspector
	public float speedX = 4f;
	public float speedJump = 10f;

	public bool _____________; // seperator 

	//  These are variables set on Start
	public Rigidbody rigid;
	public bool grounded = false;
	public CapsuleCollider feet;
	public int groundPhysicsLayerMask;
	public Vector3 groundedCheckOffset;

	// Use this for initialization
	void Start () {
		rigid = GetComponent<Rigidbody>();

		//Get the feet
		Transform feetTrans = transform.Find("Feet");
		feet = feetTrans.GetComponent<CapsuleCollider>();

		// set ground physics layer mask
		groundPhysicsLayerMask = LayerMask.GetMask("Ground");
		groundedCheckOffset = new Vector3(feet.height * 0.8f, 0, 0);
	}
	
	// FixedUpdate is called once per physics engine update (50fps)
	void FixedUpdate () {
		// Check to see whether we are grounded or not
		Vector3 CheckLoc = feet.transform.position + Vector3.up * (feet.radius * 0.9f);
		grounded = Physics.Raycast(CheckLoc, Vector3.down, feet.radius, groundPhysicsLayerMask);

		Vector3 vel = rigid.velocity;

		//Handle L and R movement
		if (Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.RightArrow)){
			vel.x = -speedX;
		}
		else if (Input.GetKey(KeyCode.RightArrow) && !Input.GetKey(KeyCode.LeftArrow)){
			vel.x = speedX;
		}
		else{
			vel.x = 0;
		}
		////////////////////This is Jason's Thought, if grounded and key down and velocity != 0,
		/// we make samus Jump into a ball and interact as a running jump.
		/// if grounded and velocity == 0, stand still jump.
		/// This also means running jump needs a bool so that you can not change velocity by pushing arrow keys mid jump
		if (Input.GetKeyDown(KeyCode.A)){
			vel.y = speedJump;
		}

		if (Input.GetKey(KeyCode.UpArrow)){
		}
		if (Input.GetKey(KeyCode.S)){
		}

		rigid.velocity = vel;
	}
}
