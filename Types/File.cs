using System.Runtime.Intrinsics.Arm;
using System.Text;

namespace Backups.Types;


public enum StoreAlg 
{
    RawBytes,
    Zip,
    GZip
};

public class File
{
    private string pathToFile;  // имя исходного файла
    private IStorage container;
    private DateTime modifyDateTime;

    /// <summary>
    /// Создание бэкапа для указанного файла
    /// </summary>
    /// <param name="path">Полный путь к файлу</param>
    /// <param name="storedPath">Папка с бэкапами этих файлов</param>
    public File(string path, IRepository repository)
    {
        //  Запоминаем путь
        pathToFile = path;
        //  Создаём объект для хранения 
        string BackupName = path;
        if(BackupName.IndexOf('/') >= 0)
            BackupName = BackupName.Substring(path.LastIndexOf('/'), path.Length - path.LastIndexOf('/'));
        container = repository.createStorageObj(BackupName);
        modifyDateTime = System.IO.File.GetLastWriteTime(pathToFile);
    }

    public static string CalculateMD5(string filename)
    {
        using (var md5 = System.Security.Cryptography.MD5.Create())
        {
            using (var stream = System.IO.File.OpenRead(filename))
            {
              

                    byte[] buffer = md5.ComputeHash(stream);
                    StringBuilder result = new StringBuilder(buffer.Length*2);

                    for(int i = 0; i < buffer.Length; i++)
                        result.Append(buffer[i].ToString("x2"));

                    return result.ToString();
            }
        }
    }

    public string GeteMD5()
    {
        using (var md5 = System.Security.Cryptography.MD5.Create())
        {
            container.stream.Seek(0, SeekOrigin.Begin);
            byte[] buffer = md5.ComputeHash(container.stream);
            StringBuilder result = new StringBuilder(buffer.Length*2);

            for(int i = 0; i < buffer.Length; i++)
                result.Append(buffer[i].ToString("x2"));

            return result.ToString();
        }
    } 
    
    public void MakeBackup()
    {
        //  Создаём бэкап. Проверку не делаем - в любом случае при отсутствии файла будет исключение
        FileStream fs = new FileStream(pathToFile, FileMode.Open);
        //  Копируем в поток
        container.stream.Seek(0, SeekOrigin.Begin);
        fs.CopyTo(container.stream);
        fs.Flush();
        fs.Close();
    }

    public void RestoreFromBackup(string newFilename = "")
    {
        //  Создаём бэкап. Проверку не делаем - в любом случае при отсутствии файла будет исключение
        FileStream fs = new FileStream(newFilename.Length == 0 ? pathToFile : newFilename, FileMode.Create);
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
        return System.IO.File.GetLastWriteTime(pathToFile) == modifyDateTime;
    }

}