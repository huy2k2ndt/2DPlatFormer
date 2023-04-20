using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowTrap : MonoBehaviour
{
    [SerializeField] private float coolDownShot = 2f, timer = 0f;
    [SerializeField] GameObject[] arrows;
    [SerializeField] Transform shotPoint;
    [SerializeField] private AudioClip arrowSound;
    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= coolDownShot) Attack();
    }
    public void Attack()
    {
        SoundManager.instance.PlaySound(arrowSound);
        timer = 0;
        int idx = FindArrowShot();
        arrows[idx].transform.position = shotPoint.position;
        arrows[idx].GetComponent<EnemyBullet>().Active(Mathf.Sign(transform.localScale.x));
    }
    public int FindArrowShot()
    {
        for (int i = 0; i < arrows.Length; ++i)
        {
            if (arrows[i].activeInHierarchy) continue;
            return i;
        }
        return 0;
    }
}
