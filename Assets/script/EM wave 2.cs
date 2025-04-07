using UnityEngine;

public class EnemyManagerWave2 : MonoBehaviour
{
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] float timeBetweenSpawns = 0.5f;
    float currentTimeBetweenSpawns;

    Transform enemiesParent;

    public static EnemyManagerWave2 Instance;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;

        if (WaveManager == null)
            WaveManager = new WaveSystem();
    }

    private void Start()
    {
        enemiesParent = GameObject.Find("Enemies").transform;
    }

    private void Update()
    {
        if (!WaveManager.WaveRunning()) return;

        currentTimeBetweenSpawns -= Time.deltaTime;
        if (currentTimeBetweenSpawns <= 0)
        {
            SpawnEnemy();
            currentTimeBetweenSpawns = timeBetweenSpawns;
        }
    }

    Vector2 RandomPosition()
    {
        return new Vector2(Random.Range(-30, 30), Random.Range(-20, 20));
    }

    void SpawnEnemy()
    {
        var e = Instantiate(enemyPrefab, RandomPosition(), Quaternion.identity);
        e.transform.SetParent(enemiesParent);
    }

    public void DestroyAllEnemies()
    {
        foreach (Transform e in enemiesParent)
            Destroy(e.gameObject);
    }

    // === Internal Wave Manager System ===
    public static WaveSystem WaveManager;

    public class WaveSystem
    {
        private bool waveRunning = false;

        public void StartWave()
        {
            waveRunning = true;
        }

        public void StopWave()
        {
            waveRunning = false;
        }

        public bool WaveRunning()
        {
            return waveRunning;
        }
    }
}
