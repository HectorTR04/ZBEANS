using UnityEngine;

public class CursorController : MonoBehaviour
{
    public static CursorController Instance;
    [SerializeField] private Texture2D m_defaultTexture;
    private Vector2 m_clickPosition;

    #region Unity Methods
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            Cursor.lockState = CursorLockMode.Confined;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        Cursor.SetCursor(m_defaultTexture, m_clickPosition, CursorMode.Auto);
    }
    #endregion
}