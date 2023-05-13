import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { SnackBuyTicketDto } from 'src/app/models/Tickets/SnackBuyTicketDto';
import { SnackResultDto } from 'src/app/models/Tickets/SnackResultDto';
import { TicketBuyTicketDto } from 'src/app/models/Tickets/TicketBuyTicketDto';
import { snackGetAllDataDto } from 'src/app/models/Tickets/snackGetAllDataDto';
import { ConcertService } from 'src/app/services/concert.service';
import { TicketsService } from 'src/app/services/tickets.service';
import {SnackService} from 'src/app/services/snack.service';

@Component({
  selector: 'app-buy',
  templateUrl: './buy.component.html'
})
export class BuyComponent implements OnInit {

  selectedTourName: String = "";
  selectedId: Number = 0;
  amount : Number = 0;
  selectedSnacks: Array<Number> = new Array<Number>()
  selectedSnacksNames: Array<String> = [];
  snackList: Array<snackGetAllDataDto> = new Array<snackGetAllDataDto>()


  constructor(private toastr: ToastrService, private ticketService: TicketsService, private service: ConcertService, private router: Router, private activatedRoute: ActivatedRoute , private snacksService: SnackService) { }

  ngOnInit(): void {
    this.activatedRoute.params.subscribe(params => {
      this.service.GetById(params["id"]).subscribe(concert => { 
        this.selectedTourName = concert.tourName
        this.selectedId = concert.concertId
      })
    })
    this.GetData();
  }

  Confirmar() {
    let dto = new TicketBuyTicketDto()
    dto.Amount = this.amount
    dto.concertId = this.selectedId
    dto.snackIds = this.selectedSnacks
    var snacksComprados = ", snacks comprados "
    this.selectedSnacksNames.forEach(element => {
      snacksComprados+= element + ", "
    });
    this.ticketService.Shopping(dto).subscribe(res => {
      let respuesta = this.selectedSnacksNames.length> 0 ? "Ticket comprado con ID: " + res.ticketId + snacksComprados : "Ticket comprado con ID: " + res.ticketId;
      this.toastr.success(respuesta)
    }, error => {
      this.toastr.error(error.error)
    })
  }

  GetData() {
    this.snacksService.Get().subscribe(res => {
      this.snackList = res
    })
  }

  AddSnackToList(id: Number , name: String ) {
    this.selectedSnacks.push(id);
    this.selectedSnacksNames.push(name);
    this.toastr.success("se agrego el snack " + name + " correctamente")
  }
}
