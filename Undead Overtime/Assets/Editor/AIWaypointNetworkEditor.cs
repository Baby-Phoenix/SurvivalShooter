using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEditor;

[CustomEditor(typeof(AIWaypointNetwork))]
public class AIWaypointNetworkEditor : Editor
{
    public override void OnInspectorGUI()
    {
        AIWaypointNetwork network = (AIWaypointNetwork)target;

        network.DisplayMode = (PathDisplayMode)EditorGUILayout.EnumPopup("Display Mode", network.DisplayMode);

        if (network.DisplayMode == PathDisplayMode.SelectedPaths)
        {
            network.UIStart = EditorGUILayout.IntSlider("Waypoint Start", network.UIStart, 0, network.Waypoints.Count - 1);
            network.UIEnd = EditorGUILayout.IntSlider("Waypoint End", network.UIEnd, 0, network.Waypoints.Count - 1);
        }
        
        DrawDefaultInspector();       
    }

    void OnSceneGUI()
    {
        AIWaypointNetwork network = (AIWaypointNetwork)target;

        for(int i = 0; i < network.Waypoints.Count; i++)
        {
            if(network.Waypoints[i] != null)
                Handles.Label(network.Waypoints[i].position, "Waypoint " + i.ToString());
        }

        switch (network.DisplayMode)
        {
            case PathDisplayMode.Connections:
                ShowConnections(network);
                break;
            case PathDisplayMode.SelectedPaths:
                ShowSelectedPaths(network);
                break;
            case PathDisplayMode.AllPaths:
                ShowAllPaths(network);
                break;
            default:
                break;
        }

        //if (network.DisplayMode == PathDisplayMode.Connections)
        //{
        //    Vector3[] linePoints = new Vector3[network.Waypoints.Count + 1];

        //    for (int i = 0; i <= network.Waypoints.Count; i++)
        //    {
        //        int index = i != network.Waypoints.Count ? i : 0;
        //        if (network.Waypoints[index] != null)
        //            linePoints[i] = network.Waypoints[index].position;
        //        else
        //            linePoints[i] = new Vector3(Mathf.Infinity, Mathf.Infinity, Mathf.Infinity);

        //    }

        //    Handles.color = Color.cyan;
        //    Handles.DrawPolyLine(linePoints);
        //}
        //else if (network.DisplayMode == PathDisplayMode.SelectedPaths)
        //{
        //    NavMeshPath path    = new NavMeshPath();

        //    if (network.Waypoints[network.UIStart] != null && network.Waypoints[network.UIEnd] != null)
        //    {
        //        Vector3 from = network.Waypoints[network.UIStart].position;
        //        Vector3 to = network.Waypoints[network.UIEnd].position;

        //        NavMesh.CalculatePath(from, to, NavMesh.AllAreas, path);
        //        Handles.color = Color.yellow;
        //        Handles.DrawPolyLine(path.corners);
        //    }
        //}
    }

    private void ShowConnections(AIWaypointNetwork network)
    {
        Vector3[] linePoints = new Vector3[network.Waypoints.Count + 1];

        for (int i = 0; i <= network.Waypoints.Count; i++)
        {
            int index = i != network.Waypoints.Count ? i : 0;
            if (network.Waypoints[index] != null)
            {
                linePoints[i] = network.Waypoints[index].position;
            }
            else
            {
                linePoints[i] = new Vector3(Mathf.Infinity, Mathf.Infinity, Mathf.Infinity);
            }
        }
        Handles.color = Color.cyan;
        Handles.DrawPolyLine(linePoints);
    }

    private void ShowSelectedPaths(AIWaypointNetwork network)
    {
        NavMeshPath path = new NavMeshPath();

        if (network.Waypoints[network.UIStart] != null && network.Waypoints[network.UIEnd] != null)
        {
            Vector3 from = network.Waypoints[network.UIStart].position;
            Vector3 to = network.Waypoints[network.UIEnd].position;

            NavMesh.CalculatePath(from, to, NavMesh.AllAreas, path);

            Handles.color = Color.yellow;
            Handles.DrawPolyLine(path.corners);
        }
    }

    private void ShowAllPaths(AIWaypointNetwork network)
    {
        for (int i = 0; i < network.Waypoints.Count; i++)
        {
            NavMeshPath path = new NavMeshPath();
            if (network.Waypoints[i] != null)
            {
                Vector3 from, to;
                if (i == network.Waypoints.Count - 1)
                {
                    from = network.Waypoints[i].position;
                    to = network.Waypoints[0].position;
                }
                else
                {
                    from = network.Waypoints[i].position;
                    to = network.Waypoints[i + 1].position;
                }

                NavMesh.CalculatePath(from, to, NavMesh.AllAreas, path);
                Handles.color = Color.yellow;
                Handles.DrawPolyLine(path.corners);
            }
        }
    }
}
