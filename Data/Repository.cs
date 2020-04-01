using Dapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using rad.net.Models;
using rad.net.Models.TransferModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace rad.net.Data
{
    public class Repository : IRepository
    {
        private readonly IConfiguration configuration;
        private readonly DataContext context;
        private readonly ILogger<Repository> logger;

        public Repository(IConfiguration configuration, DataContext context, ILogger<Repository> logger)
        {
            this.configuration = configuration;
            this.context = context;
            this.logger = logger;
        }

        public async Task<HomeReportResult> GetHomeReport()
        {
            // Procedure name: GetHomeReport 
            // use dapper
            string connectionString = configuration.GetConnectionString("Default");

            var commandText = "GetHomeReport";
            var command = new CommandDefinition(commandText, commandType: CommandType.StoredProcedure);

            var homeReport = new HomeReportResult();
            try
            {
                using SqlConnection connection = new SqlConnection(connectionString);
                homeReport = await connection.QueryFirstAsync<HomeReportResult>(command);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.GetExceptionMessage());
                return null;
            }

            return homeReport;
        }

        public async Task<IEnumerable<DailyStatResult>> GetLastWeekStatsResult()
        {
            // Procedure name: GetLastWeekStats 
            // use dapper
            string connectionString = configuration.GetConnectionString("Default");

            var commandText = "GetLastWeekStats";
            var command = new CommandDefinition(commandText, commandType: CommandType.StoredProcedure);

            IEnumerable<DailyStatResult> lastWeekStats;
            try
            {
                using SqlConnection connection = new SqlConnection(connectionString);
                lastWeekStats = await connection.QueryAsync<DailyStatResult>(command);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.GetExceptionMessage());
                return null;
            }

            return lastWeekStats;
        }

        public async Task<IEnumerable<RequestsResult>> GetRequests(RequestsParameter data)
        {
            // Procedure name: GetRequests 
            // use dapper
            string connectionString = configuration.GetConnectionString("Default");

            var commandText = "GetRequests";
            var command = new CommandDefinition(commandText, data, commandType: CommandType.StoredProcedure);

            IEnumerable<RequestsResult> result;
            try
            {
                using SqlConnection connection = new SqlConnection(connectionString);
                result = await connection.QueryAsync<RequestsResult>(command);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.GetExceptionMessage());
                return null;
            }

            return result;
        }

        public async Task<IEnumerable<VerificationsResult>> GetVerifications(VerificationsParameter data)
        {
            // Procedure name: GetVerifications 
            // use dapper
            string connectionString = configuration.GetConnectionString("Default");

            var commandText = "GetVerifications";
            var command = new CommandDefinition(commandText, data, commandType: CommandType.StoredProcedure);

            IEnumerable<VerificationsResult> result;
            try
            {
                using SqlConnection connection = new SqlConnection(connectionString);
                result = await connection.QueryAsync<VerificationsResult>(command);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.GetExceptionMessage());
                return null;
            }

            return result;
        }

        public async Task<IEnumerable<Company>> GetCompanies()
        {
            return await context.Companies.ToListAsync();
        }

        public async Task<IEnumerable<Speciality>> GetSpecialities()
        {
            return await context.Specialities.ToListAsync();
        }

        public async Task<IEnumerable<Medicine>> GetMedicines()
        {
            return await context.Medicines.ToListAsync();
        }

        public async Task<Medicine> NewMedicine(Medicine medicine)
        {
            await context.Medicines.AddAsync(medicine);
            await context.SaveChangesAsync();
            return medicine;
        }

        public async Task EditMedicine(Medicine medicine)
        {
            context.Entry(medicine).State = EntityState.Modified;
            await context.SaveChangesAsync();
        }

        public async Task DeleteMedicine(Medicine medicine)
        {
            context.Entry(medicine).State = EntityState.Deleted;
            await context.SaveChangesAsync();
        }

        public async Task<Medicine> GetMedicine(int id)
        {
            return await context.Medicines.FindAsync(id);
        }

        public async Task<IEnumerable<Medicine>> GetMedicinesByPatient(int patientId)
        {
            //return await context.Medicines
            //    .Include(a => a.PrescriptionMedicines.Where(b => b.Prescription.PatientId == patientId))
            //    .ThenInclude(a => a.Prescription)
            //    .ToListAsync();
            var q = await context.Medicines
                .Include(a => a.PrescriptionMedicines)
                .ThenInclude(a => a.Prescription)
                .ThenInclude(a => a.RawData)
                .Select(a => new
                {
                    Medicine = a,
                    PrescriptionMedicines = a.PrescriptionMedicines
                        .Where(b => b.Prescription.PatientId == patientId)
                        .Select(b => new
                        {
                            b.Medicine,
                            PrescriptionMedicine = b
                        }),
                }).ToListAsync();
            return q.Select(a => a.Medicine);
        }

        public async Task<IEnumerable<MedicineCumulativeResult>> MedicinesCumulitiveReport(MedicineCumulativeParameter data)
        {
            // Procedure name: GetMedicinesAmount 
            // use dapper
            string connectionString = configuration.GetConnectionString("Default");

            var commandText = "GetMedicinesAmount";
            var command = new CommandDefinition(commandText, data, commandType: CommandType.StoredProcedure);

            IEnumerable<MedicineCumulativeResult> result;
            try
            {
                using SqlConnection connection = new SqlConnection(connectionString);
                result = await connection.QueryAsync<MedicineCumulativeResult>(command);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.GetExceptionMessage());
                return null;
            }

            return result;
        }

        public async Task<IEnumerable<MedicineDetailsResult>> MedicineDetailsReport(MedicineCumulativeResult data)
        {
            // Procedure name: GetMedicineDetails 
            // use dapper
            string connectionString = configuration.GetConnectionString("Default");

            var commandText = "GetMedicineDetails";
            var command = new CommandDefinition(commandText, new { data.MedicineId, data.PharmacyId, data.MinDate, data.MaxDate }, commandType: CommandType.StoredProcedure);

            IEnumerable<MedicineDetailsResult> result;
            try
            {
                using SqlConnection connection = new SqlConnection(connectionString);
                result = await connection.QueryAsync<MedicineDetailsResult>(command);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.GetExceptionMessage());
                return null;
            }

            return result;
        }

        public async Task<IEnumerable<PatientCumulativeResult>> PatientsCumulitiveReport(PatientCumulativeParameter data)
        {
            // Procedure name: GetMedicinesAmount 
            // use dapper
            string connectionString = configuration.GetConnectionString("Default");

            var commandText = "GetPatientsAmount";
            var command = new CommandDefinition(commandText, data, commandType: CommandType.StoredProcedure);

            IEnumerable<PatientCumulativeResult> result;
            try
            {
                using SqlConnection connection = new SqlConnection(connectionString);
                result = await connection.QueryAsync<PatientCumulativeResult>(command);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.GetExceptionMessage());
                return null;
            }

            return result;
        }

        public async Task<IEnumerable<PatientDetailsResult>> PatientDetailsReport(PatientCumulativeResult data)
        {
            // Procedure name: GetPatientDetails 
            // use dapper
            string connectionString = configuration.GetConnectionString("Default");

            var commandText = "GetPatientDetails";
            var command = new CommandDefinition(commandText, new { data.MedicineId, data.PharmacyId, data.PatientId, data.MinDate, data.MaxDate }, commandType: CommandType.StoredProcedure);

            IEnumerable<PatientDetailsResult> result;
            try
            {
                using SqlConnection connection = new SqlConnection(connectionString);
                result = await connection.QueryAsync<PatientDetailsResult>(command);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.GetExceptionMessage());
                return null;
            }

            return result;
        }

        public async Task<IEnumerable<Patient>> GetPatients()
        {
            return await context.Patients.ToListAsync();
        }

        public async Task<Patient> GetPatient(int id)
        {
            return await context.Patients.FindAsync(id);
        }

        public async Task<Patient> GetPatientByCode(string code)
        {
            return await context.Patients.SingleOrDefaultAsync(a => a.NationalCode == code);
        }

        public async Task<Patient> NewPatient(Patient patient)
        {
            await context.Patients.AddAsync(patient);
            await context.SaveChangesAsync();
            return patient;
        }

        public async Task EditPatient(Patient patient)
        {
            context.Entry(patient).State = EntityState.Modified;
            await context.SaveChangesAsync();
        }

        public async Task DeletePatient(Patient patient)
        {
            context.Entry(patient).State = EntityState.Deleted;
            await context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Pharmacy>> GetPharmacies()
        {
            return await context.Pharmacies.ToListAsync();
        }

        public async Task<Pharmacy> GetPharmacy(int id)
        {
            return await context.Pharmacies.FindAsync(id);
        }

        public async Task<Pharmacy> GetPharmacyByCode(string code)
        {
            return await context.Pharmacies.SingleOrDefaultAsync(a => a.Code == code);
        }

        public async Task<Pharmacy> NewPharmacy(Pharmacy pharmacy)
        {
            await context.Pharmacies.AddAsync(pharmacy);
            await context.SaveChangesAsync();
            return pharmacy;
        }

        public async Task EditPharmacy(Pharmacy pharmacy)
        {
            context.Entry(pharmacy).State = EntityState.Modified;
            await context.SaveChangesAsync();
        }

        public async Task DeletePharmacy(Pharmacy pharmacy)
        {
            context.Entry(pharmacy).State = EntityState.Deleted;
            await context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Physician>> GetPhysicians()
        {
            return await context.Physicians.ToListAsync();
        }

        public async Task<Physician> GetPhysician(int id)
        {
            return await context.Physicians.FindAsync(id);
        }

        public async Task<Physician> NewPhysician(Physician physician)
        {
            await context.Physicians.AddAsync(physician);
            await context.SaveChangesAsync();
            return physician;
        }

        public async Task EditPhysician(Physician physician)
        {
            context.Entry(physician).State = EntityState.Modified;
            await context.SaveChangesAsync();
        }

        public async Task DeletePhysician(Physician physician)
        {
            context.Entry(physician).State = EntityState.Deleted;
            await context.SaveChangesAsync();
        }

        public async Task<Physician> GetPhysicianByCode(string code)
        {
            return await context.Physicians.SingleOrDefaultAsync(a => a.Code == code);
        }

        public async Task<Pharmacy> ServiceLogin(LoginParameter data)
        {
            var encPass = Helpers.Encrypt(data.Username, data.Password);
            return await context.Pharmacies.SingleOrDefaultAsync(a => a.Code == data.Username && a.Password == encPass);
        }

        public async Task NewPrescription(Prescription prescription)
        {
            await context.Prescriptions.AddAsync(prescription);
            await context.SaveChangesAsync();
        }

        public async Task NewRawData(RawData rawData)
        {
            await context.RawDatas.AddAsync(rawData);
            await context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Setting>> Settings()
        {
            return await context.Settings.ToListAsync();
        }
        public async Task Settings(Setting setting)
        {
            var s = await context.Settings.SingleOrDefaultAsync(a => a.Key == setting.Key);
            if (s == null)
                await context.Settings.AddAsync(setting);
            else
                s.Value = setting.Value;
            await context.SaveChangesAsync();
        }
        public async Task<string> Setting(string key)
        {
            string value = "";
            var setting = await context.Settings.SingleOrDefaultAsync(a => a.Key == key);
            if (setting != null)
                value = setting.Value;
            return value;
        }

        public async Task<IEnumerable<DistributionReult>> GetDistributions(DistributionsParameter data)
        {
            // Procedure name: GetDistributions
            // use dapper
            string connectionString = configuration.GetConnectionString("Default");

            var commandText = "GetDistributions";
            var command = new CommandDefinition(commandText, data, commandType: CommandType.StoredProcedure);

            IEnumerable<DistributionReult> result;
            try
            {
                using SqlConnection connection = new SqlConnection(connectionString);
                result = await connection.QueryAsync<DistributionReult>(command);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.GetExceptionMessage());
                return null;
            }

            return result;
        }
        public async Task NewDistribution(Distribution distribution)
        {
            distribution.Stock = await GetMedicineStock(distribution.MedicineId, distribution.PharmacyId) + distribution.Amount;
            await context.Distributions.AddAsync(distribution);
            await context.SaveChangesAsync();
        }
        public async Task<IEnumerable<DistributionDatailsReult>> GetDistributionDetails(DistributionsParameter data)
        {
            var q = context.Distributions.Where(a => a.PharmacyId == data.PharmacyId.Value && a.MedicineId == data.MedicineId.Value);
            if (data.DateFrom.HasValue)
                q = q.Where(a => a.DateTime >= data.DateFrom.Value.Date);
            if (data.DateTo.HasValue)
            {
                var to = data.DateTo.Value.Date.AddDays(1);
                q = q.Where(a => a.DateTime <= to);
            }
            return await q.Select(a => new DistributionDatailsReult()
            {
                Amount = a.Amount,
                DateTime = a.DateTime,
                PrescriptionId = a.PrescriptionId,
                Stock = a.Stock
            }).ToListAsync();
        }

        public async Task<double> GetMedicineStock(int medicineId, int pharmacyId)
        {
            double stock = 0;
            var lastDistribution = await context.Distributions
                .OrderByDescending(a => a.Id)
                .FirstOrDefaultAsync(a => a.MedicineId == medicineId && a.PharmacyId == pharmacyId);
            if (lastDistribution != null)
                stock = lastDistribution.Stock;
            return stock;
        }

        public async Task<string> CancelPrescription(long trackNo)
        {
            var now = DateTime.Now;
            var rawData = await context.RawDatas
                .Include(a => a.Prescription)
                .Include(a => a.Prescription.PrescriptionMedicines)
                .SingleOrDefaultAsync(a => a.TrackCode == trackNo);
            if (rawData == null)
                return "کد پیگیری معتبر نیست.";
            rawData.IsCanceled = true;
            var prescription = rawData.Prescription;
            foreach (var pm in prescription.PrescriptionMedicines)
            {
                // TODO: اضافه به موجودی
                var lastStock = await GetMedicineStock(pm.MedicineId, prescription.PharmacyId);
                prescription.Distributions.Add(
                    new Distribution()
                    {
                        Amount = pm.Amount,
                        DateTime = now,
                        MedicineId = pm.MedicineId,
                        PharmacyId = prescription.PharmacyId,
                        PrescriptionId = prescription.Id,
                        Stock = lastStock
                    }
                );

            }
            await context.SaveChangesAsync();
            return "";
        }
    }
}
