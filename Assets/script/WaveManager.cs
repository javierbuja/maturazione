using System.Collections;
using TMPro;
using UnityEngine;

public class waveManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timeText;
    [SerializeField] TextMeshProUGUI waveText;

    public static waveManager Instance;

    bool waveRunning = true;
    int currentWave = 0;
    int currentWaveTime;

    private void Awake()
    {
        if (Instance == null) Instance = this;
    }

    private void Start()
    {
        StartNewWave();
    }

    private void Update()
    {
        // testing
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

        // Start internal wave on EnemyManagerWave2 (if it exists)
        if (EnemyManagerWave2.Instance != null)
        {
            EnemyManagerWave2.WaveManager.StartWave();
        }
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

        // Destroy enemies in both managers if they exist
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
    }
}
