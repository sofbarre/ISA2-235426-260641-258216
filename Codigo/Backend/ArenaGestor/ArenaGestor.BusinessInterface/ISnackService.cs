using ArenaGestor.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArenaGestor.BusinessInterface
{
    public interface ISnackService
    {
        public void InsertSnack(Snack snack);
        public Snack GetSnackById(int snackId);
        public IEnumerable<Snack> GetSnacks();
        public void UpdateSnack(Snack sanckUpdated);
        public void DeleteSnack(int snackId);
    }
}
