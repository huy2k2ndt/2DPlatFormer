using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] public float curHealth, totalHealth = 3, iframesDuration = 2f;
    [SerializeField] private Animator ant;
    [SerializeField] private bool dead;
    [SerializeField] private int numOfFlicker = 3;
    [SerializeField] private SpriteRenderer sp;
    [SerializeField] private bool isInviolable;
    [SerializeField] private AudioClip hurtSound, dieSound;
    // Start is called before the first frame update
    private void Awake()
    {
        sp = GetComponent<SpriteRenderer>();
        ant = GetComponent<Animator>();
        curHealth = totalHealth;
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)) TakeDame(1);
    }
    public void TakeDame(float dame)
    {
        if (isInviolable) return;
        curHealth = Mathf.Max(curHealth - dame, 0);
        if (curHealth > 0)
        {
            SoundManager.instance.PlaySound(hurtSound);
            ant.SetTrigger("hurt");
            StartCoroutine(Inviolable());
        }
        else
        {
            if (dead) return;
            SoundManager.instance.PlaySound(dieSound);
            ant.SetTrigger("die");
            ChangeStatusScript(false);
            dead = true;
        }

    }
    public void ChangeStatusScript(bool status)
    {
        if (GetComponent<PlayerMovement>())
            GetComponent<PlayerMovement>().enabled = status;
        if (GetComponentInParent<EnemyPatrol>())
        {
            GetComponentInParent<EnemyPatrol>().enabled = status;
            if (GetComponent<MeleeAttack>())
                GetComponent<MeleeAttack>().enabled = status;
            if (GetComponent<RangeAttack>())
                GetComponent<RangeAttack>().enabled = status;
        }
    }
    public IEnumerator Inviolable()
    {
        isInviolable = true;
        Physics2D.IgnoreLayerCollision(8, 9, true);
        for (int i = 0; i < numOfFlicker; ++i)
        {
            sp.color = new Color(1, 0, 0, 0.5f);
            yield return new WaitForSeconds(iframesDuration / (numOfFlicker * 2));
            sp.color = Color.white;
            yield return new WaitForSeconds(iframesDuration / (numOfFlicker * 2));
        }
        Physics2D.IgnoreLayerCollision(8, 9, false);
        isInviolable = false;
    }
    public void AddHealth(float val)
    {
        curHealth += val;
        curHealth = Mathf.Min(curHealth, totalHealth);
    }
    public void DeathActive()
    {
        gameObject.SetActive(false);
    }
    public void Respwan()
    {
        ant.ResetTrigger("die");
        AddHealth(totalHealth);
        ant.Play("Player_Idle");
        ChangeStatusScript(true);
        dead = false;
    }
}
