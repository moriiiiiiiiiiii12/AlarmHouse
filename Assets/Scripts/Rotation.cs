using UnityEngine;

public class Rotation : MonoBehaviour
{
    [SerializeField] private float _speedRotation = 100f;

    private void Update()
    {
        Rotate();
    }

    private void Rotate()
    {
        string mouseXInputAxis = "Mouse X";
        float directionX = Input.GetAxis(mouseXInputAxis);

        transform.Rotate(directionX * _speedRotation * Time.deltaTime * Vector3.up);
    }
}
