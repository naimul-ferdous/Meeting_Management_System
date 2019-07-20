using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WebForms;
using MeetingManagementSystem.Models;
using MeetingManagementSystem.Reports.Dataset;
using MeetingManagementSystem.Reports.Dataset.DateWiseMeetingTableAdapters;
using MeetingManagementSystem.Reports.Dataset.MeetingDataSetTableAdapters;

namespace MeetingManagementSystem.Reports.Viewer
{
    public partial class DateWiseMeeting : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)

            {
                try
                {
                    int? venueId;
                    int? employeeId;
                    string sDate = Request.QueryString["startDate"];
                    DateTime startDate = Convert.ToDateTime(sDate);
                    string eDate = Request.QueryString["endDate"];
                    DateTime endDate = Convert.ToDateTime(eDate);
                    venueId = Convert.ToInt32(Request.QueryString["VenueId"]);
                    employeeId = Convert.ToInt32(Request.QueryString["EmployeeId"]);

                    if (venueId == 0)
                    {
                        venueId = null;
                    }
                    if (employeeId == 0)
                    {
                        employeeId = null;
                    }
                    spDateWiseMeetingTableAdapter tableAdapter = new spDateWiseMeetingTableAdapter();

                    DataTable reportData = tableAdapter.GetData(startDate, endDate, venueId, employeeId);
                    ReportViewer1.LocalReport.ReportPath = "Reports/Design/DateWiseMeeting.rdlc";
                    ReportDataSource rds = new ReportDataSource("spDateWiseMeeting", reportData);
                    ReportViewer1.LocalReport.EnableExternalImages = true;
                    ReportViewer1.LocalReport.DataSources.Clear();
                    ReportViewer1.LocalReport.DataSources.Add(rds);

                    var parameters = new List<ReportParameter>
                    {
                        new ReportParameter("StartDate", startDate.ToString("dd-MMM-yyyy").ToUpper()),
                        new ReportParameter("EndDate", endDate.ToString("dd-MMM-yyyy").ToUpper())
                    };
                    ReportViewer1.LocalReport.SetParameters(parameters);
                    ReportViewer1.LocalReport.Refresh();
                }
                catch (Exception exception)
                {
                    return;
                }

            }
        }

    }
}


