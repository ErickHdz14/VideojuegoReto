using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using static SkillTree;

public class Skill : MonoBehaviour
{

    public int id;

    public int[] ConnectedSkills;

    public void UpdateUI()
    { 
        if (skillTree.SkillLevels[id] >= skillTree.SkillCaps[id]) GetComponent<Image>().color = Color.white;  

        foreach (var connectedSkill in ConnectedSkills)
        {
            skillTree.SkillList[connectedSkill].gameObject.SetActive(skillTree.SkillLevels[id] > 0);
            skillTree.ConnectorList[connectedSkill].SetActive(skillTree.SkillLevels[id] > 0);
        }
    }



    public void Click()
    {
        skillTree.CurrentSkill = id;
        skillTree.UpdateAllSkillUI();
        skillTree.SkillImage.sprite = transform.GetChild(0).GetComponentInChildren<Image>().sprite;
    }

 
}
