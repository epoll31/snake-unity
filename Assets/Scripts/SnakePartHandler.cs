using UnityEngine;

public class SnakePartHandler : MonoBehaviour
{
    public SnakeHandler Snake;
    public GameObject PreviousPart;

    void Update()
    {
        Vector3 diff = PreviousPart.transform.position - transform.position;
        if (diff.magnitude > Snake.lengthPerPart) {
            transform.position = PreviousPart.transform.position - diff.normalized * Snake.lengthPerPart;
            transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg + 90);
        }
    }
}
