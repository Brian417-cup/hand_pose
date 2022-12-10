using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetLine : MonoBehaviour
{
    private LineRenderer line;
    public float width = 0.3f;
    public Material lineMaterial;
    public Transform origin, destination;

    // Start is called before the first frame update
    void Start()
    {
        line = this.GetComponent<LineRenderer>();
        line.material = lineMaterial;
        line.startWidth = width;
        line.endWidth = width;
    }

    // Update is called once per frame
    void Update()
    {
        line.SetPosition(0, origin.localPosition);
        line.SetPosition(1, destination.localPosition);
    }
}