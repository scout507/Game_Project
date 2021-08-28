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
    public GameManager gameManager;

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

    // Start is called before the first frame update
    void Start()
    {
        rifleDamageCostsSidescroll = JsonUtility.FromJson<LevelCostsSidescroll>(File.ReadAllText(Application.dataPath + "/weaponCosts/rifleDamageCostsSidescroll.json"));
        rifleFireRateCostsSidescroll = JsonUtility.FromJson<LevelCostsSidescroll>(File.ReadAllText(Application.dataPath + "/weaponCosts/rifleFireRateCostsSidescroll.json"));
        rifleCooldownCostsSidescroll = JsonUtility.FromJson<LevelCostsSidescroll>(File.ReadAllText(Application.dataPath + "/weaponCosts/rifleCooldownCostsSidescroll.json"));

        grenadeLauncherDamageCostsSidescroll = JsonUtility.FromJson<LevelCostsSidescroll>(File.ReadAllText(Application.dataPath + "/weaponCosts/grenadeLauncherDamageCostsSidescroll.json"));
        grenadeLauncherexplosionRadiusCostsSidescroll = JsonUtility.FromJson<LevelCostsSidescroll>(File.ReadAllText(Application.dataPath + "/weaponCosts/grenadeLauncherexplosionRadiusCostsSidescroll.json"));
        grenadeLauncherCooldownCostsSidescroll = JsonUtility.FromJson<LevelCostsSidescroll>(File.ReadAllText(Application.dataPath + "/weaponCosts/grenadeLauncherCooldownCostsSidescroll.json"));

        shotgunDamageCostsSidescroll = JsonUtility.FromJson<LevelCostsSidescroll>(File.ReadAllText(Application.dataPath + "/weaponCosts/shotgunDamageCostsSidescroll.json"));
        shotgunFireRateCostsSidescroll = JsonUtility.FromJson<LevelCostsSidescroll>(File.ReadAllText(Application.dataPath + "/weaponCosts/shotgunFireRateCostsSidescroll.json"));
        shotgunCooldownCostsSidescroll = JsonUtility.FromJson<LevelCostsSidescroll>(File.ReadAllText(Application.dataPath + "/weaponCosts/shotgunCooldownCostsSidescroll.json"));
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < ressourcesText.Length; i++)
        {
            ressourcesText[i].text = gameManager.resources[i].ToString();
        }

        rifle.villageUpgradeUiHolderSidescrolls[0].show(gameManager.rifle.damageLevel, rifleDamageCostsSidescroll, imageList);
        rifle.villageUpgradeUiHolderSidescrolls[1].show(gameManager.rifle.fireRateLevel, rifleFireRateCostsSidescroll, imageList);
        rifle.villageUpgradeUiHolderSidescrolls[2].show(gameManager.rifle.cooldownLevel, rifleCooldownCostsSidescroll, imageList);

        grenadeLauncher.villageUpgradeUiHolderSidescrolls[0].show(gameManager.grenadeLauncher.damageLevel, grenadeLauncherDamageCostsSidescroll, imageList);
        grenadeLauncher.villageUpgradeUiHolderSidescrolls[1].show(gameManager.grenadeLauncher.explosionRadiusLevel, grenadeLauncherexplosionRadiusCostsSidescroll, imageList);
        grenadeLauncher.villageUpgradeUiHolderSidescrolls[2].show(gameManager.grenadeLauncher.cooldownLevel, grenadeLauncherCooldownCostsSidescroll, imageList);

        shotgun.villageUpgradeUiHolderSidescrolls[0].show(gameManager.shotgun.damageLevel, shotgunDamageCostsSidescroll, imageList);
        shotgun.villageUpgradeUiHolderSidescrolls[1].show(gameManager.shotgun.fireRateLevel, shotgunFireRateCostsSidescroll, imageList);
        shotgun.villageUpgradeUiHolderSidescrolls[2].show(gameManager.shotgun.cooldownLevel, shotgunCooldownCostsSidescroll, imageList);
    }


    public void toggleRifleMenu()
    {
        rifleMenu.SetActive(!rifleMenu.activeSelf);
        chooseGun.SetActive(!chooseGun.activeSelf);
        backBttn.SetActive(!backBttn.activeSelf);
    }

    public void toggleGrenadeLauncherMenu()
    {
        rifleMenu.SetActive(!rifleMenu.activeSelf);
        chooseGun.SetActive(!chooseGun.activeSelf);
        backBttn.SetActive(!backBttn.activeSelf);
    }

    public void toggleShotgunMenu()
    {
        rifleMenu.SetActive(!rifleMenu.activeSelf);
        chooseGun.SetActive(!chooseGun.activeSelf);
        backBttn.SetActive(!backBttn.activeSelf);
    }
}
