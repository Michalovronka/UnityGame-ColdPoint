using UnityEngine;

public class Weapon : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float fireForce = 20f;
    public int numOfBullets;
    public int maxNumOfBullets;
    public bool isDropped;
    public AudioSource shootingSfx;
    public void Start()
    {
        maxNumOfBullets = numOfBullets;
    }

    public void Fire(bool isPlayer)
    {
        if (numOfBullets > 0)
        {
            shootingSfx.Play();
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            bullet.GetComponent<Rigidbody2D>().AddForce(firePoint.up * fireForce, ForceMode2D.Impulse);
            bullet.tag = isPlayer ? "PlayerBullet" : "EnemyBullet";
            numOfBullets--;
        }
        
    }
    
    public void Drop(Weapon weapon)
    {
        isDropped = true;
        weapon.transform.SetParent(null);
        Rigidbody2D rb = weapon.GetComponent<Rigidbody2D>();
        if (rb == null) rb = weapon.gameObject.AddComponent<Rigidbody2D>();
        rb.gravityScale = 0;
        rb.bodyType = RigidbodyType2D.Dynamic; 
        rb.linearVelocity = Vector2.zero; 
        CircleCollider2D circleCollider = weapon.GetComponent<CircleCollider2D>();
        if (circleCollider != null)
        {
            circleCollider.enabled = true;
            circleCollider.isTrigger = true;
        }
    }

    public void PickUp(Weapon weapon, PlayerController player)
    {
        player.weapon = weapon;
        
        weapon.gameObject.SetActive(true);
        
        weapon.transform.SetParent(player.transform);
        weapon.transform.localPosition = new Vector3(0.1158f, 0.2493f, 0);
        weapon.transform.localRotation = Quaternion.identity; 

        Rigidbody2D rb = weapon.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.linearVelocity = Vector2.zero; 
            rb.bodyType = RigidbodyType2D.Kinematic; 
        }
        
        Debug.Log($"Weapon picked up: {weapon.name}");
    }
}
