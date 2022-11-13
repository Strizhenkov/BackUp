namespace BackupSystem.Types;

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
        modifyDateTime = Directory.GetLastWriteTime(pathToFolder);
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

    public void CreateBackupObject()
    {
        //  Создаём бэкап. Проверку не делаем - в любом случае при отсутствии файла будет исключение
        FileStream fs = new FileStream(pathToFolder, FileMode.Open);
        //  Копируем в поток
        container.stream.Seek(0, SeekOrigin.Begin);
        fs.CopyTo(container.stream);
        fs.Flush();
        fs.Close();
    }

    public void RestoreFromBackup(string newFolderName = "")
    {
        //  Создаём бэкап. Проверку не делаем - в любом случае при отсутствии файла будет исключение
        FileStream fs = new FileStream(newFolderName.Length == 0 ? pathToFolder : newFolderName, FileMode.Create);
        //  Копируем в поток
        container.stream.Seek(0, SeekOrigin.Begin);
        container.stream.CopyTo(fs);
        fs.Close();
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