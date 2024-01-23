using ExcelTool;
using OfficeOpenXml;

namespace ExcelConsole
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string currentPath = System.AppDomain.CurrentDomain.BaseDirectory;
            Console.WriteLine(currentPath);

            var _targetFolder = Path.Combine(currentPath, "../FileFolder");
            DirectoryInfo targetFolder = new DirectoryInfo(_targetFolder);
            if (!targetFolder.Exists)
            {
                Console.WriteLine($"错误，{targetFolder} 路径不存在");
                return;
            }

            Console.WriteLine($"输入数字：");
            Console.WriteLine("1 : 处理 Fate 相关的 FateNpc 转化后的 Monster 的等级");
            Console.WriteLine("请输入:");
            var _keyinfo = Console.ReadKey();
            ProcessBase? _process = null;
            switch (_keyinfo.KeyChar)
            {
                case '1':
                {
                    _process = new ProcessForFateGuardNpcToMonsterLevel();
                    break;
                }
                default:
                {
                    throw new Exception("输入了错误的内容：" + _keyinfo.KeyChar);
                }
            }

            if (_process == null)
            {
                throw new Exception("错误，没有对应的处理方法");
            }

            _process.FolderPath = targetFolder.FullName;
            Console.WriteLine();
            try
            {
                if (_process.Process())
                {
                    Console.WriteLine("处理完成");
                }
                else
                {
                    Console.WriteLine("处理失败，请查看报错");
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            Console.ReadKey();
        }
    }

    public abstract class ProcessBase
    {
        public string FolderPath = string.Empty;

        public abstract bool Process();
    }
}