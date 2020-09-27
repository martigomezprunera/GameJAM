using System;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectSounds : MonoBehaviour
{
    public List<Sound3D> Sounds;

    public void PlaySound(string name)
    {
        Sound3D sound = Sounds.Find(s => s.Name == name);

        if (sound == null)
            throw new NullReferenceException("The sound you are trying to play does not exist. (Incorrect name?)");

        sound.Play();
    }
}
