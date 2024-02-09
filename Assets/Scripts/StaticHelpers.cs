

using UnityEngine;

public static class StaticHelpers 
{
  public static Vector2 RandomScreenPosition() {
    return Camera.main.ViewportToWorldPoint(new Vector2(Random.value, Random.value));
  }

  public static Quaternion RandomRotation() {
    float angle = Random.Range(minInclusive: -180f, 180f);
    return Quaternion.Euler(0, 0, angle);
  }
}