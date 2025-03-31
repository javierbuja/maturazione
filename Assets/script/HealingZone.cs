using System.Collections;
using UnityEngine;

public class HealingZone : MonoBehaviour
{
    private float healAmount = 10f; // Cura per secondo
    private float healInterval = 1f; // Tempo tra ogni cura
    private bool playerInside = false;
    private Player player; // Riferimento al Player

    private void OnTriggerEnter2D(Collider2D other)
    {
        player = other.GetComponent<Player>(); // Controlla se è il Player
        if (player != null)
        {
            playerInside = true;
            StartCoroutine(HealPlayer());
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.GetComponent<Player>() != null)
        {
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
    }
}
