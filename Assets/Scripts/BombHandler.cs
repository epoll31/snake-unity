using UnityEngine;

public class BombHandler : MonoBehaviour
{
  public GameObject BombPrefab;

  void Start()
  {

    AddBomb();
    AddBomb();
  }


  private Vector2 GetNewBombPosition(GameObject bomb)
  {
    Vector2 position = StaticHelpers.RandomScreenPosition(0.1f, 0.1f);
    Bounds bounds = bomb.GetComponent<Collider2D>().bounds;
    bounds.center = position;
    bounds.Expand(0.5f);


    GameObject snakeHead = GameObject.FindGameObjectWithTag("Snake");
    GameObject[] snakeParts = GameObject.FindGameObjectsWithTag("SnakePart");
    GameObject[] bombs = GameObject.FindGameObjectsWithTag("Bomb");


    GameObject[] all = new GameObject[snakeParts.Length + bombs.Length + 1];
    all[0] = snakeHead;
    snakeParts.CopyTo(all, 1);
    bombs.CopyTo(all, snakeParts.Length + 1);

    for (int i = 0; i < all.Length; i++)
    {
      if (bounds.Intersects(all[i].GetComponent<Collider2D>().bounds))
      {
        return GetNewBombPosition(bomb);
      }
    }

    return position;
  }

  public void AddBomb()
  {
    Collider2D[] colliders = StaticHelpers.GetComponentsFromSnakeParts<Collider2D>();
    GameObject bomb = Instantiate(BombPrefab, transform);

    Vector2 position = StaticHelpers.RandomPositionAvoidingColliders(new Vector2(0.1f, 0.1f), bomb.GetComponent<Collider2D>(), colliders);
   
    bomb.transform.localScale = StaticHelpers.GetRandomScale(0.1f, 0.15f); 
    bomb.transform.SetPositionAndRotation(position, StaticHelpers.RandomRotation());
  }

}
