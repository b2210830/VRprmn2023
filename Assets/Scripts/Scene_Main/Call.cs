using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Call : MaintainedWithoutGrabbing
{
    private LineRenderer _line;
    [SerializeField] Transform _call;
    [SerializeField] MaintainedObjects _mo;

    protected override void Start()
    {
        base.Start();
        _line = GetComponent<LineRenderer>();
    }

    void Update()
    {
        DrawLine();
    }

    private void DrawLine()
    {
        var pos1 = transform.position;
        var pos2 = _call.position;

        _line.SetPosition(0, pos1);
        _line.SetPosition(1, pos2);
    }

	protected override void MaintenanceAction()
	{
		_IsMaintained = true;
		GetComponent<Collider>().enabled = false;
		HideActionTitle();
        _mo.enabled = true;
	}
}
