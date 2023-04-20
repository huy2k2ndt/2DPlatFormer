using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttack : MonoBehaviour
{
    [SerializeField] private float range = 4f, attackCooldown = 2f, timer = Mathf.Infinity, offsetDis = 0f, dame = 1f;
    [SerializeField] private LayerMask layerPlayer;
    [SerializeField] private BoxCollider2D boxcol;
    [SerializeField] private Animator ant;
    [SerializeField] private Health playerHealth;
    [SerializeField] private EnemyPatrol enemyPatrol;
    [SerializeField] private AudioClip attackSound;
    // Start is called before the first frame update
    void Start()
    {
        // boxcol = GetComponent<BoxCollider2D>();
        ant = GetComponent<Animator>();
        enemyPatrol = GetComponentInParent<EnemyPatrol>();

    }
    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        bool canSightPlayer = PlayerInSight();
        if (canSightPlayer && timer >= attackCooldown)
        {

            timer = 0f;
            ant.SetTrigger("attack");
            SoundManager.instance.PlaySound(attackSound);
        }
        if (enemyPatrol) enemyPatrol.enabled = !canSightPlayer;
    }
    public bool PlayerInSight()
    {
        float sign = Mathf.Sign(transform.localScale.x);
        RaycastHit2D hit = Physics2D.BoxCast(boxcol.bounds.center + transform.right * range * offsetDis * sign, new Vector2(boxcol.bounds.size.x * range, boxcol.bounds.size.y), 0f, Vector2.left, 0f, layerPlayer);
        if (hit.collider != null) playerHealth = hit.transform.GetComponent<Health>();
        return hit.collider != null;
    }
    private void OnDrawGizmos()
    {
        float sign = Mathf.Sign(transform.localScale.x);
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(boxcol.bounds.center + transform.right * range * offsetDis * sign, new Vector2(boxcol.bounds.size.x * range, boxcol.bounds.size.y));
    }
    public void DamePlayer()
    {
        if (!PlayerInSight()) return;
        playerHealth.TakeDame(dame);
    }
}
