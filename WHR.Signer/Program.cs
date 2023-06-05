using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace WHR.Signer
{
    class Program
    {
        private static void Main(string[] args)
        {
            string [] settings = File.ReadAllLines("Settings.txt");
            var arguments = string.Join(" ", new string[] {
                "-classpath",//Java аргумент, указывающий как называется класс с методом main и где его искать
                @"""Signer.jar;lib\*""",//путь к классу с методом main и путь к библиотекам необходимым для запуска
                "Signer",//Имя класса с методом main
                $@"""{settings[0].Replace(@"""", @"\""").Replace("\r","").Replace("\n", "")}""",//xml, который должен быть подписан
                $"{settings[1]}"//Название подписи, например mev21mai
            });//Аргументы для вызова подписалки
            //Запускаем процесс Java, чтобы сработало, пути к java.exe должны быть прописаны в переменные среды PATH
            ProcessStartInfo psi = new ProcessStartInfo(@"java", arguments);
            //Указываем, что необходимо перенаправить вывод        
            psi.UseShellExecute = false;
            psi.RedirectStandardOutput = true;
            psi.RedirectStandardError = true;
            //Скрыть окно
            psi.CreateNoWindow = true;
            //Запускаем процесс
            var result = Process.Start(psi);
            //Получаем вывод из процесса

            //Читаем результат
            //process.Start();
            var output = new List<string>();
            var error = new List<string>();

            //Читаем сообщения и ошибки
            while (result.StandardError.Peek() > -1)
                error.Add(result.StandardError.ReadLine());
            //Читаем результат
            while (result.StandardOutput.Peek() > -1)
                output.Add(result.StandardOutput.ReadLine());
            result.WaitForExit();

            Console.WriteLine(string.Join(" ", output));
            File.WriteAllText(settings[0], string.Join(" ", output));
    }
    }
}
