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
    [Header("膠囊碰撞器")]
    public CapsuleCollider2D cc2d;
    [Header("剛體")]
    public Rigidbody2D rig;

    /// <summary>
    /// 是否在地板上
    /// </summary>
    public bool isGround;
    #endregion

    #region 方法區域
    // C# 括號符號都是成對出現的：() [] {} "" ''
    // 摘要：方法的說明
    // 在方法上方添加三條 /
    // 自訂方法 - 不會執行的，需要呼叫
    // API - 功能倉庫
    // 輸出功能 print("字串")
    
    /// <summary>
    /// 移動
    /// </summary>
    private void Move()
    {
        // 如果 剛體.加速度.大小 小於 10
        if (rig.velocity.magnitude < 6)
        {
            // 剛體.添加推力(二維向量)
            rig.AddForce(new Vector2(speed, 0));
        }
    }

    /// <summary>
    /// 角色的跳躍功能：跳躍動畫、播放音效與往上跳
    /// </summary>
    private void Jump()
    {
        bool jump = Input.GetKey(KeyCode.Space);

        // 顛倒運算子 !
        // 作用：將布林值變成相反
        // !true ----- false

        ani.SetBool("跳躍開關", !isGround);

        // 搬家 Alt + 上、下
        // 格式化 Ctrl + K D

        // 如果在地板上
        if (isGround)
        {
            if (jump)
            {
                isGround = false;                       // 不在地板上
                rig.AddForce(new Vector2(0, height));   // 剛體.添加推力(二維向量)
            }
        }
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

        if (key)    // 如果 玩家 按下 左邊 Ctrl 就縮小
        {
            cc2d.offset = new Vector2(-0.2f, -1.35f);       // 位移
            cc2d.size = new Vector2(1.8f, 2.15f);           // 尺寸
        }
        // 否則 恢復
        else
        {
            cc2d.offset = new Vector2(-0.2f, -0.25f);       // 位移
            cc2d.size = new Vector2(1.8f, 4.3f);            // 尺寸
        }
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

    /// <summary>
    /// 固定更新事件：一秒固定執行 50 次 - 只要有剛體就寫在這
    /// </summary>
    private void FixedUpdate()
    {
        Jump();
        Move();
    }

    /// <summary>
    /// 碰撞事件：碰到物件開始執行一次
    /// </summary>
    /// <param name="collision">碰到物件的碰撞資訊</param>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // 如果 碰到物件 的 名稱 等於 "地板"
        if (collision.gameObject.name == "地板")
        {
            // 是否在地板上 = 是
            isGround = true;
        }
    }
    #endregion
}
