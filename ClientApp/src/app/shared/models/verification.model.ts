import { UsageType } from './usage-type.enum'

export interface IVerification {
  patientName: string,
  patientNationalCode: string,
  medicineId: number,
  medicine: string,
  pharmacyId: number,
  pharmacyName: string,
  pharmacyCode: string,
  trackCode: number,
  amount: number,
  prescriptionDate: Date,
  deliveryDate: Date,
  submitDate: Date
}
