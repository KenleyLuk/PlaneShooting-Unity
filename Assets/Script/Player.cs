using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public GameObject damageEffect;
    public GameObject explosion;
    public PlayerHealthBarScript playerHealthBar;
    public CoinCount coinCountScript;
    public GameController GameController;
    public float moveSpeed;
    public float maxVelocity;
    Rigidbody2D rb;

    public FixedJoystick fixedJoystick;

    public AudioSource audioSource;
    public AudioClip damageSound;
    public AudioClip explosionSound;
    public AudioClip coinSound;

    public float health = 20f;
    float barFillAmount = 1f;
    float damage = 0;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        damage = barFillAmount / health;
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = fixedJoystick.input.x;
        float vertical = fixedJoystick.input.y;

        Vector2 movement = new Vector2(horizontal, vertical);

        // 根據搖桿輸入值移動角色
        rb.velocity = movement * moveSpeed;

        // 如果需要限制最大速度，可以使用以下代碼
        rb.velocity = Vector2.ClampMagnitude(rb.velocity, maxVelocity);
    }

    // void FixedUpdate()
    // {
    //     // float horizontal = fixedJoystick.input.x;
    //     // float vertical = fixedJoystick.input.y;

    //     // bool isMoving = false;
    //     // Vector3 targetPosition = transform.position; // 初始化目标位置为当前位置

    //     // if (Input.GetMouseButtonDown(0))
    //     // {
    //     //     targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    //     //     targetPosition.z = transform.position.z; // 保持 z 轴位置与玩家一致

    //     //     // 计算当前位置到目标位置的距离
    //     //     float distanceToTarget = targetPosition.x - transform.position.x;

    //     //     if (Mathf.Abs(distanceToTarget) > 0.1f)
    //     //     {
    //     //         isMoving = true;
    //     //     }
    //     // }

    //     // if (isMoving)
    //     // {
    //     //     // 移动角色向目标位置
    //     //     float distanceToTarget = targetPosition.x - transform.position.x;
    //     //     float targetVelocity = Mathf.Sign(distanceToTarget) * moveSpeed;
    //     //     float newVelocity = Mathf.Clamp(targetVelocity, -maxVelocity, maxVelocity);

    //     //     rb.velocity = new Vector2(newVelocity, rb.velocity.y);

    //     //     // 如果非常接近目标位置，停止移动
    //     //     if (Mathf.Abs(distanceToTarget) < 0.1f)
    //     //     {
    //     //         isMoving = false;
    //     //         rb.velocity = Vector2.zero;
    //     //     }
    //     // }

    //     // if (Input.GetMouseButtonUp(0))
    //     // {
    //     //     isMoving = false;
    //     //     rb.velocity = Vector2.zero;
    //     // }

    //     float horizontal = fixedJoystick.input.x;
    //     float vertical = fixedJoystick.input.y;

    //     bool isMoving = false;
    //     Vector3 targetPosition = transform.position; // 初始化目标位置为当前位置

    //     // 检查玩家是否按下了鼠标或触控屏幕
    //     if (Input.GetMouseButtonDown(0))
    //     {
    //         targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    //         targetPosition.z = transform.position.z; // 保持 z 轴位置与玩家一致

    //         // 计算当前位置到目标位置的距离
    //         float distanceToTarget = targetPosition.x - transform.position.x;

    //         if (Mathf.Abs(distanceToTarget) > 0.1f)
    //         {
    //             isMoving = true;
    //         }
    //     }

    //     if (isMoving)
    //     {
    //         // 移动角色向目标位置
    //         float distanceToTarget = targetPosition.x - transform.position.x;
    //         float targetVelocity = Mathf.Sign(distanceToTarget) * moveSpeed;
    //         float newVelocity = Mathf.Clamp(targetVelocity, -maxVelocity, maxVelocity);

    //         rb.velocity = new Vector2(newVelocity, rb.velocity.y);

    //         // 如果非常接近目标位置，停止移动
    //         if (Mathf.Abs(distanceToTarget) < 0.1f)
    //         {
    //             isMoving = false;
    //             rb.velocity = Vector2.zero;
    //         }
    //     }

    //     if (Input.GetMouseButtonUp(0))
    //     {
    //         isMoving = false;
    //         rb.velocity = Vector2.zero;
    //     }
    // }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("1This is a debug message." + collision.gameObject.tag.ToString());
        if(collision.gameObject.tag == "Block")
        {
            Debug.Log("2This is a debug message.");
            SceneManager.LoadScene("Menu");
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag=="EnemyBullet")
        {
            audioSource.PlayOneShot(damageSound, 0.5f);
            DamagePlayerHealthbar();
            Destroy(collision.gameObject);
            GameObject damageVfx = Instantiate(damageEffect, collision.transform.position, Quaternion.identity);
            Destroy(damageVfx, 0.05f);
            if(health<=0)
            {
                AudioSource.PlayClipAtPoint(explosionSound, Camera.main.transform.position, 0.5f);
                audioSource.PlayOneShot(explosionSound, 0.5f);
                GameController.GameOver();
                Destroy(gameObject);
                GameObject blast = Instantiate(explosion, transform.position, Quaternion.identity);
                Destroy(blast, 2f);
            }
        }
        if(collision.gameObject.tag=="Coin")
        {
            audioSource.PlayOneShot(coinSound, 0.5f);
            Destroy(collision.gameObject);
            coinCountScript.AddCount();
        }
    } 
    void DamagePlayerHealthbar()
    {
        if (health>0)
        {
            health -= 1;
            barFillAmount = barFillAmount - damage;
            playerHealthBar.SetAmount(barFillAmount);
        }
    }
}
