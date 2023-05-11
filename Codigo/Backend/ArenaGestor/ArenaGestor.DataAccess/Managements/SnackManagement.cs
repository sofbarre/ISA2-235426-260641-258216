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
    public class SnackManagement 
    {
        private readonly ArenaGestorContext context;

        public SnackManagement(ArenaGestorContext context)
        {
            this.context = context;
        }
        public void InsertSnack(Snack snack)
        {
            context.Snacks.Add(snack);
            context.SaveChanges();

        }
        public IEnumerable<Snack> GetSnacks()
        {
            return context.Snacks.ToList().OrderBy(x => x.SnackId);
        }
        public Snack GetSnackById(int id)
        {
            return context.Snacks.FirstOrDefault(snack => snack.SnackId == id);
        }
        public void UpdateSnack(Snack snackUpdated)
        {
            Snack snackDB = context.Snacks.FirstOrDefault(snack => snack.SnackId == snackUpdated.SnackId);
            snackDB.Name = snackUpdated.Name;
            snackDB.Description = snackUpdated.Description;
            snackDB.Price = snackUpdated.Price;
            context.Snacks.Update(snackDB);
            context.SaveChanges();
        }
        public void DeleteSnack(int snackId)
        {
            Snack snackDB = context.Snacks.FirstOrDefault(snack => snack.SnackId == snackId);
            context.Snacks.Remove(snackDB);
            context.SaveChanges();
        }
    }
}
