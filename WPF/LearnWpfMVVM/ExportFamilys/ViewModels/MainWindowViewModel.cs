using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using Autodesk.Revit.UI;
using Autodesk.Revit.DB;
using System.Windows.Forms;

namespace LearnWpfMVVM.ExportFamilys.ViewModels
{
    class MainWindowViewModel : NotificationObject
    {
        private List<Category> categories = new List<Category>();
        private List<Family> families = null;
        private List<CheckBoxTreeViewModel> itemsList = new List<CheckBoxTreeViewModel>();

        private Document doc = null;
        private List<DirectoryInfo> directoryInfosList = new List<DirectoryInfo>();
        List<Category> categoriesToExport = new List<Category>();
        List<Family> familiesToExport = new List<Family>();
        SaveAsOptions options = new SaveAsOptions();

        private Action _closeAction = null;

        public MainWindowViewModel(ExternalCommandData commandData, Action closeAction)
        {
            doc = commandData.Application.ActiveUIDocument.Document;
            families = FilterFamilies(doc, out categories);
            _closeAction = closeAction;

            InitializeData(); //初始化加载数据

            SelectPathCommand = new DelegateCommand();
            SelectPathCommand.ExecuteAction = new Action<object>(SelectPath);
            StartExportCommand = new DelegateCommand();
            StartExportCommand.ExecuteAction = new Action<object>(StartExport);
            SelectAllCommand = new DelegateCommand();
            SelectAllCommand.ExecuteAction = new Action<object>(SelectAll);
            SelectNoneCommand = new DelegateCommand();
            SelectNoneCommand.ExecuteAction = new Action<object>(SelectNone);

            CreateWallCommand = new DelegateCommand();
            CreateWallCommand.ExecuteAction = new Action<object>(CreateWall);
        }

        public DelegateCommand SelectPathCommand { get; set; }
        public DelegateCommand SelectAllCommand { get; set; }
        public DelegateCommand StartExportCommand { get; set; }
        public DelegateCommand SelectNoneCommand { get; set; }
        public DelegateCommand CreateWallCommand { get; set; }

        private string filePath = @"C:\Users\Administrator\Documents";

        public string FilePath
        {
            get { return filePath; }
            set
            {
                filePath = value;
                this.RaisePropertyChanged("FilePath");
            }
        }

        private ObservableCollection<CheckBoxTreeViewModel> items;

        public ObservableCollection<CheckBoxTreeViewModel> Items
        {
            get { return items; }
            set
            {
                items = value;
                RaisePropertyChanged("Items");
            }
        }

        /// <summary>
        /// 初始化数据
        /// </summary>
        private void InitializeData()
        {
            Items = new ObservableCollection<CheckBoxTreeViewModel>();

            if (families.Count != 0 && categories.Count != 0)
            {
                foreach (var family in families)
                {
                    var child = new CheckBoxTreeViewModel();
                    child.Header = family.Name;
                    child.Tag = family.FamilyCategory.Name;
                    child.Children = null;
                    child.Family = family;
                    itemsList.Add(child); //child为family
                }

                foreach (var category in categories)
                {
                    var parent = new CheckBoxTreeViewModel();
                    parent.Header = category.Name;
                    parent.Children = itemsList.Where(x => x.Tag == category.Name).ToList();
                    parent.Category = category;
                    Items.Add(parent); //parent为category
                }
            }
            else
            {
                return;
            }
        }


        /// <summary>
        /// 过滤族
        /// </summary>
        /// <param name="doc"></param>
        /// <param name="categoriesList"></param>
        /// <returns></returns>
        public List<Family> FilterFamilies(Document doc, out List<Category> categoriesList)
        {
            List<Category> categories = new List<Category>();
            List<Family> familiesList = new List<Family>();
            List<Family> uniqueFamilies = new List<Family>();
            //过滤非系统族
            List<FamilySymbol> familySymbols = new FilteredElementCollector(doc).OfClass(typeof(FamilySymbol))
                .Cast<FamilySymbol>()
                .Where(x => x.Family.IsEditable).ToList();

            foreach (var fs in familySymbols)
            {
                familiesList.Add(fs.Family);
                categories.Add(fs.Category);
            }

            categoriesList = categories.Where((x, i) => categories.FindIndex(z => z.Name == x.Name) == i).ToList();
            uniqueFamilies = familiesList.Where((x, i) => familiesList.FindIndex(z => z.Name == x.Name) == i).ToList();
            return uniqueFamilies;
        }


