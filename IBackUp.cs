namespace BackupSystem.Types;

/// <summary>
/// Тут всё переделать
/// </summary>
public interface IBackupObject
{
    /// <summary>
    /// Создать объект в хранилище
    /// </summary>
    /// <param name="path">Путь к исходному объекту</param>
    void CreateBackupObject();

    /// <summary>
    /// Проверка того, что объект был изменен
    /// </summary>
    /// <param name="path">Путь к исходному объекту</param>
    /// <returns>true если объект был изменен</returns>
    //bool IsBackupObjectChanged();

    //void DeleteBackupObject();
}