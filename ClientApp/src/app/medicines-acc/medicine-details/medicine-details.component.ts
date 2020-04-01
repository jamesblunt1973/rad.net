import { Component, Inject, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { IMedicineDetails } from '../../shared/models/medicine-details.model';

@Component({
  selector: 'app-medicine-details',
  templateUrl: './medicine-details.component.html',
  styleUrls: ['./medicine-details.component.scss']
})
export class MedicineDetailsComponent implements OnInit {

  constructor(public dialogRef: MatDialogRef<MedicineDetailsComponent>,
    @Inject(MAT_DIALOG_DATA) public list: Array<IMedicineDetails>) { }

  ngOnInit() {
  }

}
