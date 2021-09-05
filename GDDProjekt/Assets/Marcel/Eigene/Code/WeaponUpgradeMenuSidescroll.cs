using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using TMPro;

public class WeaponUpgradeMenuSidescroll : MonoBehaviour
{
    public TextMeshProUGUI[] ressourcesText;
    public Sprite[] imageList;
    public GameObject rifleMenu;
    public GameObject grenadeLauncherMenu;
    public GameObject shotgunMenu;
    public GameObject chooseGun;
    public GameObject backBttn;
    GameManager gameManager;

    public WeaponUpgradeMenuHolderSidescroll rifle;
    public WeaponUpgradeMenuHolderSidescroll grenadeLauncher;
    public WeaponUpgradeMenuHolderSidescroll shotgun;


    public LevelCostsSidescroll rifleDamageCostsSidescroll;
    public LevelCostsSidescroll rifleFireRateCostsSidescroll;
    public LevelCostsSidescroll rifleCooldownCostsSidescroll;

    public LevelCostsSidescroll grenadeLauncherDamageCostsSidescroll;
    public LevelCostsSidescroll grenadeLauncherexplosionRadiusCostsSidescroll;
    public LevelCostsSidescroll grenadeLauncherCooldownCostsSidescroll;

    public LevelCostsSidescroll shotgunDamageCostsSidescroll;
    public LevelCostsSidescroll shotgunFireRateCostsSidescroll;
    public LevelCostsSidescroll shotgunCooldownCostsSidescroll;


    public VillageUpgradeUiHolderSidescroll shield;
    public LevelCostsSidescroll shieldCostsSidescroll;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();

        rifleDamageCostsSidescroll = JsonUtility.FromJson<LevelCostsSidescroll>(File.ReadAllText(Application.dataPath + "/JSONS/rifleDamageCostsSidescroll.json"));
        rifleFireRateCostsSidescroll = JsonUtility.FromJson<LevelCostsSidescroll>(File.ReadAllText(Application.dataPath + "/JSONS/rifleFireRateCostsSidescroll.json"));
        rifleCooldownCostsSidescroll = JsonUtility.FromJson<LevelCostsSidescroll>(File.ReadAllText(Application.dataPath + "/JSONS/rifleCooldownCostsSidescroll.json"));

        grenadeLauncherDamageCostsSidescroll = JsonUtility.FromJson<LevelCostsSidescroll>(File.ReadAllText(Application.dataPath + "/JSONS/grenadeLauncherDamageCostsSidescroll.json"));
        grenadeLauncherexplosionRadiusCostsSidescroll = JsonUtility.FromJson<LevelCostsSidescroll>(File.ReadAllText(Application.dataPath + "/JSONS/grenadeLauncherexplosionRadiusCostsSidescroll.json"));
        grenadeLauncherCooldownCostsSidescroll = JsonUtility.FromJson<LevelCostsSidescroll>(File.ReadAllText(Application.dataPath + "/JSONS/grenadeLauncherCooldownCostsSidescroll.json"));

