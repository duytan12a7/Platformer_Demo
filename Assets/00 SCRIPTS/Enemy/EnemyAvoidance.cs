using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyAvoidance : MonoBehaviour
{
    [Header("Avoidance Settings")]
    [SerializeField] private float minDistanceBetweenEnemies = 0.7f;
    [SerializeField] private float avoidanceForce = 5f;
    [SerializeField] private LayerMask enemyLayer;

    [Header("Movement Limits")]
    [SerializeField] private float maxSpeed = 5f;
    [SerializeField] private bool restrictVerticalMovement = true;

    private Rigidbody2D rb;
    private CircleCollider2D detectCollider;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        detectCollider = gameObject.AddComponent<CircleCollider2D>();
        detectCollider.radius = minDistanceBetweenEnemies;
        detectCollider.isTrigger = true;
    }

    private void FixedUpdate()
    {
        Vector2 avoidanceVector = CalculateAvoidanceVector();
        ApplyAvoidanceForce(avoidanceVector);
    }

    private Vector2 CalculateAvoidanceVector()
    {
        Vector2 totalAvoidanceVector = Vector2.zero;

        Collider2D[] nearbyEnemies = Physics2D.OverlapCircleAll(
            transform.position,
            minDistanceBetweenEnemies,
            enemyLayer
        );

        foreach (Collider2D enemyCollider in nearbyEnemies)
        {
            if (enemyCollider.gameObject == gameObject)
                continue;

            Vector2 directionToEnemy = transform.position - enemyCollider.transform.position;
            float distance = directionToEnemy.magnitude;

            if (distance < minDistanceBetweenEnemies)
            {
                float avoidanceMultiplier = 1f - (distance / minDistanceBetweenEnemies);
                totalAvoidanceVector += directionToEnemy.normalized * avoidanceMultiplier;
            }
        }

        return totalAvoidanceVector;
    }

    private void ApplyAvoidanceForce(Vector2 avoidanceVector)
    {
        if (avoidanceVector != Vector2.zero)
        {
            if (restrictVerticalMovement)
            {
                avoidanceVector.y = 0;
            }

            rb.AddForce(avoidanceVector * avoidanceForce);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, minDistanceBetweenEnemies);
    }
}