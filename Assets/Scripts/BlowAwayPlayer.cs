using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlowAwayPlayer : MonoBehaviour
{
    public CMF.SidescrollerController _sc;
    public CMF.Mover _mover;
    [SerializeField]Rigidbody2D rb;
    [SerializeField] private Collider2D col;
    public GameObject _camara;
    public float rotationSpeed = 10;
    bool fliying;
    public bool asalvo;
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.O))
        {
            //FlyPlayer();
        }
        if(fliying)
            rb.rotation += Time.deltaTime * rotationSpeed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.CompareTag("WindProtection"))
        {
            asalvo = true;
        }
        if (collision.transform.CompareTag("FallZone"))
        {
            BumpPlayer(); 
        }
        if (collision.transform.CompareTag("VictoryZone"))
        {
            GameManager.Instance.Winner = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("WindProtection"))
        {
            asalvo = false;
        }
    }

    public void BumpPlayer()
    {
        _camara.transform.parent = null;
        _sc.enabled = false;
        _mover.enabled = false;
        col.enabled = false;
        rb.AddForce(new Vector2(Random.Range(-1,1), 1) * 1000);
        fliying = true;
        Invoke(nameof(CallGameOver), 1);
    }

    public void FlyPlayer()
    {
        _camara.transform.parent = null;
        _sc.enabled = false;
        _mover.enabled = false;
        col.enabled = false;
        rb.AddForce(new Vector2(-1,1) * 700);
        fliying = true;
        Invoke(nameof(CallGameOver), 1);
    }

    void CallGameOver()
    {
        GameManager.Instance.GameOver = true;
    }
}
