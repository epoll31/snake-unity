using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExplosionHandler : MonoBehaviour
{
    public GameObject ExplosionPrefab;

    // void Start() {
    //     DontDestroyOnLoad(gameObject);
    // }

    void OnCollisionEnter2D(Collision2D collision) {
        print("hit bomb: " + collision.collider.tag);
        if (collision.collider.CompareTag("Snake")) {
            GameObject explosion = Instantiate(ExplosionPrefab);
            explosion.transform.SetPositionAndRotation(transform.position, transform.rotation);

            transform.SetPositionAndRotation(StaticHelpers.RandomScreenPosition(), StaticHelpers.RandomRotation());
            DontDestroyOnLoad(explosion);

            SceneManager.LoadScene("HomeScene");

            
        }
    }
}
