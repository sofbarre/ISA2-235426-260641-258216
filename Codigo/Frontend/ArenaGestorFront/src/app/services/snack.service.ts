import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { SnackGetDto } from '../models/Snacks/SnackGetDto'; 
import { SnackPostDto } from '../models/Snacks/SnackPostDto';
import { SnackUpdateDto } from '../models/Snacks/SnackUpdateDto';
import { SnackResultDto } from '../models/Snacks/SnackResultDto';

@Injectable({
    providedIn: 'root'
  })
  export class SnackService {
    private apiUrl: string

    constructor(private http: HttpClient) {
      this.apiUrl = environment.apiURL + "snacks"
    }
  
    Get(filter?: SnackGetDto): Observable<Array<SnackResultDto>> {
        let url = this.apiUrl
        if (filter && filter.name.length > 0) {
          url = url + "?name=" + filter.name
        }
        return this.http.get<Array<SnackResultDto>>(url)
    }

    GetById(id: Number): Observable<SnackResultDto> {
      console.log("eeeee" +id);
        return this.http.get<SnackResultDto>(this.apiUrl + "/" + id.toString())
      }
    
      Insert(artist: SnackPostDto): Observable<SnackResultDto> {
        return this.http.post<SnackResultDto>(this.apiUrl, artist)
      }
    
      Update(snack: SnackUpdateDto): Observable<SnackResultDto> {
        return this.http.put<SnackResultDto>(this.apiUrl+"/"+snack.snackId, snack)
      }
    
      Delete(id: Number) {
        return this.http.delete(this.apiUrl + "/" + id.toString())
      }
    
}