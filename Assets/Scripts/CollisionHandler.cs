using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] private float _levelLoadDelay = 2f;
    [SerializeField] private AudioClip _crashClip;
    [SerializeField] private AudioClip _successClip;

    private AudioSource _audioSource;

    private bool _isControllable = true;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter(Collision other)
    {
        if (!_isControllable) { return; }
        
        switch (other.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("Friendly");
                break;
            case "Finish":
                StartSuccessSequence();
                break;
            default:
                StartCrashSequence();
                break;
        }
        
    }
    
    private void StartSuccessSequence()
    {
        _isControllable = false;
        _audioSource.Stop();
        _audioSource.PlayOneShot(_successClip);
        GetComponent<Movement>().enabled = false;
        Invoke("LoadNextLevel", _levelLoadDelay);
    }

    private void StartCrashSequence()
    {
        _isControllable = false;
        _audioSource.Stop();
        _audioSource.PlayOneShot(_crashClip);
        GetComponent<Movement>().enabled = false;
        Invoke("ReloadScene", _levelLoadDelay);
    }

    private void LoadNextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;

        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
        }

        SceneManager.LoadScene(nextSceneIndex);
    }
    
    private void ReloadScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        
        SceneManager.LoadScene(currentSceneIndex);
    }
}
