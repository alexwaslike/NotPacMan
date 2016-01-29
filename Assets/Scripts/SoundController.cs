using UnityEngine;
using System.Collections;

public class SoundController : MonoBehaviour {

    public GameObject mainCharacterCamera;

    public AudioSource ambientSource;
    public AudioSource footstepsSource;

    public AudioClip footsteps;
    public AudioClip ambience;

	void Start () {
        ambientSource = AddAudio(clip: ambience, loop: true, playAwake: true, volume: 0.2f);
        //ambientSource.Play();
        footstepsSource = AddAudio(clip: footsteps, loop: true, playAwake: false, volume: 0.8f);
    }

    public void PlayFootsteps(bool play)
    {
        if (play)
            footstepsSource.Play();
        else
            footstepsSource.Stop();
    }

    private AudioSource AddAudio(AudioClip clip, bool loop, bool playAwake, float volume){
        AudioSource newAudio = mainCharacterCamera.AddComponent<AudioSource>();
        newAudio.clip = clip; 
        newAudio.loop = loop;
        newAudio.playOnAwake = playAwake;
        newAudio.volume = volume; 
        return newAudio; 
    }
	
}
