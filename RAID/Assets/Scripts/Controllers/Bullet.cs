using UnityEngine;

public class Bullet : MonoBehaviour
{
    private int bulletDamage;
    private float bulletSpeed = 100;
    private string owner;
    public float BulletSpeed => bulletSpeed;
    void Start()
    {

    }
    public void SetBulletSpeed(float val)
    {
        bulletSpeed = val;
    }
    public void SetOwner(string val)
    {
        owner = val;
    }
    public void SetDamage(int val)
    {
        bulletDamage = val;
    }
    private void Awake()
    {

    }
    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("wall"))
        {
            Player.Instance.DecHealth(bulletDamage);
            Destroy(gameObject);
        }
        if (owner == "player")
        {
            if (collision.CompareTag("enemy"))
            {
                Enemy enemy = collision.gameObject.GetComponent<Enemy>();
                enemy.DecHealth(bulletDamage);
                Destroy(gameObject);

            }
        }
        if (owner == "enemy")
        {
            if (collision.CompareTag("Player"))
            {

                Player.Instance.DecHealth(bulletDamage);
                Destroy(gameObject);

            }
        }
    }

}
