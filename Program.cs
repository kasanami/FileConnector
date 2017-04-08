using System;
using System.IO;

namespace FileConnector
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length < 1)
            {
                return;
            }
            var sourcePath = args[0];
            var index = sourcePath.IndexOf(".part");
            if (index < 0)
            {
                Console.WriteLine("非対応のファイル名です。\".part\"が付いていません。");
                return;
            }
            try
            {
                var basePath = sourcePath.Substring(0, index + 5);
                var originalPath = sourcePath.Substring(0, index);
                using (var writeStream = new FileStream(originalPath, FileMode.Create))
                {
                    for (int no = 1; true; no++)
                    {
                        if (File.Exists(basePath + no) == false)
                        {
                            break;
                        }
                        var buffer = File.ReadAllBytes(basePath + no);
                        writeStream.Write(buffer, 0, buffer.Length);
                    }
                }
                Console.WriteLine("正常終了");
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
