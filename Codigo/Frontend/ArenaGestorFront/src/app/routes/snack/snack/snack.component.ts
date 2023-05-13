import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { SnackGetDto } from 'src/app/models/Snacks/SnackGetDto';
import { SnackResultDto } from 'src/app/models/Snacks/SnackResultDto';
import { SnackService } from 'src/app/services/snack.service';
@Component({
  selector: 'app-snack',
  templateUrl: './snack.component.html'
})
export class SnackComponent implements OnInit {

    snackList: Array<SnackResultDto> = new Array<SnackResultDto>()
    filter: String = "";
    snackToDelete: Number = 0;
  
    constructor(private toastr: ToastrService, private service: SnackService, private router: Router) { }
  
    ngOnInit(): void {
      this.GetData()
    }
  
    GetData() {
      let filterDto: SnackGetDto = { name: this.filter };
      this.service.Get(filterDto).subscribe(res => {
        this.snackList = res
      })
    }
  
    SetSnackToDelete(id: Number) {
      this.snackToDelete = id;
    }
  
    Delete() {
      this.service.Delete(this.snackToDelete).subscribe(res => {
        this.toastr.success("Artista eliminado correctamente", "Éxito")
        this.GetData();
      },
        err => {
          this.toastr.error(err.error, "Error")
        })
    }
  }
  
