using gm_safety_thirdparty.Utility;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace gm_safety_thirdparty.Models
{
    public class Employee
    {
        [JsonPropertyName("first_name")]
        public string? FirstName { get; set; }

        [JsonPropertyName("last_name")]
        public string? LastName { get; set; }

        [JsonPropertyName("date_of_joining")]
        public DateOnly DateOfJoining { get; set; }

        [JsonPropertyName("contribution_level")]
        public string? ContributionLevel { get; set; }

        [JsonPropertyName("direct_manager")]
        public string? DirectManager { get; set; }

        [JsonPropertyName("direct_manager_email")]
        public string? DirectManagerEmail { get; set; }

        [JsonPropertyName("direct_manager_employee_id")]
        [JsonConverter(typeof(StringToNullableIntConverter))]
        public int? DirectManagerEmployeeId { get; set; }

        [JsonPropertyName("division")]
        public string? Division { get; set; }

        [JsonPropertyName("employee_id")]
        [JsonConverter(typeof(StringToNullableIntConverter))]
        public int? EmployeeId { get; set; }

        [JsonPropertyName("sub_employee_type")]
        public string? SubEmployeeType { get; set; }

        [JsonPropertyName("employee_status")]
        //[JsonConverter(typeof(StringToBoolConverter))]
        public string? EmployeeStatus { get; set; }

        [JsonPropertyName("employee_type")]
        //[JsonConverter(typeof(StringToNullableIntConverter))]
        public string? EmployeeType { get; set; }

        //[Column("employee_status", TypeName = "text")]
        //public string? EmployeeStatus { get; set; }

        //[Column("employee_type", TypeName = "text")]
        //public string? EmployeeType { get; set; }


        [JsonPropertyName("job_level")]
        public string? JobLevel { get; set; }

        [JsonPropertyName("ps_group")]
        public string? PsGroup { get; set; }

        [JsonPropertyName("ps_level")]
        public string? PsLevel { get; set; }

        [JsonPropertyName("sbu")]
        public string? SBU { get; set; }

        [JsonPropertyName("date_of_birth")]
        public DateOnly DateOfBirth { get; set; }

        [JsonPropertyName("company_email_id")]
        public string? CompanyEmailId { get; set; }

        [JsonPropertyName("gender")]
        public string? Gender { get; set; }

        [JsonPropertyName("office_city")]
        public string? OfficeCity { get; set; }

        [JsonPropertyName("office_location")]
        public string? OfficeLocation { get; set; }

        [JsonPropertyName("office_region")]
        public string? OfficeRegion { get; set; }

        [JsonPropertyName("office_state")]
        public string? OfficeState { get; set; }

        [JsonPropertyName("office_mobile_no")]
        public string? OfficeMobileNo { get; set; }

        [JsonPropertyName("gmmco_dept")]
        public string? GmmcoDept { get; set; }

        [JsonPropertyName("safety_role")]
        public int? SafetyRole { get; set; }


        // DB-only metadata
        public int? CREATED_ID { get; set; } = null;
        public DateTime? CREATED_DATE { get; set; } = null;
        public int? UPDATED_ID { get; set; } = null;
        public DateTime? UPDATED_DATE { get; set; } = null;
        public string? RECORD_SOURCE_NAME { get; set; } = null;
    }
}