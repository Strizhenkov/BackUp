using System.IO.Compression;
using System.IO;
using System.Security.Cryptography.X509Certificates;

namespace BackupSystem.Types;

//  Репозиторий - ответственен за хранение набора Storage и/или Backup Object, с описанием всех
//  параметров хранения - где и как хранятся данные, с использованием каких алгоритмов и прочее
//  В теории репозиторий может в качестве своей функции создавать (генерировать) объекты сущности
//  Storage по запросу - то есть мы обращаемся к какому-то Repository и просим выдать нам Storage
//  под что-то. Тогда параметры хранения будут зависеть только от самого репозитория (одним файлом,
//  в папке навалом, или в памяти). Как следствие - мы считаем, что задача объектов Repository - только
//  выдать массив байтов для объектов, и всё.

public interface IRepository
{
    /// <summary>
    /// Возвращает вновь созданный объект Storage в данном репозитории
    /// </summary>
    /// <returns></returns>
    public IStorage createStorageObj(string name);
    public IStorage findStorageObj(string objId);
    public void DeleteStorage(IStorage storage);
    public long length { get; }
    public long bytesUsed { get; }
}

/// <summary>
/// Репозиторий для хранения объектов в памяти
/// </summary>
public class MemoryRepository : IRepository
{
    protected HashSet<string> usedNames = new HashSet<string>();
    protected uint lastIndex = 0;
    protected List<IStorage> storages = new List<IStorage>();
 
    public MemoryRepository()
    {
        lastIndex = 0;
    }

    long IRepository.length => storages.LongCount();

    long IRepository.bytesUsed => storages.Sum(s => s.Length);

    public IStorage createStorageObj(string name)
    {
        //  тут проверить, что нет объектов с таким именем
        if(usedNames.Contains(name))
        {
            //  Замечание - сделать версию к имени файла, а не к расширению
            int index = 1;
            while (usedNames.Contains(name + '(' + index.ToString() + ')')) index++;
            name = name + '(' + index.ToString() + ')';
        }

        
        
        MemoryStream memoryStream = new MemoryStream();
        var storage = new Storage(name, memoryStream, this);
        usedNames.Add(name);
        storages.Add(storage);
        return storage;
    }
    public IStorage findStorageObj(string objId) 
    {
        throw new NotImplementedException();
    }

    void IRepository.DeleteStorage(IStorage storage)
    {
        throw new NotImplementedException();
    }

    IStorage IRepository.findStorageObj(string objId)
    {
        throw new NotImplementedException();
    }
}

/// <summary>
/// Репозиторий для хранения объектов в памяти
/// </summary>
public class SolidZipRepository : IRepository, IDisposable
{
    //protected HashSet<string> usedNames = new HashSet<string>();
    //protected HashMap<string, string> actualNames = new HashMap<string, string>();
    //actualNames['simple.txt'] = "Desktop\simple.txt"
    protected List<IStorage> storages = new List<IStorage>();
    protected ZipArchive archive;
    protected string archiveName;
    protected MemoryStream baseArchiveMemStream;
    public SolidZipRepository(string zipFileName)
    {
        archiveName = zipFileName;
        baseArchiveMemStream = new MemoryStream();
        archive = new ZipArchive(baseArchiveMemStream, ZipArchiveMode.Update, true);
        //  Архив создали, но в него пока что ничего не записывали
    }

    long IRepository.length => storages.LongCount();

    long IRepository.bytesUsed => storages.Sum(s => s.Length);

    public IStorage createStorageObj(string name)
    {
        //  Тут ещё бы имена подбирать для файла
        var zipEntry = archive.CreateEntry(name);
        var storage = new Storage(name, zipEntry.Open(), this);
        storages.Add(storage);
        return storage;
    }
    
    public void Dispose()
    {
        //  Базовый dispose ?
        
        archive.Dispose();  //   Архив обязательно освободить!!!!
        using (var fileStream = new FileStream(archiveName, FileMode.Create))
        {
            baseArchiveMemStream.Seek(0, SeekOrigin.Begin);
            baseArchiveMemStream.CopyTo(fileStream);
        }
        //Console.WriteLine("Dispose");
    }

    public IStorage findStorageObj(string objId)
    {
        throw new NotImplementedException();
    }

    void IRepository.DeleteStorage(IStorage storage)
    {
        throw new NotImplementedException();
    }

    IStorage IRepository.findStorageObj(string objId)
    {
        throw new NotImplementedException();
    }
}