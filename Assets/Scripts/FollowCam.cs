using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCam : MonoBehaviour
{
    public Transform target;
    public float yBias = 3.5f;
    public float updateSpeed = 3;
    public float xBorder;
    public float yBorder;
    private Vector3 offset;

    private void Start()
    {
        offset = new Vector3(0, yBias, -10);
        Vector3 startPosition= new Vector3(Mathf.Clamp(target.position.x, -xBorder, xBorder), Mathf.Clamp(target.position.y, -yBorder, yBorder), 0) + offset;
        transform.position = startPosition;
    }

    void LateUpdate()
    {
        //transform.position = new Vector3(target.position.x, target.position.y + yBias, -10);
        Vector3 positionToMove = new Vector3(Mathf.Clamp(target.position.x, -xBorder, xBorder), Mathf.Clamp(target.position.y, -yBorder, yBorder), 0) + offset;
        var newPos = Vector3.MoveTowards(transform.position, positionToMove, updateSpeed * Time.deltaTime);
        transform.position = newPos;
    }
}
