using UnityEngine;
using System.Collections; // Importante per le Coroutine

public class Enemy : MonoBehaviour
{
    [SerializeField] int maxHealth = 100;
    [SerializeField] float originalSpeed = 5f; // Velocità originale del nemico
    [SerializeField] float speed; // Velocità attuale del nemico

    private int currentHealth;

    Animator anim;
    Transform target; // Bersaglio da seguire

    private void Start()
    {
        currentHealth = maxHealth;
        target = GameObject.FindWithTag("Player")?.transform;
        anim = GetComponent<Animator>();

        if (target == null)
        {
            Debug.LogError("⚠️ Player non trovato! Assicurati che abbia il tag 'Player'.");
        }

        speed = originalSpeed; // Imposta la velocità iniziale
        StartCoroutine(AutoDamage()); // Inizia il danno automatico ogni secondo
    }

    private void Update()
    {
        if (target != null)
        {
            Vector3 direction = target.position - transform.position;
            direction.Normalize();

            transform.position += direction * speed * Time.deltaTime;

            // Gira il nemico verso il player
            var playerToTheRight = target.position.x > transform.position.x;
            transform.localScale = new Vector2(playerToTheRight ? 1 : -1, 1);
        }
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

    // Metodo per resettare la velocità del nemico
    public void ResetSpeed()
    {
        speed = originalSpeed; // Ripristina la velocità originale
    }
}
