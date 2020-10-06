using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManagerScript : MonoBehaviour
{

    public static AudioClip PlayerShootSound, PlayerJumpSound, PlayerWalkSound, AmbientMusic;
   
    public static AudioSource AudioSource; 

    // Start is called before the first frame update
    void Start()
    {

        PlayerShootSound = Resources.Load<AudioClip>("throw");
        PlayerJumpSound = Resources.Load<AudioClip>("jump");
        PlayerWalkSound = Resources.Load<AudioClip>("dirt");
        AmbientMusic = Resources.Load<AudioClip>("happy");

        AudioSource = GetComponent<AudioSource>();
      
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void PlaySound(string clip)
    {
        switch(clip)
        {
            case "throw":
                AudioSource.PlayOneShot(PlayerShootSound);
                break;
            case "step":
                AudioSource.PlayOneShot(PlayerWalkSound);
                break;
            case "jump":
                AudioSource.PlayOneShot(PlayerJumpSound);
                break; 
            

        }
    }
}
