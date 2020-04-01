import { UsageType } from './usage-type.enum';
export interface IPatientCumulative {
  medicineId: number,
  medicine: string,
  pharmacyId: number,
  pharmacyName: string,
  pharmacyCode: string,
  patientId: number,
  patientName: string,
  patientSurName: string,
  patientNationalCode: string,
  amount: number,
  minDate: Date,
  maxDate: Date
}
