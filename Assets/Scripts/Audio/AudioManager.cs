using FMODUnity;
using UnityEngine;
using UnityEngine.Timeline;

public class AudioManager : MonoBehaviour
{
    [SerializeField]
    EventReference MusicCombatBase;
    [SerializeField]
    EventReference MusicBoss;
    public static AudioManager instance { get; private set; }
    private void Start()
    {
        RuntimeManager.PlayOneShot(MusicCombatBase);
        
    }
    private void Awake()
    {
        if(instance != null)
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
     //   RuntimeManager.PlayOneShot(MusicCombatBase);
     
        
        RuntimeManager.PlayOneShot(MusicBoss);
    }

}
