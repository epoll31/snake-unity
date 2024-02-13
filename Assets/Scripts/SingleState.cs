using System;
using UnityEngine;
using UnityEngine.SceneManagement;

[Serializable]
public enum States
{
    Home = 0,
    Playing = 1,
    Paused = 2,
}

public class StateChangedEventArgs : EventArgs
{
    public States State { get; set; }
    public States PreviousState { get; set; }
}

public class SingleState : MonoBehaviour
{
    private static SingleState _instance;
    public static SingleState Instance { get; private set; }

    private void Awake()
    {
        // If there is an instance, and it's not me, delete myself.

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
                if (from == States.Paused)
                {
                    ExplosionHandler[] explosions = FindObjectsOfType<ExplosionHandler>();
                    foreach (ExplosionHandler explosion in explosions)
                    {
                        explosion.Explode();
                    }
                    SceneManager.UnloadSceneAsync("PauseScene");
                }
                SceneManager.LoadScene("HomeScene");

                break;
        }
    }
}
