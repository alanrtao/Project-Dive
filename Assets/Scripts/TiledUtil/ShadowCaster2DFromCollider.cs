﻿using System.Reflection;
using UnityEngine;


/// <summary>
/// unitytips: ShadowCaster2DFromCollider Component
/// http://diegogiacomelli.com.br/unitytips-shadowcaster2-from-collider-component
/// <remarks>
/// Based on https://forum.unity.com/threads/can-2d-shadow-caster-use-current-sprite-silhouette.861256/
/// </remarks>
/// </summary>
public class ShadowCaster2DFromCollider {
    /*static readonly FieldInfo _meshField;
    static readonly FieldInfo _shapePathField;
    static readonly MethodInfo _generateShadowMeshMethod;

    static ShadowCaster2DFromCollider() {
        _meshField = typeof(ShadowCaster2D).GetField("m_Mesh", BindingFlags.NonPublic | BindingFlags.Instance);
        _shapePathField =
            typeof(ShadowCaster2D).GetField("m_ShapePath", BindingFlags.NonPublic | BindingFlags.Instance);

        _generateShadowMeshMethod = typeof(ShadowCaster2D)
            .Assembly
            .GetType("UnityEngine.Experimental.Rendering.Universal.ShadowUtility")
            .GetMethod("GenerateShadowMesh", BindingFlags.Public | BindingFlags.Static);
    }

    public static void Execute(GameObject g) {
        ShadowCaster2D shadowCaster = g.GetComponent<ShadowCaster2D>();
        EdgeCollider2D edgeCollider = g.GetComponent<EdgeCollider2D>();
        PolygonCollider2D polygonCollider = null;
        if (edgeCollider == null) polygonCollider = g.GetComponent<PolygonCollider2D>();

        UpdateShadow(shadowCaster, edgeCollider, polygonCollider);
    }

    public static void UpdateShadow(ShadowCaster2D shadowCaster, EdgeCollider2D edgeCollider,
        PolygonCollider2D polygonCollider) {
        var points = polygonCollider == null
            ? edgeCollider.points
            : polygonCollider.points;

        _shapePathField.SetValue(shadowCaster,);
        _meshField.SetValue(shadowCaster, new Mesh());
        _generateShadowMeshMethod.Invoke(shadowCaster,
            new object[] {_meshField.GetValue(shadowCaster), _shapePathField.GetValue(shadowCaster)});
    }*/

    private static BindingFlags accessFlagsPrivate =
        BindingFlags.NonPublic | BindingFlags.Instance;

    private static FieldInfo meshField =
        typeof(UnityEngine.Rendering.Universal.ShadowCaster2D).GetField("m_Mesh", accessFlagsPrivate);

    private static FieldInfo shapePathField =
        typeof(UnityEngine.Rendering.Universal.ShadowCaster2D).GetField("m_ShapePath", accessFlagsPrivate);

    private static MethodInfo onEnableMethod =
        typeof(UnityEngine.Rendering.Universal.ShadowCaster2D).GetMethod("OnEnable", accessFlagsPrivate);

    public static void Execute(GameObject g, bool selfShadows = false) {
        var addedCaster = g.AddComponent<UnityEngine.Rendering.Universal.ShadowCaster2D>();
        var polygon = g.GetComponent<EdgeCollider2D>();
        addedCaster = g.GetComponent<UnityEngine.Rendering.Universal.ShadowCaster2D>();
        addedCaster.selfShadows = selfShadows;
        
        Vector3[] positions = new Vector3[polygon.points.Length];

        for (int i = 0; i < polygon.points.Length; i++) {
            positions[i] = new Vector3(
                polygon.points[i].x,
                polygon.points[i].y, 
            0);
        }

        shapePathField.SetValue(addedCaster, positions);

        meshField.SetValue(addedCaster, null);

        onEnableMethod.Invoke(addedCaster, new object[0]);
    }
}