using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Shop : MonoBehaviour
{

    private void Update()
    {
        GameObject.Find("CoinsUI").GetComponent<Text>().text = StaticTracker.coins.ToString();
        GameObject.Find("HealthUpgradeText").GetComponent<Text>().text = "Health Upgrade (" + StaticTracker.healthCost + ")";
        GameObject.Find("GunDamageUpgradeText").GetComponent<Text>().text = "Gun Damage Upgrade (" + StaticTracker.gunDamageCost + ")";
        GameObject.Find("GrenadeDamageUpgradeText").GetComponent<Text>().text = "Grenade Damage Upgrade (" + StaticTracker.grenadeDamageCost + ")";


    }

    public void clickContinue()
    {
        StaticTracker.level++;
        Debug.Log(StaticTracker.level);
        SceneManager.LoadScene(StaticTracker.level);
    }

    public void clickHealth()
    {
        if ((StaticTracker.coins >= StaticTracker.healthCost))
        {
            StaticTracker.coins -= StaticTracker.healthCost;
            StaticTracker.maxHealth += StaticTracker.maxHealthIncrease;
            StaticTracker.healthCost += StaticTracker.healthCostIncrease;

        }
    }

    public void clickGunDamage()
    {
        if ((StaticTracker.coins >= StaticTracker.gunDamageCost))
        {
            StaticTracker.coins -= StaticTracker.gunDamageCost;
            StaticTracker.gunDamage += StaticTracker.gunDamageIncrease;
            StaticTracker.gunDamageCost += StaticTracker.gunDamageCostIncrease;

        }
    }

    public void clickGrenadeDamage()
    {
        if ((StaticTracker.coins >= StaticTracker.grenadeDamageCost))
        {
            StaticTracker.coins -= StaticTracker.grenadeDamageCost;
            StaticTracker.grenadeDamage += StaticTracker.grenadeDamageIncrease;
            StaticTracker.grenadeDamageCost += StaticTracker.grenadeDamageCostIncrease;

        }
    }

}
