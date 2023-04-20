using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] GameObject prevRoom, nextRoom;
    [SerializeField] CameraFollow cam;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.transform.position.x < transform.position.x) cam.MoveToRoom(nextRoom.transform);
        else cam.MoveToRoom(prevRoom.transform);
    }
}
