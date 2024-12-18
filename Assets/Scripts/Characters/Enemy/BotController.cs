using System;
using UnityEngine;
public class BotController : MonoBehaviour
{
    private static readonly int CanMove = Animator.StringToHash("MovementFloat");
    public float moveSpeed = 5f;
    public Rigidbody2D rb;
    public Weapon weapon;
    public Transform player;
    public float fireCooldown = 1f;
    public float detectionRadius;

    private Vector2 _moveDirection;
    private float _lastFireTime;
    
    private Animator _animator;
    private bool canHeBeMovin;
    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (player == null) return;
        
        _animator.SetFloat(CanMove, canHeBeMovin ? 1f: 0f);
        if (IsPlayerInRange())
        {
            canHeBeMovin = true;
            Vector2 directionToPlayer = player.position - transform.position;
            _moveDirection = directionToPlayer.normalized;
            transform.up = directionToPlayer.normalized;

            float angle = Mathf.Atan2(directionToPlayer.y, directionToPlayer.x) * Mathf.Rad2Deg - 90f;
            transform.rotation = Quaternion.Euler(0, 0, angle);
            
            if (Time.time - _lastFireTime >= fireCooldown)
            {
                weapon.Fire(false);
                _lastFireTime = Time.time;
            }
        }
        else
        {
            canHeBeMovin = false;
        }
    }

    void FixedUpdate()
    {
        if (IsPlayerInRange())
        {
            rb.linearVelocity = _moveDirection * moveSpeed; 
        }
    }

    bool IsPlayerInRange()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, detectionRadius);
        foreach (var collider in colliders)
        {
            if (collider.transform == player)
            {
                return true;
            }
        }
        return false;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}

