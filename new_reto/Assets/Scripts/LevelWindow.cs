using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class LevelWindow : MonoBehaviour
{
    private TMP_Text levelText;
    private TMP_Text ExpText;
    private Image experienceBarImage;

    private void Awake()
    {
        levelText = transform.Find("levelText").GetComponent<TMP_Text>();
        ExpText = transform.Find("ExpText").GetComponent<TMP_Text>();
        experienceBarImage = transform.Find("BarExp").Find("Exp").GetComponent<Image>();
    }

    private void Update()
    {

    }

    private void Start()
    {
        //Set values of Level Window
        SetLevelNumber(LevelSystem.Instance.GetLevelNumber());
        SetExperienceBarSize(LevelSystem.Instance.GetExperienceNormalized());

        //Subscribe to change events
        LevelSystem.Instance.OnExperienceChanged += LevelSystem_OnExperienceChanged;
        LevelSystem.Instance.OnLevelChanged += LevelSystem_OnLevelChanged;
    }

    private void SetExperienceBarSize(float experienceNormalized)
    {
        experienceBarImage.fillAmount = experienceNormalized;
        ExpText.text = LevelSystem.Instance.experience.ToString() + " / " + LevelSystem.Instance.experienceToNextLevel.ToString();
    }

    private void SetLevelNumber(int levelNumber)
    {
        levelText.text = "Nivel " + (levelNumber);
    }

    private void LevelSystem_OnExperienceChanged(object sender, System.EventArgs e)
    {
        //Actualiza experiencia, cambia tamaño de barra de exp
        SetExperienceBarSize(LevelSystem.Instance.GetExperienceNormalized());
    }

    private void LevelSystem_OnLevelChanged(object sender, System.EventArgs e)
    {
        // Actualiza nivel, cambia texto
        SetLevelNumber(LevelSystem.Instance.GetLevelNumber());
    }
}