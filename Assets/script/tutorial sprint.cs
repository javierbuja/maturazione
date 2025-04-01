using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab; // Prefab del nemico da spawnare
    [SerializeField] private Transform player; // Riferimento al player
    [SerializeField] private float spawnOffset = 5f; // Distanza di spawn a sinistra del player
    private bool hasSpawned = false; // Controlla se il nemico è già stato spawnato

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !hasSpawned)
        {
            hasSpawned = true; // Segna che il nemico è stato spawnato
            SpawnEnemy();
            StartCoroutine(DestroyStaccionAfterDelay(3f)); // Avvia il timer per distruggere lo sprite
        }
    }

    private void SpawnEnemy()
    {
        if (enemyPrefab != null && player != null)
        {
            Vector3 spawnPosition = player.position + Vector3.left * spawnOffset;
            Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
        }
        else
        {
            Debug.LogError("Enemy prefab o player non assegnato nello script EnemySpawner!");
        }
    }

    private void Start()
    {
        if (player == null)
            player = GameObject.FindWithTag("Player")?.transform;

        if (enemyPrefab == null || player == null)
            Debug.LogError("❌ Enemy prefab o player non assegnato nello script EnemySpawner!");
    }

    private IEnumerator DestroyStaccionAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        GameObject staccion = GameObject.FindWithTag("staccion");
        if (staccion != null)
        {
            Destroy(staccion);
        }
    }
}
