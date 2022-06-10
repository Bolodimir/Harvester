using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diagnostics : MonoBehaviour
{
    public Diagnostics Instance;

    public bool DiaMode;

    private void Start()
    {
        DiaMode = false;
    }
    private void FixedUpdate()
    {
        if (DiaMode)
        {
            Collider[] cols = Physics.OverlapSphere(Vector3.zero, 1000f);
            foreach(Collider col in cols)
            {
                print(col.gameObject.name);
            }
        }
    }
    public void StartDiagnostics()
    {
        DiaMode = true;
    }
}
