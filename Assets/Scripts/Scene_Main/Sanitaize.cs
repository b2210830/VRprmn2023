using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using NsUnityVr.Systems;

public class Sanitaize : MaintainedWithoutGrabbing
{
    public bool CanActivate = false;

	protected override void Start()
	{
		base.Start();
	}

	public override void ShowActionTitle()
	{
        if(!CanActivate) return;

		base.ShowActionTitle();
	}

	protected override void MaintenanceAction()
	{
        SEManager.Instance.PlaySE(SE.polish);
		base.MaintenanceAction();
	}
}
