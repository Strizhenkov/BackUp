using System.Runtime.Intrinsics.Arm;

namespace Backups.Types;


public enum StoreAlg 
{
    RawBytes,
    Zip,
    GZip
};

public class File : IBackupObject
{
    // private string rootFolder;  //  корень папки, которую сохраняем
    private string pathToFile;  // имя исходного файла
    private string pathToBackup;  //  путь к сохраненному файлу
    private Storage container;
    private DateTime modifyDateTime;

    /// <summary>
    /// Создание бэкапа для указанного файла
    /// </summary>
    /// <param name="path">Полный путь к файлу</param>
    /// <param name="storedPath">Папка с бэкапами этих файлов</param>
    public File(string path, string storedPath)
    {
        pathToFile = path;
        pathToBackup = storedPath + path.Substring(path.LastIndexOf('/'), path.Length - path.LastIndexOf('/'));
        modifyDateTime = System.IO.File.GetLastWriteTime(pathToFile);
    }

    public void CreateBackupObject(string path)
    {
        //  Разделить path на имя файла и имя папки
        //  Создать полное имя для сохранения в storedPath
        //  Как с помощью zip-арзиватора создать архив в папке
    }

    /// <summary>
    /// Проверка того, что объект был изменен
    /// </summary>
    /// <param name="path">Путь к исходному объекту</param>
    /// <returns>true если объект был изменен</returns>
    public bool IsBackupObjectChanged(string path)
    {
        return System.IO.File.GetLastWriteTime(pathToFile) == modifyDateTime;
    }

    void DeleteBackupObject(string path)
    {
        System.IO.File.Delete(path);
    }
}