        public void ExportFamilies(Document doc, Family family, string exportPath)
        {
            //及时释放资源
            using (Document famDoc = doc.EditFamily(family))
            {
                options.OverwriteExistingFile = true;
                famDoc.SaveAs(exportPath + @"\" + $"{family.Name}" + ".rfa", options);
            }
        }

        /// <summary>
        /// 创建文件夹
        /// </summary>
        /// <param name="exPath"></param>
        /// <param name="categoryName"></param>
        /// <returns></returns>
        public DirectoryInfo CreateDirectoy(string exPath, string categoryName)
        {
            string path = Path.Combine(exPath, categoryName);
            DirectoryInfo directoryInfo = Directory.CreateDirectory(path); //如果文件夹不存在，就创建它
            return directoryInfo;
        }

        /// <summary>
        /// 选择路径
        /// </summary>
        /// <param name="parameter"></param>
        private void SelectPath(object parameter)
        {
            //文件夹浏览器
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            DialogResult dialogResult = dialog.ShowDialog();
            if (dialogResult == DialogResult.Cancel) return;
            FilePath = dialog.SelectedPath.Trim();
        }

        /// <summary>
        /// 开始导出
        /// </summary>
        /// <param name="parameter"></param>
        private void StartExport(object parameter)
        {
            //创建文件夹
            if (Items.Count == 0 || itemsList.Count == 0)
            {
                return;
            }

            foreach (var item in Items)
            {
                if (item.IsChecked != false)
                {
                    categoriesToExport.Add(item.Category);
                }
            }

            if (categoriesToExport.Count == 0)
            {
                return;
            }
            else
            {
                foreach (var ca in categoriesToExport)
                {
                    directoryInfosList.Add(CreateDirectoy(this.filePath, ca.Name));
                }
            }

            foreach (var item in itemsList)
            {
                if (item.IsChecked == true)
                {
                    familiesToExport.Add(item.Family);
                }
            }

            if (familiesToExport.Count == 0)
            {
                MessageBox.Show("未选中任何族！");
                return;
            }

            for (int i = 0; i < directoryInfosList.Count; i++)
            {
                foreach (var family in familiesToExport)
                {
                    if (family.FamilyCategory.Name == directoryInfosList[i].Name)
                    {
                        ExportFamilies(doc, family, directoryInfosList[i].FullName);
                    }
                }
            }

            this._closeAction.Invoke(); //委托
            TaskDialog.Show("提示", $"导出完成，共导出{familiesToExport.Count}个族。");
        }

        /// <summary>
        /// 选择全部
        /// </summary>
        /// <param name="parameter"></param>
        private void SelectAll(object parameter)
        {
            if (Items.Count != 0)
            {
                foreach (var item in Items)
                {
                    item.IsChecked = true;
                }
            }
        }

        /// <summary>
        /// 全不选
        /// </summary>
        /// <param name="parameter"></param>
        private void SelectNone(object parameter)
        {
            if (Items.Count != 0)
            {
                foreach (var item in Items)
                {
                    item.IsChecked = false;
                }
            }
        }


        /// <summary>
        /// 开始导出
        /// </summary>
        /// <param name="parameter"></param>
        private void CreateWall(object parameter)
        {
            var curve = Line.CreateBound(new XYZ(0, 0, 0), new XYZ(100, 0, 0));
            var level = doc.ActiveView.GenLevel.Id;
            Transaction ts = new Transaction(doc, "mvvm里执行命令的测试");
            ts.Start();

            Wall.Create(doc, curve, level, false);

            ts.Commit();
        }
    }
}