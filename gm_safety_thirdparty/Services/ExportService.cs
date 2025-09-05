
using ClosedXML.Excel;
using gm_safety_thirdparty.Models;
using System.IO;

public class ExportService
{
    public byte[] ExportToExcel(List<Employee> employees)
    {
        using var workbook = new XLWorkbook();
        var worksheet = workbook.Worksheets.Add("Employees");

        // Add headers
        worksheet.Cell(1, 1).Value = "Employee ID";
        worksheet.Cell(1, 2).Value = "First Name";
        worksheet.Cell(1, 3).Value = "Last Name";
        worksheet.Cell(1, 4).Value = "Date of Joining";
        worksheet.Cell(1, 5).Value = "Contribution Level";
        worksheet.Cell(1, 6).Value = "Direct Manager";
        worksheet.Cell(1, 7).Value = "Manager Email";
        worksheet.Cell(1, 8).Value = "Manager Emp ID";
        worksheet.Cell(1, 9).Value = "Division";
        worksheet.Cell(1, 10).Value = "Sub Employee Type";
        worksheet.Cell(1, 11).Value = "Employee Status";
        worksheet.Cell(1, 12).Value = "Employee Type";
        worksheet.Cell(1, 13).Value = "Job Level";
        worksheet.Cell(1, 14).Value = "PS Group";
        worksheet.Cell(1, 15).Value = "PS Level";
        worksheet.Cell(1, 16).Value = "SBU";
        worksheet.Cell(1, 17).Value = "Date of Birth";
        worksheet.Cell(1, 18).Value = "Company Email";
        worksheet.Cell(1, 19).Value = "Gender";
        worksheet.Cell(1, 20).Value = "Office City";
        worksheet.Cell(1, 21).Value = "Office Location";
        worksheet.Cell(1, 22).Value = "Office Region";
        worksheet.Cell(1, 23).Value = "Office State";
        worksheet.Cell(1, 24).Value = "Office Mobile";
        worksheet.Cell(1, 25).Value = "GMMCO Dept";
        worksheet.Cell(1, 26).Value = "Safety Role";
        worksheet.Cell(1, 27).Value = "Record Source";

        // Add data
        for (int i = 0; i < employees.Count; i++)
        {
            var emp = employees[i];
            worksheet.Cell(i + 2, 1).Value = emp.EmployeeId;
            worksheet.Cell(i + 2, 2).Value = emp.FirstName;
            worksheet.Cell(i + 2, 3).Value = emp.LastName;
            worksheet.Cell(i + 2, 4).Value = emp.DateOfJoining.ToString("dd-MM-yyyy");
            worksheet.Cell(i + 2, 5).Value = emp.ContributionLevel;
            worksheet.Cell(i + 2, 6).Value = emp.DirectManager;
            worksheet.Cell(i + 2, 7).Value = emp.DirectManagerEmail;
            worksheet.Cell(i + 2, 8).Value = emp.DirectManagerEmployeeId;
            worksheet.Cell(i + 2, 9).Value = emp.Division;
            worksheet.Cell(i + 2, 10).Value = emp.SubEmployeeType;
            worksheet.Cell(i + 2, 11).Value = emp.EmployeeStatus;
            worksheet.Cell(i + 2, 12).Value = emp.EmployeeType;
            worksheet.Cell(i + 2, 13).Value = emp.JobLevel;
            worksheet.Cell(i + 2, 14).Value = emp.PsGroup;
            worksheet.Cell(i + 2, 15).Value = emp.PsLevel;
            worksheet.Cell(i + 2, 16).Value = emp.SBU;
            worksheet.Cell(i + 2, 17).Value = emp.DateOfBirth.ToString("dd-MM-yyyy");
            worksheet.Cell(i + 2, 18).Value = emp.CompanyEmailId;
            worksheet.Cell(i + 2, 19).Value = emp.Gender;
            worksheet.Cell(i + 2, 20).Value = emp.OfficeCity;
            worksheet.Cell(i + 2, 21).Value = emp.OfficeLocation;
            worksheet.Cell(i + 2, 22).Value = emp.OfficeRegion;
            worksheet.Cell(i + 2, 23).Value = emp.OfficeState;
            worksheet.Cell(i + 2, 24).Value = emp.OfficeMobileNo;
            worksheet.Cell(i + 2, 25).Value = emp.GmmcoDept;
            worksheet.Cell(i + 2, 26).Value = emp.SafetyRole;
            worksheet.Cell(i + 2, 27).Value = emp.RECORD_SOURCE_NAME;
        }

        using var stream = new MemoryStream();
        workbook.SaveAs(stream);
        return stream.ToArray();
    }

}
