using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private Animator ant;
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private Transform shotPoint;
    [SerializeField] private AudioClip fireBallSound;
    [SerializeField] private GameObject[] fireBalls;
    [SerializeField] private float attackCoolDown = 2f, timer = Mathf.Infinity;
    private void Awake()
    {
        ant = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        bool isAttack = Input.GetMouseButton(0);
        if (isAttack && playerMovement.canAttack() && timer > attackCoolDown) Attack();
        timer += Time.deltaTime;
    }
    public void Attack()
    {
        timer = 0;
        SoundManager.instance.PlaySound(fireBallSound);
        ant.SetTrigger("attack");
        int idx = findFireBall();
        fireBalls[idx].transform.position = shotPoint.position;
        fireBalls[idx].GetComponent<Projectile>().SetDir(Mathf.Sign(transform.localScale.x));
    }
    public int findFireBall()
    {
        for (int i = 0; i < fireBalls.Length; ++i)
        {
            if (fireBalls[i].activeInHierarchy) continue;
            return i;
        }
        return 0;
    }
}
