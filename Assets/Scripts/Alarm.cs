using System.Collections;
using UnityEngine;

public class Alarm : MonoBehaviour
{
    [SerializeField] AudioSource _audioSource;
    [SerializeField] AudioClip _audioClip;

    [SerializeField] private float _currentVolume = 0f;
    [Range(0f, 1f)] [SerializeField] private float _minVolume = 0f;
    [Range(0f, 1f)] [SerializeField] private float _maxVolume = 1f;
    [SerializeField] private float _stepVolume = 0.1f;

    private Coroutine _volumeRoutine;

    private void Awake()
    {
        _audioSource.clip = _audioClip;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Scammer scammer) == false) return;

        _audioSource.volume = _minVolume;

        if (_volumeRoutine != null)
            StopCoroutine(_volumeRoutine);

        _audioSource.Play();
        _volumeRoutine = StartCoroutine(FadeVolume(_maxVolume));
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out Scammer scammer) == false) return;

        if (_volumeRoutine != null)
            StopCoroutine(_volumeRoutine);

        _volumeRoutine = StartCoroutine(FadeOutAndStop());
    }

    private IEnumerator FadeVolume(float targetVolume)
    {
        while (Mathf.Abs(_currentVolume - targetVolume) > 0.01f)
        {
            _currentVolume = Mathf.MoveTowards(_currentVolume, targetVolume, _stepVolume * Time.deltaTime);
            _audioSource.volume = _currentVolume;

            yield return null;
        }

        _currentVolume = targetVolume;
        _audioSource.volume = _currentVolume;
    }
    
    private IEnumerator FadeOutAndStop()
    {
        yield return FadeVolume(_minVolume);    
        _audioSource.Stop();                    
    }
}
