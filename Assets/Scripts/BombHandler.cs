using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombHandler : MonoBehaviour
{
    public GameObject BombPrefab;
    public int bombCount = 1;
    private List<GameObject> bombs;

    void Start()
    {
        bombs = new List<GameObject>();
    }

    void Update()
    {
        if (bombs.Count < bombCount) {
            AddBomb();
        }
    }

    void AddBomb() {
        GameObject bomb = Instantiate(BombPrefab, transform);
        bomb.transform.SetPositionAndRotation(StaticHelpers.RandomScreenPosition(), StaticHelpers.RandomRotation());
        bombs.Add(bomb);
    }
}
