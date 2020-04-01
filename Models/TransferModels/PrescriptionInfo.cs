using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace rad.net.Models
{
    public class PrescriptionData
    {
        public PrescriptionInfo PrescriptionInfo { get; set; }
    }
    public class PrescriptionInfo
    {
        public Payload Payload { get; set; }

        public string PayloadClass { get; set; }

        public string SenderId { get; set; }

        public string PlainId { get; set; }

        public string EncryptedId { get; set; }

        public string ReceiverId { get; set; }

        public string Service { get; set; }

        public string Action { get; set; }

        public string Version { get; set; }

        public string Timeout { get; set; }
    }

    public class Payload
    {
        public OriginVo OriginVo { get; set; }

        public AdmissionInfo AdmissionInfo { get; set; }

        public InsuranceInfo InsuranceInfo { get; set; }

        public PrescriptionCostInfo PrescriptionCostInfo { get; set; }

        public List<MedicationsInfo> MedicationsInfo { get; set; }

        public object CombinationalMedicationsInfo { get; set; }

        public object MedicalEquipmentsInfo { get; set; }

        public PatientInfo PatientInfo { get; set; }

        public PharmacyInfo PharmacyInfo { get; set; }

        public Date PrescriptionDate { get; set; }

        public long TherapyDuration { get; set; }
    }

    public class AdmissionInfo
    {
        public Date AdmissionDate { get; set; }

        public byte AdmissionType { get; set; }

        public HealthCenterInfo HealthCenterInfo { get; set; }
    }

    public class Date
    {
        public string DateContent { get; set; }

        public byte DateCalendarType { get; set; }
    }

    public class HealthCenterInfo
    {
        public int? HealthCenterId { get; set; }

        public byte OrganizationType { get; set; }

        public AttendingDoctorInfo AttendingDoctorInfo { get; set; }
    }

    public class AttendingDoctorInfo
    {
        public SchemaContent MedicalCouncilCode { get; set; }

        public int Speciality { get; set; }
    }

    public class SchemaContent
    {
        public string Content { get; set; }

        public string SchemaAgency { get; set; }
    }

    public class InsuranceInfo
    {
        public SchemaContent InsuranceNumber { get; set; }

        public Date InsuranceBookletExpirationDate { get; set; }

        public int InsuranceBookletPageNumber { get; set; }

        public int InsuranceOrganizationCode { get; set; }

        public bool HaveSupplementaryInsurance { get; set; }
    }

    public class MedicationsInfo
    {
        public AcceptedMedicationItemInfo AcceptedMedicationItemInfo { get; set; }

        public CheckedOutMedicationItemInfo CheckedOutMedicationItemInfo { get; set; }

        public bool UseSubstitutionMedication { get; set; }
    }

    public class AcceptedMedicationItemInfo
    {
        public SchemaContent GenericCode { get; set; }

        public Context FrequencyUsage { get; set; }

        public MedicationCount MedicationCount { get; set; }
    }

    public class Context
    {
        public string Content { get; set; }

        public byte LanguageCode { get; set; }
    }

    public class MedicationCount
    {
        public double Content { get; set; }

        public long UnitCode { get; set; }
    }

    public class CheckedOutMedicationItemInfo
    {
        public SchemaContent NationalDrugCode { get; set; }

        public Context FrequencyUsage { get; set; }

        public MedicationCount DeliveredMedicationCount { get; set; }

        public Date MedicationDeliverDate { get; set; }

        public Costs Costs { get; set; }
    }

    public class Costs
    {
        public Currency TotalCost { get; set; }

        public Currency InsuranceContribution { get; set; }

        public Currency PatientContribution { get; set; }

        public Currency DifferenceOfInsuranceCoverAndPrice { get; set; }

        public Currency SupplementaryContribution { get; set; }

        public Currency PayableByPatient { get; set; }
    }

    public class Currency
    {
        public int Content { get; set; }

        public int CurrencyCode { get; set; }
    }

    public class OriginVo
    {
        public string ProviderName { get; set; }

        public string ProviderSoftwareVersion { get; set; }

        public string ProviderSoftwareUser { get; set; }
    }

    public class PatientInfo
    {
        public Context FullName { get; set; }

        public SchemaContent PatientId { get; set; }

        public Date BirthDate { get; set; }

        public SchemaContent MobilePhoneNumber { get; set; }

        public byte? Gender { get; set; }

        public bool? HasAttendant { get; set; }

        public Context FullNameOfAttendant { get; set; }

        public SchemaContent AttendantId { get; set; }
    }

    public class PharmacyInfo
    {
        public SchemaContent PharmacyCode { get; set; }

        public TechnicalDirectorInfo TechnicalDirectorInfo { get; set; }

        public SchemaContent PharmacistFounderInfo { get; set; }

        public SchemaContent DispenserInfo { get; set; }
    }

    public class TechnicalDirectorInfo
    {
        public SchemaContent TMedicalCouncilCode { get; set; }

        public bool HasDeputy { get; set; }

        public SchemaContent Deputy { get; set; }
    }

    public class PrescriptionCostInfo
    {
        public Currency TotalCost { get; set; }

        public Currency TechnicalCost { get; set; }

        public Currency Discount { get; set; }

        public OtherExpense OtherExpense { get; set; }

        public Currency Tax { get; set; }

        public Currency PayableByPatient { get; set; }

        public Currency Automation { get; set; }

        public Currency Subsidy { get; set; }

        public Currency InternetConfirmation { get; set; }
    }

    public class OtherExpense
    {
        public Context TitleOfMedicalExpenses { get; set; }

        public Currency CostOfMedicalExpenses { get; set; }
    }
}
