using System;
using System.Diagnostics;
using Microsoft.VisualBasic.FileIO;
using System.Collections;
using System.IO;
using System.Reflection;

namespace getprocs
{

    class Program
    
    {

        // Отключить вывод warning'ов в выводе программы
        #pragma warning disable 168, 219
        
        
                static void Main(string[] args)
        {

        // Console.WriteLine(args[0]);

        // Читаем csv файл
        
        
        using (TextFieldParser parser = new TextFieldParser(args[0]))
        
        {
            ArrayList result = new ArrayList();
            parser.TextFieldType = FieldType.Delimited;
            parser.SetDelimiters(",");
            

            while (!parser.EndOfData) 
            {
                int procfound = 0;
                int procnotfound = 0;
                string fields = parser.ReadLine();
                // Console.WriteLine("CSV line: " + fields.Split(',')[1] + " " + fields.Split(',')[0]);


                // Обработка процессов 
                Process[] processlist = Process.GetProcesses();

                for (int i = 0; i < processlist.Length; i++)
                {
                    
                    Process theprocess = processlist[i];
                    // Console.WriteLine("Process: {0} ID: {1}", theprocess.ProcessName, theprocess.Id);

                try {

                    string runnedproc = theprocess.MainModule.FileName.ToLower();
                    // Console.WriteLine(runnedproc);

                    if (runnedproc == fields.Split(',')[0].ToLower()) {
                        // Console.WriteLine(fields.Split(',')[1] + " FOUND");
                        procfound = 1;
                    }
                    // else if (theprocess.MainModule.FileName != fields.Split(',')[0]) {
                    //     procnotfound = 1;
                    // }
                    // else {
                    //     procnotfound = 1;
                    // }
                }

                catch (System.ComponentModel.Win32Exception ex)
                    {
                        // Console.WriteLine(theprocess.ProcessName);
                        // procnotfound = 0;
                    }
                
                }


            // Добавляем инфу о процессе в массив (выборочно)
                if (Char.ToString(fields.Split(',')[1][0]) == "$" && procfound == 0) {
                            // Console.WriteLine ("Important process");
                            result.AddRange(new string[] { fields.Split(',')[1] + @"=-1" });
                        }
                else if (procfound == 1) {
                    result.AddRange(new string[] { fields.Split(',')[1] + "=1" });
                }

                

            }


            string resstring = "allprocesses";
            foreach (object o in result)
            {
                DateTime epochStart=new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            long unixtimestamp = (DateTime.UtcNow - epochStart).Ticks*100;
            Console.WriteLine(resstring+" "+o+" "+unixtimestamp+"\r\n", Environment.NewLine);
            }
            

        }

        


        }
    }
}
