
using UnityEngine;

public class FoodHandler : MonoBehaviour
{
  public GameObject MousePrefab;

  public void Start()
  {
    AddFood();
  }

  public void AddFood() {
    GameObject newFood = Instantiate(MousePrefab, transform);
    UpdateFoodPosition(newFood);
  }

  public void UpdateFoodPosition(GameObject food)
  {
    GameObject snakeHead = GameObject.FindGameObjectWithTag("Snake");
    GameObject[] snakeParts = GameObject.FindGameObjectsWithTag("SnakePart");
    Collider2D[] colliders = new Collider2D[snakeParts.Length + 1];
    colliders[0] = snakeHead?.GetComponent<Collider2D>();
    for (int i = 0; i < snakeParts.Length; i++)
    {
      colliders[i + 1] = snakeParts[i].GetComponent<Collider2D>();
    }

    Vector2 newPosition = StaticHelpers.RandomPositionAvoidingColliders(new Vector2(0.1f, 0.1f), food.GetComponent<Collider2D>(), colliders);

    food.transform.localScale = StaticHelpers.GetRandomScale(0.075f, 0.125f);
    food.transform.SetPositionAndRotation(newPosition, StaticHelpers.RandomRotation());
  }
} 
