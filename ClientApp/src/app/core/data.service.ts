import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { ICompany } from '../shared/models/company.model';
import { DailyStat } from '../shared/models/daily-stat';
import { IDistributionDetails } from '../shared/models/distribution-details.model';
import { IDistributionReportItem } from '../shared/models/distribution-report-item.model';
import { HomeReportData } from '../shared/models/home-report';
import { IMedicineCumulative } from '../shared/models/medicine-acc.model';
import { IMedicineDetails } from '../shared/models/medicine-details.model';
import { IMedicine } from '../shared/models/medicine.model';
import { IPatientCumulative } from '../shared/models/patient-acc.model';
import { IPatientDetails } from '../shared/models/patient-details.model';
import { IPatient } from '../shared/models/patient.model';
import { IPharmacy } from '../shared/models/pharmacy.model';
import { IPhysician } from '../shared/models/physician.model';
import { IRequest } from '../shared/models/request.model';
import { ISetting } from '../shared/models/setting';
import { ISpeciality } from '../shared/models/speciality.model';
import { IVerification } from '../shared/models/verification.model';


@Injectable()
export class DataService {

  constructor(private http: HttpClient) { }

  getHomeReport() {
    return this.http.get<HomeReportData>(environment.apiUrl + 'main/report');
  }

  getLastWeekStats() {
    return this.http.get<DailyStat[]>(environment.apiUrl + 'main/lastWeekStats');
  }

  getChartFake(file) {
    return this.http.get(environment.mock + file);
  }

  getCompanies() {
    return this.http.get<ICompany[]>(environment.apiUrl + 'companies');
  }

  getSpecialities() {
    return this.http.get<ISpeciality[]>(environment.apiUrl + 'specialities');
  }

  // Medicines
  getMedicines() {
    return this.http.get<IMedicine[]>(environment.apiUrl + 'medicines');
  }

  deleteMedicine(id: number) {
    return this.http.delete(environment.apiUrl + 'medicines/' + id);
  }

  updateMedicine(medicine: IMedicine) {
    return this.http.put(environment.apiUrl + 'medicines/' + medicine.id, medicine);
  }

  newMedicine(medicine: IMedicine) {
    return this.http.post(environment.apiUrl + 'medicines', medicine);
  }

  getMedicinesCumulativeReport(model) {
    return this.http.post<IMedicineCumulative[]>(environment.apiUrl + 'medicines/cumulative', model);
  }

  getMedicineDetailsReport(model) {
    return this.http.post<IMedicineDetails[]>(environment.apiUrl + 'medicines/details', model);
  }

  // Patients
  getPatients() {
    return this.http.get<IPatient[]>(environment.apiUrl + 'patients');
  }

  deletePatient(id: number) {
    return this.http.delete(environment.apiUrl + 'patients/' + id);
  }

  updatePatient(patient: IPatient) {
    return this.http.put(environment.apiUrl + 'patients/' + patient.id, patient);
  }

  newPatient(patient: IPatient) {
    return this.http.post(environment.apiUrl + 'patients', patient);
  }

  getPatientsCumulativeReport(model) {
    return this.http.post<IPatientCumulative[]>(environment.apiUrl + 'patients/cumulative', model);
  }

  getPatientDetailsReport(model) {
    return this.http.post<IPatientDetails[]>(environment.apiUrl + 'patients/details', model);
  }

  // Pharmacies
  getPharmacies() {
    return this.http.get<IPharmacy[]>(environment.apiUrl + 'pharmacies');
  }

  deletePharmacy(id: number) {
    return this.http.delete(environment.apiUrl + 'pharmacies/' + id);
  }

  updatePharmacy(pharmacy: IPharmacy) {
    return this.http.put(environment.apiUrl + 'pharmacies/' + pharmacy.id, pharmacy);
  }

  newPharmacy(pharmacy: IPharmacy) {
    pharmacy.password = '123456';
    return this.http.post<IPharmacy>(environment.apiUrl + 'pharmacies', pharmacy);
  }

  sendPassword(pharmacy: IPharmacy) {
    return this.http.post<any>(environment.apiUrl + 'pharmacies/sms', pharmacy);
  }

  // Physicians
  getPhysicians() {
    return this.http.get<IPhysician[]>(environment.apiUrl + 'physicians');
  }

  deletePhysician(id: number) {
    return this.http.delete(environment.apiUrl + 'physicians/' + id);
  }

  updatePhysician(physician: IPhysician) {
    return this.http.put(environment.apiUrl + 'physicians/' + physician.id, physician);
  }

  newPhysician(physician: IPhysician) {
    return this.http.post(environment.apiUrl + 'physicians', physician);
  }

  // Request
  getRequests(model) {
    return this.http.post<IRequest[]>(environment.apiUrl + 'main/requests', model);
  }

  // Verifications
  getVerifications(model) {
    return this.http.post<IVerification[]>(environment.apiUrl + 'main/verifications', model);
  }

  // Distributions
  getDistributions(model) {
    return this.http.post<IDistributionReportItem[]>(environment.apiUrl + 'distributions', model);
  }

  newDistribution(model) {
    return this.http.post(environment.apiUrl + 'distributions/new', model);
  }

  getDistributionDetailsReport(model) {
    return this.http.post<IDistributionDetails[]>(environment.apiUrl + 'distributions/details', model);
  }

  // settings
  changePass(model) {
    return this.http.post(environment.apiUrl + 'settings/password', model);
  }

  updateSettings(model: ISetting) {
    return this.http.post(environment.apiUrl + 'settings', model);
  }
}
