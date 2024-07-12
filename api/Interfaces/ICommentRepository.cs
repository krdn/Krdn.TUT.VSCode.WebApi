using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using api.Models;

namespace api.Interfaces
{
    public interface ICommentRepository
    {
        Task<Comment> CreateAsync(Comment commentModel);
        Task<List<Comment>> GetAllAsync();
        Task<Comment?> GetByIdAsync(int id);
        Task<Comment?> UpdateAsync(int id, Comment commentModel);
        Task<Comment?> DeleteAsync(int id);
    }
}