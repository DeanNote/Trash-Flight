using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed;

    [SerializeField]
    private GameObject[] weapons;
    private int weaponIndex = 0;

    [SerializeField]
    private Transform shootTransform;

    [SerializeField]
    private float shootInterval = 0.05f;
    private float lastShotTime = 0f;

    // Update is called once per frame
    void Update()
    {
        // float horizontalInput = Input.GetAxisRaw("Horizontal");
        // float verticalInput = Input.GetAxisRaw("Vertical");
        // Vector3 moveTo = new Vector3(horizontalInput, verticalInput, 0f);
        // transform.position += moveTo * moveSpeed * Time.deltaTime;

        // 키보드로 제어하기
        // Vector3 moveTo = new Vector3(moveSpeed * Time.deltaTime, 0, 0);
        // if(Input.GetKey(KeyCode.LeftArrow)){
        //     transform.position -= moveTo;
        // }else if(Input.GetKey(KeyCode.RightArrow)){
        //     transform.position += moveTo;
        // }

        //마우스로 제어하기
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        float toX = Mathf.Clamp(mousePos.x, -2.35f, 2.35f);// 최대 최소 계산해주는 메서드 사이값은 그냥 그대로
        transform.position = new Vector3(toX,transform.position.y,transform.position.z);

        //미사일 발사
        if(GameManager.instance.isGameOver == false){ //게임오버시 미사일 멈춤
            Shoot();
        }
    }

    void Shoot(){
        if(Time.time - lastShotTime > shootInterval){
            Instantiate(weapons[weaponIndex], shootTransform.position,Quaternion.identity);
            lastShotTime = Time.time;
        }
    }
    
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag == "Enemy" || other.gameObject.tag == "Boss"){
            GameManager.instance.SetGameOver();
            Destroy(gameObject);
            
        }else if(other.gameObject.tag == "Coin"){
            Destroy(other.gameObject);
            GameManager.instance.IncreaseCoin();
        }
    }

    public void Upgrade(){
        weaponIndex+=1;
        if(weaponIndex >= weapons.Length){
            weaponIndex = weapons.Length-1;
        }
    }
}
