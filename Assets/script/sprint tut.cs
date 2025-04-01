using UnityEngine;
using System.Collections;

public class DestroyStaccion : MonoBehaviour
{
    [SerializeField] private Transform player; // Riferimento al player
    private bool playerInside = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInside = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInside = false;
        }
    }

    private void Update()
    {
        if (playerInside && Input.GetKeyDown(KeyCode.LeftShift))
        {
            StartCoroutine(DestroyStaccionAfterDelay(2f)); // Avvia il timer per distruggere "staccion"
        }
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
