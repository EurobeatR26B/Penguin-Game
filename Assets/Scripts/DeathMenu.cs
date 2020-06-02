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
    // Start is called before the first frame update
    void Start()
    {
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
        Debug.Log("TOGGLING");
        gameObject.SetActive(true);        
        isShown = true;
        //endScore.text = score.ToString();
    }

    public void ToMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
