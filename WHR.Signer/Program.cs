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
            string[] settings = File.ReadAllLines("Settings.txt");
            var arguments = $"-classpath \"C:\\jcpv20\\Signer.jar;C:\\jcpv20\\javadoc\\*\" Signer \"{settings[0].Replace("\"", "\\\"").Replace("\r", "").Replace("\n", "")}\" {settings[1]}";
            var psi = new ProcessStartInfo("java", arguments)
            {
                UseShellExecute = false,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                CreateNoWindow = true
            };
            //Запускаем процесс
            var result = Process.Start(psi);

            //Получаем вывод из процесса
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