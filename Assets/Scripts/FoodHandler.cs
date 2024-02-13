
using UnityEngine;

public class FoodHandler : MonoBehaviour
{
  public GameObject MousePrefab;

  GameObject active;

  public void Start()
  {
    active = Instantiate(MousePrefab, transform);
    UpdateFoodPosition();
  }

  public void UpdateFood()
  {
    UpdateFoodPosition();
  }

  private Vector2 GetNewFoodPosition()
  {
    Vector2 position = StaticHelpers.RandomScreenPosition(0.1f, 0.1f);
    Bounds bounds = active.GetComponent<Collider2D>().bounds;
    bounds.center = position;
    bounds.Expand(1f);


    GameObject snakeHead = GameObject.FindGameObjectWithTag("Snake");
    GameObject[] snakeParts = GameObject.FindGameObjectsWithTag("SnakePart");
    GameObject[] bombs = GameObject.FindGameObjectsWithTag("Bomb");


    GameObject[] all = new GameObject[snakeParts.Length + bombs.Length + 1];
    all[0] = snakeHead;
    snakeParts.CopyTo(all, 1);
    bombs.CopyTo(all, snakeParts.Length + 1);

    for (int i = 0; i < all.Length; i++)
    {
      if (all[i] == null)
      {
        print(i + " is null");
        continue;
      }
      print(i + ": " + all[i].tag + " " + all[i]);
      Collider2D collider = all[i].GetComponent<Collider2D>();

      if (collider == null || bounds.Intersects(collider.bounds))
      {
        return GetNewFoodPosition();
      }
    }

    return position;
  }

  private void UpdateFoodPosition()
  { 
    GameObject snakeHead = GameObject.FindGameObjectWithTag("Snake");
    GameObject[] snakeParts = GameObject.FindGameObjectsWithTag("SnakePart");
    Collider2D[] colliders = new Collider2D[snakeParts.Length + 1];
    colliders[0] = snakeHead?.GetComponent<Collider2D>();
    for (int i = 0; i < snakeParts.Length; i++)
    {
      colliders[i + 1] = snakeParts[i].GetComponent<Collider2D>();
    }

    Vector2 newPosition = StaticHelpers.RandomPositionAvoidingColliders(new Vector2(0.1f, 0.1f), active.GetComponent<Collider2D>(), colliders);

    active.transform.localScale = StaticHelpers.GetRandomScale(0.075f, 0.125f);
    active.transform.SetPositionAndRotation(newPosition, StaticHelpers.RandomRotation());
  }
}
