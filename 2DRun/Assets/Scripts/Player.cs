using UnityEngine;
using UnityEngine.UI;

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

    [Header("速度"), Tooltip("角色的移動速度"), Range(1, 1500)]
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
    public AudioClip soundCoin;
    [Header("角色是否死亡"), Tooltip("True 代表死亡，False 代表尚未死亡")]
    public bool dead;
    [Header("動畫控制器")]
    public Animator ani;
    [Header("膠囊碰撞器")]
    public CapsuleCollider2D cc2d;
    [Header("剛體")]
    public Rigidbody2D rig;
    [Header("金幣文字")]
    public Text textCoin;
    [Header("血條")]
    public Image imgHp;
    [Header("音效來源")]
    public AudioSource aud;
    [Header("結束畫面")]
    public GameObject final;
    [Header("標題")]
    public Text textTitle;
    [Header("本次的金幣數量")]
    public Text textCurrent;

    /// <summary>
    /// 最大血量
    /// </summary>
    private float hpMax;

    /// <summary>
    /// 是否在地板上
    /// </summary>
    private bool isGround;
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
        bool jump = Input.GetKeyDown(KeyCode.Space);

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
                aud.PlayOneShot(soundJump);
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

        // 如果 按下 左邊 Ctrl 播放一次音效
        if (Input.GetKeyDown(KeyCode.LeftControl)) aud.PlayOneShot(soundSlide);

        if (key)    // 如果 玩家 按下 左邊 Ctrl 就縮小
        {
            cc2d.offset = new Vector2(-0.2f, -1f);       // 位移
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
        hp -= 20;                           // 血量遞減 30
        imgHp.fillAmount = hp / hpMax;      // 血條.填滿長度 = 血量 / 血量最大值
        aud.PlayOneShot(soundHit, 5);

        if (hp <= 0) Dead();                // 如果 血量 <= 0 死亡
    }

    /// <summary>
    /// 吃金幣：金幣數量增加、更新介面、金幣音效
    /// </summary>
    /// (參數) 語法：參數類型 參數名稱
    private void EatCoin(Collider2D collision)
    {
        coin++;                             // 金幣數量遞增 1
        Destroy(collision.gameObject);      // 刪除(碰到物件.遊戲物件)
        textCoin.text = "金幣：" + coin;     // 文字介面.文字 = "金幣：" + 金幣數量
        aud.PlayOneShot(soundCoin);
    }

    /// <summary>
    /// 死亡：動畫、遊戲結束
    /// </summary>
    private void Dead()
    {
        if (dead) return;                       // 如果 死亡 就 跳出

        speed = 0;
        dead = true;
        ani.SetTrigger("死亡觸發");             // 死亡觸發
        final.SetActive(true);                  // 結束畫面.啟動設定(是)
        textTitle.text = "恭喜你死掉了~";
        textCurrent.text = "本次的金幣數量：" + coin;
    }

    /// <summary>
    /// 過關
    /// </summary>
    private void Pass()
    {
        final.SetActive(true);
        textTitle.text = "恭喜你獲勝了~";
        textCurrent.text = "本次的金幣數量：" + coin;
        speed = 0;
        rig.velocity = Vector3.zero;
    }
    #endregion

    #region 事件區域
    // 開始 Start
    // 播放遊戲時執行一次
    // 初始化：
    private void Start()
    {
        hpMax = hp;     // 血量最大值 = 血量
    }

    // 更新 Update
    // 播放遊戲後一秒執行約 60 次 - 60FPS
    // 移動、監聽玩家鍵盤、滑鼠與觸控
    private void Update()
    {
        if (dead) return;

        Slide();
        Jump();

        if (transform.position.y <= -6) Dead(); // 如果 Y 軸 <= -6 死亡
    }

    /// <summary>
    /// 固定更新事件：一秒固定執行 50 次 - 只要有剛體就寫在這
    /// </summary>
    private void FixedUpdate()
    {
        if (dead) return;

        Move();
    }

    /// <summary>
    /// 碰撞事件：碰到物件開始執行一次
    /// 碰到有碰撞器的物件執行
    /// 沒有勾選 Is Trigger
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

        // 如果 碰到物件 的 名稱 等於 "懸空地板"
        if (collision.gameObject.name == "懸空地板")
        {
            // 是否在地板上 = 是
            isGround = true;
        }
    }

    /// <summary>
    /// 觸發事件：碰到勾選 Is Trigger 的物件執行一次
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 如果 碰到物件.標籤 == "金幣" 呼叫吃金幣方法(金幣碰撞)
        if (collision.tag == "金幣") EatCoin(collision);

        if (collision.tag == "障礙物") Hit();

        if (collision.name == "傳送門") Pass();
    }
    #endregion
}