        shotgunDamageCostsSidescroll = JsonUtility.FromJson<LevelCostsSidescroll>(File.ReadAllText(Application.dataPath + "/JSONS/shotgunDamageCostsSidescroll.json"));
        shotgunFireRateCostsSidescroll = JsonUtility.FromJson<LevelCostsSidescroll>(File.ReadAllText(Application.dataPath + "/JSONS/shotgunFireRateCostsSidescroll.json"));
        shotgunCooldownCostsSidescroll = JsonUtility.FromJson<LevelCostsSidescroll>(File.ReadAllText(Application.dataPath + "/JSONS/shotgunCooldownCostsSidescroll.json"));
        shieldCostsSidescroll = JsonUtility.FromJson<LevelCostsSidescroll>(File.ReadAllText(Application.dataPath + "/JSONS/shield.json"));
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < ressourcesText.Length; i++)
        {
            ressourcesText[i].text = gameManager.resources[i].ToString();
        }

        if (rifleMenu.activeSelf)
        {
            rifle.villageUpgradeUiHolderSidescrolls[0].show(gameManager.rifle.damageLevel, rifleDamageCostsSidescroll, imageList);
            rifle.villageUpgradeUiHolderSidescrolls[1].show(gameManager.rifle.fireRateLevel, rifleFireRateCostsSidescroll, imageList);
            rifle.villageUpgradeUiHolderSidescrolls[2].show(gameManager.rifle.cooldownLevel, rifleCooldownCostsSidescroll, imageList);
        }

        if (grenadeLauncherMenu.activeSelf)
        {
            grenadeLauncher.villageUpgradeUiHolderSidescrolls[0].show(gameManager.grenadeLauncher.damageLevel, grenadeLauncherDamageCostsSidescroll, imageList);
            grenadeLauncher.villageUpgradeUiHolderSidescrolls[1].show(gameManager.grenadeLauncher.explosionRadiusLevel, grenadeLauncherexplosionRadiusCostsSidescroll, imageList);
            grenadeLauncher.villageUpgradeUiHolderSidescrolls[2].show(gameManager.grenadeLauncher.cooldownLevel, grenadeLauncherCooldownCostsSidescroll, imageList);
        }

        if (shotgunMenu.activeSelf)
        {
            shotgun.villageUpgradeUiHolderSidescrolls[0].show(gameManager.shotgun.damageLevel, shotgunDamageCostsSidescroll, imageList);
            shotgun.villageUpgradeUiHolderSidescrolls[1].show(gameManager.shotgun.fireRateLevel, shotgunFireRateCostsSidescroll, imageList);
            shotgun.villageUpgradeUiHolderSidescrolls[2].show(gameManager.shotgun.cooldownLevel, shotgunCooldownCostsSidescroll, imageList);
        }

        shield.show(gameManager.esLvl, shieldCostsSidescroll, imageList);
    }

    public void toggleRifleMenu()
    {
        rifleMenu.SetActive(!rifleMenu.activeSelf);
        chooseGun.SetActive(!chooseGun.activeSelf);
        backBttn.SetActive(!backBttn.activeSelf);
    }

    public void toggleGrenadeLauncherMenu()
    {
        grenadeLauncherMenu.SetActive(!grenadeLauncherMenu.activeSelf);
        chooseGun.SetActive(!chooseGun.activeSelf);
        backBttn.SetActive(!backBttn.activeSelf);
    }

    public void toggleShotgunMenu()
    {
        shotgunMenu.SetActive(!shotgunMenu.activeSelf);
        chooseGun.SetActive(!chooseGun.activeSelf);
        backBttn.SetActive(!backBttn.activeSelf);
    }

    //0: rifle, 1: grenadeLauncher, 2: shotgun
    public void upgradeDamage(int index)
    {
        int level = 0;
        LevelCostsSidescroll levelCostsSidescroll = new LevelCostsSidescroll();

        switch (index)
        {
            case 0:
                level = gameManager.rifle.damageLevel;
                levelCostsSidescroll = rifleDamageCostsSidescroll;
                break;
            case 1:
                level = gameManager.grenadeLauncher.damageLevel;
                levelCostsSidescroll = grenadeLauncherDamageCostsSidescroll;
                break;
            case 2:
                level = gameManager.shotgun.damageLevel;
                levelCostsSidescroll = shotgunDamageCostsSidescroll;
                break;
        }

        level++;


        if (level >= levelCostsSidescroll.level.Length) return;

        for (int i = 0; i < levelCostsSidescroll.level[level].componenten.Length; i++)
        {
            if (levelCostsSidescroll.level[level].componenten[i].amount > gameManager.resources[levelCostsSidescroll.level[level].componenten[i].id]) return;
        }

        for (int i = 0; i < levelCostsSidescroll.level[level].componenten.Length; i++)
        {
            gameManager.resources[levelCostsSidescroll.level[level].componenten[i].id] -= levelCostsSidescroll.level[level].componenten[i].amount;
        }

        switch (index)
        {
            case 0:
                gameManager.rifle.damageLevel++;
                gameManager.rifle.damage *= 1.1f;
                break;
            case 1:
                gameManager.grenadeLauncher.damageLevel++;
                gameManager.grenadeLauncher.damage *= 1.1f;
                break;
            case 2:
                gameManager.shotgun.damageLevel++;
                gameManager.shotgun.damage *= 1.1f;
                break;
        }
    }

    //0: rifle, 1: shotgun
    public void upgradeFireRate(int index)
    {
        int level = 0;
        LevelCostsSidescroll levelCostsSidescroll = new LevelCostsSidescroll();

        switch (index)
        {
            case 0:
                level = gameManager.rifle.fireRateLevel;
                levelCostsSidescroll = rifleFireRateCostsSidescroll;
                break;
            case 1:
                level = gameManager.shotgun.fireRateLevel;
                levelCostsSidescroll = shotgunFireRateCostsSidescroll;
                break;
        }

        level++;

        if (level >= levelCostsSidescroll.level.Length) return;

        for (int i = 0; i < levelCostsSidescroll.level[level].componenten.Length; i++)
        {
            if (levelCostsSidescroll.level[level].componenten[i].amount > gameManager.resources[levelCostsSidescroll.level[level].componenten[i].id]) return;
        }

        for (int i = 0; i < levelCostsSidescroll.level[level].componenten.Length; i++)
        {
            gameManager.resources[levelCostsSidescroll.level[level].componenten[i].id] -= levelCostsSidescroll.level[level].componenten[i].amount;
        }

        switch (index)
        {
            case 0:
                gameManager.rifle.fireRateLevel++;
                gameManager.rifle.fireRate *= 1.1f;
                break;
            case 1:
                gameManager.shotgun.fireRateLevel++;
                gameManager.shotgun.fireRate *= 1.1f;
                break;
        }
    }

    //0: rifle, 1: grenadeLauncher, 2: shotgun
    public void upgradeCooldown(int index)
    {
        int level = 0;
        LevelCostsSidescroll levelCostsSidescroll = new LevelCostsSidescroll();

        switch (index)
        {
            case 0:
                level = gameManager.rifle.cooldownLevel;
                levelCostsSidescroll = rifleCooldownCostsSidescroll;
                break;
            case 1:
                level = gameManager.grenadeLauncher.cooldownLevel;
                levelCostsSidescroll = grenadeLauncherCooldownCostsSidescroll;
                break;
            case 2:
                level = gameManager.shotgun.cooldownLevel;
                levelCostsSidescroll = shotgunCooldownCostsSidescroll;
                break;
        }

        level++;

        if (level >= levelCostsSidescroll.level.Length) return;

        for (int i = 0; i < levelCostsSidescroll.level[level].componenten.Length; i++)
        {
            if (levelCostsSidescroll.level[level].componenten[i].amount > gameManager.resources[levelCostsSidescroll.level[level].componenten[i].id]) return;
        }

        for (int i = 0; i < levelCostsSidescroll.level[level].componenten.Length; i++)
        {
            gameManager.resources[levelCostsSidescroll.level[level].componenten[i].id] -= levelCostsSidescroll.level[level].componenten[i].amount;
        }

        switch (index)
        {
            case 0:
                gameManager.rifle.cooldownLevel++;
                gameManager.rifle.overheatLossRate *= 1.1f;
                break;
            case 1:
                gameManager.grenadeLauncher.cooldownLevel++;
                gameManager.grenadeLauncher.overheatLossRate *= 1.1f;
                break;
            case 2:
                gameManager.shotgun.cooldownLevel++;
                gameManager.shotgun.overheatLossRate *= 1.1f;
                break;
        }
    }

    public void upgradeExplosionRadius()
    {
        LevelCostsSidescroll levelCostsSidescroll = grenadeLauncherexplosionRadiusCostsSidescroll;
        int level = gameManager.grenadeLauncher.explosionRadiusLevel;
        level++;

        if (level >= levelCostsSidescroll.level.Length) return;

        for (int i = 0; i < levelCostsSidescroll.level[level].componenten.Length; i++)
        {
            if (levelCostsSidescroll.level[level].componenten[i].amount > gameManager.resources[levelCostsSidescroll.level[level].componenten[i].id]) return;
        }

        for (int i = 0; i < levelCostsSidescroll.level[level].componenten.Length; i++)
        {
            gameManager.resources[levelCostsSidescroll.level[level].componenten[i].id] -= levelCostsSidescroll.level[level].componenten[i].amount;
        }

        gameManager.grenadeLauncher.explosionRadiusLevel++;
        gameManager.grenadeLauncher.explosionRadius *= 1.1f;
    }

    public void upgradeShield()
    {
        LevelCostsSidescroll levelCostsSidescroll = shieldCostsSidescroll;
        int level = gameManager.esLvl;
        level++;

        if (level >= levelCostsSidescroll.level.Length) return;

        for (int i = 0; i < levelCostsSidescroll.level[level].componenten.Length; i++)
        {
            if (levelCostsSidescroll.level[level].componenten[i].amount > gameManager.resources[levelCostsSidescroll.level[level].componenten[i].id]) return;
        }

        for (int i = 0; i < levelCostsSidescroll.level[level].componenten.Length; i++)
        {
            gameManager.resources[levelCostsSidescroll.level[level].componenten[i].id] -= levelCostsSidescroll.level[level].componenten[i].amount;
        }

        gameManager.esLvl++;
        gameManager.maxEs *= 1.1f;
    }
}
