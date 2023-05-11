import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { SnackBuyTicketDto } from 'src/app/models/Tickets/SnackBuyTicketDto';
import { SnackResultDto } from 'src/app/models/Tickets/SnackResultDto';
import { TicketBuyTicketDto } from 'src/app/models/Tickets/TicketBuyTicketDto';
import { snackGetAllDataDto } from 'src/app/models/Tickets/snackGetAllDataDto';
import { ConcertService } from 'src/app/services/concert.service';
import { snacksService } from 'src/app/services/snacks.service';
import { TicketsService } from 'src/app/services/tickets.service';

@Component({
  selector: 'app-buy',
  templateUrl: './buy.component.html'
})
export class BuyComponent implements OnInit {

  selectedTourName: String = "";
  selectedId: Number = 0;
  amount : Number = 0;
  selectedSnacks: Array<SnackBuyTicketDto> = new Array<SnackBuyTicketDto>()
  snackList: Array<snackGetAllDataDto> = new Array<snackGetAllDataDto>()


  constructor(private toastr: ToastrService, private ticketService: TicketsService, private service: ConcertService, private router: Router, private activatedRoute: ActivatedRoute , private snacksService: snacksService) { }

  ngOnInit(): void {
    this.activatedRoute.params.subscribe(params => {
      this.service.GetById(params["id"]).subscribe(concert => { 
        this.selectedTourName = concert.tourName
        this.selectedId = concert.concertId
      })
    })
  }

  Confirmar() {
    let dto = new TicketBuyTicketDto()
    dto.Amount = this.amount
    dto.concertId = this.selectedId
    this.ticketService.Shopping(dto).subscribe(res => {
      this.toastr.success("Ticket comprado con ID: " + res.ticketId)
    }, error => {
      this.toastr.error(error.error)
    })
  }


  ConfirmarCompraSnacks() {
    let dto = this.selectedSnacks;
    this.snacksService.Shopping(dto).subscribe(res => {
      this.toastr.success("Snacks comprados")
    }, error => {
      this.toastr.error(error.error)
    })
  }

  GetData() {
    this.snacksService.Get().subscribe(res => {
      this.snackList = res
    })
  }

  AddSnackToList(id: Number) {
    var snack = new SnackBuyTicketDto();
    snack.snackId =id;
    this.selectedSnacks.push(snack);
  }
}
