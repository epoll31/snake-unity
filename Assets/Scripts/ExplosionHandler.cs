using UnityEngine;

public class ExplosionHandler : MonoBehaviour
{
    public GameObject ExplosionPrefab;

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Snake"))
        {
            ExplodeAllBombs();
        }
    }

    public void ExplodeAllBombs() {
      ExplosionHandler[] explosions = FindObjectsOfType<ExplosionHandler>();
      foreach (ExplosionHandler explosion in explosions)
      {
          explosion.Explode();
      }
    }
    public void Explode()
    {
        GameObject explosion = Instantiate(ExplosionPrefab);
        explosion.transform.SetPositionAndRotation(transform.position, transform.rotation);

        transform.SetPositionAndRotation(StaticHelpers.RandomScreenPosition(), StaticHelpers.RandomRotation());
        DontDestroyOnLoad(explosion);

        SingleState.Instance.State = States.Home;
    }

    void OnDestroy()
    {
      if (SingleState.Instance.State == States.Home)
      {
        return;
      }
      FindObjectOfType<BombHandler>()?.AddBomb();
    } 
}
