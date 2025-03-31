using UnityEngine;

public class MusicManager : MonoBehaviour
{
    private static MusicManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Mantiene la musica anche nei cambi di scena
        }
        else
        {
            Destroy(gameObject); // Evita musica duplicata
        }
    }
}
