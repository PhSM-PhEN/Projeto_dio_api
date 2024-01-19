using Microsoft.EntityFrameworkCore;
using Projeto_dio_api.Models;
using Projeto_dio_api.Controllers;

namespace Projeto_dio_api.Context
{
    public class OrganizadorContext : DbContext
    {
        public OrganizadorContext(DbContextOptions<OrganizadorContext> options) : base(options) 
        { 
        }
        public DbSet<Tarefa> Tarefas { get; set; }
    }
}
