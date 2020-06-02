using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotController : MonoBehaviour
{

    public CharacterController characterController;

    private Vector3 direction;
    private float forwardSpeed;

    private float jumpForce = 6;
    private float gravity = -20;

    // Start is called before the first frame update
    private void Start()
    {
        forwardSpeed = Random.Range(9.5f, 10);
        characterController = gameObject.GetComponent<CharacterController>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (!GameManager.isGameStarted)
        {
            return;
        }

        GetComponent<Animator>().SetBool("isGameStarted", true);
        direction.z = forwardSpeed;

        if (this.characterController.isGrounded)
        {
            GetComponent<Animator>().SetBool("isGrounded", true);
            direction.y = -1;

            GameObject[] obstacles = GameObject.FindGameObjectsWithTag("Obstacle");
            if (obstacles.Length != 0)
            {
                // Get min distance
                float minDist = int.MaxValue;
                GameObject closest = obstacles[0];
                foreach (GameObject obstacle in obstacles)
                {
                    float distanceToCurrentObstacle = Vector3.Distance(transform.position, obstacle.transform.position);
                    if (distanceToCurrentObstacle < minDist)
                    {
                        minDist = distanceToCurrentObstacle;
                        closest = obstacle;
                    }
                }

                if (minDist <= 5)
                {
                    Jump();
                    Debug.Log(name + " jumped, obstacle is at distance: " + minDist);
                }
            }

        }
        else
        {
            GetComponent<Animator>().SetBool("isGrounded", false);
            direction.y += gravity * Time.deltaTime;
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
        if (other.tag == "Player")
        {
            GetComponent<Animator>().SetTrigger("isHit");
            direction.z -= 20;
            characterController.Move(direction * Time.deltaTime);
        } 
    }

}
