using UnityEngine;

public class EnemyManagerWave3 : MonoBehaviour
{
    public static EnemyManagerWave3 Instance;

    [SerializeField] GameObject enemyPrefab;
    [SerializeField] float timeBetweenSpawns = 1f;
    float currentTimeBetweenSpawns;


    Transform enemiesParent;

    private void Awake()
    {
        if (Instance == null) Instance = this;
    }

    private void Start()
    {
        enemiesParent = GameObject.Find("Enemies").transform;
    }

    private void Update()
    {
        if (!waveManager.Instance.WaveRunning()) return;

        currentTimeBetweenSpawns -= Time.deltaTime;
        if (currentTimeBetweenSpawns <= 0)
        {
            SpawnEnemy();
            currentTimeBetweenSpawns = timeBetweenSpawns;
        }
    }

    void SpawnEnemy()
    {
        GameObject e = Instantiate(enemyPrefab, RandomPosition(), Quaternion.identity);
        e.transform.SetParent(enemiesParent);
    }

    Vector2 RandomPosition()
    {
        return new Vector2(Random.Range(-35, 35), Random.Range(-25, 25));
    }

    public void DestroyAllEnemies()
    {
        foreach (Transform child in enemiesParent)
        {
            Destroy(child.gameObject);
        }
    }
}
