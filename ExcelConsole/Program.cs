using ExcelTool;
using OfficeOpenXml;

namespace ExcelConsole
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string currentPath = System.AppDomain.CurrentDomain.BaseDirectory;
            var _targetFolderPath = Path.Combine(currentPath, "../FileFolder");
            DirectoryInfo targetFolder = new DirectoryInfo(_targetFolderPath);
            if (!targetFolder.Exists)
            {
                Console.WriteLine($"错误，{targetFolder} 路径不存在");
                return;
            }

            Console.WriteLine($"输入数字：");
            Console.WriteLine("1 : 处理 FateGuard 相关的 FateNpc 转化后的 Monster 的等级");
            Console.WriteLine("2 : 处理端游 FateGuard 导入后 创建物相关");
            Console.WriteLine("3 : 处理端游 FateGuard 导入后 Npc相关");
            Console.WriteLine("4 : 处理端游 FateNpc 全部导入 Monster");
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
                case '2':
                {
                    _process = new ProcessForFateMonsterAndReference();
                    break;
                }
                case '3':
                {
                    _process = new ProcessForFateNPCAbout();
                    break;
                }
                case '4':
                {
                    _process = new ProcessForFateNpcToMonster();
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
            catch (Exception ex)
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