using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireTrap : MonoBehaviour
{
    [SerializeField] private float activeTime = 2f, triggerDuration = 2f;
    [SerializeField] private Animator ant;
    [SerializeField] private SpriteRenderer sp;
    [SerializeField] private bool active = false, trigger = false;
    [SerializeField] private float dame = 1f;
    [SerializeField] private AudioClip fireTrapSound;
    [SerializeField] private GameObject player = null;
    [SerializeField] private BoxCollider2D boxcol;
    [SerializeField] private LayerMask layerPlayer;
    private void Awake()
    {
        ant = GetComponent<Animator>();
        sp = GetComponent<SpriteRenderer>();
        boxcol = GetComponent<BoxCollider2D>();
    }
    public bool CheckColWithPlayer()
    {
        RaycastHit2D hit = Physics2D.BoxCast(boxcol.bounds.center, new Vector2(boxcol.bounds.size.x, boxcol.bounds.size.y), 0f, Vector2.left, 0f, layerPlayer);
        if (hit.collider != null) player = hit.collider.gameObject;
        return hit.collider != null;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(boxcol.bounds.center, new Vector2(boxcol.bounds.size.x, boxcol.bounds.size.y));
    }
    private void Update()
    {
        if (!CheckColWithPlayer()) return;
        if (!trigger) StartCoroutine(ActiveFireTrap());
        if (active) player.GetComponent<Health>().TakeDame(dame);
    }
    public IEnumerator ActiveFireTrap()
    {
        sp.color = Color.red;
        trigger = true;
        yield return new WaitForSeconds(triggerDuration);
        SoundManager.instance.PlaySound(fireTrapSound);
        sp.color = Color.white;
        active = true;
        ant.SetBool("active", true);
        yield return new WaitForSeconds(activeTime);
        active = trigger = false;
        ant.SetBool("active", false);
    }
}
