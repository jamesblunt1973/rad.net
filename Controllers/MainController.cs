using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using rad.net.Data;
using rad.net.Models;

namespace rad.net.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MainController : ControllerBase
    {
        private readonly IRepository repo;

        public MainController(IRepository repository)
        {
            repo = repository;
        }

        [HttpGet("report")]
        public async Task<IActionResult> HomeReport()
        {
            return Ok(await repo.GetHomeReport());
        }

        [HttpGet("lastWeekStats")]
        public async Task<IActionResult> LastWeekStats()
        {
            return Ok(await repo.GetLastWeekStatsResult());
        }

        [HttpPost("requests")]
        public async Task<IActionResult> GetRequests(RequestsParameter data)
        {
            return Ok(await repo.GetRequests(data));
        }

        [HttpPost("verifications")]
        public async Task<IActionResult> GetVerifications(VerificationsParameter data)
        {
            return Ok(await repo.GetVerifications(data));
        }

        [HttpPost("/api/v1/login")]
        [AllowAnonymous]
        public async Task<IActionResult> ServiceLogin(LoginParameter data)
        {
            Pharmacy pharmacy;
            ServiceLoginResult result;
            try
            {
                pharmacy = await repo.ServiceLogin(data);
                if (pharmacy == null)
                {
                    result = new ServiceLoginResult()
                    {
                        Message = "نام کاربری یا کلمه عبور اشتباه است",
                        MessageCode = 401,
                        Result = null
                    };
                }
                else
                {
                    result = new ServiceLoginResult()
                    {
                        Message = "احراز هویت موفقیت آمیز بود",
                        MessageCode = 1,
                        Result = new TokenWrapper()
                        {
                            Token = "Bearer " + Helpers.GenerateJwtToken(pharmacy.Code)
                        }
                    };
                }
            }
            catch
            {
                result = new ServiceLoginResult()
                {
                    Message = "خطای داخلی سرور ، در صورت تکرار لطفا با پشتیبانی تماس حاصل بفرمایید",
                    MessageCode = 500,
                    Result = null
                };
            }
            return Ok(result);
        }

        [HttpPost("/api/v1/prescription")]
        public async Task<IActionResult> Prescription(PrescriptionData data)
        {
            try
            {
                var info = data.PrescriptionInfo;

                var code = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
                if (string.IsNullOrEmpty(code))
                    return ErrorResult("توکن نامعتبر است", 403);

                var pharmacy = await repo.GetPharmacyByCode(code);
                if (pharmacy == null)
                    return ErrorResult("توکن نامعتبر است، اطلاعات داروخانه یافت نشد", 403);

                if (info == null || info.Payload == null)
                    return ErrorResult("داده های ورودی نامعتبر می‌باشند", 400);

                var patientInfo = info.Payload.PatientInfo;
                if (patientInfo == null ||
                    patientInfo.PatientId == null ||
                    string.IsNullOrEmpty(patientInfo.PatientId.Content))
                    return ErrorResult("کد ملی بیمار یافت نشد", 400);

                if (info.Payload.MedicationsInfo.Count == 0)
                    return ErrorResult("اطلاعات دارو یافت نشد", 400);

                var patient = await repo.GetPatientByCode(patientInfo.PatientId.Content);

                if (patient == null)
                    patient = await repo.NewPatient(CreatePatientEntity(patientInfo));

                var now = DateTime.Now;
                var medicines = await repo.GetMedicinesByPatient(patient.Id);
                var illegalDrug = "";
                var prescriptionMedicines = new List<PrescriptionMedicine>();
                var status = Status.NoTracked; // وضعیت پیش فرض
                foreach (var med in info.Payload.MedicationsInfo)
                {
                    var genericCode = med.AcceptedMedicationItemInfo.GenericCode;
                    var medicine = medicines.SingleOrDefault(a => a.GenericCode == genericCode.Content);
                    if (medicine != null)
                    {
                        status = Status.Verified; // اگر یکی از داروها، در لیست وجود داشت وضعیت به تایید شده تغیر میکند
                        //var usedAmount = medicine.PrescriptionMedicines
                        //    .Where(a => a.Prescription.PatientId == patient.Id &&
                        //        !a.Prescription.RawData.IsCanceled &&
                        //        a.Prescription.DeliveryDate > now.AddDays(-a.Medicine.UsagePeriod))
                        //    .Sum(a => a.Amount);
                        double usedAmount = 0;
                        foreach (var pm in medicine.PrescriptionMedicines)
                        {
                            if (pm.Prescription.PatientId != patient.Id)
                                continue;
                            if (pm.Prescription.RawData.IsCanceled)
                                continue;
                            if (pm.Prescription.DeliveryDate <= now.AddDays(-pm.Medicine.UsagePeriod))
                                continue;
                            usedAmount += pm.Amount;
                        }
                        if (usedAmount + med.AcceptedMedicationItemInfo.MedicationCount.Content > medicine.MaximumUsageAmount)
                        {
                            illegalDrug += " /" + medicine.FaName + "، " + medicine.Dosage;
                            status = Status.Unverified; // چنانچه داروی مورد نظر تایید نشود، وضعیت به تایید نشده تغیر میکند
                        }
                        prescriptionMedicines.Add(new PrescriptionMedicine()
                        {
                            Amount = med.AcceptedMedicationItemInfo.MedicationCount.Content,
                            MedicineId = medicine.Id,
                        });
                    }
                }

                var rnd = new Random(now.Millisecond);
                var rawData = new RawData()
                {
                    IsCanceled = false,
                    Json = JsonSerializer.Serialize(info),
                    PharmacyId = pharmacy.Id,
                    ReceivedDate = now,
                    Status = status,
                    TrackCode = Helpers.Random(rnd)
                };
                await repo.NewRawData(rawData);

                if (status == Status.NoTracked)
                {
                    // تمام اقلام نسخه مجاز می‌باشند چون هیچ یک از داروهای آن در لیست داروها یافت نشد
                    return Ok(new ServicePrescriptionResult()
                    {
                        IllegalDrug = "",
                        MessageCode = 1,
                        Message = "کلیه اقلام نسخه مجاز می‌باشند.",
                        TrackNo = rawData.TrackCode
                    });
                }
                else if (status == Status.Unverified)
                {
                    // برخی اقلام فاقد اعتبار مجاز می‌باشند
                    return Ok(new ServicePrescriptionResult()
                    {
                        IllegalDrug = illegalDrug,
                        MessageCode = 0,
                        Message = " برخی اقلام فاقد اعتبار مجاز می‌باشند.",
                        TrackNo = rawData.TrackCode
                    });
                }

                // در غیر اینصورت باید نسخه و داروهای آن ثبت شوند

                var medicalCouncilCode = info.Payload.AdmissionInfo?.HealthCenterInfo?.AttendingDoctorInfo?.MedicalCouncilCode?.Content;

                Physician physician = null;
                if (!string.IsNullOrEmpty(medicalCouncilCode))
                    physician = await repo.GetPhysicianByCode(medicalCouncilCode);
                if (physician == null)
                {
                    var specialities = await repo.GetSpecialities();
                    physician = await repo.NewPhysician(CreatePhysicianEntity(info.Payload.AdmissionInfo.HealthCenterInfo.AttendingDoctorInfo, specialities));
                }

                var prescription = new Prescription()
                {
                    DeliveryDate = info.Payload.AdmissionInfo.AdmissionDate.DateContent.ToGregoryDate() ?? now,
                    PatientId = patient.Id,
                    PharmacyId = pharmacy.Id,
                    PhysicianId = physician.Id,
                    PrescriptionDate = info.Payload.PrescriptionDate.DateContent.ToGregoryDate() ?? now,
                    SubmitDate = now,
                    PrescriptionMedicines = prescriptionMedicines,
                    Distributions = new List<Distribution>()
                };
                foreach (var pm in prescriptionMedicines)
                {
                    var lastStock = await repo.GetMedicineStock(pm.MedicineId, prescription.PharmacyId);
                    prescription.Distributions.Add(
                        new Distribution()
                        {
                            Amount = -pm.Amount,
                            DateTime = now,
                            MedicineId = pm.MedicineId,
                            PharmacyId = prescription.PharmacyId,
                            PrescriptionId = prescription.Id,
                            Stock = lastStock - pm.Amount
                        }
                    );
                }
                rawData.Prescription = prescription;
                prescription.RawData = rawData;
                await repo.NewPrescription(prescription);
                return Ok(new ServicePrescriptionResult()
                {
                    IllegalDrug = "",
                    MessageCode = 1,
                    Message = "کلیه اقلام نسخه مجاز می‌باشند.",
                    TrackNo = rawData.TrackCode
                });
            }
            catch (Exception ex)
            {
                return ErrorResult(ex.Message, 500);
            }
        }

        [HttpGet("/api/v1/medicines")]
        public async Task<IActionResult> Medicines()
        {
            var code = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(code))
                return ErrorResult("توکن نامعتبر است", 403);
            return Ok(await repo.GetMedicines());
        }

        [HttpDelete("/api/v1/prescription")]
        public async Task<IActionResult> Cancel(CancelPrescriptionParameter data)
        {
            var code = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(code))
                return ErrorResult("توکن نامعتبر است", 403);
            if (code != data.PharmacyCode)
                return ErrorResult("کد داروخانه نامعتبر است", 400);
            var trackNo = Convert.ToInt64(data.TrackNo);
            var result = await repo.CancelPrescription(trackNo);
            return Ok(new { result });
        }

        private Physician CreatePhysicianEntity(AttendingDoctorInfo info, IEnumerable<Speciality> specialities)
        {
            var speciality = specialities.FirstOrDefault(a => a.Code == info.Speciality);
            return new Physician()
            {
                Code = info.MedicalCouncilCode.Content,
                Name = "",
                SpecialityId = speciality.Id
            };
        }

        private IActionResult ErrorResult(string message, int code)
        {
            return StatusCode(code, new ServicePrescriptionResult()
            {
                IllegalDrug = "",
                MessageCode = code,
                Message = message,
                TrackNo = 0
            });
        }

        private Patient CreatePatientEntity(PatientInfo patientInfo)
        {
            var genderInfo = patientInfo.Gender;
            bool? gender = null;
            if (genderInfo > 0 && genderInfo != null)
            {
                gender = genderInfo == 2;
            }
            var fullName = patientInfo.FullName?.Content;
            return new Patient()
            {
                BirthDate = patientInfo.BirthDate?.DateContent.ToGregoryDate(),
                Cell = patientInfo.MobilePhoneNumber?.Content ?? "",
                Gender = gender,
                NationalCode = patientInfo.PatientId.Content,
                Name = fullName,
                Tell = ""
            };
        }
    }
}