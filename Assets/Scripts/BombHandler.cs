using UnityEngine;

public class BombHandler : MonoBehaviour
{
  public GameObject BombPrefab;

  void Start()
  {
    AddBomb();
    AddBomb();
  }

  public void AddBomb()
  {
    Collider2D[] colliders = new Collider2D[1];
    colliders[0] = GameObject.FindGameObjectWithTag("SnakeHead")?.GetComponent<Collider2D>();
    GameObject bomb = Instantiate(BombPrefab, transform);

    Vector2 position = StaticHelpers.RandomPositionAvoidingColliders(new Vector2(0.1f, 0.1f), bomb.GetComponent<Collider2D>(), colliders);
   
    bomb.transform.localScale = StaticHelpers.GetRandomScale(0.1f, 0.15f); 
    bomb.transform.SetPositionAndRotation(position, StaticHelpers.RandomRotation());
  }

}
