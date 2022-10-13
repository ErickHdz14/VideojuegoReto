using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SkillTree : MonoBehaviour
{
    public static SkillTree skillTree;
    void Awake()
    {
        if (skillTree == null)
        {
            skillTree = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public int[] SkillLevels;
    public int[] SkillCaps;
    public string[] SkillNames;
    public string[] SkillDescriptions;

    public List<Skill> SkillList;
    public GameObject SkillHolder;

    public List<GameObject> ConnectorList;
    public GameObject ConnectorHolder;

    public int SkillPoint;
    public int CurrentSkill;

    public TMP_Text TitleText;
    public TMP_Text DescriptionText;
    public TMP_Text[] Puntos;
    public Image SkillImage;

    private void Start()
    {
        CurrentSkill = 0;
        SkillPoint = 20;
        SkillLevels = new int[6];

        //Cost of each skill
        SkillCaps = new[] { 5, 5, 3, 5, 5, 2 };

        SkillNames = new[] { "Monedas", "Experiencia", "Monedas", "Experiencia", "Monedas", "Experiencia", };
        SkillDescriptions = new[]
        {
            "Duplica tus monedas actuales",
            "Recibe 200 puntos de experiencia",
            "Recibe 5000 monedas instantaneamente",
            "Recibe 300 puntos de experiencia",
            "Duplica tus monedas por segundo que obtienes actualmente",
            "Recibe 100 puntos de experiencia",
        };

        foreach (var skill in SkillHolder.GetComponentsInChildren<Skill>()) SkillList.Add(skill);

        foreach (var connector in ConnectorHolder.GetComponentsInChildren<RectTransform>()) ConnectorList.Add(connector.gameObject);

        for (var i = 0; i < SkillList.Count; i++) SkillList[i].id = i;

        SkillList[0].ConnectedSkills = new[] {1 , 2};
        SkillList[1].ConnectedSkills = new[] { 3 };
        SkillList[2].ConnectedSkills = new[] { 4 };
        SkillList[3].ConnectedSkills = new[] { 5 };

        UpdateAllSkillUI();
    }

    public void UpdateAllSkillUI()
    {
        TitleText.text = $"{SkillNames[CurrentSkill]}";
        DescriptionText.text = $"Costo:  {SkillCaps[CurrentSkill]} Puntos\n{SkillDescriptions[CurrentSkill]}";
        foreach (var punto in Puntos)  punto.text = $"Puntos:  {SkillPoint}";
        foreach (var skill in SkillList) skill.UpdateUI();
    }

    public void AddSkillPoints(int Points)
    {
        SkillPoint += Points;
        UpdateAllSkillUI();
    }

    public void Buy()
    {
        if (SkillPoint < 1 || SkillLevels[CurrentSkill] >= SkillCaps[CurrentSkill] || SkillPoint < SkillCaps[CurrentSkill]) return;
        SkillPoint -= SkillCaps[CurrentSkill];
        SkillLevels[CurrentSkill] += SkillCaps[CurrentSkill];
        UpdateAllSkillUI();
        CheckAbility(CurrentSkill);
    }

    public void CheckAbility(int ability)
    {
        switch (ability)
        {
            case 0:
                //Duplicar monedas
                Game.Instance.AddCoins(Game.Instance.Coins);
                break;
            case 1:
                //Agregar experiencia
                LevelSystem.Instance.AddExperience(200);
                break;
            case 2:
                //Agregar 5000 Monedas 
                Game.Instance.AddCoins(5000);
                break;
            case 3:
                //Agregar experiencia
                LevelSystem.Instance.AddExperience(300);
                break;
            case 4:
                //Duplicar monedas por segundo
                Game.Instance.AddCoinsPerSecond(Game.Instance.CoinsPerSecond);
                break;
            case 5:
                //Agregar experiencia
                LevelSystem.Instance.AddExperience(100);
                break;
            default:
                break;
        }
    }

}
