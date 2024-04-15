using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music : MonoBehaviour
{
    [SerializeField] private AudioSource MusicSource;
    public bool muted;

    public static Music instance;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
            Mute();
    }

    void Start()
    {
        MusicSource.Play();
    }

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Mute()
    {
        muted = !muted;
        if (muted)
            MusicSource.volume = 0f;
        else MusicSource.volume = 0.08f;
    }
}
