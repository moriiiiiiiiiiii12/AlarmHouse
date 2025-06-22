using System.Collections;
using UnityEngine;

public class Alarm : MonoBehaviour
{
    [SerializeField] AudioSource _audioSource;
    [SerializeField] AudioClip _audioClip;

    [SerializeField] private float _minVolume = 0f;
    [SerializeField] private float _maxVolume = 1f;
    [SerializeField] private float _stepVolume = 0.1f; 
    private float _currentVolume = 0f;

    private void Start() => _audioSource.clip = _audioClip;
    
    private void OnTriggerEnter(Collider other)
    {
        _audioSource.Play();
        StartCoroutine(IncreaseVolume());
    }

    private void OnTriggerExit(Collider other)
    {
        StopCoroutine(IncreaseVolume()); 
        _audioSource.Stop();

        _currentVolume = 0f;
        _audioSource.volume = _currentVolume; 
    }

    private IEnumerator IncreaseVolume()
    {
        while (_currentVolume < _maxVolume)
        {
            _currentVolume = Mathf.MoveTowards(_currentVolume, _maxVolume, _stepVolume * Time.deltaTime);
            _audioSource.volume = _currentVolume;

            yield return null; 
        }
    }
}
