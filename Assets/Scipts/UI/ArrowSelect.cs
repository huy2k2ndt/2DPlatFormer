using UnityEngine;
using UnityEngine.UI;

public class ArrowSelect : MonoBehaviour
{
    [SerializeField] private RectTransform rect;
    [SerializeField] private RectTransform[] options;
    [SerializeField] private int curIdx = 0;
    [SerializeField] private AudioClip switchSound, selectSound;
    // Start is called before the first frame update
    void Start()
    {
        rect = GetComponent<RectTransform>();
    }

    public void CheckKey()
    {
        bool isUp = Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow);
        if (isUp) ChangeOption(-1);
        bool isDown = Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow);
        if (isDown) ChangeOption(1);
    }
    public void CheckSelect()
    {
        if (!Input.GetKeyDown(KeyCode.Return)) return;
        SoundManager.instance.PlaySound(selectSound);
        options[curIdx].GetComponent<Button>().onClick.Invoke();
    }
    // Update is called once per frame
    void Update()
    {
        CheckKey();
        CheckSelect();
    }
    public void ChangeOption(int change)
    {
        curIdx += change;
        if (curIdx < 0) curIdx = options.Length - 1;
        else if (curIdx >= options.Length) curIdx = 0;
        SoundManager.instance.PlaySound(switchSound);
        rect.position = new Vector3(rect.position.x, options[curIdx].position.y, 0);
    }
}
