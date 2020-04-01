import { UsageType } from './usage-type.enum';

export interface IMedicine {
  id?: number,
  genericCode: string,
  ircCode: string,
  faName: string,
  enName: string,
  brandFaName: string,
  brandEnName: string,
  usageType: UsageType,
  unit: string,
  dosage: number,
  usagePeriod: number,
  maximumUsageAmount: number,
  producerId: number,
  distributorId: number,
  edit?: boolean
}
