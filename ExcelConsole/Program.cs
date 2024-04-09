namespace ExcelConsole
{
    public abstract class ProcessBase
    {
        public string FolderPath = string.Empty;

        public abstract bool Process();
    }

    internal class Program
    {
        private static void Main(string[] args)
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
            Console.WriteLine("5 : 处理 Monster RandomPos 列名字");
            Console.WriteLine("6 : 处理 Monster 的 RandomPopRange 数据");
            Console.WriteLine("7 : 处理 Monster ID 映射 FateNpcID");
            Console.WriteLine("8 : 导出 FateArrayNpcYell 表");
            Console.WriteLine("9 : 处理 Monster 的 FateID");
            Console.WriteLine("9 : 处理 护送Fate 的 NpcID");
            Console.WriteLine("请输入:");
            string? _keyinfo = Console.ReadLine();
            int.TryParse(_keyinfo, out int _inputKey);
            ProcessBase? _process = null;
            switch (_inputKey)
            {
                case 1:
                {
                    _process = new ProcessForFateGuardNpcToMonsterLevel();
                    break;
                }
                case 2:
                {
                    _process = new ProcessForFateMonsterAndReference();
                    break;
                }
                case 3:
                {
                    _process = new ProcessForFateNPCAbout();
                    break;
                }
                case 4:
                {
                    _process = new ProcessForFateNpcToMonster();
                    break;
                }
                case 5:
                {
                    _process = new ProcessForCreateNewColumForFatePopGroup();
                    break;
                }
                case 6:
                {
                    _process = new ProcessForFatePopGroupRandomPopPos();
                    break;
                }
                case 7:
                {
                    _process = new ProcessForUseNewMonsterID();
                    break;
                }
                case 8:
                {
                    _process = new ProcessForExportNpcYellArray();
                    break;
                }
                case 9:
                {
                    _process = new ProcessForMonsterFateID();
                    break;
                }
                case 10:
                {
                    break;
                }
                default:
                {
                    throw new Exception("输入了错误的内容：" + _keyinfo);
                }
            }

            if (_process == null)
            {
                throw new Exception("错误，没有对应的处理方法");
            }

            _process.FolderPath = Path.Combine(targetFolder.FullName, $"{_keyinfo}");
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
}