import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Observable } from 'rxjs';
import { TicketGetTicketResultDto } from '../models/Tickets/TicketGetTicketResultDto';
import { TicketScanTicketDto } from '../models/Tickets/TicketScanTicketDto';
import { TicketScanTicketResultDto } from '../models/Tickets/TicketScanTicketResultDto';
import { TicketBuyTicketDto } from '../models/Tickets/TicketBuyTicketDto';
import { TicketBuyTicketResultDto } from '../models/Tickets/TicketBuyTicketResultDto';
import { TicketSellTicketDto } from '../models/Tickets/TicketSellTicketDto';
import { TicketSellTicketResultDto } from '../models/Tickets/TicketSellTicketResultDto';
import { SnackBuyTicketDto } from '../models/Tickets/SnackBuyTicketDto';
import { SnackResultDto } from '../models/Tickets/SnackResultDto';
import { snackGetAllDataDto } from '../models/Tickets/snackGetAllDataDto';

@Injectable({
  providedIn: 'root'
})
export class snacksService {

  private apiUrl: string

  constructor(private http: HttpClient) {
    this.apiUrl = environment.apiURL + "tickets"
  }

  // GetOfLoggedUser(): Observable<Array<TicketGetTicketResultDto>> {
  //   return this.http.get<Array<TicketGetTicketResultDto>>(this.apiUrl)
  // }

  Shopping(snack: Array<SnackBuyTicketDto>): Observable<Array<SnackResultDto>> {
    return this.http.post<Array<SnackResultDto>>(this.apiUrl + "/ShoppingSnack", snack)
  }

  Get(): Observable<Array<snackGetAllDataDto>> {
    let url = this.apiUrl;

    return this.http.get<Array<snackGetAllDataDto>>(url)
  }


}
