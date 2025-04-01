using System.Collections;
using UnityEngine;

public class HealingZone : MonoBehaviour
{
    private float healAmount = 10f; // Cura per secondo
    private float healInterval = 1f; // Tempo tra ogni cura
    private bool playerInside = false;
    private Player player; // Riferimento al Player
    private bool isHealing = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
         // Controlla se è il Player
        if (other.CompareTag("Player") && !isHealing)
        {
            player = other.GetComponent<Player>();
            isHealing = true;
            playerInside = true;
            StartCoroutine(HealPlayer());
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("exit");
            playerInside = false; // Esce dalla zona di cura
        }
    }

    private IEnumerator HealPlayer()
    {
        while (playerInside && player != null)
        {
            player.Heal(healAmount); // Chiama la funzione Heal nel Player
            yield return new WaitForSeconds(healInterval);
        }

        isHealing = false;
    }
}
