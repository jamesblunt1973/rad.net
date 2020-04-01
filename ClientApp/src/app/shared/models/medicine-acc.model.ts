import { UsageType } from './usage-type.enum';

export interface IMedicineCumulative {
  medicineId: number,
  medicine: string,
  pharmacyId: number,
  pharmacyName: string,
  pharmacyCode: string,
  amount: number,
  minDate: Date,
  maxDate: Date
}
