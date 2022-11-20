using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class PlayerHealthUI : MonoBehaviour
{
    public HealthManager health;
    public Image healthImage;
    // Update is called once per frame
    void Update()
    {
        healthImage.fillAmount = (float) health.healthPoints / health.maxHealthPoints;

        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
