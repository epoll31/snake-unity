using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ExplosionHandler : MonoBehaviour
{
    public GameObject ExplosionPrefab;

    void OnCollisionEnter2D(Collision2D collision) {
        print("hit bomb: " + collision.collider.tag);
        if (collision.collider.CompareTag("Snake")) {
            GameObject bomb = Instantiate(ExplosionPrefab);
            bomb.transform.SetPositionAndRotation(transform.position, transform.rotation);

            transform.SetPositionAndRotation(StaticHelpers.RandomScreenPosition(), StaticHelpers.RandomRotation());

        }
    }
}
