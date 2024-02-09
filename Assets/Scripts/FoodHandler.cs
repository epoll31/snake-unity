
using UnityEngine;

public class FoodHandler : MonoBehaviour
{
    public GameObject MousePrefab;

    GameObject active;

    public void Start() {
        active = Instantiate(MousePrefab, transform);
        UpdateFoodPosition();
    }

    public void UpdateFood() {
        UpdateFoodPosition();
    }

    private void UpdateFoodPosition() {
        active.transform.SetPositionAndRotation(StaticHelpers.RandomScreenPosition(0.1f, 0.1f), StaticHelpers.RandomRotation());
    }
}
