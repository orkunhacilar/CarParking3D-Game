using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelYukle : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (!PlayerPrefs.HasKey("Elmas")) //Elmas diye bir string kaydedilmismi bak eger false gelirse bir alta in ve elmas degerini kaydederek basla
        {
            PlayerPrefs.SetInt("Elmas", 0);
            PlayerPrefs.SetInt("Level", 1);
        }

        SceneManager.LoadScene(PlayerPrefs.GetInt("Level"));
    }

    
}
