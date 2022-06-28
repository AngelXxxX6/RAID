using UnityEngine;

public class AmmoPack : MonoBehaviour
{
    [SerializeField] private int ammo;
    [SerializeField] private int weaponType;
    void Start()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Destroy(gameObject);
            Player.Instance.AddAmmo(ammo, weaponType);
        }
    }
    void Update()
    {

    }
}
