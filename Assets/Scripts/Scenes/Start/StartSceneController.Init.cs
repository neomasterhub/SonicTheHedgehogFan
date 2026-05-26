using UnityEngine;
using UnityEngine.SceneManagement;
using static SharedConsts.ConvertValues;

/// <summary>
/// Init.
/// </summary>
public partial class StartSceneController
{
  private void Awake()
  {
    InitializeEngine();
  }

  private void InitializeEngine()
  {
    QualitySettings.vSyncCount = 0;
    Application.targetFrameRate = FramePerSec;
  }

  private void Start()
  {
    SceneManager.LoadScene("Test");
  }
}
