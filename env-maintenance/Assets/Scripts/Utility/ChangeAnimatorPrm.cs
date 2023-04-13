
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeAnimatorPrm : StateMachineBehaviour
{
    [SerializeField]
    private string prmName;
    [SerializeField]
    private bool value=false;

    //このスクリプトを割り当てたステートに入ったらboolをfalseにすることにより、Boolをキャンセル可能なTrigger型として使う
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool(prmName, value);
    }
}
