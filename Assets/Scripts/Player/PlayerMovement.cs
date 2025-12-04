using UnityEngine;
using UnityEngine.Windows;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float m_speed;
    [SerializeField] private float m_sensitivity;

    public void Move(Vector2 moveInput, CharacterController controller)
    {
        Vector3 moveDirection = new Vector3(moveInput.x, 0, moveInput.y);
        controller.Move(m_speed * Time.deltaTime * moveDirection);
    }

    public void Look(Vector2 lookInput)
    {
        float mouseX = lookInput.x;
        transform.Rotate(mouseX * m_sensitivity * Vector3.up);
    }
}
