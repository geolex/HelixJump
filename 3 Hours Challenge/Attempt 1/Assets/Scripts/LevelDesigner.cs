using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelDesigner : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] GameObject m_cylinder;
    [SerializeField] GameObject m_helix;


    [Header("Prefabs")]
    [SerializeField] GameObject m_trapPrefab;
    [SerializeField] GameObject m_tilePrefab;

    [SerializeField] GameObject m_playerPrefab;


    [Header("Config")]
    [SerializeField] int m_nbLevels = 20;
    [SerializeField] float m_levelHeight = 5f;

    [SerializeField][Range(0,1)] float m_entranceSpawnRate;
    [SerializeField][Range(0,1)] float m_trapSpawnRate;


    [Header("UI")]
    [SerializeField] UIManager m_uiManager;


    [Header("Gameplay")]
    [SerializeField] int m_combo = 1;
    [SerializeField] int m_score = 0;
    [SerializeField] int m_currentPlayerLevel = 0;






    // reference to the player
    Player m_player;

    private void Awake()
    {
        m_player = Instantiate(m_playerPrefab).GetComponent<Player>();
        m_player.SetLevelDesigner(this);
    }

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 0f;
        GenerateLevel(m_entranceSpawnRate, m_trapSpawnRate);

        StartCoroutine(PlayCountdown(3));
    }




    // Update is called once per frame
    void Update()
    {
        int newPlayerLevel = m_nbLevels - Mathf.CeilToInt(m_player.transform.position.y / m_levelHeight);

        m_combo += (newPlayerLevel - m_currentPlayerLevel);
        m_score += (newPlayerLevel - m_currentPlayerLevel) * m_combo;

        m_currentPlayerLevel = newPlayerLevel;

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Quit();
        }

        if(m_player == null)
        {
            Lose();
        }
    }




    void GenerateLevel(float _entranceSpawnRate, float _trapSpawnRate)
    {

#region Adjusts cylinder scale to size of level
        Vector3 cylinder_scale = m_cylinder.transform.localScale;
        cylinder_scale.y = m_nbLevels * m_levelHeight;
        m_cylinder.transform.localScale = cylinder_scale;
#endregion

#region Spawn Victory Disk

#endregion

        for (int i = 1; i < m_nbLevels; ++i)
        {
            float height = i * m_levelHeight;

            float orientation = Random.Range(0, 12) * 30;

            for (int angle = 30; angle < 360; angle += 30)
            {
                GameObject currentTile = null;

                float currentRandomValue = Random.value;

                //Debug.Log(currentRandomValue);

                if (currentRandomValue > _entranceSpawnRate + _trapSpawnRate)
                {
                    currentTile = Instantiate(m_tilePrefab);
                }
                else if (currentRandomValue < _entranceSpawnRate)             // if _entranceSpawnRate AND _trapSpawnRate are crazy high, _entranceSpawnRate will take precedence
                {

                }
                else
                {
                    currentTile = Instantiate(m_trapPrefab);
                }

                if (currentTile != null)
                {
                    currentTile.transform.parent = m_helix.transform;
                    currentTile.transform.position = new Vector3(0f, height, 0f);
                    currentTile.transform.eulerAngles = new Vector3(0f, angle + orientation, 0f);
                }
            }

            
        }

#region Spawn Player
        m_player.transform.position = new Vector3(-2f, m_levelHeight * m_nbLevels, 0f);
#endregion
    }

    public float GetLevelSize()
    {
        return m_nbLevels * m_levelHeight;
    }

    public Player GetPlayer()
    {
        return m_player;
    }

    public float GetProgression()
    {
        return  ((float)m_currentPlayerLevel) / (m_nbLevels-1);
    }

    public int GetScore()
    {
        return m_score;
    }

    public int GetCombo()
    {
        return m_combo;
    }

    public void ResetCombo()
    {
        m_combo = 1;
    }

    IEnumerator PlayCountdown(int _seconds)
    {
        yield return new WaitForSecondsRealtime(_seconds);

        Time.timeScale = 1f;
    }

    public void Lose()
    {
        if(m_uiManager == null)
        {
            Quit();
        }
        else
        {
            Time.timeScale = 0f;
            m_uiManager.DisplayRestartMenu();
        }
    }

    public void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
    }
}
