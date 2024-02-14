using UnityEngine;

public class SnakeFoodCollider : MonoBehaviour
{
    public SnakeHandler snakeHandler;
    public FoodHandler foodHandler;

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Mouse"))
        {
            SingleState.Instance.Score += 1;
            snakeHandler.AddParts(1);
            foodHandler.UpdateFood();
        }
    }
}
