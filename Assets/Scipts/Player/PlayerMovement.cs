using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody2D myBody;
    [SerializeField] Animator ant;
    [SerializeField] private float speed = 5f, xDir, jumpForce = 10f, wallTimeCooldown = 0f;
    [SerializeField] private LayerMask groundLayer, wallGround;
    [SerializeField] private BoxCollider2D boxcol;
    [SerializeField] private AudioClip jumpSound;
    private void Awake()
    {
        myBody = GetComponent<Rigidbody2D>();
        ant = GetComponent<Animator>();
        boxcol = GetComponent<BoxCollider2D>();
    }
    // Start is called before the first frame update
    void Start()
    {

    }
    public void Flip()
    {
        float xScale = transform.localScale.x;
        if (xScale * xDir >= 0) return;
        transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
    }
    public void Move()
    {
        xDir = Input.GetAxisRaw("Horizontal");
        if (ant) ant.SetBool("run", xDir != 0);
        myBody.velocity = new Vector2(xDir * speed, myBody.velocity.y);
        bool isJump = Input.GetKeyDown(KeyCode.Space);
        if (!isJump) return;
        Jump();
    }
    public void handleOnWall()
    {
        if (isOnWall() && !isOnGround())
        {
            myBody.velocity = Vector2.zero;
            myBody.gravityScale = 0;
        }
        else myBody.gravityScale = 2.5f;
    }
    // Update is called once per frame
    void Update()
    {
        handleOnWall();
        Move();
        Flip();
        ant.SetBool("grounded", isOnGround());
        wallTimeCooldown += Time.deltaTime;
    }
    public void Jump()
    {
        bool onGround = isOnGround();
        if (onGround)
        {
            if (Input.GetKeyDown(KeyCode.Space)) SoundManager.instance.PlaySound(jumpSound);
            myBody.velocity = new Vector2(myBody.velocity.x, jumpForce);
            ant.SetTrigger("jump");
        }
        else if (isOnWall() && !onGround && wallTimeCooldown > 0.2f)
        {
            wallTimeCooldown = 0f;
            if (xDir == 0)
            {
                myBody.velocity = new Vector2(-Mathf.Sign(transform.localScale.x) * 10, 0);
                transform.localScale = new Vector3(-Mathf.Sign(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }
            else myBody.velocity = new Vector2(-Mathf.Sign(transform.localScale.x) * 3, 6);
        }
    }
    public bool isOnGround()
    {
        RaycastHit2D raycat = Physics2D.BoxCast(boxcol.bounds.center, boxcol.bounds.size, 0, Vector2.down, 0.1f, groundLayer);
        return raycat.collider != null;
    }
    public bool isOnWall()
    {
        RaycastHit2D raycat = Physics2D.BoxCast(boxcol.bounds.center, boxcol.bounds.size, 0, new Vector2(transform.localScale.x >= 1 ? 1 : -1, 0), 0.1f, wallGround);
        return raycat.collider != null;
    }
    public bool canAttack()
    {
        return xDir == 0 && isOnGround();
    }
}
