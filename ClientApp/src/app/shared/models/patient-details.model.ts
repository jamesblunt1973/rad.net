export interface IPatientDetails {
  id: number,
  deliveryDate: Date,
  prescriptionDate: Date,
  submitDate: Date,
  pharmacyName: string,
  pharmacyCode: string,
  physicianName: string,
  physicianCode: string,
  patientName: string,
  patientNationalCode: string,
  patientCell: string,
  patientTell: string,
  amount: number,
  unit: string
}
