using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    public Transform bar;

    void Start()
    {
        SetSize(1f);
    }

    // Update is called once per frame
    void Update()
    {
        // 在這裡可以添加需要在每一幀更新的邏輯
    }

    public void SetSize(float size)
    {
        bar.localScale = new Vector2(size, 1f);
    }
}