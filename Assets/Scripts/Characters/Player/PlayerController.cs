using TMPro;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private static readonly int HasGun = Animator.StringToHash("hasGun");
    private static readonly int HasGunInt = Animator.StringToHash("hasGunInt");
    
    private static readonly int IsRunning = Animator.StringToHash("isMoving");
    private static readonly int IsRunningInt = Animator.StringToHash("isMovingInt");
    
    public float moveSpeed = 5f;
    public Rigidbody2D rb;
    public Weapon weapon;

    public TextMeshProUGUI ShowWinText;
    private Vector2 _moveDirection;
    private Vector2 _mousePosition;

    private float _comboCooldown = 4f;
    public int combo;
    private float _lastComboTime;
    
    private int _numberOfEnemies;

    private Animator _animator;
    
    void Start()
    {
        BotController[] enemies = FindObjectsByType<BotController>(FindObjectsSortMode.None);
        _numberOfEnemies = enemies.Length;
        _animator = GetComponent<Animator>();
    }
    void Update()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");


        _animator.SetBool(HasGun, weapon);
        _animator.SetBool(IsRunning, moveX != 0 || moveY != 0);
        _animator.SetFloat(HasGunInt, weapon ? 1f : 0f);
        _animator.SetFloat(IsRunningInt, moveX != 0 || moveY != 0 ? 1f : 0f);
        
        
        
        if (Input.GetMouseButtonDown(0) && weapon != null) 
        {
            weapon.Fire(true);
        }
        
        if (Input.GetMouseButtonDown(1) && weapon != null)
        {
            weapon.Drop(weapon);
            weapon = null;
        }
        
        if (Input.GetMouseButtonDown(1) && weapon == null)
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0;
            RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);
            Debug.Log($"hit");
            if (hit.collider != null)
            {
                Weapon weaponOnGround = hit.collider.GetComponent<Weapon>();
                Debug.Log($"{weaponOnGround}");
                if (weaponOnGround != null && weaponOnGround.isDropped)
                {
                    weaponOnGround.PickUp(weaponOnGround, this);
                }
            }
        }
        
        _moveDirection = new Vector2(moveX, moveY).normalized;
        _mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Vector2 direction = new Vector2(_mousePosition.x - transform.position.x, _mousePosition.y - transform.position.y);
        transform.up = direction;
        
        rb.linearVelocity = new Vector2(_moveDirection.x * moveSpeed, _moveDirection.y * moveSpeed);
        
        if (Time.time - _lastComboTime >= _comboCooldown)
        {
            combo = 0;
        }
    }

    public void AddCombo()
    {
        combo++;
        _lastComboTime = Time.time;
        Debug.Log("Combo:" + combo);
    }
    
    public void CheckWinCondition()
    {            
        _numberOfEnemies--;
        if (_numberOfEnemies == 0)
        {
            ShowWinText.gameObject.SetActive(true); 
            ShowWinText.enabled = true;
            Debug.Log("WIN");
        }
    }
}
