using System;
using System.Collections.Generic;
using Tools;
using UnityEngine;

public class PhantomBehaviour : MonoBehaviour
{

    public enum PhantomType { Easy, Medium, Hard }
    
    public int m_health;
    public PhantomConfig m_phantomConfig;
    public Transform m_player;
    private Rigidbody2D _phantomRb;
    private Vector2 _movement;
    public PhantomType m_type;
    
    public List<PhantomBehaviour> m_livingPhantoms;
    
    

    public ScoreBehaviour m_scoreBehaviour;

    
    [Header("Movement Settings")]
    public float m_speed = 1f;
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
    

    public void Configure(PhantomType type)
    {
        m_type = type;
        switch (type)
        {
            case PhantomType.Easy:
                m_speed = 2f;
                m_health = 50;
                break;
            case PhantomType.Medium:
                m_speed = 3.5f;
                m_health = 100;
                break;
            case PhantomType.Hard:
                m_speed = 5f;
                m_health = 200;
                break;
        }
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
            m_scoreBehaviour.m_scoreConfig.AddScore(m_phantomConfig.m_scoreValueOnDeath);

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

    private void OnDisable()
    {
        OnPhantomDeath(this);
    }

    public void OnPhantomDeath(PhantomBehaviour pb)
    {
        if (m_livingPhantoms.Contains(pb))
        {
            m_livingPhantoms.Remove(pb);
        }
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
           gameObject.SetActive(false);
        }
    }
}
