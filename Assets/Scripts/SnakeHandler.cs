using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class SnakeHandler : MonoBehaviour
{
    public GameObject headPrefab;
    public GameObject bodyPrefab;
    public float lengthPerPart;
    
    GameObject head;
    GameObject lastPart;
    int count = 0;
    public float speed = 4;

    void Start()
    {
        head = lastPart = Instantiate(headPrefab, transform);

        AddParts(10);
    }


    void AddParts(int count) {
        for (int i = 0; i < count; i++) {
            AddPart();
        }
    }
    void AddPart() {

        GameObject go =Instantiate(bodyPrefab, transform);
        go.GetComponent<SpriteRenderer>().sortingOrder = -count++;

        SnakePartHandler newPart = go.GetComponent<SnakePartHandler>();

        newPart.Snake = this;
        newPart.PreviousPart = lastPart;

        lastPart = newPart.gameObject;
    }

    void Update()
    {
        var view = Camera.main.ScreenToViewportPoint(Input.mousePosition);
        if (view.x > 0 && view.x < 1 && view.y > 0 && view.y < 1) {
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = Camera.main.nearClipPlane;
            Vector2 target = Camera.main.ScreenToWorldPoint(mousePos);
            // target.Set(target.x, target.y, 0);


            float angle = MathF.Atan2(target.y - head.transform.position.y, target.x - head.transform.position.x) / MathF.PI * 180 + 90;

            head.transform.rotation = Quaternion.Euler(0, 0, angle);
            Vector2 diff = target - (Vector2)head.transform.position;
            if (diff.magnitude > lengthPerPart) {
                head.transform.position = head.transform.position + (Vector3)diff.normalized * speed * Time.deltaTime;
            }
        }
    }
}
