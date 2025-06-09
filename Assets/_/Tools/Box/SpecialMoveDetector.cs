using System;
using UnityEngine;

namespace Tools
{
    public class SpecialMoveDetector : MonoBehaviour
    {
        public event Action OnSpecialMoveDetected;

        public Vector2 m_direction;
        public enum RotationState { None, Clockwise, CounterClockwise }
        private RotationState currentState = RotationState.None;
        
        private float _previousAngle = 0f;
        private float _accumulatedRotation = 0f;
        private float _lastDeltaAngle = 0f;
        
        public float LastDetectedAngle { get; private set; }
        public int LastDetectedDirection { get; private set; }

        public void RegisterRotation(RotationState direction)
        {
            // Logique de séquence : horloge → anti-horloge → validé
           /*if (currentState == RotationState.None && direction == RotationState.Clockwise)
            {
                currentState = RotationState.Clockwise;
            }
            else if (currentState == RotationState.Clockwise && direction == RotationState.CounterClockwise)
            {
                currentState = RotationState.None;
                OnSpecialInputSuccess?.Invoke(); // combo validé
            }
            else
            {
                currentState = RotationState.None;
            }
            */
            Debug.Log($"Current Direction: {m_direction}");

        }

        private void Update()
        {
            //RegisterRotation(RotationState.None);
            DetectFullRotation();
        }
        
        private void DetectFullRotation()
        {
            if (m_direction.sqrMagnitude < 0.001f)
                return;

            float currentAngle = Mathf.Atan2(m_direction.y, m_direction.x) * Mathf.Rad2Deg;
            if (currentAngle < 0) currentAngle += 360f;

            float deltaAngle = Mathf.DeltaAngle(_previousAngle, currentAngle);

            // Initialisation du sens si c’est le tout premier delta non nul
            if (Mathf.Abs(_lastDeltaAngle) < 0.001f)
            {
                _lastDeltaAngle = deltaAngle;
            }

            // Si le joueur change de direction, reset
            if (Mathf.Sign(deltaAngle) != Mathf.Sign(_lastDeltaAngle) && Mathf.Abs(deltaAngle) > 1f)
            {
                Debug.Log("Changement de sens détecté → reset.");
                _accumulatedRotation = 0f;
                _lastDeltaAngle = deltaAngle;
                _previousAngle = currentAngle;
                return;
            }

            _accumulatedRotation += deltaAngle;

            //Debug.Log($"Accumulated: {_accumulatedRotation}");

            if (Mathf.Abs(_accumulatedRotation) >= 360f)
            {
                LastDetectedAngle = currentAngle;
                LastDetectedDirection = Math.Sign(_accumulatedRotation); // 1 or -1
                Debug.Log("Tour complet détecté !");
                OnSpecialMoveDetected?.Invoke();
                _accumulatedRotation = 0f;
            }

            _lastDeltaAngle = deltaAngle;
            _previousAngle = currentAngle;
        }

    }

}
