using UnityEngine;

public class BombHandler : MonoBehaviour
{
    public GameObject BombPrefab;

    void Start() {

        AddBomb();
        AddBomb();
    }

    public void AddBomb() {
        GameObject bomb = Instantiate(BombPrefab, transform);
        bomb.transform.SetPositionAndRotation(StaticHelpers.RandomScreenPosition(0.1f, 0.1f), StaticHelpers.RandomRotation());
    }
}
