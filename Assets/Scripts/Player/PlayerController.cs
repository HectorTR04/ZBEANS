using System;
using System.Security.Cryptography;
using TMPro;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Camera m_playerCam;
    [SerializeField] private float m_maxHealth;
    [SerializeField] private float m_detectionRange;
    [SerializeField] private TextMeshProUGUI m_scoreText;

    [Header("Sanity")]
    [SerializeField] private float m_maxSanity;
    [SerializeField] private float m_sanityGainPerSecond;
    [SerializeField] private float m_sanityLossPerSecond;

    private PlayerInput m_input;
    private PlayerInput.MovementActions m_movementActions;
    private PlayerMovement m_playerMovement;
    private CharacterController m_characterController;
    private StatusController m_sanityController;
    private GunController m_gunController;

    public float CurrentHealth { get; set; }
    public float CurrentSanity { get; set; }
    public float CurrentScore { get; set; }

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
        m_sanityController = GetComponent<StatusController>();
        m_gunController = GetComponent<GunController>();
        m_sanityController.Initialize(m_maxSanity, m_maxHealth);
        Cursor.lockState = CursorLockMode.Confined;
    }

    private void Update()
    {
        m_sanityController.UpdateSanity(m_sanityGainPerSecond, m_sanityLossPerSecond);
        m_sanityController.UpdateSanityBar(CurrentSanity);
        m_sanityController.UpdateHealthBar(CurrentHealth);
        m_gunController.GunHandling();
        if (m_playerMovement == null) return;
        m_playerMovement.Move(m_movementActions.Walk.ReadValue<Vector2>(), m_characterController);
        m_playerMovement.Look();
        m_scoreText.text = $"Score: {CurrentScore}"; 
    }
    #endregion

    public void IncreaseScore(float scoreIncrease)
    {
        CurrentScore += scoreIncrease;
    }

    public bool IsSmoking()
    {
        if (Input.GetKey(KeyCode.F))
        {
            return true;
        }
        return false;
    } 
    public bool IsLookingAtEnemy()
    {
        Ray ray = new(transform.position, transform.forward);

        if (Physics.Raycast(ray, out RaycastHit hit))
        {

            if (hit.collider.CompareTag("Enemy"))
            {
                float distance = Vector3.Distance(transform.position, hit.collider.transform.position);

                if (distance < m_detectionRange)
                {
                    return true;
                }
            }
        }
        return false;
    }
}
