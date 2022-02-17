using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private UnityEngine.UI.Text m_PointsText = null;
    [SerializeField] private CubeSpawner m_CubeSpawnerScript = null;

    private static GameManager Instance;
    static public GameManager Get() { return Instance; }

    [HideInInspector] public int m_CubesCount = 0;
    private int m_GainedPoints = 0;

    private void Awake()
    {
        UnityEngine.Assertions.Assert.IsNotNull(m_PointsText, "PointsText is null");
        UnityEngine.Assertions.Assert.IsNotNull(m_CubeSpawnerScript, "CubeSpawnerScript is null");

        Instance = this;
        DontDestroyOnLoad(Instance.gameObject);

        HideCursor();
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void OnDestroy()
    {
        Instance = null;
    }

    private void OnApplicationFocus(bool focus)
    {
        if (focus)
            HideCursor();
    }

    public void ResetGameState()
    {
        m_GainedPoints = 0;
        m_CubesCount = 0;

        SetPointsCountText();
    }

    public void IncrementPoints()
    {
        ++m_GainedPoints;
        SetPointsCountText();
    }

    public void SpawnCube()
    {
        m_CubeSpawnerScript.SpawnCube();
    }

    private void SetPointsCountText()
    {
        m_PointsText.text = m_GainedPoints.ToString();
    }

    private void HideCursor()
    {
        Cursor.visible = false;
    }
}
