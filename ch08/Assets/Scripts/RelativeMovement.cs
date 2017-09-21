using UnityEngine;
using System.Collections;

// 3rd-person movement that picks direction relative to target (usually the camera)
// commented lines demonstrate snap to direction and without ground raycast
//
// To setup animated character create an animation controller with states for idle, running, jumping
// transition between idle and running based on added Speed float, set those not atomic so that they can be overridden by...
// transition both idle and running to jump based on added Jumping boolean, transition back to idle

[RequireComponent(typeof(CharacterController))]
public class RelativeMovement : MonoBehaviour {
	[SerializeField] private Transform target;
	
	public float moveSpeed = 6.0f;
	public float rotSpeed = 15.0f;
	public float jumpSpeed = 15.0f;
	public float gravity = -9.8f;
	public float terminalVelocity = -20.0f;
	public float minFall = -1.5f;

	private float _vertSpeed;
	private ControllerColliderHit _contact;

	private CharacterController _charController;
	private Animator _animator;

	// Use this for initialization
	void Start() {
		_vertSpeed = minFall;

		_charController = GetComponent<CharacterController>();
		_animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update() {

		// start with zero and add movement components progressively
		Vector3 movement = Vector3.zero;

		// x z movement transformed relative to target
		float horInput = Input.GetAxis("Horizontal");
		float vertInput = Input.GetAxis("Vertical");
		if (horInput != 0 || vertInput != 0) {
			movement.x = horInput * moveSpeed;
			movement.z = vertInput * moveSpeed;
			movement = Vector3.ClampMagnitude(movement, moveSpeed);

			Quaternion tmp = target.rotation;
			target.eulerAngles = new Vector3(0, target.eulerAngles.y, 0);
			movement = target.TransformDirection(movement);
			target.rotation = tmp;

			// face movement direction
			//transform.rotation = Quaternion.LookRotation(movement);
			Quaternion direction = Quaternion.LookRotation(movement);
			transform.rotation = Quaternion.Lerp(transform.rotation,
			                                     direction, rotSpeed * Time.deltaTime);
		}
		_animator.SetFloat("Speed", movement.sqrMagnitude);

		// raycast down to address steep slopes and dropoff edge
		bool hitGround = false;
		RaycastHit hit;
		if (_vertSpeed < 0 && Physics.Raycast(transform.position, Vector3.down, out hit)) {
			float check = (_charController.height + _charController.radius) / 1.9f;
			hitGround = hit.distance <= check;	// to be sure check slightly beyond bottom of capsule
		}

		// y movement: possibly jump impulse up, always accel down
		// could _charController.isGrounded instead, but then cannot workaround dropoff edge
		if (hitGround) {
			if (Input.GetButtonDown("Jump")) {
				_vertSpeed = jumpSpeed;
			} else {
				_vertSpeed = minFall;
				_animator.SetBool("Jumping", false);
			}
		} else {
			_vertSpeed += gravity * 5 * Time.deltaTime;
			if (_vertSpeed < terminalVelocity) {
				_vertSpeed = terminalVelocity;
			}
			if (_contact != null ) {	// not right at level start
				_animator.SetBool("Jumping", true);
			}

			// workaround for standing on dropoff edge
			if (_charController.isGrounded) {
				if (Vector3.Dot(movement, _contact.normal) < 0) {
					movement = _contact.normal * moveSpeed;
				} else {
					movement += _contact.normal * moveSpeed;
				}
			}
		}
		movement.y = _vertSpeed;

		movement *= Time.deltaTime;
		_charController.Move(movement);
	}

	// store collision to use in Update
	void OnControllerColliderHit(ControllerColliderHit hit) {
		_contact = hit;
	}
}
