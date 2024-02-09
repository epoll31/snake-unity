using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SnakeFoodCollider : MonoBehaviour
{
    public SnakeHandler snakeHandler;
    public FoodHandler foodHandler;

    void OnCollisionEnter2D(Collision2D collision) {
        print("hit: " + collision.otherCollider.tag);
        if (collision.collider.CompareTag("Mouse")) {
            snakeHandler.AddParts(1);
            foodHandler.UpdateFood();
        }
    }
}
