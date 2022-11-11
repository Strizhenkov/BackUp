// See https://aka.ms/new-console-template for more information
using Backups.Types;
using File = Backups.Types.File;


string filename = "simple.txt";
string s = "Now it's brand new file!";
string filename2 = "simple2.txt";
string s2 = "Second";
string filename3 = "simple3.txt";
string s3 = "Third";
System.IO.File.WriteAllText(filename, s);
System.IO.File.WriteAllText(filename2, s2);
System.IO.File.WriteAllText(filename3, s3);

Console.WriteLine(File.CalculateMD5(filename3));

    using(SolidZipRepository repository = new SolidZipRepository("zipped.zip")) {

    //  Тут создадим новый файл для проверки
    
        Backups.Types.File file = new Backups.Types.File(filename, repository);
        Backups.Types.File file2 = new Backups.Types.File(filename2, repository);
        Backups.Types.File file3 = new Backups.Types.File(filename3, repository);
        file.MakeBackup();
        file2.MakeBackup();
        file3.MakeBackup();
        file.RestoreFromBackup();
        file2.RestoreFromBackup();
        file.RestoreFromBackup("simple4.txt");
        Console.WriteLine(File.CalculateMD5(filename3) == file3.GeteMD5());
        Console.WriteLine(File.CalculateMD5(filename3) == file2.GeteMD5());
    }
    
    
    /*  1. Попробовать сделать репозитории с просто хранением файлов внутри какой-то папки
     *  2. Раздельные архивы zip
     *  3. Попробовать запихивать в репозиторий файлы с полными именами  Bin\Files\simple.txt
     *  4. Сделать базовый класс репозитория, который управляется с именами хранимых объектов
     *  5. Продумтаь структуру остальную
     *  6. Что с папками (Folder)
     *
     */