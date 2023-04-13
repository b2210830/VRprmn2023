using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using NsUnityVr.Systems;

public class Shodoku : MaintainedWithoutGrabbing
{
    public bool CanActivate = true;

	protected override void Start()
	{
		base.Start();
		ShowActionTitle();
	}

	public override void ShowActionTitle()
	{
        if(!CanActivate) return;

		base.ShowActionTitle();
	}

	protected override void MaintenanceAction()
	{
        SEManager.Instance.PlaySE(SE.shodoku);
        var effect_pre = Resources.Load<GameObject>("Prefabs/Effect");
        var effect = Instantiate<GameObject>(effect_pre, transform.position, Quaternion.identity);
        // effect.transform.SetParent(transform);
		_IsMaintained = true;
		GetComponent<Collider>().enabled = false;
		HideActionTitle();
	}
}
