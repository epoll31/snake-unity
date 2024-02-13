using UnityEngine;

public class PauseButtonHandler : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        SingleState.Instance.StateChanged += OnStateChanged;
    }

    private void OnStateChanged(object sender, StateChangedEventArgs e)
    {
      gameObject.SetActive(e.State != States.Paused);
    }

    void OnDestroy() {
      SingleState.Instance.StateChanged -= OnStateChanged;
    }

}
