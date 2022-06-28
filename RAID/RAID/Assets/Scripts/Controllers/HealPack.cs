using UnityEngine;

public class HealPack : MonoBehaviour
{
    [SerializeField] private int health;
    void Start()
    {

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Destroy(gameObject);
            Player.Instance.AddHealth(health);
        }
    }
    void Update()
    {

    }
}
