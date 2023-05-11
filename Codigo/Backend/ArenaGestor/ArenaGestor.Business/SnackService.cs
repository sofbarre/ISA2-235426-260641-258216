using ArenaGestor.BusinessInterface;
using ArenaGestor.DataAccess.Managements;
using ArenaGestor.DataAccessInterface;
using ArenaGestor.Domain;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArenaGestor.Business
{
    public class SnackService 
    {
        private readonly SnackManagement snackManagement;

        public SnackService(SnackManagement snackManagement)
        {
            this.snackManagement = snackManagement;
        }   

        public void InsertSnack(Snack snack)
        {
            
        }

        public Snack GetSnackById(int snackId)
        {
            return null;
        }
        public IEnumerable<Snack> GetSnacks()
        {
            return null;
        }
        public void UpdateSnack(Snack sanckUpdated)
        {

        }
        public void DeleteSnack(int snackId)
        {
            
        }
    }
}
