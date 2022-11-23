using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Practice.Data.Model;

namespace Practice.Data
{
    public class PracticeIdentityDbContext : IdentityDbContext<PracticeUser>
    {

        public PracticeIdentityDbContext(DbContextOptions<PracticeIdentityDbContext> options) : base(options) { }

        public DbSet <ToDoList> toDoLists { get; set; } 
        
        
       
    }
}
 