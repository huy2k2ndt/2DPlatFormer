using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Sideways : MonoBehaviour
{
    [SerializeField] private float distance = 3f, leftEgde, rightEgde, dame = 1f, speed = 5f;
    [SerializeField] private bool moveLeft;
    private void Awake()
    {
        moveLeft = false;
        leftEgde = transform.position.x - distance;
        rightEgde = transform.position.x + distance;
    }
    private void Update()
    {
        float xPos = transform.position.x;
        float newXPos = xPos;
        if (moveLeft)
        {
            if (xPos > leftEgde) newXPos = xPos - speed * Time.deltaTime;
            else moveLeft = false;
        }
        else
        {
            if (xPos < rightEgde) newXPos = xPos + speed * Time.deltaTime;
            else moveLeft = true;
        }
        transform.position = new Vector3(newXPos, transform.position.y, transform.position.z);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.gameObject.CompareTag("Player")) return;
        other.gameObject.GetComponent<Health>().TakeDame(dame);
    }
}
