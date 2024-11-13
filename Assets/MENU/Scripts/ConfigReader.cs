using System;
using System.IO;
using UnityEngine;

public class ConfigReader : MonoBehaviour
{
    public string filePath; // Nombre del archivo de configuración
    private string shortText;
    public string fullPath;

    void Awake()
    {
        // Construir la ruta completa en el directorio persistente de Unity
        fullPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\" + filePath;
        // Intentar leer el archivo y capturar cualquier error
        try
        {
            if (File.Exists(fullPath))
            {
                shortText = File.ReadAllText(fullPath);
                GetComponent<conexion>().COM = shortText;
            }
            else
            {
                Debug.LogError("Archivo no encontrado: " + fullPath);
            }
        }
        catch (Exception ex)
        {
            Debug.LogError("Error al leer el archivo: " + ex.Message);
        }
    }
}
