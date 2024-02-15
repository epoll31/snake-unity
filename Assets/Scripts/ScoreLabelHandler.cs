using System;
using TMPro;
using UnityEngine;

public class ScoreLabelHandler : MonoBehaviour
{
  private TextMeshProUGUI tmp;

  // Start is called before the first frame update
  void Start()
  {
    tmp = GetComponent<TextMeshProUGUI>();
  }

  // Update is called once per frame
  void Update()
  {
    tmp.text = SingleState.Instance.gameData.Score.ToString();
  }
}
