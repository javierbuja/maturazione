using UnityEngine;

public class DestroyAfterTime : MonoBehaviour
{
    [SerializeField] private float destroyTime = 3f; // Tempo prima della distruzione

    private void Start()
    {
        Destroy(gameObject, destroyTime); // Distrugge l'oggetto dopo destroyTime secondi
    }
}