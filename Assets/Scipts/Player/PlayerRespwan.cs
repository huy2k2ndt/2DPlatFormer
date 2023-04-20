using UnityEngine;

public class PlayerRespwan : MonoBehaviour
{
    [SerializeField] private Transform curCheckPoint = null;
    [SerializeField] private Health playerHealth;
    [SerializeField] private AudioClip checkPointSound;
    [SerializeField] private UIManage uiManage;

    private void Awake()
    {
        uiManage = FindFirstObjectByType<UIManage>();
        playerHealth = GetComponent<Health>();
    }

    public void CheckRespwan()
    {
        if (!curCheckPoint)
        {
            uiManage.GameOver();
            return;
        }
        transform.position = curCheckPoint.position;
        playerHealth.Respwan();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag != "CheckPoint") return;
        SoundManager.instance.PlaySound(checkPointSound);
        curCheckPoint = other.transform;
        other.GetComponent<Animator>().SetTrigger("appear");
        other.GetComponent<Collider2D>().enabled = false;
    }
}
