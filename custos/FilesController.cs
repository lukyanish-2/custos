namespace custos;

using System;


public class FilesController
{
    private static readonly string ApplicationFolder = Environment.GetFolderPath(
        Environment.SpecialFolder.ApplicationData) + "/custos";
    
    public FilesController()
    {
        if (!Directory.Exists(ApplicationFolder))
        {
            Directory.CreateDirectory(ApplicationFolder);
        }
    }
}