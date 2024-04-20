using FMODUnity;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField]
    EventReference MusicBase;
    public static AudioManager instance { get; private set; }
    private void Awake()
    {
        if(instance != null)
        {
        }
        instance = this;
        RuntimeManager.PlayOneShot(MusicBase);
    }
    public void PlayOneShot(EventReference sound, Vector3 worldPos)
    {
        RuntimeManager.PlayOneShot(sound, worldPos);
    }

}
