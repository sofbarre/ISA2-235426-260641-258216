using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ArenaGestor.Domain
{
    public class TicketSnack
    {
        public Guid Id { get; set; }    
        public Guid idTicket { get; set; }
        public int idSnack { get; set; }

    }
}
