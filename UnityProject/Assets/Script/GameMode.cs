using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMode : MonoBehaviour
{
    [SerializeField] private GameObject Hero;
    [SerializeField] private GameObject Enemy;
    [SerializeField] private GameObject Boss;
    [SerializeField] private GameObject UIPlay;
    [SerializeField] private GameObject UIWin;
    [SerializeField] private GameObject UILose;
    [SerializeField] public GameObject BossBar;

    public bool IsInputEnable;

    private GameObject[] SpawnEnemy;
    private GameObject SpawnBoss;
    private GameObject SpawnPlayer;
    private bool IsPlaying;

    void Start()
    {
        IsInputEnable = true;

        IsPlaying = true;

        // Spawn enemys,player and boss
        SpawnEnemy = GameObject.FindGameObjectsWithTag("SpawnEnemy");
        SpawnBoss = GameObject.FindGameObjectWithTag("SpawnBoss");
        SpawnPlayer = GameObject.FindGameObjectWithTag("SpawnPlayer");

        Instantiate(Hero, SpawnPlayer.transform.position, Quaternion.identity);

        Instantiate(Boss, SpawnBoss.transform.position, Quaternion.identity);

        foreach (GameObject i in SpawnEnemy)
        {
            Instantiate(Enemy, i.transform.position, Quaternion.identity);
        }
    }

    void Update()
    {
        // End game if lose or win
        if (IsPlaying)
        {
            PlayerActions Player = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<PlayerActions>();

            Enemy Boss = GameObject.FindGameObjectWithTag("Boss").GetComponentInChildren<Enemy>();

            if (Player.IsDead) Lose();

            if (Boss.IsDead) Win();
        }
    }

    // Show or hide boss life bar when boos detected
    public void ShowBossBar ()
    {
        BossBar.SetActive(true);
    }

    public void HideBossBar()
    {
        BossBar.SetActive(false);
    }

    // Win and lose + quit and restart functions
    void Win ()
    {
        UIWin.SetActive(true);
        IsPlaying = false;
        IsInputEnable = false;
        HideBossBar();
    }

    void Lose ()
    {
        UILose.SetActive(true);
        IsPlaying = false;
        IsInputEnable = false;
    }

    public void Quit()
    {
        Application.Quit();
        Debug.LogError("Quit");
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
