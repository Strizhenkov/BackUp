// See https://aka.ms/new-console-template for more information
using BackupSystem.Types;
using File = BackupSystem.Types.File;


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

using(SolidZipRepository repository = new SolidZipRepository("repo1.zip")) {
    RestorePoint pt1 = new RestorePoint(repository);
    pt1.AddFile(filename);
    pt1.AddFile(filename2);
    pt1.AddFile(filename3);
    pt1.MakeBackup();
}
//  Проверить!
{
  //  Как восстановить? Есть только сохранённый репозиторий 
  //RestorePoint pt1 = new RestorePoint("store/repo1.zip");
  //pt1.RestoreBackup();
}


    using(SolidZipRepository repository = new SolidZipRepository("zipped.zip")) {

    //  Тут создадим новый файл для проверки
    
        BackupSystem.Types.File file = new BackupSystem.Types.File(filename, repository);
        BackupSystem.Types.File file2 = new BackupSystem.Types.File(filename2, repository);
        BackupSystem.Types.File file3 = new BackupSystem.Types.File(filename3, repository);
        file.CreateBackupObject();
        file2.CreateBackupObject();
        file3.CreateBackupObject();
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