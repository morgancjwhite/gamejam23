using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private float scrollSpeed;
    [SerializeField] private float scrollWidthHorizontal;
    [SerializeField] private float scrollWidthVertical;

    private void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        MoveCamera();
    }

    private void MoveCamera()
    {
        float xpos = Input.mousePosition.x;
        float ypos = Input.mousePosition.y;
        Vector3 movement = new Vector3(0, 0, 0);

        //horizontal camera movement
        if (xpos >= 0 && xpos < scrollWidthHorizontal)
        {
            movement.x -= scrollSpeed;
        }
        else if (xpos <= Screen.width && xpos > Screen.width - scrollWidthHorizontal)
        {
            movement.x += scrollSpeed;
        }

        //vertical camera movement
        if (ypos >= 0 && ypos < scrollWidthVertical)
        {
            movement.y -= scrollSpeed;
        }
        else if (ypos <= Screen.height && ypos > Screen.height - scrollWidthVertical)
        {
            movement.y += scrollSpeed;
        }

        //calculate desired camera position based on received input
        Vector3 origin = transform.position;
        Vector3 destination = origin;
        destination.x += movement.x;
        destination.y += movement.y;
        destination.z += movement.z;

        //if a change in position is detected perform the necessary update
        if (destination != origin)
        {
            transform.parent.transform.position =
                Vector3.MoveTowards(origin, destination, Time.deltaTime * scrollSpeed);
        }
    }
}