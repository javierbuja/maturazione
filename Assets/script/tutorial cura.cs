using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tutorialcura : MonoBehaviour
{
    [SerializeField] private GameObject staccion;
    private Player player;
    private bool hasEntered;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    private void Update()
    {
        if (hasEntered)
        {
            if (player.GetCurrentHealth() == 100 && staccion != null) //controlla la vita del player 
            {
                Destroy(staccion); //distrugge la staccionata 
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            hasEntered = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            hasEntered = false;
        }
    }
}
