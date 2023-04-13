using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NsUnityVr.Systems;

public class Trash : MaintainedWithoutGrabbing
{
	protected override void MaintenanceAction()
	{
        SEManager.Instance.PlaySE(SE.kira);
        var effect_pre = Resources.Load<GameObject>("Prefabs/Effect");
        var effect = Instantiate<GameObject>(effect_pre, transform.position, Quaternion.identity);

		_IsMaintained = true;
        var og = GetComponent<OVRGrabbable>();
        og.allowOffhandGrab = false; // つかめなく
        var rd = GetComponent<Renderer>();
        rd.enabled = false; // みえなく
	}

	private void OnCollisionEnter(Collision other)
    {
        if(_IsMaintained) return;

		if(other.gameObject.tag == "Can")
        {
            MaintenanceAction();
        }
    }
}
