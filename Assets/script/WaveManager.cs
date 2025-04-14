using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement; 
using UnityEngine.UI;

public class waveManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timeText;
    [SerializeField] TextMeshProUGUI waveText;

    [Header("UI Fine Ondata")]
    [SerializeField] GameObject waveEndUI;
    [SerializeField] Button nextWaveButton;
    [SerializeField] string nextSceneName = "wave 2";

    public static waveManager Instance;

    bool waveRunning = true;
    int currentWave = 0;
    int currentWaveTime;

    private void Awake()
    {
        if (Instance == null) Instance = this;

        if (nextWaveButton != null)
            nextWaveButton.onClick.AddListener(LoadNextScene);
    }

    private void Start()
    {
        StartNewWave();
        if (waveEndUI != null) waveEndUI.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
            StartNewWave();
    }

    public bool WaveRunning() => waveRunning;

    private void StartNewWave()
    {
        StopAllCoroutines();
        timeText.color = Color.white;
        currentWave++;
        waveRunning = true;
        currentWaveTime = 30;
        waveText.text = "Wave:" + currentWave;
        StartCoroutine(WaveTimer());

        if (EnemyManagerWave2.Instance != null)
            EnemyManagerWave2.WaveManager.StartWave();

        if (waveEndUI != null) waveEndUI.SetActive(false);
    }

    IEnumerator WaveTimer()
    {
        while (waveRunning)
        {
            yield return new WaitForSeconds(1f);
            currentWaveTime--;
            timeText.text = currentWaveTime.ToString();

            if (currentWaveTime <= 0)
                WaveComplete();
        }

        yield return null;
    }

    private void WaveComplete()
    {
        StopAllCoroutines();

        if (EnemyManager.Instance != null)
            EnemyManager.Instance.DestroyAllEnemies();

        if (EnemyManagerWave2.Instance != null)
        {
            EnemyManagerWave2.Instance.DestroyAllEnemies();
            EnemyManagerWave2.WaveManager.StopWave();
        }

        waveRunning = false;
        currentWaveTime = 30;
        timeText.text = currentWaveTime.ToString();
        timeText.color = Color.red;

        if (waveEndUI != null) waveEndUI.SetActive(true);
    }

    private void LoadNextScene()
    {
        SceneManager.LoadScene(nextSceneName);
    }
}
