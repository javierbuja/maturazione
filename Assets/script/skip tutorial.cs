using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public void GoToWave1()
    {
        Debug.Log("OHI OHI OHI");
        SceneManager.LoadScene("wave 1"); 
    }
}
