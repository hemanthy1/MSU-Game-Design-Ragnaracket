using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour
{

    //public GameObject HowToPlay1;
    //public GameObject HowToPlay2;
    //public GameObject HowToPlay3;
    //public GameObject HowToPlay4;
    public GameObject ControlsPage;
    public GameObject CreditsPage;
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

    //public void PageChangeNext(string pageName)
    //{
    //    //Checks which button was pressed on a page, denoted by pageName
    //    //Depending on which button was pressed, will make different pages appear/disappear
    //    if (pageName == "HowToPlay1")
    //    {

    //        HowToPlay1.SetActive(false);
    //        HowToPlay2.SetActive(true);
    //    }

    //    else if (pageName == "HowToPlay2")
    //    {

    //        HowToPlay2.SetActive(false);
    //        HowToPlay3.SetActive(true);
    //    }

    //    else if (pageName == "HowToPlay3")
    //    {

    //        HowToPlay3.SetActive(false);
    //        HowToPlay4.SetActive(true);
    //    }

    //    else if (pageName == "HowToPlay4")
    //    {

    //        HowToPlay4.SetActive(false);
    //        HowToPlay1.SetActive(true);
    //    }

    //}

    //public void PageChangePrevious(string pageName)
    //{
    //    //Checks which button was pressed on a page, denoted by pageName
    //    //Depending on which button was pressed, will make different pages appear/disappear
    //    if (pageName == "HowToPlay1")
    //    {

    //        HowToPlay1.SetActive(false);
    //        HowToPlay4.SetActive(true);
    //    }

    //    else if (pageName == "HowToPlay2")
    //    {

    //        HowToPlay2.SetActive(false);
    //        HowToPlay1.SetActive(true);
    //    }

    //    else if (pageName == "HowToPlay3")
    //    {

    //        HowToPlay3.SetActive(false);
    //        HowToPlay2.SetActive(true);
    //    }

    //    else if (pageName == "HowToPlay4")
    //    {

    //        HowToPlay4.SetActive(false);
    //        HowToPlay3.SetActive(true);
    //    }
    //}

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
        Debug.Log("Load main game");
    }

    public void QuitGame()
    {
        Debug.Log("Game Quit");
        Application.Quit();
    }

}
