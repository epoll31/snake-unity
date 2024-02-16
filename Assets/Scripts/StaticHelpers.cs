

using UnityEngine;

public static class StaticHelpers 
{
  public static Vector2 RandomScreenPosition() {
    return Camera.main.ViewportToWorldPoint(new Vector2(Random.value, Random.value));
  }

  public static Vector2 RandomScreenPosition(Vector2 margin) => RandomScreenPosition(margin.x, margin.y);
  public static Vector2 RandomScreenPosition(float marginX, float marginY) {
    return Camera.main.ViewportToWorldPoint(new Vector2(marginX + Random.value * (1 - marginX * 2), marginY + Random.value * (1 - marginY * 2)));
  }

  public static Quaternion RandomRotation() {
    float angle = Random.Range(minInclusive: -180f, 180f);
    return Quaternion.Euler(0, 0, angle);
  }

  public static Vector2 RandomPositionAvoidingColliders(Vector2 margin, Collider2D collider, Collider2D[] others) {
    Vector2 position = RandomScreenPosition(margin);
    Bounds bounds = collider.bounds;
    bounds.center = position;
    bounds.Expand(1f);

    for (int i = 0; i < others.Length; i++) {
      if (others[i] == null) {
        continue;
      }

      if (bounds.Intersects(others[i].bounds)) {
        return RandomPositionAvoidingColliders(margin, collider, others);
      }
    }

    return position;
  }

  public static T[] GetComponentsFromSnakeParts<T>() {
    GameObject snakeHead = GameObject.FindGameObjectWithTag("SnakeHead");
    GameObject[] snakeParts = GameObject.FindGameObjectsWithTag("SnakePart");
    T[] components = new T[snakeParts.Length + 1];
    components[0] = snakeHead.GetComponent<T>();
    for (int i = 0; i < snakeParts.Length; i++) {
      components[i + 1] = snakeParts[i].GetComponent<T>();
    }
    return components;
  }

  public static Vector3 GetRandomScale(float min = 0.75f, float max = 2f) {
    float scale = Random.Range(min, max);
    return new Vector3(scale, scale, 1);
  }
}
