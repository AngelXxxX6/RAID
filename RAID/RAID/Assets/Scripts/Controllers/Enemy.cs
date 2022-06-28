using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    private int health;
    public int Health => health;
    private Text healthUI;
    private float enemySpeed;
    [SerializeField] private string enemyWeapon;
    [SerializeField] private GameObject bullet;
    private Vector2 target;
    private bool isPlayerVisible;
    void Start()
    {
        isPlayerVisible = false;
        enemySpeed = 1f;
        healthUI = FindHpUI().GetComponent<Text>();
        SetHealth(30);
        healthUI.text = Health.ToString();
        StartCoroutine(ShootInPlayer());
    }
    public float GetEnemySpeed()
    {
        return enemySpeed;
    }
    public void SetHealth(int val)
    {
        health = val;
        healthUI.text = Health.ToString();
    }
    public void DecHealth(int val)
    {
        health -= val;
        if (health <= 0)
        {
            Destroy(gameObject);
            EventManager.OnEnemyDead();
        }
        healthUI.text = Health.ToString();
    }
    public Transform FindHpUI()
    {
        Transform canvas = transform.Find("Canvas");
        Transform hp = canvas.Find("HP");
        return hp;
    }

    void Update()
    {
        target = Player.Instance.MyPosition().transform.position - transform.position;

        if (isPlayerVisible)
            transform.position = Vector2.MoveTowards(transform.position, target, enemySpeed * Time.deltaTime);
    }
    private void OnBecameVisible()
    {
        isPlayerVisible = true;
    }
    private void OnBecameInvisible()
    {
        isPlayerVisible = false;
    }
    void Shoot(string name)
    {
        if (isPlayerVisible)
        {
            GameObject clone = Instantiate(bullet, gameObject.transform.position, Quaternion.identity);
            Bullet bulletClone = clone.GetComponent<Bullet>();
            bulletClone.SetBulletSpeed(10);
            bulletClone.SetOwner("enemy");
            var speed = bulletClone.BulletSpeed;
            switch (name)
            {
                case "Pistol": bulletClone.SetDamage(1); break;
                case "AK47": bulletClone.SetDamage(10); break;
                default: break;
            }
            Rigidbody2D rb = clone.GetComponent<Rigidbody2D>();
            rb.velocity = target * speed;


            StartCoroutine(bulletLive(clone));
        }
    }

    IEnumerator bulletLive(GameObject gameObject)
    {
        yield return new WaitForSeconds(4f);
        Destroy(gameObject);
    }
    IEnumerator ShootInPlayer()
    {
        yield return new WaitForSeconds(2f);
        Shoot(enemyWeapon);
        StartCoroutine(ShootInPlayer());
    }
}
