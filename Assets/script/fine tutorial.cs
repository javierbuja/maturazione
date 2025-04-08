using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneChangerOnTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) 
        {
            StartCoroutine(ChangeSceneAfterDelay());
        }
    }

    IEnumerator ChangeSceneAfterDelay()
    {
        yield return new WaitForSeconds(0.1f); // Aspetta 1 secondo
        SceneManager.GetSceneByName("wave 1"); // Carica la scena chiamata "wave 1"
        SceneManager.LoadScene("wave 1");
    }
}
