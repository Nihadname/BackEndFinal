﻿using BackEndFinal.Models;
using System.Linq.Expressions;

namespace BackEndFinal.Services.interfaces
{
    public interface ICourseService
    {
        Task<List<Course>> GetAlCourseAsync(int skip, int take, params Expression<Func<Course, object>>[] includes);
        Task<Course> GetCourseByIdAsync(int? id);
        Task<List<Course>> SearchCoursesAsync(string keyword, int skip, int take, params Expression<Func<Course, object>>[] includes);

        Task AddCourseAsync(Course Course);
        Task UpdateCourseAsync(Course Course);
        Task DeleteCourseAsync(Course Course);
    }
}
