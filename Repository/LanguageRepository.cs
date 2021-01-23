using Microsoft.EntityFrameworkCore;
using practise.BookStoreApp.Data;
using practise.BookStoreApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace practise.BookStoreApp.Repository
{
    public class LanguageRepository
    {
        private readonly BookStoreContext _context = null;
        public LanguageRepository(BookStoreContext context)
        {
            _context = context;
        }

        public async Task<List<LanguageModel>> GetLanguages()
        {
            return  await _context.Language.Select(x => new LanguageModel()
            {
                Id=x.Id,
                Description= x.Description,
                Text = x.Text

            }).ToListAsync();

        }
    }
}
