import { Component, Inject, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { IPatientDetails } from '../../shared/models/patient-details.model';

@Component({
  selector: 'app-patient-details',
  templateUrl: './patient-details.component.html',
  styleUrls: ['./patient-details.component.scss']
})
export class PatientDetailsComponent implements OnInit {

  constructor(public dialogRef: MatDialogRef<PatientDetailsComponent>,
    @Inject(MAT_DIALOG_DATA) public list: Array<IPatientDetails>) { }

  ngOnInit() {
  }

}
