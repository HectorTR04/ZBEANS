using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Camera m_playerCam;
    [SerializeField] private float m_speed;
    [SerializeField] private float m_sensitivity;

    public void Move(Vector2 moveInput, CharacterController controller)
    {
        Vector3 moveDirection = new(moveInput.x, 0, moveInput.y);
        controller.Move(m_speed * Time.deltaTime * moveDirection);
    }

    public void Look()
    {
        Ray ray = m_playerCam.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            Vector3 hitPoint = hit.point;

            Vector3 direction = hitPoint - transform.position;
            direction.y = 0f;

            transform.rotation = Quaternion.LookRotation(direction);
        }
    }
}
