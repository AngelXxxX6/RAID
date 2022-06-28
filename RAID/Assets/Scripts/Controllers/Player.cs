using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    private Camera cam;
    private float playerSpeed = 3f;
    private Rigidbody2D rig;
    private Vector2 dir;
    private int weapon;
   
      
  
    public static Player Instance;
    private int health;
    private Text healthUI;
    private Text weaponUI;
    
    private int[] ammo;
    private Text ammoUI;
   
    [SerializeField] private GameObject bullet;
    private string[] weapons;
    private int countWeapon;
    private void Awake()
    {
        
        healthUI = GameObject.Find("HealthUI").GetComponent<Text>();
        ammoUI = GameObject.Find("AmmoUI").GetComponent<Text>();
        weaponUI = GameObject.Find("WeaponNameUI").GetComponent<Text>();
        
        if (!Instance)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        countWeapon = 2;
        rig = GetComponent<Rigidbody2D>();
        cam = Camera.main;
        ammo = new int[countWeapon];
        weapons = new string[countWeapon];


    }
    private void Start()

    {
       
        PlayerPrefs.DeleteAll();
        
        weapons[0] = "Pistol";
        weapons[1] = "AK47";
        health = 100;
        weapon = 0;
        ammo[0] = 8;
        ammo[1] = 30;
        weaponUI.text = weapons[0];
        ammoUI.text = ammo[weapon].ToString();
        healthUI.text = health.ToString();
        
       
    }
   
    
    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Shoot(weapon, weapons[weapon]);
        }
        if (Input.GetAxis("Mouse ScrollWheel") > 0.1)
        {
            int weapon2 = weapon + 1;
            if (weapon2 == weapons.Length)
            {
                weapon = 0;
                ammoUI.text = ammo[weapon].ToString();
                weaponUI.text = weapons[weapon];
            }
            else
            {
                weapon++;
                ammoUI.text = ammo[weapon].ToString();
                weaponUI.text = weapons[weapon];
            }
        }
        if (Input.GetAxis("Mouse ScrollWheel") < -0.1)
        {
            int weapon2 = weapon - 1;
            if (weapon2 < 0)
            {
                weapon = weapons.Length - 1;
                ammoUI.text = ammo[weapon].ToString();
                weaponUI.text = weapons[weapon];
            }
            else
            {
                weapon--;
                ammoUI.text = ammo[weapon].ToString();
                weaponUI.text = weapons[weapon];
            }
        }
    }
    public Transform MyPosition()
    {
        return gameObject.GetComponent<Transform>();
    }
    private void FixedUpdate()
    {
        Vector2 currentPos = rig.position;
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector2 inputVector = new Vector2(horizontalInput, verticalInput);
        inputVector = Vector2.ClampMagnitude(inputVector, 1);
        dir = inputVector;
        Vector2 movement = inputVector * playerSpeed;
        Vector2 newPos = currentPos + movement * Time.fixedDeltaTime;
        rig.MovePosition(newPos);
        Vector3 camCenter = new Vector3(cam.transform.position.x, cam.transform.position.y, cam.transform.position.z);

        if (rig.position.x < camCenter.x)
        {
            Vector3 camPos = Vector3.Lerp(camCenter, new Vector3(newPos.x, camCenter.y, -10), playerSpeed * Time.fixedDeltaTime);
            cam.transform.position = camPos;
        }
        if (rig.position.y > camCenter.y)
        {
            Vector3 camPos = Vector3.Lerp(camCenter, new Vector3(camCenter.x, newPos.y, -10), playerSpeed * Time.fixedDeltaTime);
            cam.transform.position = camPos;
        }
    }
    void Shoot(int num, string name)
    {
        if (ammo[num] > 0)
        {
            DecAmmo(1, num);
            GameObject clone = Instantiate(bullet, gameObject.transform.position, Quaternion.identity);
            Bullet bulletClone = clone.GetComponent<Bullet>();
            var speed = bulletClone.BulletSpeed;
            bulletClone.SetOwner("player");
            switch (name)
            {
                case "Pistol": bulletClone.SetDamage(1); break;
                case "AK47": bulletClone.SetDamage(10); break;
                default: break;
            }
            Rigidbody2D rb = clone.GetComponent<Rigidbody2D>();
            if (dir != new Vector2(0, 0))
                rb.velocity = dir * speed;
            else
            {
                rb.velocity = new Vector2(0, speed);
            }
            StartCoroutine(bulletLive(clone));
        }
    }
    IEnumerator bulletLive(GameObject gameObject)
    {
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }
    #region UTILITY
    public void AddHealth(int val)
    {
        health += val;
        if (health > 100)
            health = 100;
        healthUI.text = health.ToString();
    }
    public void DecHealth(int val)
    {
        health -= val;
        healthUI.text = health.ToString();
    }
    public void DecAmmo(int val, int num)
    {

        ammo[num] -= val;
        if (ammo[num] < 0)
            ammo[num] = 0;
        ammoUI.text = ammo[weapon].ToString();

    }
    public void AddAmmo(int val, int num)
    {
        ammo[num] += val;
        if (weapons[num] == "Pistol" && ammo[num] > 8)
        {
            ammo[num] = 8;
        }
        if (weapons[num] == "AK47" && ammo[num] > 30)
        {
            ammo[num] = 30;
        }
        ammoUI.text = ammo[weapon].ToString();
    }
    #endregion
    
}
