using System;
using System.Collections.Generic;
using UnityEditor;
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
    Settings = 5,
    Info = 6,
    Back = 7,
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
public enum ControlType
{
    Joystick = 0,
    Drag = 1
}

[Serializable]
public class Settings
{
    [Range(0, 1)]
    public float MusicVolume = 0.5f;
    [Range(0, 1)]
    public float SoundVolume = 0.5f;
    public ControlType ControlType;

    public void LoadSettings()
    {
        MusicVolume = PlayerPrefs.GetFloat("MusicVolume", 0.5f);
        SoundVolume = PlayerPrefs.GetFloat("SoundVolume", 0.5f);
        ControlType = (ControlType)PlayerPrefs.GetInt("ControlType", 0);
    }

    public void SaveSettings()
    {
        PlayerPrefs.SetFloat("MusicVolume", MusicVolume);
        PlayerPrefs.SetFloat("SoundVolume", SoundVolume);
        PlayerPrefs.SetInt("ControlType", (int)ControlType);
    }
}

public class SoundEffectAttribute : Attribute { }

[Serializable]
public enum Sounds
{
    ButtonPress = 0,
    EatMouse = 1,
    Explosion = 2,
    Thud = 3,
    Music = 4,
}

[Serializable]
public enum SoundType
{
    Music = 0,
    SoundEffect = 1
}

[Serializable]
public class Sound
{
    public Sounds sound;
    public AudioClip clip;
    public SoundType type;
}

public class SingleState : MonoBehaviour
{
    private static SingleState _instance;
    public static SingleState Instance { get; private set; }

    public GameData gameData = null;
    public GameData previousGameData = null;
    public Stats stats;
    public Settings settings;

    public Sound[] sounds;
    public AudioSource MusicSource;

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
        settings = new Settings();
        settings.LoadSettings();

        PlayClip(Sounds.Music);
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

            if (value == States.Back)
            {
                value = previousStates.Pop();
            }
            else
            {
                previousStates.Push(_state);
            }

            States previousState = _state;
            _state = value;
            StateChanged?.Invoke(this, new StateChangedEventArgs { State = _state, PreviousState = previousState });
        }
    }
    private Stack<States> previousStates = new Stack<States>();

    [CustomEditor(typeof(SingleState))]
    public class StackPreview : Editor
    {
        public override void OnInspectorGUI()
        {
            GUILayout.Label("Current State: " + SingleState.Instance.State);
            // get the target script as TestScript and get the stack from it
            var ts = (SingleState)target;
            var stack = ts.previousStates;

            // some styling for the header, this is optional
            var bold = new GUIStyle();
            bold.fontStyle = FontStyle.Bold;
            GUILayout.Label("Items in my stack", bold);

            // add a label for each item, you can add more properties
            // you can even access components inside each item and display them
            // for example if every item had a sprite we could easily show it 
            foreach (var item in stack)
            {
                GUILayout.Label(item.ToString());
            }
        }
    }

    private void OnStateChanged(object sender, StateChangedEventArgs e)
    {
        print("State changed : " + e.PreviousState + " -> " + e.State);

        if (e.PreviousState == States.Settings)
        {
            settings.SaveSettings();
        }

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
            case States.Info:
                SceneManager.LoadScene("InfoScene");
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

    public void PlayClip(Sounds sound)
    {
        Sound s = Array.Find(sounds, s => s.sound == sound);
        GameObject go = new GameObject("SoundClip");
        go.transform.parent = transform;
        AudioSource source = go.AddComponent<AudioSource>();
        source.clip = s.clip;

        if (s.type == SoundType.Music)
        {
            source.volume = settings.MusicVolume;
            source.loop = true;
            source.Play();
            MusicSource = source;
            return;
        }

        if (s.type == SoundType.SoundEffect)
        {
            source.volume = settings.SoundVolume;
        }

        source.Play();
        Destroy(go, s.clip.length);
    }
}
