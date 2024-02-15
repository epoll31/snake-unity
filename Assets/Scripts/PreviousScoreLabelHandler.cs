using TMPro;
using UnityEngine;

public class PreviousScoreLabelHandler : MonoBehaviour
{
    private TextMeshProUGUI tmp;
    void Start()
    {
        tmp = GetComponent<TextMeshProUGUI>();
        if (SingleState.Instance.previousGameData != null && SingleState.Instance.previousGameData.Time != 0)
        {
          tmp.text = "You Scored: " + SingleState.Instance.previousGameData.Score.ToString();
        }
        else {
          tmp.text = "";
        }
    }
}
