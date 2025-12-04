using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private PlayerInput m_input;
    private PlayerInput.MovementActions m_movementActions;
    private PlayerMovement m_playerMovement;
    private CharacterController m_characterController;

    #region Unity Methods
    private void OnEnable()
    {
        m_movementActions.Enable();
    }
    private void OnDisable()
    {
        m_movementActions.Disable();
    }
    private void Awake()
    {
        m_input = new PlayerInput();
        m_movementActions = m_input.Movement;
        m_playerMovement = GetComponent<PlayerMovement>();
        m_characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        if (m_playerMovement == null) return;
        m_playerMovement.Move(m_movementActions.Walk.ReadValue<Vector2>(), m_characterController);
        //m_playerMovement.Look(m_movementActions.Look.ReadValue<Vector2>());
    }
    #endregion
}
