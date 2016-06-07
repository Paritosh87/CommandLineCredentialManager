using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CredentialManager
{
    class Program
    {
        
        public static string TargetName { get; set; }
        public static string UserName { get; set; }
        public static string Password { get; set; }
        public static Action<string> deleg;
        static void Main(string[] args)
        {
            TargetName = "Test.com";
            UserName = "testuser";
            Password = "testpwd";

            Console.WriteLine("1. Add \r\n2. Delete ");
            
            switch (Console.Read())
            {
                case '1': deleg = Exec; deleg(String.Format("/add:{0} /user:{1} /pass:{2}", TargetName, UserName, Password)); break; // deleg t= Exec; t(String.Format("/add:{0} /user:{1} /pass:{2}",TargetName,UserName,Password)); break;
                case '2': deleg = Exec; deleg(String.Format("/del:{0} ", TargetName)); break;//deleg t1= Exec; t1(String.Format("/del:{0} ", TargetName));
                default: Console.WriteLine("Enter a 1 or 2"); break;
            }
            Console.Read();
        }
        private static void Exec(string cmd)
        {
            ProcessStartInfo processStartInfo = new ProcessStartInfo
            {
                FileName = @"C:\Windows\System32\cmdkey.exe",
                Arguments = cmd,
                UseShellExecute = false,
                CreateNoWindow = true,
                RedirectStandardOutput = true
            };

            using (var process = Process.Start(processStartInfo))
            {
                Console.WriteLine(process.StandardOutput.ReadToEnd());
                if (!process.WaitForExit(TimeSpan.FromSeconds(10).Milliseconds))
                {
                    process.Kill();
                }
            }

        }
    }
}
