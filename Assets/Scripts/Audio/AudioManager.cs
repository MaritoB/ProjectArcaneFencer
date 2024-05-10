using FMODUnity;
using UnityEngine;
using UnityEngine.Timeline;

public class AudioManager : MonoBehaviour
{
    private FMOD.Studio.EventInstance MusicCombatInstance;
    private FMOD.Studio.EventInstance MusicBossInstance;

    [SerializeField]
    EventReference MusicCombatBase;
    [SerializeField]
    EventReference MusicBoss;
    [SerializeField]
    [Range(0, 1)]
    private float BossVolumen;
    public static AudioManager instance { get; private set; }
    private void Start()
    {
        MusicCombatInstance = RuntimeManager.CreateInstance(MusicCombatBase);
        MusicBossInstance = RuntimeManager.CreateInstance(MusicBoss);
        MusicCombatInstance.start();


        //RuntimeManager.PlayOneShot(MusicCombatBase);


    }
    private void Awake()
    {
        if (instance != null)
        {
        }
        instance = this;
    }
    public void PlayOneShot(EventReference sound, Vector3 worldPos)
    {
        RuntimeManager.PlayOneShot(sound, worldPos);
    }
    public void PlayBossMusic()
    {
        MusicBossInstance.setVolume(100);
        MusicCombatInstance.setVolume(0);
        MusicBossInstance.start();

    }
    public void FinishBossMusic()
    {
        MusicBossInstance.setVolume(0);
        MusicBossInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        MusicCombatInstance.setVolume(100);
        MusicBossInstance.start();

    }
    

}
