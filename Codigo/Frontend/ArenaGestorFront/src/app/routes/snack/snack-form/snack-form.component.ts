import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { SnackGetDto } from 'src/app/models/Snacks/SnackGetDto';
import { SnackPostDto } from 'src/app/models/Snacks/SnackPostDto';
import { SnackResultDto } from 'src/app/models/Snacks/SnackResultDto';
import { SnackService } from 'src/app/services/snack.service';

@Component({
  templateUrl: './snack-form.component.html',
})
export class SnackFormComponent implements OnInit {

  mode: String = "Insertar";
  model: SnackPostDto = new SnackPostDto()

  constructor(private service: SnackService, private toastr: ToastrService, private router: Router) { }

  ngOnInit(): void {
    
  }

  Confirmar() {
    this.service.Insert(this.model).subscribe(res => {
      this.toastr.success("Artista agregado correctamente", "Éxito")
      this.router.navigate(["/administracion/snacks"])
    },
      err => {
        this.toastr.error(err.error, "Error")
      })
  }
}
