using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BakeNavMesh_FG : MonoBehaviour
{
    public void Bake(GameObject[] objectsToInclude)
    {
        // Asegúrate de que los objetos tengan colliders y estén configurados correctamente
        // Limpia el NavMesh actual
        NavMesh.RemoveAllNavMeshData();

        // Configura los parámetros para el bake
        NavMeshBuildSettings buildSettings = NavMesh.GetSettingsByID(0);

        // Crea una lista de fuentes para el bake
        var sources = new List<NavMeshBuildSource>();

        foreach (var obj in objectsToInclude)
        {
            // Asegúrate de que el objeto tenga un MeshFilter
            MeshFilter filter = obj.GetComponent<MeshFilter>();
            if (filter != null)
            {
                NavMeshBuildSource source = new NavMeshBuildSource
                {
                    shape = NavMeshBuildSourceShape.Mesh,
                    sourceObject = filter.sharedMesh,
                    transform = obj.transform.localToWorldMatrix,
                    area = 0 // Área por defecto
                };
                sources.Add(source);
            }
        }

        // Define los límites del bake
        Bounds bounds = new Bounds();
        foreach (var obj in objectsToInclude)
        {
            bounds.Encapsulate(obj.GetComponent<Collider>().bounds);
        }

        // Bake del NavMesh
        NavMesh.AddNavMeshData(NavMeshBuilder.BuildNavMeshData(buildSettings, sources, bounds, Vector3.zero, Quaternion.identity));
    }
}
