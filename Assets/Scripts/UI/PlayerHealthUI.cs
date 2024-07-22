using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthUI : MonoBehaviour
{
    PlayerStats playerStats;
    HealthSystem healthSystem;
    public Image[] heartImages = new Image[10];
    private Image heartImage;

    void Start()
    {
        playerStats = GameObject.Find("Player").GetComponent<PlayerStats>();
        healthSystem = new HealthSystem(playerStats.playerHealthCapacity);

        for (int i = 0; i < heartImages.Length; i++)
        {
            heartImage = GameObject.Find($"Hearts ({i})").GetComponent<Image>();
            heartImages[i] = heartImage;
        }
    }
    // Update is called once per frame
    void Update()
    {
        UpdateHealthUI();
    }

    void UpdateHealthUI()
    {
        for (int i = 0; i < heartImages.Length; i++)
        {
            if (i < healthSystem.GetHealth)
            {
                heartImages[i].color = Color.green;
            }
            else
            {
                heartImages[i].color = Color.red;
            }

            if (i < playerStats.playerHealthCapacity)
            {
                heartImages[i].enabled = true;
            }
            else
            {
                heartImages[i].enabled = false;
            }
        }
    }
}
