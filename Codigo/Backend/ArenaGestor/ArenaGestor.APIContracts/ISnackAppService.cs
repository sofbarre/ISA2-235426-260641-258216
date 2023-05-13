using ArenaGestor.APIContracts.Artist;
using ArenaGestor.APIContracts.Snack;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArenaGestor.APIContracts
{
    public interface ISnackAppService
    {
        IActionResult GetSnacks();
        public IActionResult PostSnack([FromBody] SnackPostDto snack);
        public IActionResult PutSnack([FromRoute]int snackId,[FromBody] SnackPutDto snack);
        public IActionResult GetSnackById([FromRoute] int snackId);
        public IActionResult DeleteSnack([FromRoute] int snackId);
    }
}
