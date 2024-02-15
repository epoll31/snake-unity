using UnityEngine;

public class ExplosionHandler : MonoBehaviour
{
    public GameObject ExplosionPrefab;

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Snake"))
        {
            SingleState.Instance.State = States.GameOver;
            ExplodeAllBombs();
        }
    }

    public void ExplodeAllBombs()
    {
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

        SingleState.Instance.PlayClip(Sounds.Explosion);

    }

    void OnDestroy()
    {
        if (SingleState.Instance.State != States.Playing)
        {
            return;
        }
        FindObjectOfType<BombHandler>()?.AddBomb();
    }
}
