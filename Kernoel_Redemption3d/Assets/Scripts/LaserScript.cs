using UnityEngine;

public class LaserScript : MonoBehaviour
{
    public Transform startPoint;
    public Transform endPoint;
    private LineRenderer laserLine;
    // Start is called before the first frame update
    private void Start()
    {
        laserLine = GetComponent<LineRenderer>();
        // laserLine.SetWidth(.2f, .2f);
        laserLine.startWidth = .2f;
        laserLine.endWidth = .2f;
    }

    // Update is called once per frame
    private void Update()
    {
        laserLine.SetPosition(0, startPoint.position);
        laserLine.SetPosition(1, endPoint.position);
    }
}
