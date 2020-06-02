using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private CharacterController characterController;
    public Vector3 direction;
    public float forwardSpeed = 10;

    private int currentLane = 2; // Initial lane of the player
    private int desiredLane = 2; // 2: Middle lane, 0 and 1: Left lanes, 3 and 4: Right lanes
    public float laneDistance = 2; // Distance between two lanes

    public float jumpForce;
    public float gravity = -20;

    public AudioSource gemSound;
    public AudioSource bonusSound;

    public Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.isGameStarted)
        {
            return;
        }

        animator.SetBool("isGameStarted", true);
        direction.z = forwardSpeed;

        if (characterController.isGrounded)
        {
            animator.SetBool("isGrounded", true);
            direction.y = -1;
            if (SwipeManager.swipeUp)
            {
                Jump();
            }
        } else
        {
            animator.SetBool("isGrounded", false);
            direction.y += gravity * Time.deltaTime;
        }
        

        if (SwipeManager.swipeRight)
        {
            desiredLane = desiredLane == 4 ? 4 : desiredLane += 1;
        }

        if (SwipeManager.swipeLeft)
        {
            desiredLane = desiredLane == 0 ? 0 : desiredLane - 1;
        }

        Vector3 newPosition = transform.position.z * transform.forward + transform.position.y * transform.up;

        if (desiredLane < currentLane) // Going left
        {
            switch (desiredLane)
            {
                case 3:
                    newPosition += Vector3.left * laneDistance;
                    break;
                case 1:
                    newPosition += Vector3.left * laneDistance * 2;
                    break;
                case 0:
                    newPosition += Vector3.left * laneDistance * 5;
                    break;
            }
            
        } else if (desiredLane > currentLane) // Going right
        {
            switch (desiredLane)
            {
                case 1:
                    newPosition += Vector3.right * laneDistance;
                    break;
                case 3:
                    newPosition += Vector3.right * laneDistance * 2;
                    break;
                case 4:
                    newPosition += Vector3.right * laneDistance * 5;
                    break;
            }
        }

        //transform.position = Vector3.Lerp(transform.position, newPosition, 60 * Time.deltaTime);
        if (transform.position == newPosition)
            return;
        Vector3 diff = newPosition - transform.position;
        Vector3 moveDir = diff.normalized * 25 * Time.deltaTime;
        if (moveDir.sqrMagnitude < diff.sqrMagnitude)
        {
            characterController.Move(moveDir);
        }
        else
        {
            characterController.Move(diff);
        }
    }

    private void FixedUpdate()
    {
        if (!GameManager.isGameStarted)
        {
            return;
        }
        characterController.Move(direction * Time.fixedDeltaTime);
    }

    private void Jump()
    {
        direction.y = jumpForce; 
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Bonus")
        {
            bonusSound.Play();
            direction.z += 100;
            characterController.Move(direction * Time.deltaTime);
        }

        if (other.tag == "Gem")
        {
            gemSound.Play();
        }
    }


}
