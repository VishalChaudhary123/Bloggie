using Bloggie.Web.Data;
using Bloggie.Web.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace Bloggie.Web.Repositories
{
    public class TagRepository : ITagRepository
    {
        private readonly BloggieDbContext _db;

        public TagRepository(BloggieDbContext db)
        {
            _db = db;
        }
        public async Task<Tag> AddTagAsync(Tag tag)
        {
            await _db.Tags.AddAsync(tag);
            await _db.SaveChangesAsync();
            return tag;
        }

        public async Task<Tag?> DeleteTagAsync(Guid id)
        {
           var existingTag = await _db.Tags.FirstOrDefaultAsync(t => t.Id == id);
            if (existingTag != null) 
            {
               _db.Tags.Remove(existingTag);
                await _db.SaveChangesAsync();
                return existingTag;
            }
            return null;
        }

        public async Task<IEnumerable<Tag>> GetAllTagsAsync()
        {
           return await _db.Tags.ToListAsync();
        }

        public async Task<Tag?> GetTagByIdAsync(Guid id)
        {
           return await _db.Tags.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Tag?> UpdateTagAsync(Tag tag)
        {
            var existingTag = await _db.Tags.FirstOrDefaultAsync(t => t.Id == tag.Id);

            if (existingTag != null) 
            {
               existingTag.Name = tag.Name;
                existingTag.DisplayName = tag.DisplayName;

                await _db.SaveChangesAsync();

                return existingTag;
            }
            return null;
           
        }
    }
}
