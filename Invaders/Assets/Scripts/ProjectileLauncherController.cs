using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileLauncherController : MonoBehaviour
{
    ProjectileLauncher projectileLauncher;

    void Awake()
    {
        projectileLauncher = GetComponent<ProjectileLauncher>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!Input.GetKey(KeyCode.Mouse0))
            projectileLauncher.SetLockState(true);
        else
            projectileLauncher.SetLockState(false);

    }
}
