using UnityEngine;

public class Bullet : MonoBehaviour
{
    private bool _isCharacterPassable = true;
    void Start()
    {
        Destroy(gameObject, 2f);
    }

    private bool hasCollided = false;

    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        if (hasCollided) return;

        CharactersBasicBehaviour character = hitInfo.GetComponent<CharactersBasicBehaviour>();
        if (character != null)
        { 
            _isCharacterPassable = character.Hit(hitInfo, tag);
        }

        if (!_isCharacterPassable)
        {
            hasCollided = true;
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (hasCollided) return;

        CharactersBasicBehaviour character = collision.gameObject.GetComponent<CharactersBasicBehaviour>();
        if (character == null)
        {
            hasCollided = true;
            Destroy(gameObject); 
        }
    }

}
