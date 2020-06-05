using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class DeathMenu : MonoBehaviour
{
    Text endScore;
    public Image deathBackground;
    private bool isShown = false;
    private float transition;

    private int lifeStore;
    // Start is called before the first frame update
    void Start()
    {
        lifeStore = GodScript.Lives;
        transition = 0f;
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (!isShown) return;

        transition += Time.deltaTime;
        deathBackground.color = Color.Lerp(new Color(0, 0, 0, 0), Color.black, transition);
    }

    public void ToggleEndMenu(int score)
    {
        gameObject.SetActive(true);        
        isShown = true;
        
        //endScore.text = score.ToString();
    }

    public void ToMenu()
    {
        GodScript.TotalScore += GodScript.CurrentScore;
        GodScript.CurrentScore = 0;
        //GodScript.SaveData();
        SceneManager.LoadScene("Menu");
    }

    public void Restart()
    {
        GodScript.Lives = lifeStore;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
