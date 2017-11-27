using UnityEngine;
using System.Collections;

public class CLGameAudio : MonoBehaviour
{
    public AudioClip NotEnoughEnergy;
    public AudioClip Battle;
    public AudioClip Equip;
    public AudioClip Login;
    public AudioClip Account;

    private static CLGameAudio sSingletonInstance;

    CLGameAudio()
    {
        CLGameAudio.sSingletonInstance = this;
    }

    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    public static CLGameAudio Instance
    {
        get
        {
            return sSingletonInstance;
        }
    }
}