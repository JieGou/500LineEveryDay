using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfDemo.Model;

namespace WpfDemo.Db
{
    public class LocalDb
    {
        public LocalDb()
        {
            Init();
        }

        private List<Student> Studentls;

        private void Init()
        {
            Studentls = new List<Student>();

            for (int i = 0; i < 30; i++)
            {
                Studentls.Add(new Student()
                {
                    Id = i, Name = $"Sample{i}"
                });
            }
        }

        public List<Student> GetStudents()
        {
            return Studentls;
        }

        public void AddStudent(Student stu)
        {
            Studentls.Add(stu);
        }

        public void DelStudent(int id)
        {
            var model = Studentls.FirstOrDefault(t => t.Id == id);

            if (model != null)
            {
                Studentls.Remove(model);
            }
        }

        public List<Student> GetStudentsByName(string name)
        {
            return Studentls.Where(q => q.Name.Contains(name)).ToList();
        }
    }
}