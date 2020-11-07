using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public float PlayerScore;
    public float Damage;
    public float Health;
    public float HealthCap { get { return 10 * Convert.ToSingle(Math.Pow(2, LevelStage - 1) * IsBoss); ; } }

    public float Timer;

    public int LevelStage;
    public int LevelStageMax;
    public int Kills;
    public int KillsMax;
    public int IsBoss;
    public int TimerCap;

    public Text PlayerScoreText;
    public Text DamageText;
    public Text LevelStageText;
    public Text KillsText;
    public Text HealthText;
    public Text TimerText;

    public GameObject Back;
    public GameObject Forward;

    public Image HealthBar;
    public Image TimerBar;

    private void Start()
    {
        Damage = 1.0F;
        LevelStage = 1;
        LevelStageMax = 1;
        KillsMax = 10;
        Health = 10;
        IsBoss = 1;
        TimerCap = 30;
    }

    private void Update()
    {
        PlayerScoreText.text = $"Score - {PlayerScore.ToString("F2")}";


        KillsText.text = $"Killed aliens {Kills} / {KillsMax}";
        HealthText.text = $"{Health} / {HealthCap} HP";
        DamageText.text = $"Your damage - {Damage}";
        TimerText.text = $"{Timer} / {TimerCap}";
        HealthBar.fillAmount = Health / HealthCap;

        if (LevelStage > 1) Back.gameObject.SetActive(true);
        else Back.gameObject.SetActive(false);

        if (LevelStage != LevelStageMax) Forward.gameObject.SetActive(true);
        else Forward.gameObject.SetActive(false);

        IsBossChecker();
    }

    public void Hit()
    {
        Health -= Damage;
        if (Health <= 0)
        {
            PlayerScore += Convert.ToSingle(Math.Ceiling(HealthCap / 14));
            if (LevelStage == LevelStageMax)
            {
                Kills++;
                if (Kills >= KillsMax)
                {
                    Kills = 0;
                    LevelStage++;
                    LevelStageMax++;
                }
            }
            IsBossChecker();
            Health = HealthCap;
            if (IsBoss == 10) { Timer = TimerCap; KillsMax = 1; }
            KillsMax = 10;
        }
    }

    public void IsBossChecker()
    {
        if (LevelStage % 5 == 0)
        {
            IsBoss = 10; LevelStageText.text = $"BOSS Level - {LevelStage}"; Timer = Time.deltaTime;
            if (Timer <= 0) { LevelStage--; Health = HealthCap; }
            TimerText.text = $"{Timer} / {TimerCap}";
            TimerBar.gameObject.SetActive(true);
            TimerBar.fillAmount = Timer / TimerCap;
        }
        else { IsBoss = 1; LevelStageText.text = $"Level - {LevelStage}"; TimerText.text = ""; TimerBar.gameObject.SetActive(false); }
    }
    public void ToBack()
    {
        if (LevelStage > 1) LevelStage--;
    }
    public void ToForward()
    {
        if (LevelStage != LevelStageMax) LevelStage++;
    }
}
