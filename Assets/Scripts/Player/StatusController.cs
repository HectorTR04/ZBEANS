using UnityEngine;

[RequireComponent (typeof(PlayerController))]
public class StatusController : MonoBehaviour
{
    [SerializeField] private RectTransform m_sanityBar;
    [SerializeField] private RectTransform m_healthBar;

    private PlayerController m_playerController;

    private readonly float m_timeBetweenUpdates = 1f;
    private float m_maxSanity;
    private float m_maxHealth;
    private float m_sanityBarWidth;
    private float m_sanityBarHeight;
    private float m_healthBarWidth;
    private float m_healthBarHeight;
    private float timer = 0f;


    #region Unity Methods
    private void Awake()
    {
        m_playerController = GetComponent<PlayerController>();
    }
    #endregion

    public void Initialize(float maxSanity, float maxHealth)
    {
        m_playerController.CurrentSanity = maxSanity; 
        m_playerController.CurrentHealth = maxHealth;
        m_sanityBarWidth = m_sanityBar.rect.width;
        m_sanityBarHeight = m_sanityBar.rect.height;
        m_healthBarWidth = m_healthBar.rect.width;
        m_healthBarHeight = m_healthBar.rect.height;
        m_maxSanity = maxSanity;
        m_maxHealth = maxHealth;
    }

    public void UpdateSanity(float sanityGain, float sanityLoss)
    {
        timer += Time.deltaTime;
        if (timer > m_timeBetweenUpdates)
        {
            IncreaseSanity(sanityGain);
            DecreaseSanity(sanityLoss);
            timer = 0f;
        }
    }

    public void TakeDamage(EnemyAgent agent)
    {
        m_playerController.CurrentHealth -= agent.Damage;
    }

    public void UpdateSanityBar(float currentSanity)
    {
        float newWidth = (currentSanity / m_maxSanity) * m_sanityBarWidth;
        m_sanityBar.sizeDelta = new Vector2(newWidth, m_sanityBarHeight);
    }

    public void UpdateHealthBar(float currentHealth)
    {
        float newWidth = (currentHealth / m_maxHealth) * m_healthBarWidth;
        m_healthBar.sizeDelta = new Vector2(newWidth, m_healthBarHeight);
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
}
