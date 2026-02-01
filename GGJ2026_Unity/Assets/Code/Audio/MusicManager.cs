using SadUtils;
using System.Collections;
using UnityEngine;


[AddComponentMenu("DevKit/Systems/Sounds/Music Manager")]
[RequireComponent(typeof(AudioSource))]
public class MusicManager : MonoBehaviour
{
    public static MusicManager instance;

    //data
    public UnityDictionary<string, Sound> tracks;

    [Header("Settings")]
    [Tooltip("Time it takes for the last track to be muted")]
    public float muteTime = 0.5f;
    [Tooltip("Time it takes for the new track to reach normal volume")]
    public float transitionTime = 1f;

    //vars
    private bool switching;
    //external components
    private AudioSource source;

    //======================== setup ========================
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            SetupNonDestroy();
            SetupAudioSource();
        }
    }
    private void SetupNonDestroy()
    {
        transform.parent = null;
        DontDestroyOnLoad(gameObject);
    }

    private void SetupAudioSource()
    {
        source = GetComponent<AudioSource>();
        source.loop = true;
    }

    //================== setup track ==================
    private void SetupTrack(Sound sound)
    {
        //settings
        float volume = source.volume;
        sound.SetupSource(source);
        source.volume = volume; //keep old volume
                                //play
        source.Play();
    }

    //================== manage music track ==================
    public void SwitchTrack(string name)
    {
        if (!switching)
        {
            if (tracks.ContainsKey(name))
            {
                StartCoroutine(SwitchCo(tracks[name]));
            }
            else { Debug.LogError(transform.name + " contains no sound for " + name); }
        }
    }

    //===================== switch music track =====================
    private IEnumerator SwitchCo(Sound nextSound)
    {
        switching = true;
        yield return MuteLastTrack();
        SetupTrack(nextSound);
        yield return UnmuteNextTrack(nextSound);
        source.volume = nextSound.volume;
        switching = false;
    }

    private IEnumerator MuteLastTrack()
    {
        float timer = 0f;
        float startVolume = source.volume;
        while (source.volume > 0f)
        {
            timer += Time.deltaTime;
            source.volume = startVolume - Mathf.Lerp(0, startVolume, timer / muteTime);
            yield return null;
        }
    }

    private IEnumerator UnmuteNextTrack(Sound nextSound)
    {
        float timer = 0f;
        while (source.volume < nextSound.volume)
        {
            timer += Time.deltaTime;
            source.volume = Mathf.Lerp(0, nextSound.volume, timer / transitionTime);
            yield return null;
        }
    }
}

