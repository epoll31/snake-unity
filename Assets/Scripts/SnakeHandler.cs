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
    public FoodHandler foodHandler;
    public float lengthPerPart;
    
    GameObject head;
    GameObject lastPart;
    int count = 0;
    public float speed = 2;
    public float coastSpeed = 1;
    
    Vector2 direction;

    void Start()
    {
        head = lastPart = Instantiate(headPrefab, transform);
        SnakeFoodCollider snakeFoodCollider = head.GetComponent<SnakeFoodCollider>();
        snakeFoodCollider.snakeHandler = this;
        snakeFoodCollider.foodHandler = foodHandler;

        // head.GetComponent<SpriteRenderer>().sortingOrder = -++count;


        AddParts(4);
    }


    public void AddParts(int count) {
        for (int i = 0; i < count; i++) {
            AddPart();
        }
    }
    void AddPart() {

        GameObject go =Instantiate(bodyPrefab, transform);
        go.transform.position = lastPart.transform.position;

        go.GetComponent<SpriteRenderer>().sortingOrder = -++count;

        SnakePartHandler newPart = go.GetComponent<SnakePartHandler>();

        newPart.Snake = this;
        newPart.PreviousPart = lastPart;

        lastPart = newPart.gameObject;
    }

    void Update()
    {
        if (Input.touchCount > 0) {
            Touch touch = Input.GetTouch(0);
            // var view = Camera.main.ScreenToViewportPoint();
            // if (view.x > 0 && view.x < 1 && view.y > 0 && view.y < 1) {
                Vector3 mousePos = touch.position;
                mousePos.z = Camera.main.nearClipPlane;
                Vector2 target = Camera.main.ScreenToWorldPoint(mousePos);
                // target.Set(target.x, target.y, 0);


                direction = target - (Vector2)head.transform.position;
                float angle = MathF.Atan2(direction.y, direction.x) / MathF.PI * 180 + 90;

                head.transform.rotation = Quaternion.Euler(0, 0, angle);
                
                if (direction.magnitude > lengthPerPart) {
                    head.transform.position = head.transform.position + speed * Time.deltaTime * (Vector3)direction.normalized;
                }
            // }
        }
        else {
            head.transform.position = head.transform.position + coastSpeed * Time.deltaTime * (Vector3)direction.normalized;
        }
    }
}
