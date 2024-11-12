using UnityEngine;
using System.IO;

public class ExportToOBJ : MonoBehaviour
{
    [UnityEditor.MenuItem("Tools/Export Selected Object to OBJ")]
    static void ExportSelectedObjectToOBJ()
    {
        var selectedObject = UnityEditor.Selection.activeGameObject;
        if (selectedObject == null)
        {
            Debug.LogWarning("No GameObject selected!");
            return;
        }

        string path = UnityEditor.EditorUtility.SaveFilePanel("Export OBJ", "", selectedObject.name + ".obj", "obj");

        if (!string.IsNullOrEmpty(path))
        {
            using (StreamWriter writer = new StreamWriter(path))
            {
                MeshFilter meshFilter = selectedObject.GetComponent<MeshFilter>();
                if (meshFilter != null)
                {
                    Mesh mesh = meshFilter.sharedMesh;

                    foreach (Vector3 v in mesh.vertices)
                        writer.WriteLine($"v {v.x} {v.y} {v.z}");

                    foreach (Vector3 n in mesh.normals)
                        writer.WriteLine($"vn {n.x} {n.y} {n.z}");

                    foreach (Vector3 uv in mesh.uv)
                        writer.WriteLine($"vt {uv.x} {uv.y}");

                    for (int i = 0; i < mesh.triangles.Length; i += 3)
                    {
                        writer.WriteLine($"f {mesh.triangles[i] + 1} {mesh.triangles[i + 1] + 1} {mesh.triangles[i + 2] + 1}");
                    }
                }
            }

            Debug.Log("Object exported to OBJ successfully!");
        }
    }
}
