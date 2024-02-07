using System.Collections;
using System.Collections.Generic;
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
        }
    }
}
