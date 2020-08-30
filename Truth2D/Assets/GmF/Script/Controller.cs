using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    public bool isAuto = false;
    public List<KeyCode> KeyCodes = new List<KeyCode>();
    bool useMouseRightClick = false;

    public virtual void Awake()
    {
        
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            PlayerEnterControllerCheck();
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            PlayerEnterControllerCheck();
        }
    }

    void PlayerEnterControllerCheck()
    {
        if(isAuto || CheckKeys() || (useMouseRightClick && Input.GetMouseButtonDown(0)) )
        {
            Do();
        }
    }

    bool CheckKeys()
    {
        foreach(KeyCode key in KeyCodes)
        {
            if (Input.GetKeyDown(key))
            {
                return true;
            }
        }

        return false;
    }

    public virtual void Do()
    {

    }
}
