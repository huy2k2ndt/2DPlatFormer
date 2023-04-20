
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    // [SerializeField] private float currPosX, speed = 5f;
    // [SerializeField] private Vector3 velocity = Vector3.zero;
    // Start is called before the first frame update
    [SerializeField] Transform target;

    // Update is called once per frame
    void Update()
    {
        // transform.position = Vector3.SmoothDamp(transform.position, new Vector3(currPosX, transform.position.y, transform.position.z), ref velocity, speed * Time.deltaTime);
        transform.position = new Vector3(target.position.x, target.position.y, transform.position.z);
    }
    public void MoveToRoom(Transform room)
    {
        // currPosX = room.position.x;
    }
}
