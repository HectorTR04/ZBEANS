using UnityEngine;

public class AmmoCrate : MonoBehaviour
{
    [SerializeField] private float m_ammo;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (other.gameObject.TryGetComponent<GunController>(out var gunController))
            {
                gunController.PickUpAmmo(m_ammo);
                gameObject.SetActive(false);
            }
        }
    }
}
