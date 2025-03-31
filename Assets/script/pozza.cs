using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedZone : MonoBehaviour
{
    private List<Enemy> enemiesInside = new List<Enemy>(); // Lista dei nemici dentro il collider
    private float destroyTimer = 5f; // Timer per distruggere l'oggetto dopo 5 secondi

    private void Start()
    {
        StartCoroutine(DestroyAfterTime()); // Avvia il timer per autodistruggersi
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Enemy enemy = other.GetComponent<Enemy>();
        if (enemy != null && !enemiesInside.Contains(enemy))
        {
            enemy.SetSpeed(1.3f); // Rallenta il nemico
            enemiesInside.Add(enemy);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        Enemy enemy = other.GetComponent<Enemy>();
        if (enemy != null && enemiesInside.Contains(enemy))
        {
            enemy.ResetSpeed(); // Ripristina la velocità originale
            enemiesInside.Remove(enemy);
        }
    }

    private IEnumerator DestroyAfterTime()
    {
        yield return new WaitForSeconds(destroyTimer); // Aspetta 5 secondi
        Destroy(gameObject); // Distrugge l'oggetto
    }
}
