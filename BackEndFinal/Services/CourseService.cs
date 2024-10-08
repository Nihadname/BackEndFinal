﻿using BackEndFinal.Models;
using BackEndFinal.Services.interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using WebApplication11.Repositories.interfaces;

namespace BackEndFinal.Services
{
    public class CourseService : ICourseService
    {
        private readonly IRepository<Course> _courseService;

        public CourseService(IRepository<Course> courseService)
        {
            _courseService = courseService;
        }

        public Task AddCourseAsync(Course Course)
        {
            if(Course == null) throw new ArgumentNullException(nameof(Course));
            return _courseService.AddAsync(Course);
        }

        public Task DeleteCourseAsync(Course Course)
        {
            if (Course == null) throw new ArgumentNullException(nameof(Course));
            return _courseService.DeleteAsync(Course);
        }

        public Task<List<Course>> GetAlCourseAsync(int skip, int take, params Expression<Func<Course, object>>[] includes)
        {
            return _courseService.GetAllAsync(skip, take, includes);
        }

        public IQueryable<Course> GetAllCourseQuery()
        {
            return _courseService.GetAllQuery();
        }

        public Task<Course> GetCourseByIdAsync(int? id, params Expression<Func<Course, object>>[] includes)
        {
           return _courseService.GetByIdAsync(id,includes);
        }

        public async Task<List<Course>> SearchCoursesAsync(string keyword, int skip, int take, params Expression<Func<Course, object>>[] includes)
        {
            IQueryable<Course> query = _courseService.GetAllQuery();

            if (!string.IsNullOrEmpty(keyword))
            {
                query = query.Where(c => c.Title.Contains(keyword) || c.Description.Contains(keyword));
            }

            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            if (skip > 0)
            {
                query = query.Skip(skip);
            }

            if (take > 0)
            {
                query = query.Take(take);
            }

            return await query.ToListAsync();
        }


        public Task UpdateCourseAsync(Course Course)
        {
            return _courseService.UpdateAsync(Course);
        }
    }
}
