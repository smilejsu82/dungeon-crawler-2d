using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterMove : MonoBehaviour
{
    public enum eDirState
    {
        Front, Back, Right, Left
    }

    public enum eBehaviorState
    {
        Move, Attack
    }

    private Transform target;
    public float speed = 3;
    [SerializeField] private eDirState dirState;
    [SerializeField] private eBehaviorState behaviorState;
    private Vector3 dir;
    private Coroutine routine;
    private System.Action onMoveComplete;
    private System.Action onNotFoundTarget;
    private Animator anim;

    public void Init(Transform target)
    {
        this.anim = this.GetComponent<Animator>();
        this.target = target;
        this.dirState = eDirState.Front;
        this.behaviorState = eBehaviorState.Move;

        this.onMoveComplete = () =>
        {
            if (this.routine != null)
            {
                StopCoroutine(this.routine);
            }
            this.routine = StartCoroutine(AttackRoutine());
        };

        this.onNotFoundTarget = () =>
        {
            if (this.routine != null)
            {
                StopCoroutine(this.routine);
            }
            this.routine = StartCoroutine(MoveRoutine());
        };

        this.routine = StartCoroutine(this.MoveRoutine());
    }

    private IEnumerator AttackRoutine()
    {
        while (true)
        {
            var distance = Vector3.Distance(this.target.position, this.transform.position);
            if (distance > 1)
            {
                break;
            }
            yield return null;
        }

        Debug.Log("not found target");

        yield return new WaitForSeconds(0.5f);

        this.onNotFoundTarget();
    }

    private IEnumerator MoveRoutine()
    {
        while (true)
        {
            var distance = Vector3.Distance(this.target.position, this.transform.position);
            if (distance < 1)
            {
                break;
            }

            dir = (this.target.position - this.transform.position).normalized;

            //소수점 첫째자리 버림 
            var dirx = System.Math.Truncate(dir.x * 10) / 10;

            if (dirx == 0)
            {
                if (dir.y > 0)
                {
                    this.dirState = eDirState.Back;
                    this.anim.Play("Back_Run");
                }
                else if (dir.y < 0)
                {
                    this.dirState = eDirState.Front;
                    this.anim.Play("Front_Run");
                }
            }
            else
            {
                if (dirx > 0)
                {
                    this.dirState = eDirState.Right;
                    this.anim.Play("Right_Run");
                }
                else if (dirx < 0)
                {
                    this.dirState = eDirState.Left;
                    this.anim.Play("Left_Run");
                }
            }

            this.transform.Translate(dir * this.speed * Time.deltaTime);
            yield return null;
        }

        Debug.Log("move complete");

        this.onMoveComplete();
    }
}
