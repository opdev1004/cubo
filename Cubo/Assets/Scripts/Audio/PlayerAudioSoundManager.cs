using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudioSoundManager : MonoBehaviour
{
	public AudioClip beam;
	public AudioClip charging;
	private AudioSource ASComponent;

    // Start is called before the first frame update
    void Start()
    {
        ASComponent = GetComponent<AudioSource>();
    }
	
	public void PlayCharging()
	{
		ASComponent.Stop();
		ASComponent.clip = charging;
		ASComponent.Play();
	}

	public void PlayBeam()
	{
		ASComponent.Stop();
		ASComponent.clip = beam;
		ASComponent.Play();
	}
}
