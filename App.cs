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
        var prefab = Resources.Load<GameObject>("Boss");
        var monsterGo = Instantiate(prefab);
        var monsterMove = monsterGo.GetComponent<MonsterMove>();
        monsterMove.Init(this.player.transform);

        player.Init(left.position.x, right.position.x, top.position.y, bottom.position.y);
    }
}
