using System;
using System.Collections.Generic;
using Interface;
using Spine.Unity;
using Tools;
using UnityEngine;

public class PhantomBehaviour : MonoBehaviour, IHasHealth
{
    
    public enum PhantomType { Easy, Medium, Hard, Boss}
    
    public float m_health;
    public float m_maxHealth;
    public float m_scoreValueOnDeath;
    public int m_phantomDamage;
    
    public int m_currentWave;
    public PhantomConfig m_phantomConfig;
    public Transform m_player;
    private Rigidbody2D _phantomRb;
    private Vector2 _movement;
    public PhantomType m_type;
    
    private SpriteRenderer _spriteRenderer;
    
    [Header("Phantom Sprites")]
    private SkeletonMecanim _skeletonAnimation;

    
    private void ChangeSkin(string skinName)
    {
         _skeletonAnimation.initialSkinName = skinName;
        _skeletonAnimation.Initialize(true);
    }
    
    public Sprite m_phantomSprite;
    public Sprite m_mediumPhantomSprite;
    public Sprite m_hardPhantomSprite;
    public Sprite m_bossPhantomSprite;
    
    [Header("Movement Type")]
    private MovementType movementType = MovementType.Velocity;
    
    [Header("Optional Settings")]
    public float stopDistance = 0.5f; // Distance à laquelle s'arrêter
    public float maxSpeed = 5f; 
    
    [Header("Debug")]
    public List<PhantomBehaviour> m_livingPhantoms;
    public ScoreBehaviour m_scoreBehaviour;
    public WaveConfig m_waveConfig;
    private float m_speed;
    
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
        //_spriteRenderer = GetComponent<SpriteRenderer>();
        _skeletonAnimation = GetComponent<SkeletonMecanim>();
    }

    void Start()
    {
        //m_health = m_phantomConfig.m_health;
    }
    

    public void Configure(PhantomType type)
    {
        //Debug.Log($"[CONFIGURE] Phantom Type: {type}, WaveConfig Null? {m_waveConfig == null}");
        m_type = type;
        switch (type)
        {
            case PhantomType.Easy:
                m_speed = (m_phantomConfig.m_basicSpeed * m_phantomConfig.m_basicSpeedModifier) + (m_waveConfig.m_basicSpeedIncrement * m_currentWave);
                m_health = m_phantomConfig.m_basicHealth + (m_waveConfig.m_basicHealthIncrement * m_currentWave);
                m_scoreValueOnDeath = m_phantomConfig.m_basicScoreValueOnDeath;
                m_phantomDamage = m_phantomConfig.m_basicDamage;
                //_spriteRenderer.sprite = m_phantomSprite;
                ChangeSkin("ghost1");
                break;
            case PhantomType.Medium:
                m_speed = (m_phantomConfig.m_mediumSpeed * m_phantomConfig.m_mediumSpeedModifier) + (m_waveConfig.m_mediumSpeedIncrement * m_currentWave);
                m_health = m_phantomConfig.m_mediumHealth + (m_waveConfig.m_mediumHealthIncrement * m_currentWave);
                m_scoreValueOnDeath = m_phantomConfig.m_mediumScoreValueOnDeath;
                m_phantomDamage = m_phantomConfig.m_mediumPhantomDamage;
                //_spriteRenderer.sprite = m_mediumPhantomSprite;
                ChangeSkin("ghost2");

                break;
            case PhantomType.Hard:
                m_speed = m_phantomConfig.m_hardSpeed * m_phantomConfig.m_hardSpeedModifier + (m_waveConfig.m_hardSpeedIncrement * m_currentWave);
                m_health = m_phantomConfig.m_hardHealth  + (m_waveConfig.m_hardHealthIncrement * m_currentWave);
                m_scoreValueOnDeath = m_phantomConfig.m_hardScoreValueOnDeath;
                m_phantomDamage = m_phantomConfig.m_hardPhantomDamage;
                //_spriteRenderer.sprite = m_hardPhantomSprite;
                ChangeSkin("ghost3");

                break;
            case PhantomType.Boss:
                m_speed = m_phantomConfig.m_bossSpeed * m_phantomConfig.m_bossSpeedModifier + (m_waveConfig.m_bossSpeedIncrement * m_currentWave);
                m_health = m_phantomConfig.m_bossHealth + (m_waveConfig.m_bossHealthIncrement * m_currentWave);
                m_scoreValueOnDeath = m_phantomConfig.m_bossScoreValueOnDeath;
                m_phantomDamage = m_phantomConfig.m_bossPhantomDamage;
                //_spriteRenderer.sprite = m_bossPhantomSprite;
                ChangeSkin("ghost4");

                break;
        }
        m_maxHealth = m_health;
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

    public void TakeDamage(float damage)
    {
        m_health -= damage;
        if (m_health <= 0)
        {
            gameObject.SetActive(false);
            m_scoreBehaviour.m_scoreConfig.AddScore(m_scoreValueOnDeath);

        }
    }

    private void MoveWithVelocity(Vector2 direction)
    {
        // Contrôle direct de la vélocité
        float speed = m_speed;
        _phantomRb.linearVelocity = direction * speed;
    }

    private void MoveWithTransform(Vector2 direction)
    {
        // Déplacement direct (ignore la physique)
        float speed = m_speed;
        transform.Translate(direction * speed * Time.deltaTime);
    }

    private void MoveWithAddForceImproved(Vector2 direction)
    {
        // AddForce avec limitation de vitesse maximale
        float force = m_speed;
        
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

    public MonoBehaviour GetGameObjectWithHealth()
    {
        return this;
    }

    public float CurrentHealth => m_health;
    public float MaxHealth => m_maxHealth;
}
