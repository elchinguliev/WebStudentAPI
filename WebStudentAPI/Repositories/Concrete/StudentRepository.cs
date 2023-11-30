using System.Linq.Expressions;
using WebStudentAPI.Data;
using WebStudentAPI.Entities;
using WebStudentAPI.Repositories.Abstract;

namespace WebStudentAPI.Repositories.Concrete
{
    public class StudentRepository : IStudentRepository
    {
        private readonly StudentDBContext _context;

        public StudentRepository(StudentDBContext context)
        {
            _context = context;
        }

        public void Add(Student entity)
        {
            _context.Students.Add(entity);
            _context.SaveChanges();
        }

        public void Delete(Student entity)
        {
            _context.Students.Remove(entity);
            _context.SaveChanges();
        }

        public Student Get(Expression<Func<Student, bool>> expression)
        {
            var student = _context.Students.SingleOrDefault(expression);
            return student;
        }

        public IEnumerable<Student> GetAll()
        {
            var students = _context.Students;
            return students;
        }

        public void Update(Student entity)
        {
            _context.Students.Update(entity);
            _context.SaveChanges();
        }
    }
}
