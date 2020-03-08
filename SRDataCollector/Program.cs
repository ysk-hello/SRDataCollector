using SRDataCollector.Model;
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;

namespace SRDataCollector
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("start!");

            if (Environment.GetCommandLineArgs().Length != 2)
            {
                Console.WriteLine("invalid args.");
                return;
            }

            // コマンドライン引数が1つの場合、以降の処理を実行
            var cmdArg = Environment.GetCommandLineArgs()[1].ToLower();
            Console.WriteLine(cmdArg);

            if(cmdArg == "create")
            {
                using (var db = new SRContext())
                {
                    // データベース作成
                    var isCreated = db.Database.EnsureCreated();
                    Console.WriteLine(isCreated);
                }
            }
            else if (cmdArg == "delete")
            {
                using (var db = new SRContext())
                {
                    // データベース削除
                    var isDeleted = db.Database.EnsureDeleted();
                    Console.WriteLine(isDeleted);
                }
            }
            else if(cmdArg == "collect")
            {
                using (var db = new SRContext())
                {
                    // List化
                    var members = db.Member.ToList();
                    foreach (var member in members)
                    {
                        var data = Data.CreateData(member);
                        Console.WriteLine($"{data.Member.Name}  {data.FollowerNum}");

                        db.Data.Add(data);
                        db.SaveChanges();
                    }
                }
            }
            else
            {
                Console.WriteLine("empty arg.");
            }
        }
    }
}
