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
            
        }
        public IEnumerable<Snack> GetSnacks()
        {
            return null;
        }
        public Snack GetSnackById(int id)
        {
            return null;
        }
        public void UpdateSnack(Snack snackUpdated)
        {
            
        }
        public void DeleteSnack(int snackId)
        {
            
        }
    }
}
