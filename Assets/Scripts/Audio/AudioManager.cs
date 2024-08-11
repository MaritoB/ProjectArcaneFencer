using FMODUnity;
using UnityEngine;
using UnityEngine.Timeline;

public class AudioManager : MonoBehaviour
{
    private FMOD.Studio.EventInstance MusicCombatInstance;
    private FMOD.Studio.EventInstance MusicBossInstance;
    public float musicVolumen;
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
        MusicCombatInstance.setVolume(musicVolumen);
        MusicCombatInstance.start();
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
        if (sound.IsNull) { return; }
        RuntimeManager.PlayOneShot(sound, worldPos);
    }
    public void PlayBossMusic()
    {
        MusicCombatInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        //MusicCombatInstance.setVolume(0);
        MusicBossInstance.setVolume(musicVolumen);
        MusicBossInstance.start();

    }
    public void TurnOffMusic()
    {
        MusicCombatInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        MusicBossInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        MusicCombatInstance.setVolume(0);
        MusicBossInstance.setVolume(0);

    }
    public void FinishBossMusic()
    {
       // MusicBossInstance.setVolume(0);
        MusicBossInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        MusicCombatInstance.setVolume(musicVolumen);
        MusicCombatInstance.start();

    }
    

}
