using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExcelTool
{
    public class LogManager
    {
        private static LogManager mIns = new LogManager();
        public LogManager Ins()
        {
            return mIns;
        }

        public class SingleLogRecordInfo
        {
            public SingleLogRecordInfo()
            {
            }

            public SingleLogRecordInfo(string title, string content)
            {
                Title = title;
                Content = content;
            }

            public string Title = string.Empty;

            public string Content = string.Empty;
        }

        private List<SingleLogRecordInfo> mLogRecordList = new List<SingleLogRecordInfo>();

        private StringBuilder mBuilder = new StringBuilder();

        private bool mIsRecording = false;

        private Dictionary<string, StringBuilder> mOneErrorMap = new Dictionary<string, StringBuilder>();

        public void ShowMessageBox(string title, string content)
        {
            MessageBox.Show(content, title);
        }

        public void StartRecord()
        {
            if (mIsRecording)
            {
                return;
            }

            mIsRecording = true;
            mLogRecordList.Clear();
        }

        public void RecordInOneError(string title, string content)
        {
            if (string.IsNullOrEmpty(title))
            {
                title = "默认错误";
            }

            if (!mOneErrorMap.ContainsKey(title))
            {
                mOneErrorMap[title] = new StringBuilder();
            }

            var _builder = mOneErrorMap[title];
            _builder.Append(content);
            _builder.Append("\r\n");
        }


        public void PopAndShowOneError(string title)
        {
            if (string.IsNullOrEmpty(title))
            {
                title = "默认错误";
            }

            var _errorContent = mOneErrorMap[title].ToString();

            MessageBox.Show(_errorContent, title);

            mOneErrorMap.Remove(title);
        }

        public void RecordMessage(string title, string content)
        {
            SingleLogRecordInfo _msg = new SingleLogRecordInfo(title, content); //TODO 这里搞个缓存池
            mLogRecordList.Add(_msg);
        }

        public void EndRecordAndShowRecord()
        {
            mIsRecording = false;
            mBuilder.Clear();
            for (int i = 0; i < mLogRecordList.Count; ++i)
            {

            }
        }
    }
}
