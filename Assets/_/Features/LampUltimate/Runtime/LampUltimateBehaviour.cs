using System;
using Tools;
using UnityEngine;

public class LampUltimateBehaviour : MonoBehaviour
{
    public float rotationSpeed = 360f;
    public float ultimateDuration = 1f;

    public bool isUsingUltimate;
    private float timeElapsed;

    [SerializeField]
    private MonoBehaviour _lookAtTarget;
    [SerializeField]
    private LampConfig _lampConfig;
    [SerializeField]
    private ScoreConfig _scoreConfig;
    [SerializeField]
    private SpecialMoveDetector _specialMoveDetector;

    private float _scoreTresholdForUltimate;
    private float baseDamage;
    private float baseRange;

    private bool isReadyToUltimate;
    
    private int rotationDirection = 1;
    
    
    public void TriggerUltimate(float startAngle, int directionSign)
    {
        _lookAtTarget.enabled = false;

        baseDamage = _lampConfig.m_damage;
        baseRange = _lampConfig.m_lenght;

        _lampConfig.m_damage += _lampConfig.m_ultimateDamageBonus;
        _lampConfig.m_lenght += _lampConfig.m_ultimateRangeBonus;

        isUsingUltimate = true;
        timeElapsed = 0f;

        // Positionne la lampe dans la direction du dernier angle
        transform.rotation = Quaternion.Euler(0, 0, startAngle);

        // Applique la direction
        rotationDirection = directionSign; // 1 ou -1
        isReadyToUltimate = false;
    }


    private void Awake()
    {
        _scoreTresholdForUltimate = _scoreConfig.m_scoreTresholdForUltimate;
        _specialMoveDetector.OnSpecialMoveDetected += CheckIfCanUlt;

    }

    private void Update()
    {
        if (_scoreConfig.m_scoreValue >= _scoreTresholdForUltimate)
        {
            _scoreTresholdForUltimate += _scoreConfig.m_scoreTresholdForUltimate;
            isReadyToUltimate = true;
        }
        if (!isUsingUltimate) return;

        // Effectue la rotation automatique
        transform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime * rotationDirection);
        _lampConfig.m_lenght += _lampConfig.m_ultimateRangeBonus;
        _lampConfig.m_damage += _lampConfig.m_ultimateDamageBonus;
        timeElapsed += Time.deltaTime;

        if (timeElapsed >= ultimateDuration)
        {
            // Restaure les valeurs
            _lampConfig.m_damage = baseDamage;
            _lampConfig.m_lenght = baseRange;

            _lookAtTarget.enabled = true;
            isUsingUltimate = false;
        }
    }

    private void CheckIfCanUlt()
    {
        if (isReadyToUltimate == true)
        {
            float angle = _specialMoveDetector.LastDetectedAngle;
            int dir = _specialMoveDetector.LastDetectedDirection;
            TriggerUltimate(angle, dir);
            isReadyToUltimate = false;
            //isUsingUltimate = true;
            //_lookAtTarget.enabled = false;
        }
    }
}

