using ArenaGestor.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArenaGestor.DataAccessInterface
{
    public interface ISnackManagement
    {
        public void InsertSnack(Snack snack);
        public IEnumerable<Snack> GetSnacks();
        public Snack GetSnackById(int id);
        public void UpdateSnack(Snack snackUpdated);
        public void DeleteSnack(int snackId);

    }
}
