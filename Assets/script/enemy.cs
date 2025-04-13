using UnityEngine;
using System.Collections; // Importante per le Coroutine

public class Enemy : MonoBehaviour
{
    [SerializeField] int maxHealth = 100;
    [SerializeField] float originalSpeed = 5f; // Velocità originale del nemico
    [SerializeField] float boostedSpeed = 20f; // Velocità aumentata nel trigger
    [SerializeField] float speed; // Velocità attuale del nemico

    [Header("Stats")]
    [Header("Charger")]
    [SerializeField] bool isCharger;
    [SerializeField] float distanceToCharge = 5f;
    [SerializeField] float chargeSpeed = 12f;
    [SerializeField] float prepareTime;

    bool isCharging = false;
    bool isPreparingCharge = false;

    private int currentHealth;
    Animator anim;
    Transform target; // Bersaglio da seguire
    private Transform bait; // Riferimento all'oggetto esca
    private bool isAttractedToBait = false; // Flag per sapere se sta seguendo l'esca

    private void Start()
    {
        currentHealth = maxHealth;
        target = GameObject.FindWithTag("Player")?.transform;
        anim = GetComponent<Animator>();
        bait = GameObject.FindWithTag("Bait")?.transform; // Cerca l'esca

        if (target == null)
        {
            Debug.LogError("⚠️ Player non trovato! Assicurati che abbia il tag 'Player'.");
        }

        speed = originalSpeed; // Imposta la velocità iniziale
        StartCoroutine(AutoDamage()); // Inizia il danno automatico ogni secondo
    }

    private void Update()
    {
        Transform currentTarget = isAttractedToBait && bait != null ? bait : target;

        if (isPreparingCharge) return;

        if (currentTarget != null)
        {
            Vector3 direction = currentTarget.position - transform.position;
            direction.Normalize();

            transform.position += direction * speed * Time.deltaTime;

            // Gira il nemico verso il bersaglio attuale
            var targetToTheRight = currentTarget.position.x > transform.position.x;
            transform.localScale = new Vector2(targetToTheRight ? 1 : -1, 1);

            if (isCharger && !isCharging && Vector2.Distance(transform.position, target.position) < distanceToCharge)
            {
                isPreparingCharge = true;
                Invoke("StartCharging", prepareTime);

            }
        }
    }

    void StartCharging()
    { 
        isPreparingCharge = false;
        isCharging = true;
        speed = chargeSpeed;
    }

    public void Hit(int damage)
    {
        currentHealth -= damage;
        anim.SetTrigger("Hit");

        if (currentHealth <= 0)
        {
            StopAllCoroutines(); // Ferma il danno automatico se il nemico muore
            Destroy(gameObject); // Distrugge il nemico
        }
    }

    public void SetSpeed(float newSpeed)
    {
        speed = newSpeed;
    }

    IEnumerator AutoDamage()
    {
        while (currentHealth > 0)
        {
            Hit(20); // Infligge 20 danni ogni secondo
            yield return new WaitForSeconds(1f); // Aspetta 1 secondo prima di infliggere nuovamente danno
        }
    }

    public void ResetSpeed()
    {
        speed = originalSpeed; // Ripristina la velocità originale
    }

    // Metodo per attirare il nemico verso l'esca
    public void AttractToBait(Transform baitTransform, float duration)
    {
        bait = baitTransform;
        isAttractedToBait = true;
        StartCoroutine(StopAttractingAfterTime(duration));
    }

    private IEnumerator StopAttractingAfterTime(float duration)
    {
        yield return new WaitForSeconds(duration);
        isAttractedToBait = false;
    }

    // Metodo per aumentare la velocità quando entra nel trigger
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log($"Entrato nel trigger: {other.gameObject.name}");
        if (other.CompareTag("SpeedBoostZone"))
        {
            Debug.Log("Velocità aumentata!");
            speed = boostedSpeed;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        Debug.Log($"Uscito dal trigger: {other.gameObject.name}");
        if (other.CompareTag("SpeedBoostZone"))
        {
            Debug.Log("Velocità ripristinata!");
            speed = originalSpeed;
        }
    }
}
