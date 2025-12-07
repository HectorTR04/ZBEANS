using TMPro;
using UnityEngine;

public class GunController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI m_ammoText;
    [SerializeField] private float m_timeBetweenFiring;
    [SerializeField] private float m_timeToReloadBullet;
    private PlayerController m_playerController;
    private float m_currentAmmo;
    private float m_maxAmmo;
    private float m_reserveAmmo;
    private bool m_canFire;
    private float timer;

    private void Awake()
    {
        m_playerController = GetComponent<PlayerController>();
        m_reserveAmmo = 5;
        m_maxAmmo = 20;
        m_currentAmmo = 20;
        UpdateUI();
        m_canFire = true;
    }

    public void GunHandling()
    {
        timer += Time.deltaTime;
        if (Input.GetMouseButton(0) && m_currentAmmo > 0 && m_canFire)
        {
            m_currentAmmo--;
            m_canFire = false;
            if (m_playerController.IsLookingAtEnemy())
            {
                CheckEnemyHit();
            }
        }
        if (Input.GetKey(KeyCode.R))
        {
            if (timer >= m_timeToReloadBullet)
            {
                Reload();
            }
        }
        if(timer >= m_timeBetweenFiring)
        {
            m_canFire = true;
            timer = 0f;
        }
        UpdateUI();
    }

    private void CheckEnemyHit()
    {
        Ray ray = new(transform.position, transform.forward);

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            if (hit.collider.CompareTag("Enemy"))
            {
                if (hit.collider.TryGetComponent<ZombieAgent>(out var hitAgent))
                {
                    hitAgent.Damage();
                }
            }
        }
    }

    private void Reload()
    {
        if(m_reserveAmmo > 0 && m_currentAmmo < m_maxAmmo)
        {
            m_reserveAmmo--;
            m_currentAmmo++;
        }
    }  
    
    private void UpdateUI()
    {
        m_ammoText.text = $"{m_currentAmmo} / {m_reserveAmmo}";
    }
}
