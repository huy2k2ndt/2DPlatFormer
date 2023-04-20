using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float speed = 5f, dir, lifeTime = 0f, maxLifeTime = 2f, dame = 1f;
    [SerializeField] private Animator ant;
    [SerializeField] BoxCollider2D boxcol;
    [SerializeField] private bool hit;
    private void Awake()
    {
        ant = GetComponent<Animator>();
        boxcol = GetComponent<BoxCollider2D>();
    }


    // Update is called once per frame
    void Update()
    {
        if (hit) return;
        lifeTime += Time.deltaTime;
        if (lifeTime >= maxLifeTime) DeathActive();
        float moveSpeed = speed * Time.deltaTime * dir;
        transform.Translate(moveSpeed, 0, 0);
    }
    public void SetDir(float direction)
    {
        lifeTime = 0f;
        dir = direction;
        float xScale = transform.localScale.x;
        if (Mathf.Sign(xScale) != Mathf.Sign(dir)) xScale *= -1;
        transform.localScale = new Vector3(xScale, transform.localScale.y, transform.localScale.z);
        gameObject.SetActive(true);
        boxcol.enabled = true;
        hit = false;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" || other.tag == "Enemy")
        {
            ant.SetTrigger("explose");
            hit = true;
            boxcol.enabled = false;
            other.GetComponent<Health>().TakeDame(dame);
        }
    }
    public void DeathActive()
    {
        gameObject.SetActive(false);
    }
}
