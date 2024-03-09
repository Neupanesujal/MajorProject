using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
public class UImanager : MonoBehaviour
{
    public static UImanager instance;
    public GameObject playerDiedUI,levelCompleteUI,options;
    bool escapePressed = false;

    public TMP_Text _mouseX,_mouseY,health,enemyCount,calorie;

    Scene scene;
    
    int counterEnemy = -1;
    void Awake()
    {
         instance = this;
        scene = SceneManager.GetActiveScene();
    }
    void Start()
    {
        EnemyCount();
        DisableSettings();
        SetHealth(100);
    }
    
    public void playerDied()
    {
        playerDiedUI.SetActive(true);
        EnableCursor();
    }
    
    public void revivePlayer()
    {
        playerDiedUI.SetActive(false);
        DisableSettings();
    }



    public void levelComplete()
    {
        levelCompleteUI.SetActive(true);
        EnableCursor();
    }

    public void Settings()
    {
        escapePressed =  escapePressed? false :  true;
        options.SetActive(escapePressed);  
        Time.timeScale =  escapePressed ?   0 : 1; 
        if(escapePressed) EnableCursor();
        else DisableSettings();
    }

    public void DisableSettings()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        escapePressed = false;
        options.SetActive(false);
        Time.timeScale = 1;
    }

    void EnableCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    public void MainMenu()
    {
        SceneManager.LoadScene("UI");
    }

    
    public void Replay()
    {
        SceneManager.LoadScene(scene.name);
    }
    
    public void next()
    {
        SceneManager.LoadScene(scene.buildIndex + 1, LoadSceneMode.Single);
    }

    public void mouseX(float mouseX)
    {
        MouseController.instance.SetX(mouseX);
        _mouseX.text = mouseX.ToString();
    }

    public void mouseY(float mouseY)
    {
        MouseController.instance.SetY(mouseY);
        _mouseY.text = mouseY.ToString();
    }

    public void SetHealth(int Health)
    {
        health.text = "Health: " + Health.ToString();
    }

    public void EnemyCount()
    {
        counterEnemy ++;
        enemyCount.text = "Kills: " + counterEnemy.ToString();
    }


    // ADs


    public void AdsRevive()
    {
        // Ads.instance.ShowRewardedVideo();
    }
    
    public void ShowAdsError()
    {
        print("// error server didnot respond turn on wifi"); 
    }

    public void ShowAdsError(string error)
    {
        print(error);// show err 
    }

    public void SetCalorie()
    {
        calorie.text = "Calorie Burn" + calcCalo();
    }

    float elapsedTime;
    float calcCalo()
    {
        elapsedTime += Time.deltaTime;
        return 2.5f * 7.7f * 55 * 2.2f * elapsedTime / 200 / 60;
    }
}
