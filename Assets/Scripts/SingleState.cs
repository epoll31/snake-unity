using System;
using UnityEngine;
using UnityEngine.SceneManagement;

[Serializable]
public enum States
{
  Home = 0,
  Playing = 1,
  Paused = 2,
  GameOver = 3,
  Stats = 4,
  Settings = 5
}

public class StateChangedEventArgs : EventArgs
{
  public States State { get; set; }
  public States PreviousState { get; set; }
}

[Serializable]
public class GameData
{
  public int Score;
  public float Time;
  public int Length;

  public GameData()
  {
    Score = 0;
    Time = 0;
    Length = 0;
  }

  public GameData(GameData from)
  {
    Score = from.Score;
    Time = from.Time;
    Length = from.Length;
  }
}

[Serializable]
public enum Sounds
{
  ButtonPress = 0,
  GameOver = 1,
  EatMouse = 2,
  Explosion = 3,
}

[Serializable]
public class Sound
{
  public Sounds sound;
  public AudioClip clip;
}

public class SingleState : MonoBehaviour
{
  private static SingleState _instance;
  public static SingleState Instance { get; private set; }

  public GameData gameData = null;
  public GameData previousGameData = null;
  public Stats stats;

  public Sound[] sounds;

  private void Awake()
  {
    if (Instance != null && Instance != this)
    {
      Destroy(this);
    }
    else
    {
      Instance = this;
      DontDestroyOnLoad(gameObject);
      Initialize();
    }
  }

  private void Initialize()
  {
    StateChanged += OnStateChanged;
    gameData = new GameData();
    previousGameData = null;
    stats = new Stats();
    stats.PullStats();
  }

  public event EventHandler<StateChangedEventArgs> StateChanged;

  private States _state = States.Home;
  public States State
  {
    get { return _state; }
    set
    {
      if (_state == value)
      {
        return;
      }

      States previousState = _state;
      _state = value;
      StateChanged?.Invoke(this, new StateChangedEventArgs { State = _state, PreviousState = previousState });
    }
  }

  private void OnStateChanged(object sender, StateChangedEventArgs e)
  {
    print("State changed : " + e.PreviousState + " -> " + e.State);

    ChangeScene(e.PreviousState, e.State);
  }

  private void ChangeScene(States from, States to)
  {
    switch (to)
    {
      case States.Playing:
        if (from == States.Paused)
        {
          SceneManager.SetActiveScene(SceneManager.GetSceneByName("PlayScene"));
          SceneManager.UnloadSceneAsync("PauseScene");
        }
        else if (from == States.Home)
        {
          SceneManager.LoadScene("PlayScene");
        }

        break;
      case States.Paused:
        if (from == States.Playing)
        {
          SceneManager.LoadScene("PauseScene", LoadSceneMode.Additive);
        }
        break;
      case States.Home:
        SceneManager.LoadScene("HomeScene");
        if (from != States.GameOver)
        {
          previousGameData = new GameData();
        }

        break;
      case States.GameOver:
        if (from == States.Paused)
        {
          ExplosionHandler[] explosions = FindObjectsOfType<ExplosionHandler>();
          foreach (ExplosionHandler explosion in explosions)
          {
            explosion.Explode();
          }

          SceneManager.UnloadSceneAsync("PauseScene");
        }
        previousGameData = new GameData(gameData);
        stats.AddGameData(gameData);
        stats.PushStats();
        gameData = new GameData();

        SingleState.Instance.State = States.Home;
        break;
      case States.Stats:
        SceneManager.LoadScene("StatsScene");
        break;
      case States.Settings:
        SceneManager.LoadScene("SettingsScene");
        break;
    }
  }

  private void Update()
  {
    if (SingleState.Instance.State == States.Playing)
    {
      gameData.Time += Time.deltaTime;
    }
  }

  public void TriggerSound(Sounds sound)
  {
    AudioClip clip = Array.Find(sounds, s => s.sound == sound).clip;
    AudioSource.PlayClipAtPoint(clip, transform.position);
  }

  public void PlayClip(Sounds sound)
  {
    AudioClip clip = Array.Find(sounds, s => s.sound == sound).clip;
    GameObject go = new GameObject("SoundClip");
    go.transform.parent = transform;
    AudioSource source = go.AddComponent<AudioSource>();
    source.clip = clip;
    source.Play();
    Destroy(go, clip.length);
  }
}
