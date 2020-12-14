using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Keyboardmover_q5 : MonoBehaviour
{
    protected Vector3 saveStep;
    protected Vector3 currPosition;
    protected Vector3 NewPosition()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            saveStep = Vector3.left;
            currPosition = transform.position + saveStep;
            return currPosition;
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            saveStep = Vector3.right;
            currPosition = transform.position + saveStep;
            return currPosition;
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            saveStep = Vector3.down;
            currPosition = transform.position + saveStep;
            return currPosition;
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            saveStep = Vector3.up;
            currPosition = transform.position + saveStep;
            return currPosition;
        }
        else
        {
            currPosition = transform.position;
            return transform.position;
        }
    }


    void Update()
    {
        transform.position = NewPosition();
    }
}
