using System;
using Tools;
using UnityEngine;

public class PhantomBehaviour : MonoBehaviour
{

    public int m_health;
    public PhantomConfig m_phantomConfig;
    public Transform m_player;
    private Rigidbody2D _phantomRb;
    private Vector2 _movement;

    
    [Header("Movement Settings")]
    public float m_speed = 10f;
    public float m_speedModifier = 1f;
    
    [Header("Movement Type")]
    public MovementType movementType = MovementType.AddForce;
    
    [Header("Optional Settings")]
    public float stopDistance = 0.5f; // Distance à laquelle s'arrêter
    public float maxSpeed = 5f; 
    
    public enum MovementType
    {
        AddForce,           // Physique réaliste
        Velocity,           // Contrôle direct
        Transform,          // Déplacement direct (non-physique)
        AddForceImproved    // AddForce avec limitation de vitesse
    }
    private void Awake()
    {
        _phantomRb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        m_health = m_phantomConfig.m_health;
    }

    private void OnEnable()
    {
    
    }

    
    private void Move()
    {
        
        Vector2 direction = (m_player.transform.position - transform.position);
        
        // Vérifier la distance pour éviter de "vibrer" autour du joueur
        if (direction.magnitude <= stopDistance)
        {
            // Arrêter le mouvement si trop proche
            if (movementType == MovementType.Velocity)
            {
                _phantomRb.linearVelocity = Vector2.zero;
            }
            return;
        }
        
        // Normaliser la direction
        direction = direction.normalized;
        /*var direction = m_player.transform.position - transform.position;
        Debug.Log(direction);
        _movement = new Vector2(direction.x, direction.y);
        _phantomRb.AddForce(_movement * (m_phantomConfig.m_speed * m_phantomConfig.m_speedModifier * Time.deltaTime), ForceMode2D.Force);
    */
        switch (movementType)
        {
            /*case MovementType.AddForce:
                MoveWithAddForce(direction);
                break;*/
                
            case MovementType.Velocity:
                MoveWithVelocity(direction);
                break;
                
            case MovementType.Transform:
                MoveWithTransform(direction);
                break;
                
            /*case MovementType.AddForceImproved:
                MoveWithAddForceImproved(direction);
                break;*/
        }
    }

    public void TakeDamage(int damage)
    {
        m_health -= damage;
        if (m_health <= 0)
        {
            gameObject.SetActive(false);
        }
    }
    private void MoveWithAddForce(Vector2 direction)
    {
        // SANS Time.deltaTime car AddForce gère déjà le temps
        float force = m_speed * m_speedModifier;
        _phantomRb.AddForce(direction * force, ForceMode2D.Force);
    }

    private void MoveWithVelocity(Vector2 direction)
    {
        // Contrôle direct de la vélocité
        float speed = m_speed * m_speedModifier;
        _phantomRb.linearVelocity = direction * speed;
    }

    private void MoveWithTransform(Vector2 direction)
    {
        // Déplacement direct (ignore la physique)
        float speed = m_speed * m_speedModifier;
        transform.Translate(direction * speed * Time.deltaTime);
    }

    private void MoveWithAddForceImproved(Vector2 direction)
    {
        // AddForce avec limitation de vitesse maximale
        float force = m_speed * m_speedModifier;
        
        // Limiter la vitesse maximale
        if (_phantomRb.linearVelocity.magnitude < maxSpeed)
        {
            _phantomRb.AddForce(direction * force, ForceMode2D.Force);
        }
        else
        {
            // Si trop rapide, utiliser la vélocité pour maintenir la direction
            _phantomRb.linearVelocity = _phantomRb.linearVelocity.normalized * maxSpeed;
        }
    }
    
    // Update is called once per frame
    void Update()
    {
        Move();
    }
    
}
