using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Damage : MonoBehaviour
{
    [SerializeField] protected float dame = 1f;
    protected void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.gameObject.CompareTag("Player")) return;
        other.gameObject.GetComponent<Health>().TakeDame(dame);
    }
}
