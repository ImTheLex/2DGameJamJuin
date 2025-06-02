using System.Collections;
using UnityEngine;

namespace Player.Runtime
{
    public class PlayerFov : MonoBehaviour
    {
        public float radius;
        [Range(0, 360)] public float angle;

        public GameObject playerRef;

        public LayerMask targetMask;
        public LayerMask obstructionMask;

        public bool canSeePlayer;

        private void Start()
        {
            playerRef = GameObject.FindGameObjectWithTag("Player");
            StartCoroutine(FOVRoutine());
        }

        private IEnumerator FOVRoutine()
        {
            WaitForSeconds wait = new WaitForSeconds(0.2f);

            while (true)
            {
                yield return wait;
                FieldOfViewCheck();
            }
        }

        private void FieldOfViewCheck()
        {
            //Collider2D[] rangeChecks = Physics2D.OverlapCircleAll(transform.position, radius, targetMask);

            /*if (rangeChecks.Length != 0)
            {*/

                Vector3 mousePos = Input.mousePosition;
                Vector2 mouseWorldPos = Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, 0f));

                // Optional: Zero out the Z-coordinate for 2D
                /*
                mouseWorldPos.z = 0f;
                */
                
                Debug.Log(mouseWorldPos);
                Vector2 directionToTarget = (mouseWorldPos - (Vector2)transform.position);

                Vector2 facing = GetFacingDirection();
                float angleToTarget = Vector2.Angle(facing, directionToTarget);

                if (angleToTarget < angle / 2)
                {
                    float distanceToTarget = Vector2.Distance(transform.position, mouseWorldPos);

                    // Utilisation de Physics2D.Raycast
                    RaycastHit2D hit = Physics2D.Raycast(transform.position, directionToTarget, distanceToTarget, obstructionMask);
                    
                    if (hit.collider == null)
                        canSeePlayer = true;
                    else
                        canSeePlayer = false;
                }
                else
                    canSeePlayer = false;
            /*}
            else if (canSeePlayer)
                canSeePlayer = false;*/
        }

        // Méthode pour obtenir la direction vers laquelle l'objet fait face en 2D
        private Vector2 GetFacingDirection()
        {
            // Option 1: Basé sur la rotation Z (le plus commun en 2D)
            float angle = transform.eulerAngles.y * Mathf.Deg2Rad;
            return new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
            
            // Option 2: Si votre objet utilise transform.right comme direction
            // return transform.right;
            
            // Option 3: Si votre objet utilise transform.up comme direction
            // return transform.up;
        }
    }
}
