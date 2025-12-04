using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    [Header("Initial Position")]
    [SerializeField] private float m_xDistanceFromPlayer;
    [SerializeField] private float m_yDistanceFromPlayer;
    [SerializeField] private float m_zDistanceFromPlayer;

    [Header("Initial Rotation")]
    [SerializeField] private float m_xRotation;
    [SerializeField] private float m_yRotation;
    [SerializeField] private float m_zRotation;

    [Header("Speed")]
    [SerializeField] private float m_camSmoothTime;

    private Transform m_playerTransform;
    private Vector3 m_positionOffset;
    private Vector3 m_currentVelocity;

    #region Unity Methods
    private void Awake()
    {
        m_playerTransform = GameObject.FindWithTag("Player").transform;
        if (m_playerTransform == null)
        {
            Debug.Log("Player not found in scene"); 
            return;
        }    
        SetInitialTransform();
        m_positionOffset = m_playerTransform.position - transform.position;
    }

    private void Update()
    {
        Vector3 targetPosition = m_playerTransform.position - m_positionOffset;
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref m_currentVelocity, m_camSmoothTime);
    }
    #endregion

    private void SetInitialTransform()
    {
        Vector3 origin = m_playerTransform.position;
        transform.SetPositionAndRotation(
            origin += new Vector3(m_xDistanceFromPlayer, m_yDistanceFromPlayer, m_zDistanceFromPlayer), 
            Quaternion.Euler(m_xRotation, m_yRotation, m_zRotation));
    }
}
