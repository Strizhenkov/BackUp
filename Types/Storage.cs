using BackupSystem.Types;
using System.IO.Compression;
using System.Runtime.CompilerServices;

namespace BackupSystem.Types;

//  Интерфейс доступа к хранилищу (файлу)
public interface IStorage
{
    //  Добавить информацию о времени создания, может быть хеш, может ещё что

    //  Поток для чтения/записи
    Stream stream
    {
        get;
    }
    //  Длина в байтах
    long Length
    {
        get;
    }
    //  Удаление хранилища
    public void Free();
}

public class Storage : IStorage
{
    //private string pathToBackup; //  путь к сохраненному файлу

    //  Конструктор - должен реализовывать поток чтения/записи
    //    в соответствии с некоторым алгоритмом
    private IRepository parent;
    private Stream _stream;
    private string _name;

    public Storage(string name, Stream dataStream, IRepository parentRepository)
    {
        _stream = dataStream;
        parent = parentRepository;
        _name = name;
    }

    public Stream stream
    {
        get => _stream;
    }

    public long Length
    {
        get => stream.Length;
    }
    public void Free()
    {
        //  Тут пока непонятно что делать и зачем
    }
}
/*
public class ZipStorage : IStorage
{
    //private string pathToBackup; //  путь к сохраненному файлу

    //  Конструктор - должен реализовывать поток чтения/записи
    //    в соответствии с некоторым алгоритмом
    private IRepository parent;
    private Stream _stream;
    private Stream archiveStream;

    public ZipStorage(Stream dataStream, IRepository parentRepository)
    {
        _stream = dataStream;
        parent = parentRepository;

        /////         --------------           
        ZipArchive archive = new ZipArchive(dataStream, ZipArchiveMode.Create, true);
            {
           

                var demoFile = archive.CreateEntry("content.dat");

                using (var entryStream = demoFile.Open())
                using (var streamWriter = new StreamWriter(entryStream))
                {
                    streamWriter.Write("Bar!");
                }
            }

            using (var fileStream = new FileStream(@"C:\Temp\test.zip", FileMode.Create))
            {
                memoryStream.Seek(0, SeekOrigin.Begin);
                memoryStream.CopyTo(fileStream);
            }
        }


    }

    public Stream stream
    {
        get => _stream;
    }

    public long Length
    {
        get => stream.Length;
    }
    public void Free()
    {

    }
}
*/