import { Component, OnInit, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { IDistributionDetails } from '../../shared/models/distribution-details.model';

@Component({
  selector: 'app-distribution-details',
  templateUrl: './distribution-details.component.html',
  styleUrls: ['./distribution-details.component.scss']
})
export class DistributionDetailsComponent implements OnInit {

  constructor(public dialogRef: MatDialogRef<DistributionDetailsComponent>,
    @Inject(MAT_DIALOG_DATA) public list: Array<IDistributionDetails>) { }

  ngOnInit() {
  }

}
