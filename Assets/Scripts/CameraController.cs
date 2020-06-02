using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public Transform player;
    private Vector3 offset;
    // Start is called before the first frame update
    void Start()
    {
        offset = transform.position - player.position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 newCameraPosition = new Vector3(transform.position.x, transform.position.y, offset.z + player.position.z);
        //transform.position = Vector3.Lerp(transform.position, newCameraPosition, 10 * Time.deltaTime);
        transform.position = newCameraPosition;
    }
}
