using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private UnityEngine.UI.Text m_PointsText = null;

    private static GameManager Instance;
    static public GameManager Get() { return Instance; }

    [HideInInspector] public int m_CubesCount = 0;
    private int m_GainedPoints = 0;

    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(Instance.gameObject);   
    }

    private void OnDestroy()
    {
        Instance = null;
    }    

    public void ResetGameState()
    {
        m_GainedPoints = 0;
        m_CubesCount = 0;

        m_PointsText.text = m_GainedPoints.ToString();
    }

    public void IncrementPoints()
    {
        ++m_GainedPoints;
        m_PointsText.text = m_GainedPoints.ToString();
    }
}
