using ArenaGestor.DataAccessInterface;
using ArenaGestor.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileSystemGlobbing.Internal.PatternContexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArenaGestor.DataAccess.Managements
{
    public class SnackManagement : ISnackManagement
    {
        private readonly DbContext context;
        private readonly DbSet<Snack> snacks;

        public SnackManagement(DbContext context)
        {
            this.context = context;
            this.snacks = context.Set<Snack>();

        }
        public void InsertSnack(Snack snack)
        {
            snacks.Add(snack);
            context.SaveChanges();

        }
        public IEnumerable<Snack> GetSnacks()
        {
            return snacks.ToList().OrderBy(x => x.SnackId);
        }
        public Snack GetSnackById(int id)
        {
            return snacks.FirstOrDefault(snack => snack.SnackId == id);
        }
        public void UpdateSnack(Snack snackUpdated)
        {
            Snack snackDB = snacks.FirstOrDefault(snack => snack.SnackId == snackUpdated.SnackId);
            snackDB.Name = snackUpdated.Name;
            snackDB.Description = snackUpdated.Description;
            snackDB.Price = snackUpdated.Price;
            snacks.Update(snackDB);
            context.SaveChanges();
        }
        public void DeleteSnack(int snackId)
        {
            Snack snackDB = snacks.FirstOrDefault(snack => snack.SnackId == snackId);
            snacks.Remove(snackDB);
            context.SaveChanges();
        }
    }
}
