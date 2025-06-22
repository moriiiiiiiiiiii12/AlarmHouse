
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private float _speedMove = 20f;    

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        string verticalInputAxis = "Vertical";
        string horizontalInputAxis = "Horizontal";
        
        float directionX = Input.GetAxis(verticalInputAxis);
        float directionY = Input.GetAxis(horizontalInputAxis);

        transform.Translate(Vector3.forward * _speedMove * Time.deltaTime * directionX);
        transform.Translate(Vector3.right * _speedMove * Time.deltaTime * directionY);
    }
}
