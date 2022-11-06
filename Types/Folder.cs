namespace Backups.Types;

public class Folder : IBackupObject
{
    // private string rootFolder;  //  корень папки, которую сохраняем
    private string pathToFolder;  // имя исходной папки
    private string pathToBackup;  //  путь к сохраненной папки
    private Storage container;
    private DateTime modifyDateTime;
    private List<File> _files;
    private List<Folder> _folders;

    /// <summary>
    /// Создание бэкапа для указанного файла
    /// </summary>
    /// <param name="path">Полный путь к файлу</param>
    /// <param name="storedPath">Папка с бэкапами этих файлов</param>
    public Folder(string path, string storedPath)
    {
        pathToFolder = path;
        pathToBackup = storedPath + path.Substring(path.LastIndexOf('/'), path.Length - path.LastIndexOf('/'));
        modifyDateTime = System.IO.File.GetLastWriteTime(pathToFolder);
        _files = new List<File>();
        _folders = new List<Folder>();
    }

    public void AddFileToFolder(File file)
    {
        _files.Add(file);
    }

    public void AddFolderToFolder(Folder folder)
    {
        _folders.Add(folder);
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
        return System.IO.File.GetLastWriteTime(pathToFolder) == modifyDateTime;
    }

    public void DeleteBackupObject(string path)
    {
        Directory.Delete(path);
    }
}