using SceneTransitionSystem;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioListenerActivator : MonoBehaviour
{
    private AudioListener audioListener;

    private void Awake()
    {
        audioListener = GetComponent<AudioListener>();
        
        SceneManager.sceneUnloaded += EnableAudioListener;
    }

    private void EnableAudioListener(Scene sceneUnloadedContext)
    {
        if (audioListener && !sceneUnloadedContext.name.Equals(GenericTransitionManager.TransitionSceneName))
        {
            audioListener.enabled = true;
            SceneManager.sceneUnloaded -= EnableAudioListener;
        }
    }

    private void OnDestroy()
    {
        SceneManager.sceneUnloaded -= EnableAudioListener;
    }
}