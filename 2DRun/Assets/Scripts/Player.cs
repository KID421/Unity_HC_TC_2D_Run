using UnityEngine;

public class Player : MonoBehaviour
{
    #region 欄位區域
    /* 說明區域
    // 命名規則
    // 1. 用有意義的名稱
    // 2. 不要使用數字開頭
    // 3. 不要使用特殊符號包含：@#/* 空格
    // 4. 可以使用中文 (不建議)

    // 欄位語法
    // 修飾詞 類型 欄位名稱 = 值;
    // 沒有 = 值 會以預設值為主
    // 整數、浮點數 預設值 0
    // 字串 預設值 ""
    // 布林值 預設值 false

    // 私人 private - 僅限於此類別使用 | 不會顯示 - 預設值
    // 公開 public  - 所有類別皆可使用 | 會顯示

    // 欄位屬性 [屬性名稱()]
    // 標題 Header
    // 提示 Tooltip
    // 範圍 Range
    */

    [Header("速度"), Tooltip("角色的移動速度"), Range(10, 1500)]
    public int speed = 50;
    [Header("血量"), Tooltip("這是角色的血量喔~"), Range(0, 1000)]
    public float hp = 999.9f;
    [Header("金幣數量"), Tooltip("儲存角色吃了幾顆金幣")]
    public int coin;
    [Header("跳躍高度"), Range(100, 2000)]
    public int height = 500;
    [Header("音效區域")]
    public AudioClip soundJump;
    public AudioClip soundSlide;
    public AudioClip soundHit;
    [Header("角色是否死亡"), Tooltip("True 代表死亡，False 代表尚未死亡")]
    public bool dead;
    [Header("動畫控制器")]
    public Animator ani;
    #endregion

    #region 方法區域
    // C# 括號符號都是成對出現的：() [] {} "" ''
    // 摘要：方法的說明
    // 在方法上方添加三條 /
    // 自訂方法 - 不會執行的，需要呼叫
    // API - 功能倉庫
    // 輸出功能 print("字串")

    /// <summary>
    /// 角色的跳躍功能：跳躍動畫、播放音效與往上跳
    /// </summary>
    private void Jump()
    {
        print("跳躍");
    }

    /// <summary>
    /// 角色的滑行功能：滑行動畫、播放音效、縮小碰撞範圍
    /// </summary>
    private void Slide()
    {
        // 布林值 = 輸入.取得按鍵(按鍵代碼列舉.左邊 Ctrl)
        bool key = Input.GetKey(KeyCode.LeftControl);

        // 動畫控制器代號
        ani.SetBool("滑行開關", key);
    }

    /// <summary>
    /// 碰到障礙物時受傷：扣血
    /// </summary>
    private void Hit()
    {

    }

    /// <summary>
    /// 吃金幣：金幣數量增加、更新介面、金幣音效
    /// </summary>
    private void EatCoin()
    {

    }

    /// <summary>
    /// 死亡：動畫、遊戲結束
    /// </summary>
    private void Dead()
    {

    }
    #endregion

    #region 事件區域
    // 開始 Start
    // 播放遊戲時執行一次
    // 初始化：
    private void Start()
    {
        
    }

    // 更新 Update
    // 播放遊戲後一秒執行約 60 次 - 60FPS
    // 移動、監聽玩家鍵盤、滑鼠與觸控
    private void Update()
    {
        Slide();
    }
    #endregion
}
