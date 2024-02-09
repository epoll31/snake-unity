

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
}