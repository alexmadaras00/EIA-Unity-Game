using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuMusic : MonoBehaviour
{
    private new AudioSource audio;
    // Start is called before the first frame update
    void Start()
    {
        audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(PlayerPrefs.GetInt("music") == 0)
        {
            audio.mute = true;
        }
        else
        {
            audio.mute = false;
        }
    }
}
