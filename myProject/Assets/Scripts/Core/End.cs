using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class End : MonoBehaviour
{
    private UIManager uiManager;

    private void Awake()
    {
        uiManager = FindObjectOfType<UIManager>();
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            uiManager.End();
        }
    }
}
