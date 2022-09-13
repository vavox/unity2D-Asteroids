using System.Collections.Generic;
using UnityEngine;

public static class AudioManager
{
    #region Fields
    static bool initialized = false;
    static AudioSource audioSource;
    static Dictionary<AudioClipName, AudioClip> audioClips = new Dictionary<AudioClipName, AudioClip>();
    #endregion

    #region Public properties
    public static bool Initialized
    {
        get { return initialized; }
    }
    #endregion

    #region  Public methods
    // Initializes the audio manager
    public static void Initialize(AudioSource source)
    {
        initialized = true;
        audioSource = source;
        audioClips.Add(AudioClipName.AsteroidHit, Resources.Load<AudioClip>("Sound/explosion-sound"));
        audioClips.Add(AudioClipName.PlayerDeath, Resources.Load<AudioClip>("Sound/shipExplosion-sound"));
        audioClips.Add(AudioClipName.PlayerShot, Resources.Load<AudioClip>("Sound/shooting-sound"));
        audioClips.Add(AudioClipName.PlayerHit, Resources.Load<AudioClip>("Sound/shipHit-sound"));
        audioClips.Add(AudioClipName.MenuButtonClick, Resources.Load<AudioClip>("Sound/MenuButtonClick"));
    }

    // Plays the audio clip with the given name
    public static void Play(AudioClipName name)
    {
        audioSource.PlayOneShot(audioClips[name]);
    }
    #endregion
}
