using UnityEngine;
using UnityEngine.SceneManagement;  // 引用 場景管理 API

/// <summary>
/// 場景控制：切換場景、離開遊戲
/// </summary>
public class SceneControl : MonoBehaviour
{
    /// <summary>
    /// 離開遊戲
    /// </summary>
    public void Quit()
    {
        Application.Quit();     // 應用程式.離開()
    }

    /// <summary>
    /// 載入場景
    /// </summary>
    public void LoadScene()
    {
        SceneManager.LoadScene("遊戲場景"); // 場景管理.載入場景("場景名稱")
    }

    /// <summary>
    /// 延遲載入場景
    /// </summary>
    public void DelayLoadScene()
    {
        Invoke("LoadScene", 0.7f);      // 延遲呼叫("方法名稱"，延遲時間)
    }

    /// <summary>
    /// 延遲離開遊戲
    /// </summary>
    public void DelayQuit()
    {
        Invoke("Quit", 0.7f);
    }
}
