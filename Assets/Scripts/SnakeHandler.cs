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
            
            Vector3 mousePos = touch.position;
            mousePos.z = Camera.main.nearClipPlane;
            Vector2 target = Camera.main.ScreenToWorldPoint(mousePos);


            direction = target - (Vector2)head.transform.position;
            float angle = MathF.Atan2(direction.y, direction.x) / MathF.PI * 180 + 90;

            head.transform.rotation = Quaternion.Euler(0, 0, angle);
            
            if (direction.magnitude > lengthPerPart) {
                head.transform.position = head.transform.position + speed * Time.deltaTime * (Vector3)direction.normalized;
            }
        }
        else {
            float halfHeight = Camera.main.orthographicSize;
            float halfWidth = halfHeight * Screen.width / Screen.height;

            if (head.transform.position.y < Camera.main.transform.position.y - halfHeight || head.transform.position.y > Camera.main.transform.position.y + halfHeight) {
                direction.y *= -1;
            }
            if (head.transform.position.x < Camera.main.transform.position.x - halfWidth || head.transform.position.x > Camera.main.transform.position.x + halfWidth) {
                direction.x *= -1;
            }

            float angle = MathF.Atan2(direction.y, direction.x) / MathF.PI * 180 + 90;

            
            head.transform.SetPositionAndRotation(head.transform.position + coastSpeed * Time.deltaTime * (Vector3)direction.normalized, Quaternion.Euler(0, 0, angle));
        }
    }
}
