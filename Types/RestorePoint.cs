namespace BackupSystem.Types;

public enum StorageFormat
{
    Memory,
    SolidZip
};

public class RestorePoint
{
  
    /// <summary>
    /// Список объектов для восстановления
    /// </summary>
    List<IBackupObject> items = new List<IBackupObject>();

    private IRepository repository;
    
    /// <summary>
    /// Момент времени, в который снята точка восстановления
    /// </summary>
    DateTime moment;
  
    /// <summary>
    /// Создать точку восстановления с указанием формата хранилища
    /// </summary>
    public RestorePoint(IRepository repo)
    {
        repository = repo;
    }

    public RestorePoint(string pathToRepo)
    {
        
    }
    
    /// <summary>
    /// Зафиксировать состояние для восстановления и сохранить контролирумые объекты - по сути это и есть создание точки восстановления
    /// </summary>
    public void MakeBackup()
    {
        foreach (var elem in items)
            elem.CreateBackupObject();
    }

    /// <summary>
    /// Восстановить состояние - заменить содержание контролируемых объектов на то, что сохранено в точке восстановления
    /// Тут возможны различные параметры - ну, к примеру, стоит ли спрашивать разрешение на перезапись хранимых объектов.
    /// </summary>
    public void RestoreBackup()
    {
        
    }

    /// <summary>
    /// Добавить папку к коллекции контролируемых объектов
    /// </summary>
    /// <param name="folderName"></param>
    public void AddFolder(string folderName)
    {

    }

    public void AddBackupObject(IBackupObject obj)
    {
        items.Add(obj);
    }
    
    /// <summary>
    /// Добавить файл к контролируемой коллекции
    /// </summary>
    /// <param name="fileName"></param>
    public void AddFile(string fileName) 
    {
        items.Add(new File(fileName, repository));
    }

}