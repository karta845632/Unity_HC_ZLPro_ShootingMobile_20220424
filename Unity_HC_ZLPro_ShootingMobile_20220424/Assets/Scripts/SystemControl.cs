using Photon.Pun.UtilityScripts;
using UnityEditor.Timeline;
using UnityEngine;

//namespace 命名空間：程式區塊
namespace RKO
{   
    /// <summary>
    /// 控制系統：荒野亂鬥移動功能
    /// 虛擬搖桿控制角色移動
    /// </summary>
    public class SystemControl : MonoBehaviour
    {
        [SerializeField, Header("虛擬搖桿")]
        private Joystick joystick;
        [SerializeField, Header("移動速度"), Range(0, 300)]
        private float speed = 3.5f;
        [SerializeField, Header("角色方向圖示")]
        private Transform traDirectionIcon;
        [SerializeField, Header("角色旋轉速度"), Range(0, 100)]
        private float speedTurn = 1.5f;

        private Rigidbody rig;

        private void Awake()
        {
            rig = GetComponent<Rigidbody>();
        }

        private void Update()
        {
            // GetJoystickValue();
            UpdateDirectionIconPos();
            LookDirectionIcon();
        }

        private void FixedUpdate()
        {
            Move();
        }

        /// <summary>
        /// 取得虛擬搖桿值
        /// </summary>
        private void GetJoystickValue()
        {
            print("<color=yellow>水平：" + joystick.Horizontal + "</color>");
        }

        /// <summary>
        /// 移動功能
        /// </summary>
        private void Move()
        {
            //剛體.加速度 = 三維向量(Ｘ．Ｙ．Ｚ)
            rig.velocity = new Vector3(-joystick.Horizontal, 0, -joystick.Vertical ) * speed;
        }
        /// <summary>
        /// 更新角色方向圖示的座標
        /// </summary>
        private void UpdateDirectionIconPos()
        {
            //新座標  = 角色的座標 + 三維向量(虛擬搖桿的水平與垂直) * 方向圖示的範圍)
            Vector3 pos =transform.position + new Vector3(-joystick.Horizontal, 0.5f, -joystick.Vertical) * 2.5f;
            //更新方向圖示的座標 = 新座標
            traDirectionIcon.position = pos;
        }

        /// <summary>
        /// 面向方向圖示
        /// </summary>
        private void LookDirectionIcon()
        {
            //取得面向角度 = 四位元.面向角度(方向圖示 - 角色) - 方向圖示與角色的向量
            Quaternion look = Quaternion.LookRotation(traDirectionIcon.position - transform.position);
            //角色的角度 = 四位元.插植(角色的角度，面向角度，旋轉角度 x 一幀的時間)
            transform.rotation = Quaternion.Lerp(transform.rotation, look, speedTurn * Time.deltaTime);
            //角色的歐拉角度 = 三維向量(0，原本的歐拉角度Y，0)
            transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
        
        }
    }
}

