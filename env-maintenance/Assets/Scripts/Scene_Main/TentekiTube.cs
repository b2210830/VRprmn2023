using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TentekiTube : MonoBehaviour
{
    [SerializeField] Transform _tenteki;
    [SerializeField] Animator _patient;
    private LineRenderer _line;

    private int middlePoints = 10;
    public Vector3 controlPoint = new Vector3(0, -2, 0);
    public bool considerDistance = false;
    public float distanceEffect = 0.2f;

    // Start is called before the first frame update
    void Start()
    {
        _line = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        DrawTubeLine();
    }

    private void DrawTubeLine()
    {
        var pos1 = _tenteki.position;
        var pos2 = _patient.GetBoneTransform(HumanBodyBones.LeftHand).position;

        var de = 1.0f;
        if (considerDistance) {
            de = (pos1 - pos2).magnitude * distanceEffect;
        }
        var control = (pos1 + pos2) / 2 + controlPoint * de;

        var totalPoints = middlePoints + 2;
        _line.positionCount = totalPoints;

        _line.SetPosition(0, pos1);
        for (int i = 1; i <= middlePoints; i++)
        {
            var t = (float)i / (float)(totalPoints - 1);
            var mpos = SampleCurve(pos1, pos2, control, t);
            _line.SetPosition(i, mpos);
        }
        _line.SetPosition(totalPoints-1, pos2);
    }

    Vector3 SampleCurve(Vector3 start, Vector3 end, Vector3 control, float t)
    {
        // Interpolate along line S0: control - start;
        Vector3 Q0 = Vector3.Lerp(start, control, t);
        // Interpolate along line S1: S1 = end - control;
        Vector3 Q1 = Vector3.Lerp(control, end, t);
        // Interpolate along line S2: Q1 - Q0
        Vector3 Q2 = Vector3.Lerp(Q0, Q1, t);
        return Q2; // Q2 is a point on the curve at time t
    }
}
