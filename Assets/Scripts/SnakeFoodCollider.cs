using UnityEngine;

public class SnakeFoodCollider : MonoBehaviour
{
    public SnakeHandler snakeHandler;
    public FoodHandler foodHandler;

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Mouse"))
        {
            SingleState.Instance.gameData.Score += 1;
            if (SingleState.Instance.gameData.Score % 25 == 0) {
              foodHandler.AddFood();
            }
            if (SingleState.Instance.gameData.Score % 10 == 0) {
              FindObjectOfType<BombHandler>()?.AddBomb();
            }
            if (SingleState.Instance.gameData.Score % 15 == 0) {
              snakeHandler.transform.localScale *= 0.95f;
              snakeHandler.lengthPerPart *= 0.95f; 
            }

            snakeHandler.AddParts(1);
            snakeHandler.lengthPerPart *= 0.99f;
            foodHandler.UpdateFoodPosition(collision.collider.gameObject);
            SingleState.Instance.PlayClip(Sounds.EatMouse);
        }
    }
}
