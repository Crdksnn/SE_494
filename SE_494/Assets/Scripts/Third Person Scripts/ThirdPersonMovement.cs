using System;
using Unity.VisualScripting;
using UnityEditor.Animations;
using UnityEngine;

public class ThirdPersonMovement : MonoBehaviour
{
    //MOVEMENT VARIABLES
    [Header("Movement Configures")]
    public CharacterController controller;
    public Transform cam;
    public float moveSpeed = 6;
    public float turnSmoothTime = 0.1f;
    private float _turnSmoothVelocity;
    
    //GRAVITY VARIABLES
    [Header("Gravity Configures")]
    private bool _isGrounded;
    public float gravity = -9.18f;
    public Transform groundCheck;
    public float groundDistance = .4f;
    public LayerMask groundMask;
    Vector3 _velocity;
    
    //ANIMATOR CONTROLLER
    [Header("Animator Controller")]
    [SerializeField] private Animator animator;
    
    //SCENE MANAGER
    [Header("Scene Manager")]
    [SerializeField] private SceneFader sceneFader;
    
    void Start()
    {
        
    }
    
    
    void Update()
    {
        Movement();
    }

    private void Movement()
    {
        //Changing animation
        animator.SetBool("isMoving", false);
        
        //Each frame we are checking collide with ground
        _isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (_isGrounded && _velocity.y < 0)
            _velocity.y = -3f;
        
        _velocity.y += gravity * Time.deltaTime;
        controller.Move(_velocity * Time.deltaTime);
        
        //Getting move from (WASD - Arrow Keys) input from user
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        //Changing player position
        if (direction.magnitude >= 0.1f)
        {
            animator.SetBool("isMoving", true);
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref _turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDir.normalized * moveSpeed * Time.deltaTime);
        }
    }
    
}
