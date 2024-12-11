using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float speed;
    Rigidbody rb;
    float xInput;
    float yInput;
    Vector3 moveDirection;
    private Animator animator;


    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    void Start()
    {
        
    }


    void Update()
    {
        // restart game if fall off platform
        if (transform.position.y < -5f) {
            SceneManager.LoadScene("Game");
        }
    }

    void FixedUpdate()
    {
        // get player input
        xInput = Input.GetAxis("Horizontal");
        yInput = Input.GetAxis("Vertical");

        // rotate player if there's input
        moveDirection = new Vector3(xInput, 0, yInput);
        if (moveDirection.magnitude > 0.1f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10f);
        }

        // move players
        Vector3 movement = new Vector3(xInput, 0, yInput) * speed * Time.deltaTime;
        rb.MovePosition(transform.position + movement);

        // animation
        if (moveDirection.magnitude == 0)   //no movement
        {
            animator.SetFloat("speed", 0);
        }
        else    // movement
        { 
            animator.SetFloat("speed", 1);
        }
    }
}
