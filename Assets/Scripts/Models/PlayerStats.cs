using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public enum PlayerState
    {
        DetectiveMode,
        GunMode,
    }
    public int playerHealthCapacity = 10;
    [HideInInspector] public int totalplayerHealthCapacity = 10;
    [HideInInspector] public int attackDamage;
    [HideInInspector] public int noOfbullets;
    [HideInInspector] public int totalAvailableBullets;
    [HideInInspector] public Vector2 playerLocation;
}
