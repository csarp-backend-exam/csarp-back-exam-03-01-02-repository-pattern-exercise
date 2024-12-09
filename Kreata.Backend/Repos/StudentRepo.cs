using Kreata.Backend.Context;
using Kreata.Backend.Repos.Base;
using Kreta.Shared.Models;

namespace Kreata.Backend.Repos
{
    public class StudentRepo<TDbContext> : BaseRepo<TDbContext, Student>, IStudentRepo where TDbContext : KretaContext
    {
        public StudentRepo(TDbContext? dbContext) : base(dbContext)
        {
        }
    }
}
