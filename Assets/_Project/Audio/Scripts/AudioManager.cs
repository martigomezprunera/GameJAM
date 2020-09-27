﻿using System;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    public List<Sound> Sounds;


    private void Awake()
    {
        #region Singletone
        if (Instance != null)
        {
            Destroy(gameObject);

            return;
        }

        Instance = this;

        DontDestroyOnLoad(gameObject);

        #endregion

        foreach (Sound sound in Sounds)
        {
            sound.SetupSource(gameObject);
        }
    }

    public void PlaySound(string name)
    {
        Sound sound = Sounds.Find(s => s.Name == name);

        if (sound == null)
            throw new NullReferenceException("The sound you are tryong to play does not exist. (Incorrect name?)");


        sound.Play();
    }
}