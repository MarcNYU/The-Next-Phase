using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementCC : MonoBehaviour {
    
    public int playerNumber;          // Used to identify which tank belongs to which player.  This is set by this player's manager.
    public float speed = 6f;            // The speed that the player will move at.

    Animator anim;                      // Reference to the animator component.
    int floorMask;                      // A layer mask so that a ray can be cast just at gameobjects on the floor layer.

    public float Gravity = -9.81f;
    public float GroundDistance = 0.2f;
    public float DashDistance = 5f;
    public LayerMask Ground;
    public Vector3 Drag;

    public AudioSource movementAudio;         // Reference to the audio source used to play engine sounds. NB: different to the shooting audio source.
    public AudioClip engineIdling;            // Audio to play when the player isn't moving.
    public AudioClip engineDriving;           // Audio to play when the player is moving.
    public float pitchRange = 0.2f;           // The amount by which the pitch of the engine noises can vary.
    private float originalPitch;              // The pitch of the audio source at the start of the scene.

    private CharacterController controller;
    private Vector3 velocity;
    private bool isGrounded = true;
    private Transform groundChecker;


	private void Awake()
	{
        // Create a layer mask for the floor layer.
        floorMask = LayerMask.GetMask("Floor");

        // Set up references.
        anim = GetComponent<Animator>();
	}

	void Start()
    {
        controller = GetComponent<CharacterController>();
        groundChecker = transform.GetChild(0);

        // Store the original pitch of the audio source.
        originalPitch = movementAudio.pitch;
    }

    void Update()
    {
        EngineAudio();

        isGrounded = Physics.CheckSphere(groundChecker.position, GroundDistance, Ground, QueryTriggerInteraction.Ignore);
        if (isGrounded && velocity.y < 0)
            velocity.y = 0f;

        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        controller.Move(move * Time.deltaTime * speed);
        if (move != Vector3.zero)
            transform.forward = move;
        
        if (Input.GetButtonDown("Dash"))
        {
            Debug.Log("Dash");
            velocity += Vector3.Scale(transform.forward, DashDistance * new Vector3((Mathf.Log(1f / (Time.deltaTime * Drag.x + 1)) / -Time.deltaTime), 0, (Mathf.Log(1f / (Time.deltaTime * Drag.z + 1)) / -Time.deltaTime)));
        }


        velocity.y += Gravity * Time.deltaTime;

        velocity.x /= 1 + Drag.x * Time.deltaTime;
        velocity.y /= 1 + Drag.y * Time.deltaTime;
        velocity.z /= 1 + Drag.z * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);
    }

    void FixedUpdate()
    {
        // Animate the player.
        Animating(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
    }

    private void EngineAudio()
    {
        // If there is no input (the tank is stationary)...
        if (Mathf.Abs(Input.GetAxis("Horizontal")) < 0.1f && Mathf.Abs(Input.GetAxis("Vertical")) < 0.1f)
        {
            // ... and if the audio source is currently playing the driving clip...
            if (movementAudio.clip == engineDriving)
            {
                // ... change the clip to idling and play it.
                movementAudio.clip = engineIdling;
                movementAudio.pitch = Random.Range(originalPitch - pitchRange, originalPitch + pitchRange);
                movementAudio.Play();
            }
        }
        else
        {
            // Otherwise if the tank is moving and if the idling clip is currently playing...
            if (movementAudio.clip == engineIdling)
            {
                // ... change the clip to driving and play.
                movementAudio.clip = engineDriving;
                movementAudio.pitch = Random.Range(originalPitch - pitchRange, originalPitch + pitchRange);
                movementAudio.Play();
            }
        }
    }

    void Animating(float h, float v)
    {
        // Create a boolean that is true if either of the input axes is non-zero.
        bool walking = h != 0f || v != 0f;

        // Tell the animator whether or not the player is walking.
        anim.SetBool("IsWalking", walking);
    }
}
