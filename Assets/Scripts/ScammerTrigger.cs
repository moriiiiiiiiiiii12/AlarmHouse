using UnityEngine;

public class ScammerTrigger : MonoBehaviour
{
    [SerializeField] private Alarm _alarm;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Scammer scammer) == true)
            _alarm.IncreaseVolume();
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out Scammer scammer) == true)
            _alarm.DecreaseVolume();
    }
}
