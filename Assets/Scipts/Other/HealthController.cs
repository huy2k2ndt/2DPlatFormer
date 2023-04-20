using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthController : MonoBehaviour
{
    [SerializeField] private Health playerHealth;
    [SerializeField] private Image healthBarTotal, healthBarCur;
    private void Awake()
    {
        healthBarTotal.fillAmount = healthBarCur.fillAmount = playerHealth.totalHealth / 10;
    }


    // Update is called once per frame
    void Update()
    {
        healthBarCur.fillAmount = playerHealth.curHealth / 10;
    }
}
