using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI healthText;
    public Slider CooldownSlider;

    Animator anim;
    Rigidbody2D rb;

    [SerializeField] float moveSpeed = 6;
    [SerializeField] float sprintSpeed = 10;
    [SerializeField] float sprintCooldown = 2f;
    [SerializeField] float sprintRechargeRate = 0.5f;

    [SerializeField] GameObject laghettoPrefab;
    [SerializeField] float spawnInterval = 0.2f;
    private float spawnTimer = 0f;

    int maxHealth = 100;
    int currentHealth;

    bool dead = false;
    bool canSprint = true;
    float cooldownTimer = 0f;

    float moveHorizontal, moveVertical;
    Vector2 movement;

    int facingDirection = 1;

    [SerializeField] private AudioClip sprintSound;
    private AudioSource audioSource;

    public int GetCurrentHealth()
    {
        return currentHealth;
    }

    private void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();

        currentHealth = maxHealth;
        healthText.text = maxHealth.ToString();

        cooldownTimer = sprintCooldown; 
    }

    private void Update()
    {
        if (dead)
        {
            movement = Vector2.zero;
            anim.SetFloat("velocity", 0);
            return;
        }

        moveHorizontal = Input.GetAxisRaw("Horizontal");
        moveVertical = Input.GetAxisRaw("Vertical");

        movement = new Vector2(moveHorizontal, moveVertical).normalized;
        anim.SetFloat("velocity", movement.magnitude);

        if (movement.x != 0)
            facingDirection = movement.x > 0 ? 1 : -1;

        transform.localScale = new Vector2(facingDirection, 1);

        bool isHoldingShift = Input.GetKey(KeyCode.LeftShift);

        if (Input.GetKeyDown(KeyCode.LeftShift) && cooldownTimer >= sprintCooldown)
        {
            Hit(10);
            if (sprintSound != null && audioSource != null)
                audioSource.PlayOneShot(sprintSound);

            canSprint = true;
        }

        if (isHoldingShift && canSprint)
        {
            spawnTimer += Time.deltaTime;

            if (spawnTimer >= spawnInterval)
            {
                Instantiate(laghettoPrefab, transform.position, Quaternion.identity);
                spawnTimer = 0f;
            }

            cooldownTimer -= Time.deltaTime;
            if (cooldownTimer <= 0f)
            {
                cooldownTimer = 0f;
                canSprint = false;
            }
        }
        else
        {
            spawnTimer = 0f;

            if (cooldownTimer < sprintCooldown)
            {
                cooldownTimer += Time.deltaTime * sprintRechargeRate;

                if (cooldownTimer >= sprintCooldown)
                {
                    cooldownTimer = sprintCooldown;
                    canSprint = true;
                }
            }
        }

        CooldownSlider.value = cooldownTimer / sprintCooldown;
    }

    private void FixedUpdate()
    {
        float currentSpeed = (Input.GetKey(KeyCode.LeftShift) && canSprint) ? sprintSpeed : moveSpeed;
        rb.velocity = movement * currentSpeed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Enemy enemy = collision.gameObject.GetComponent<Enemy>();
        if (enemy != null)
            Hit(20);
    }

    public void Hit(int damage)
    {
        anim.SetTrigger("hit");
        currentHealth -= damage;
        healthText.text = Mathf.Clamp(currentHealth, 0, maxHealth).ToString();

        if (currentHealth <= 0)
            Die();
    }

    void Die()
    {
        dead = true;
        GameManager.instance.GameOver();
    }

    public void Heal(float amount)
    {
        if (dead) return;

        currentHealth = Mathf.Min(currentHealth + (int)amount, maxHealth);
        healthText.text = currentHealth.ToString();
    }
}
