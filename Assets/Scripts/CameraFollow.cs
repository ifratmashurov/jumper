using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // ссылка на игрока
    public float smoothSpeed = 5f;
    public Vector3 offset = new Vector3(0f, 1f, -10f); // -10 по Z обязательно для 2D

    void LateUpdate()
    {
        if (target != null)
        {
            Vector3 desiredPosition = target.position + offset;
            // Плавное следование за персонажем
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
            transform.position = smoothedPosition;
        }
    }
}