using UnityEngine;
using UnityEngine.SceneManagement;

public class ExplosionHandler : MonoBehaviour
{
    public GameObject ExplosionPrefab;

    void OnCollisionEnter2D(Collision2D collision)
    {
        print("hit bomb: " + collision.collider.tag);
        if (collision.collider.CompareTag("Snake"))
        {
            Explode();
        }
    }

    public void Explode()
    {
        GameObject explosion = Instantiate(ExplosionPrefab);
        explosion.transform.SetPositionAndRotation(transform.position, transform.rotation);

        transform.SetPositionAndRotation(StaticHelpers.RandomScreenPosition(), StaticHelpers.RandomRotation());
        DontDestroyOnLoad(explosion);

        SceneManager.LoadScene("HomeScene");
    }
}
