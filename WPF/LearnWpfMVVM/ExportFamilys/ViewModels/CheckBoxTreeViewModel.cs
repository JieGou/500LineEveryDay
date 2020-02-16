using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.DB;

namespace LearnWpfMVVM.ExportFamilys.ViewModels
{
    class CheckBoxTreeViewModel : NotificationObject
    {
        public string Tag { set; get; }

        private bool? _IsChecked = false;
        private List<CheckBoxTreeViewModel> _children;
        public Family Family { get; set; }
        public Category Category { get; set; }

        public bool? IsChecked
        {
            get => _IsChecked;
            set
            {
                SetIsChecked(value, true, true);
            }
        }

        public string Header { get; set; }

        public List<CheckBoxTreeViewModel> Children
        {
            get => _children;
            set
            {
                _children = value;
                SetParentValue();
            }
        }

        public CheckBoxTreeViewModel Parent { get; private set; }

        /// <summary>
        /// 设置节点IsChecked的值
        /// </summary>
        /// <param name="value"></param>
        /// <param name="updateChildren"></param>
        /// <param name="updateParent"></param>
        private void SetIsChecked(bool? value, bool updateChildren, bool updateParent)
        {
            if (value == _IsChecked) return;

            _IsChecked = value;

            if (updateChildren && _IsChecked.HasValue && Children != null)
            {
                this.Children.ForEach(c => c.SetIsChecked(_IsChecked, true, false));
            }

            if (updateChildren && Parent != null)
            {
                Parent.VerifyCheckState();
            }

            this.RaisePropertyChanged("IsChecked");
        }

        /// <summary>
        /// 验证并设置父级节点的IsChecked的值
        /// </summary>
        private void VerifyCheckState()
        {
            bool? state = null;

            for (int i = 0; i < this.Children.Count; ++i)
            {
                bool? current = this.Children[i].IsChecked;

                if (i == 0)
                {
                    state = current;
                }
                else if (state != current)
                {
                    state = null;
                    break;
                }
            }

            this.SetIsChecked(state, false, true);
        }


        /// <summary>
        /// 数据初始化时设置父节点的值
        /// </summary>
        private void SetParentValue()
        {
            if (this.Children != null)
            {
                this.Children.ForEach(ch => ch.Parent = this);
            }
        }
    }
}