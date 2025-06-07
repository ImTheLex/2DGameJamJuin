using System;
using UnityEngine;

namespace Player.Runtime
{
    public class LookAtTarget : MonoBehaviour
    {
        public enum LocalForwardAxis
        {
            Up,
            Forward
        }

        [Header("Orientation Settings")] 
        public LocalForwardAxis m_forwardAxis = LocalForwardAxis.Up;
        
        [SerializeField]
        private float m_initialRotationAngle;

        [Header("Rotation Settings")] 
        public float rotationSpeed = 180f; // Degrés par seconde

        [Header("Optional Settings")] public bool smoothRotation = true;
        public bool followMouse = true;

        [Header("Debug")] public Vector2 targetPosition;

        private Vector2 _currentDirection;
        private Camera mainCamera;

        #region Unity API
        
            private void Start()
            {
                // Cache la référence à la caméra pour éviter les appels répétés
                mainCamera = Camera.main;
                if (mainCamera == null)
                {
                    Debug.LogError("Aucune caméra principale trouvée! Ajoutez le tag 'MainCamera' à votre caméra.");
                }

                // Initialise la direction actuelle
                _currentDirection =  GetLocalForward();
            }

            void Update()
            {
                if (!followMouse || mainCamera == null) return;

                // Obtenir la position de la souris dans le monde
                Vector2 mouseWorldPos = GetMouseWorldPosition();
                targetPosition = mouseWorldPos; // Pour le debug

                // Calculer la direction vers la cible
                Vector2 targetDirection = (mouseWorldPos - (Vector2)transform.position).normalized;

                //Debug.Log("DIRECTION" + targetDirection); 
                if (smoothRotation)
                {
                    // Rotation progressive
                    SmoothRotateTowards(targetDirection);
                }
                else
                {
                    // Rotation instantanée
                    InstantRotateTowards(targetDirection);
                }
            }
            
        #endregion

        #region Main Methods

        
            private Vector3 GetLocalForward()
            {
                //Switch simplifié
                return m_forwardAxis switch
                {
                    LocalForwardAxis.Up => transform.up,
                    LocalForwardAxis.Forward => transform.forward,
                    _ => transform.forward
                };
            }

            private Vector2 GetMouseWorldPosition()
            {
                Vector3 mouseScreenPos = Input.mousePosition;
                
                // Convertir la position de l'écran vers le monde
                // Pour une caméra orthographique 2D, on utilise la position Z de la caméra
                mouseScreenPos.z = mainCamera.transform.position.z * -1;
                
                return mainCamera.ScreenToWorldPoint(mouseScreenPos);
            }

            private void SmoothRotateTowards(Vector2 targetDirection)
            {
                // Appliquer la rotation
                ApplyRotation(_currentDirection);
            }

            private void InstantRotateTowards(Vector2 targetDirection)
            {
                _currentDirection = targetDirection;
                ApplyRotation(_currentDirection);
            }

            private void ApplyRotation(Vector2 direction)
            {
                // Calculer l'angle en degrés
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                
                // Ajuster l'angle selon l'orientation de votre sprite
                // Si votre sprite "regarde" vers le haut par défaut, soustrayez 90°
                angle -= m_initialRotationAngle;
                
                // Appliquer la rotation
                transform.rotation = Quaternion.Euler(0, 0, angle);
            }

            // Méthodes publiques pour contrôler le comportement
            public void SetFollowMouse(bool follow)
            {
                followMouse = follow;
            }

            public void LookAtPosition(Vector2 position)
            {
                Vector2 targetDirection = (position - (Vector2)transform.position).normalized;
                
                if (smoothRotation)
                    SmoothRotateTowards(targetDirection);
                else
                    InstantRotateTowards(targetDirection);
            }
        #endregion
    }
}