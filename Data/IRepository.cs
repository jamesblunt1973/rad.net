using rad.net.Models;
using rad.net.Models.TransferModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace rad.net.Data
{
    public interface IRepository
    {
        Task<HomeReportResult> GetHomeReport();
        Task<IEnumerable<DailyStatResult>> GetLastWeekStatsResult();
        Task<IEnumerable<Company>> GetCompanies();
        Task<IEnumerable<Speciality>> GetSpecialities();
        Task<IEnumerable<RequestsResult>> GetRequests(RequestsParameter data);
        Task<IEnumerable<VerificationsResult>> GetVerifications(VerificationsParameter data);
        // Medicine
        Task<IEnumerable<Medicine>> GetMedicines();
        Task<Medicine> GetMedicine(int id);
        Task<Medicine> NewMedicine(Medicine medicine);
        Task EditMedicine(Medicine medicine);
        Task DeleteMedicine(Medicine medicine);
        Task<IEnumerable<MedicineCumulativeResult>> MedicinesCumulitiveReport(MedicineCumulativeParameter data);
        Task<IEnumerable<MedicineDetailsResult>> MedicineDetailsReport(MedicineCumulativeResult data);
        Task<IEnumerable<Medicine>> GetMedicinesByPatient(int patientId);
        // Patient
        Task<IEnumerable<Patient>> GetPatients();
        Task<Patient> GetPatient(int id);
        Task<Patient> GetPatientByCode(string code);
        Task<Patient> NewPatient(Patient patient);
        Task EditPatient(Patient patient);
        Task DeletePatient(Patient patient);
        Task<IEnumerable<PatientCumulativeResult>> PatientsCumulitiveReport(PatientCumulativeParameter data);
        Task<IEnumerable<PatientDetailsResult>> PatientDetailsReport(PatientCumulativeResult data);
        // Pharmacies
        Task<IEnumerable<Pharmacy>> GetPharmacies();
        Task<Pharmacy> GetPharmacy(int id);
        Task<Pharmacy> GetPharmacyByCode(string code);
        Task<Pharmacy> NewPharmacy(Pharmacy pharmacy);
        Task EditPharmacy(Pharmacy pharmacy);
        Task DeletePharmacy(Pharmacy pharmacy);
        // Physicians
        Task<IEnumerable<Physician>> GetPhysicians();
        Task<Physician> GetPhysician(int id);
        Task<Physician> NewPhysician(Physician physician);
        Task EditPhysician(Physician physician);
        Task DeletePhysician(Physician physician);
        // Settings
        Task<IEnumerable<Setting>> Settings(); // Get all settings
        Task Settings(Setting setting); // Update setting
        Task<string> Setting(string key);
        // Service
        Task<Pharmacy> ServiceLogin(LoginParameter data);
        Task<Physician> GetPhysicianByCode(string medicalCouncilCode);
        Task NewPrescription(Prescription prescription);
        Task NewRawData(RawData rawData);
        // Distributions
        Task<IEnumerable<DistributionReult>> GetDistributions(DistributionsParameter data);
        Task NewDistribution(Distribution distribution);
        Task<IEnumerable<DistributionDatailsReult>> GetDistributionDetails(DistributionsParameter data);
        Task<double> GetMedicineStock(int medicineId, int pharmacyId);
        Task<string> CancelPrescription(long trackNo);
    }
}
