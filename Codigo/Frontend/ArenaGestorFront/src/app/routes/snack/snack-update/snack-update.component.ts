import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { SnackGetDto } from 'src/app/models/Snacks/SnackGetDto';
import { SnackUpdateDto } from 'src/app/models/Snacks/SnackUpdateDto';
import { SnackResultDto } from 'src/app/models/Snacks/SnackResultDto';
import { SnackService } from 'src/app/services/snack.service';

@Component({
  templateUrl: './snack-update.component.html'
})
export class SnackUpdateComponent implements OnInit {

  mode: String = "Editar";
  model: SnackUpdateDto = new SnackUpdateDto();

  constructor(private service: SnackService, private toastr: ToastrService, private router: Router, private id: Number ) { }

  ngOnInit(): void {
      this.service.GetById(this.id).subscribe(snack => {
        this.model.snackId = snack.snackId
        this.model.name = snack.name
        this.model.description=snack.description
        this.model.price = snack.price
      })
  }

  Confirmar() {
    this.service.Update(this.model).subscribe(res => {
      this.toastr.success("Snack actualizado correctamente", "Éxito")
      this.router.navigate(["/administracion/snacks"])
    },
      err => {
        this.toastr.error(err.error, "Error")
      })
  }

}
