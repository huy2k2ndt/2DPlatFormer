using UnityEngine;

public class SpikeHead : Enemy_Damage
{
    [SerializeField] private float timer = 0f, checkTimerAttack = 2f, speed = 5f, range = 10f;
    [SerializeField] private Vector3 destination;
    [SerializeField] private Vector3[] directions;
    [SerializeField] private bool attacking;
    [SerializeField] private LayerMask layerPlayer;
    [SerializeField] private AudioClip impactSound;
    // Start is called before the first frame update
    void Start()
    {
        attacking = false;
        directions = new Vector3[4];
    }
    private void OnEnable()
    {
        Stop();
    }

    // Update is called once per frame
    void Update()
    {
        if (attacking)
        {
            transform.Translate(destination * Time.deltaTime * speed);
        }

        timer += Time.deltaTime;
        if (timer >= checkTimerAttack) CheckAttackPlayer();
    }
    public void CheckAttackPlayer()
    {
        CalPostionDetect();
        foreach (Vector3 dir in directions)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, dir, range, layerPlayer);
            Debug.DrawRay(transform.position, dir, Color.red);
            if (hit.collider && !attacking)
            {
                attacking = true;
                timer = 0f;
                destination = new Vector3(dir.x, dir.y, dir.z);
                SoundManager.instance.PlaySound(impactSound);
                return;
            }
        }
    }
    public void CalPostionDetect()
    {
        directions[0] = transform.right * range;
        directions[1] = -transform.right * range;
        directions[2] = transform.up * range;
        directions[3] = -transform.up * range;
    }
    public void Stop()
    {
        destination = transform.position;
        attacking = false;
    }
    private new void OnTriggerEnter2D(Collider2D other)
    {
        base.OnTriggerEnter2D(other);
        Stop();
    }
}
