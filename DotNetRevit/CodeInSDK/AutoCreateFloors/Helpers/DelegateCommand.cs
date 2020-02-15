using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace RevitDevelopmentFoudation.CodeInSDK.AutoCreateFloors.Helpers
{
    public class DelegateCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;
        public Action<object> ExecuteAction { get; set; }
        public Func<object, bool> CanExecuteFunc { get; set; }

        //用来判断这个命令能不能执行
        public bool CanExecute(object parameter)
        {
            if (CanExecuteFunc == null)
            {
                return true;
            }
            return CanExecuteFunc(parameter);
        }
        //当命令执行的时候，需要做什么事
        public void Execute(object parameter)
        {
            if (ExecuteAction == null)
            {
                return;
            }
            ExecuteAction(parameter);//把这个要执行的命令委托给了这个委托所指向的方法
        }
    }
}