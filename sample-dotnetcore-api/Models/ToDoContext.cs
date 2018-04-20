using Microsoft.EntityFrameworkCore;

namespace sampledotnetcoreapi.Models
{
    public class ToDoContext : DbContext
    {
        #region Constructors
        public ToDoContext(DbContextOptions<ToDoContext> options) : base(options)
        {
        }
        #endregion

        #region Properties
        public DbSet<ToDoItem> ToDoItems { get; set; }
        #endregion
    }
}
