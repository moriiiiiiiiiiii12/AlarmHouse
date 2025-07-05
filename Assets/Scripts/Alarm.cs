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
        _audioSource.volume = _minVolume;
        _currentVolume = _minVolume;
    }

    public void IncreaseVolume()
    {
        VolumeChange(_maxVolume);
    }

    public void DecreaseVolume()
    {
        VolumeChange(_minVolume);
    }

    public void VolumeChange(float targetVolume)
    {
        if (_volumeRoutine != null)
            StopCoroutine(_volumeRoutine);
        
        if(_audioSource.isPlaying == false)
            _audioSource.Play();

        _volumeRoutine = StartCoroutine(FadeVolume(targetVolume));
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

        if (Mathf.Abs(_currentVolume - _minVolume) > 0.01f)
        {
            _audioSource.Stop();
        }
    }
}
