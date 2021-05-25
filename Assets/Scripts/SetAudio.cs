using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetAudio : MonoBehaviour
{
    public AudioSource audio;

    // Start is called before the first frame update
    void Start()
    {
        audio = GetComponent<AudioSource>();
        if (PlayerPrefs.GetInt("music") == 1)
        {        
            audio.Play();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
