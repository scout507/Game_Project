using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class VillageUpgradeUiHolderSidescroll : MonoBehaviour
{
    public GameObject level;
    public GameObject ressource1Image;
    public GameObject ressource1Text;
    public GameObject ressource2Image;
    public GameObject ressource2Text;
    public GameObject ressource3Image;
    public GameObject ressource3Text;

    [HideInInspector] public TextMeshProUGUI levelTextMesh;
    [HideInInspector] public Image ressource1ImageFormat;
    [HideInInspector] public TextMeshProUGUI ressource1TextMesh;
    [HideInInspector] public Image ressource2ImageFormat;
    [HideInInspector] public TextMeshProUGUI ressource2TextMesh;
    [HideInInspector] public Image ressource3ImageFormat;
    [HideInInspector] public TextMeshProUGUI ressource3TextMesh;

    void Start()
    {
        levelTextMesh = level.GetComponent<TextMeshProUGUI>();

        ressource1ImageFormat = ressource1Image.GetComponent<Image>();
        ressource1TextMesh = ressource1Text.GetComponent<TextMeshProUGUI>();

        ressource2ImageFormat = ressource2Image.GetComponent<Image>();
        ressource2TextMesh = ressource2Text.GetComponent<TextMeshProUGUI>();

        ressource3ImageFormat = ressource3Image.GetComponent<Image>();
        ressource3TextMesh = ressource3Text.GetComponent<TextMeshProUGUI>();
    }
}
