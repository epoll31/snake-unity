using UnityEngine;


public class AudioHandler : MonoBehaviour
{
  public void TriggerSound(int sound)
  {
    SingleState.Instance.PlayClip((Sounds)sound);
  }
}


