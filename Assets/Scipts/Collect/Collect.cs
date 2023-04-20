using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collect : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private float valHealth = 1f;
    [SerializeField] private AudioClip pickupSound;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.gameObject.CompareTag("Player")) return;
        SoundManager.instance.PlaySound(pickupSound);
        other.gameObject.GetComponent<Health>().AddHealth(valHealth);
        --valHealth;
        valHealth = Mathf.Max(0, valHealth);
        gameObject.SetActive(false);
    }
}
