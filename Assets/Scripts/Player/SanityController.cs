using UnityEditor.U2D;
using UnityEngine;

[RequireComponent (typeof(PlayerController))]
public class SanityController : MonoBehaviour
{
    [SerializeField] private RectTransform m_sanityBar;

    private PlayerController m_playerController;

    private readonly float m_timeBetweenUpdates = 1f;
    private float m_maxSanity;
    private float m_sanityBarWeight;
    private float m_sanityBarHeight;
    private float timer = 0f;
    

    #region Unity Methods
    private void Awake()
    {
        m_playerController = GetComponent<PlayerController>();
    }
    #endregion

    public void Initialize(float maxSanity)
    {
        m_playerController.CurrentSanity = maxSanity; 
        m_sanityBarWeight = m_sanityBar.rect.width;
        m_sanityBarHeight = m_sanityBar.rect.height;
        m_maxSanity = maxSanity;
    }

    public void UpdateSanity(float sanityGain, float sanityLoss)
    {
        timer += Time.deltaTime;
        if(timer > m_timeBetweenUpdates)
        {
            IncreaseSanity(sanityGain);
            DecreaseSanity(sanityLoss);
            timer = 0f;
        }
    }

    private void IncreaseSanity(float sanityIncrease)
    {
        if (!m_playerController.IsSmoking()) return;
        if (m_playerController.CurrentSanity < m_maxSanity)
        {
            m_playerController.CurrentSanity += sanityIncrease;
        }
    }
    private void DecreaseSanity(float sanityDrop)
    {
        if (!m_playerController.IsLookingAtEnemy()) return;
        if(m_playerController.CurrentSanity > 0)
        {
            m_playerController.CurrentSanity -= sanityDrop;
        }
    }

    public void UpdateSanityBar(float currentSanity)
    {
        float newWidth = (currentSanity / m_maxSanity) * m_sanityBarWeight;
        m_sanityBar.sizeDelta = new Vector2(newWidth, m_sanityBarHeight);
    }
}
