using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelEndUI : MonoBehaviour
{

    public UnityEngine.UI.Text timeText;
    public UnityEngine.UI.Text missText;
    public UnityEngine.UI.Text perfectHitText;
    public EnemyHealth enemyHealth;
    public LossPlane lossPlane;
    public PlayerHit playerHit;
    public Canvas levelUI;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OpenPage(GameObject page)
    {
        page.SetActive(true);
    }


    public void ClosePage(GameObject page)
    {
        page.SetActive(false);
    }

    public void GoToMain()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void GoToGame()
    {
        SceneManager.LoadScene("AdamScene2");
    }


    public void DisplayLevelUI()
    {
        int minutes = (int)enemyHealth.timeAlive / 60;
        int seconds = (int)enemyHealth.timeAlive % 60;
        timeText.text = "Time Taken: " + minutes.ToString() + ":" + seconds.ToString();
        missText.text = "Number of Missed Shuttlecocks: " + lossPlane.shuttleMisses.ToString();
        perfectHitText.text = "Number of Perfect Hits: " + playerHit.perfectHits.ToString();
        OpenPage(levelUI.gameObject);
    }
}
