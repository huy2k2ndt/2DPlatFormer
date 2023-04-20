using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    [SerializeField] private Transform leftPoint, rightPoint, enemy;
    [SerializeField] private float speed = 2f, idleDuration = 2f, idleTimer = 0f;
    [SerializeField] private bool movingleft = false;
    [SerializeField] private Vector3 initScale;
    [SerializeField] private Animator ant;
    // Start is called before the first frame update
    void Start()
    {
        initScale = enemy.localScale;
    }
    private void OnDisable()
    {
        ant.SetBool("moving", false);
    }

    // Update is called once per frame
    void Update()
    {
        if (movingleft)
        {
            if (enemy.position.x > leftPoint.position.x) MoveToPoint(-1);
            else ChangeDirection();
        }
        else
        {
            if (enemy.position.x < rightPoint.position.x) MoveToPoint(1);
            else ChangeDirection();
        }
    }
    public void ChangeDirection()
    {
        ant.SetBool("moving", false);
        idleTimer += Time.deltaTime;
        if (idleTimer < idleDuration) return;
        idleTimer = 0f;
        movingleft = !movingleft;
    }
    public void MoveToPoint(float dir)
    {
        ant.SetBool("moving", true);
        enemy.localScale = new Vector3(Mathf.Abs(initScale.x) * dir, initScale.y, initScale.z);
        float newXPos = enemy.position.x + Time.deltaTime * speed * dir;
        enemy.position = new Vector3(newXPos, enemy.position.y, enemy.position.z);
    }
}
