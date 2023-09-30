using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileLauncherController : MonoBehaviour
{
    [SerializeField] private ProjectileLauncher projectileLauncher;
    [SerializeField] private AmmoRefiller refiller;

    // Update is called once per frame
    void Update()
    {
        if(!Input.GetKey(KeyCode.Mouse0))
            projectileLauncher.SetLockState(true);
        else
        {
            projectileLauncher.SetLockState(false);
            refiller.TemporarilyDisable();
        }
            

    }
}
