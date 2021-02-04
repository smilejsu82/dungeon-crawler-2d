using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.UI;


public class App : MonoBehaviour
{
    public Transform left;
    public Transform right;
    public Transform top;
    public Transform bottom;

    public Player player;

    // Start is called before the first frame update
    void Start()
    {
        player.Init(left.position.x, right.position.x, top.position.y, bottom.position.y);
    }
}
