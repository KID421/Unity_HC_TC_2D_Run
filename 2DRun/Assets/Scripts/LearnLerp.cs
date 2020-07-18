using UnityEngine;

public class LearnLerp : MonoBehaviour
{
    public int A = 0;
    public int B = 10;

    public float C = 0;
    public float D = 100;

    public Vector2 v2A = new Vector2(0, 0);
    public Vector2 v2B = new Vector2(100, 100);

    public Color cA = new Color(1, 0, 0);
    public Color cB = new Color(1, 0, 1);

    private void Start()
    {
        // 結果 = 數學函式.插值(A點，B點，百分比)
        // 百分比 0 - 1
        float r = Mathf.Lerp(A, B, 0.7f);

        print(r);
    }

    private void Update()
    {
        C = Mathf.Lerp(C, D, 0.5f * Time.deltaTime);

        v2A = Vector2.Lerp(v2A, v2B, 0.5f * Time.deltaTime);

        cA = Color.Lerp(cA, cB, 0.5f * Time.deltaTime);
    }
}
