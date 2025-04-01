using System.Collections;
using UnityEngine;

public class BaitZone : MonoBehaviour
{
    [SerializeField] private GameObject escaPrefab; // L'oggetto che attira i nemici
    [SerializeField] private float baitDuration = 5f; // Tempo di attrazione
    private bool canActivate = false;
    private Player player;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // Se il player entra nel trigger
        {
            player = other.GetComponent<Player>();
            canActivate = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            canActivate = false;
        }
    }

    private void Update()
    {
        if (canActivate && Input.GetKeyDown(KeyCode.E)) // Premendo "E"
        {
            if (player != null)
            {
                player.Hit(20); // Il player perde 20 di vita
                StartCoroutine(ActivateBait());
            }
        }
    }

    private IEnumerator ActivateBait()
    {
        GameObject bait = Instantiate(escaPrefab, transform.position, Quaternion.identity);
        bait.SetActive(true);
        yield return new WaitForSeconds(baitDuration);
        Destroy(bait); // Rimuove l'esca dopo 5 secondi
    }
}
