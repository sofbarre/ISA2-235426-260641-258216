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
            if (snack.Name.Trim().Equals("") || snack.Price < 0)
            {
                throw new Exception("Precio o nombre de snack no validos");
            }
            else
            {
                this.snackManagement.InsertSnack(snack);
            }

        }

        public Snack GetSnackById(int snackId)
        {
            Snack snack = this.snackManagement.GetSnackById(snackId);
            if (snack == null)
            {
                throw new Exception("Snack no eoncontrado");
            }
            else
            {
                return snack;
            }
        }
        public IEnumerable<Snack> GetSnacks()
        {
            return this.snackManagement.GetSnacks();
        }
        public void UpdateSnack(Snack sanckUpdated)
        {
            if (sanckUpdated.Name.Trim().Equals("") || sanckUpdated.Price < 0)
            {
                throw new Exception("Precio o nombre de snack no validos");
            }
            else
            {
                this.snackManagement.UpdateSnack(sanckUpdated);
            }
        }
        public void DeleteSnack(int snackId)
        {
            if (GetSnackById(snackId) == null)
            {
                throw new Exception("Snack inexistente");
            }
            else
            {
                this.snackManagement.DeleteSnack(snackId);
            }
        }
    }
}
