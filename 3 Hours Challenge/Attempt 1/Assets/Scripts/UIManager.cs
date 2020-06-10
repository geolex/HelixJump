using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] Text m_scoreText;
    [SerializeField] Text m_comboText;
    [SerializeField] Slider m_progressionSlider;

    [SerializeField] GameObject m_restartMenu;

    [Header("Game Elements")]
    [SerializeField] LevelDesigner m_levelDesigner;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        // Update Progression bar
        m_progressionSlider.value = m_levelDesigner.GetProgression();

        // Update Combo
        m_comboText.text = "X" + m_levelDesigner.GetCombo();


        // Update Score
        m_scoreText.text = "Score : " + m_levelDesigner.GetScore();
    }

    public void DisplayRestartMenu()
    {
        m_restartMenu.SetActive(true);
    }

    public void onRestart_Click() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
