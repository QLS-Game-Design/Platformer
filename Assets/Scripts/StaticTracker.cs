using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticTracker : MonoBehaviour
{
    public static float coins = 0;
    public static int level = 1;
    public static float gunDamage = 1f;
    public static float gunDamageCost = 5f;
    public static float gunDamageIncrease = 1f;
    public static float gunDamageCostIncrease = 5f;
    public static float grenadeDamage = 3f;
    public static float grenadeDamageCost = 5f;
    public static float grenadeDamageIncrease = 1f;
    public static float grenadeDamageCostIncrease = 5f;
    public static float maxHealth = 3f;
    public static float healthCost = 10f;
    public static float maxHealthIncrease = 1f;
    public static float healthCostIncrease = 10f;

    public static void reset()
    {
        coins = 0;
        level = 1;
        gunDamage = 1f;
        gunDamageCost = 5f;
        gunDamageIncrease = 1f;
        gunDamageCostIncrease = 5f;
        grenadeDamage = 3f;
        grenadeDamageCost = 5f;
        grenadeDamageIncrease = 1f;
        grenadeDamageCostIncrease = 5f;
        maxHealth = 3;
        healthCost = 10;
        maxHealthIncrease = 1f;
        healthCostIncrease = 10f;
    }

    // Update is called once per frame
    public static void UpdateCoins()
    {
        coins = GameObject.Find("Player").GetComponent<Player1>().coins;
    }
}
