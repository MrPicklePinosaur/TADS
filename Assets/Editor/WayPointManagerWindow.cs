﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class WayPointManagerWindow : EditorWindow
{
    public GameObject wayPoint1;
    public GameObject wayPoint2;
    public Transform wayPointRoot;
    [MenuItem("Tools/WayPoint Editor")]
    public static void Open()
    {
        GetWindow<WayPointManagerWindow>();
    }
    
    
    
    

    private void OnGUI()
    {
        SerializedObject obj = new SerializedObject(this);
        
        EditorGUILayout.PropertyField(obj.FindProperty("wayPoint1"));
        EditorGUILayout.PropertyField(obj.FindProperty("wayPoint2"));
        EditorGUILayout.PropertyField(obj.FindProperty("wayPointRoot"));
        
        EditorGUILayout.BeginVertical();
        DrawButtons();
        EditorGUILayout.EndVertical();
        
        obj.ApplyModifiedProperties();
        
    }

    void DrawButtons()
    {
        if (GUILayout.Button("Connect Waypoints"))
        {
            wayPoint1.GetComponent<WayPoint>().connections.Add(wayPoint2.GetComponent<WayPoint>());
            wayPoint2.GetComponent<WayPoint>().connections.Add(wayPoint1.GetComponent<WayPoint>());
        }

        else if (GUILayout.Button("Create TrafficPoint"))
        {
            GameObject tp = new GameObject("Traffic Point ("+wayPointRoot.childCount+")",typeof(TrafficLightPoint));
            
            tp.transform.SetParent(wayPointRoot,false);
            Selection.activeGameObject = tp;
            
        }
        else if (GUILayout.Button("Create WayPoint"))
        {
            GameObject wp = new GameObject("Way Point ("+wayPointRoot.childCount+")",typeof(WayPoint));
            wp.transform.SetParent(wayPointRoot,false);
            Selection.activeGameObject = wp;

        }else if (GUILayout.Button("Create Point In Between"))
        {
            GameObject wp = new GameObject("Way Point ("+wayPointRoot.childCount+")",typeof(WayPoint));
            
            wp.transform.SetParent(wayPointRoot,false);
            wp.transform.position = (wayPoint1.transform.position + wayPoint2.transform.position) / 2;
            wayPoint1.GetComponent<WayPoint>().connections.Remove(wayPoint2.GetComponent<WayPoint>());
            wayPoint2.GetComponent<WayPoint>().connections.Remove(wayPoint1.GetComponent<WayPoint>());
            
            wayPoint1.GetComponent<WayPoint>().connections.Add(wp.GetComponent<WayPoint>());
            wayPoint2.GetComponent<WayPoint>().connections.Add(wp.GetComponent<WayPoint>());
            wp.GetComponent<WayPoint>().connections.Add(wayPoint1.GetComponent<WayPoint>());
            wp.GetComponent<WayPoint>().connections.Add(wayPoint2.GetComponent<WayPoint>());
        }
        else if (GUILayout.Button("Break Connection"))
        {
            wayPoint1.GetComponent<WayPoint>().connections.Remove(wayPoint2.GetComponent<WayPoint>());
            wayPoint2.GetComponent<WayPoint>().connections.Remove(wayPoint1.GetComponent<WayPoint>());
        }
        
    }
    
    
}
