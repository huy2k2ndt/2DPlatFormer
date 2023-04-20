using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : Enemy_Damage
{
    [SerializeField] private float speed = 10f, dir, maxLifeTime = 3f, lifeTime = 0f;
    private void Awake()
    {
        gameObject.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        lifeTime += Time.deltaTime;
        if (lifeTime >= maxLifeTime) gameObject.SetActive(false);
        float moveSpeed = speed * Time.deltaTime * dir;
        transform.Translate(moveSpeed, 0, 0);
    }
    public void Active(float direction)
    {
        lifeTime = 0f;
        dir = direction;
        float xScale = transform.localScale.x;
        if (Mathf.Sign(xScale) != Mathf.Sign(dir)) xScale *= -1;
        transform.localScale = new Vector3(xScale, transform.localScale.y, transform.localScale.z);
        gameObject.SetActive(true);
    }
    private new void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag != "Player") return;
        base.OnTriggerEnter2D(other);
        gameObject.SetActive(false);
    }
}
