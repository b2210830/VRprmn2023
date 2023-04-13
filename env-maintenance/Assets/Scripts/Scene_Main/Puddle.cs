using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using NsUnityVr.Systems;

public class Puddle : MaintainedWithoutGrabbing
{
	protected override void MaintenanceAction()
	{
        SEManager.Instance.PlaySE(SE.polish);
		base.MaintenanceAction();
		var rd = GetComponent<Renderer>();
		rd.enabled = false;
	}
}
