using UnityEngine;

public class LaserScript : MonoBehaviour
{
    public Transform startPoint;
    public Transform endPoint;

    private LineRenderer _laserLine;

    private void Start()
    {
        _laserLine = GetComponent<LineRenderer>();
        // laserLine.SetWidth(.2f, .2f);
        _laserLine.startWidth = .2f;
        _laserLine.endWidth = .2f;
    }

    private void Update()
    {
        _laserLine.SetPosition(0, startPoint.position);
        _laserLine.SetPosition(1, endPoint.position);
    }
}