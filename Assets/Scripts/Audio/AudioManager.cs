using FMODUnity;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField]
    EventReference MusicBase;
    public static AudioManager instance { get; private set; }
    private void Start()
    {
        RuntimeManager.PlayOneShot(MusicBase);
        
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

}
