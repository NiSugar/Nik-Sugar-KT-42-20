﻿using Microsoft.EntityFrameworkCore;
using NiSugarKT_42_20.Database;
using NiSugarKT_42_20.Filters.StudentFilters;
using NiSugarKT_42_20.Models;

namespace NiSugarKT_42_20.Interfaces.StudentInterfaces
{
    public interface IStudentService
    {
        public Task<Student[]> GetStudentsByGroupAsync(StudentGroupFilter filter, CancellationToken cancellationToken);
        public Task<Student[]> GetStudentsByGroupIDAsync(StudentGroupFiltersID filter, CancellationToken cancellationToken);
        public Task<Student[]> GetStudentsByExistAsync(StudentExistFilter filter, CancellationToken cancellationToken);
        public Task<Student[]> GetStudentsByFIOAsync(StudentFIOFilter filter, CancellationToken cancellationToken);
    }

    public class StudentService : IStudentService
    {
        private readonly StudentDbContext _dbContext;
        public StudentService(StudentDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public Task<Student[]> GetStudentsByGroupAsync(StudentGroupFilter filter, CancellationToken cancellationToken = default)
        {
            var students = _dbContext.Set<Student>().Where(w => w.Group.GroupName == filter.GroupName).ToArrayAsync(cancellationToken);

            return students;
        }

        //public Task<Student[]> GetStudentsByGroupIDAsync(StudentGroupFilter filter, CancellationToken cancellationToken = default)
        //{
        //    var students = _dbContext.Set<Student>().Where(w => w.GroupId.GroupId == filter.GroupId).ToArrayAsync(cancellationToken);

        //    return students;
        //}

        public Task<Student[]> GetStudentsByGroupIDAsync(StudentGroupFiltersID filter, CancellationToken cancellationToken = default)
        {
            var students = _dbContext.Set<Student>().Where(w => w.Group.GroupId == filter.GroupId).ToArrayAsync(cancellationToken);

            return students;
        }

        public Task<Student[]> GetStudentsByExistAsync(StudentExistFilter filter, CancellationToken cancellationToken = default)
        {
            var students = _dbContext.Set<Student>().Where(w => w.StudentExist == filter.Exist).ToArrayAsync(cancellationToken);

            return students;
        }
        public Task<Student[]> GetStudentsByFIOAsync(StudentFIOFilter filter, CancellationToken cancellationToken = default)
        {
            var students = _dbContext.Set<Student>().Where(w => (w.FirstName == filter.FIO) || (w.MiddleName == filter.FIO) || (w.LastName == filter.FIO)).ToArrayAsync(cancellationToken);

            return students;
        }
    }
}
