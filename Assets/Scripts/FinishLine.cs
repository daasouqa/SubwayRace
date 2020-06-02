using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishLine : MonoBehaviour
{
    private void Start()
    {
        transform.Rotate(-90, 0, 0);
        Vector3 newPos = transform.position;
        newPos.y = -0.5f;
        transform.position= newPos;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" || other.tag == "Bot")
        {
            GameManager.orderList.Add(other.gameObject);
            Debug.Log("N" + GameManager.orderList.Count + ": " + other.gameObject.name);
            if(other.tag == "Player")
            {
                if(GameManager.orderList.Count == 1)
                {
                    GameManager.win = true;
                }
                else
                {
                    GameManager.gameOver = true;
                }
            }
        }
    }
}
