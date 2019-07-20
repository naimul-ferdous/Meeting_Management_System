using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MeetingManagementSystem.Reports.Dataset.MeetingDataSetTableAdapters;
using Microsoft.Reporting.WebForms;

namespace MeetingManagementSystem.Reports.viewer
{
    public partial class MeetingViewer : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                sprMeetingInfoTableAdapter tableAdapter = new sprMeetingInfoTableAdapter();
                DataTable dataTable = tableAdapter.GetData();

                ReportDataSource dataSource = new ReportDataSource("spMeeting", dataTable);
                ReportViewer1.LocalReport.ReportPath = "Reports/Design/MeetingDesigner.rdlc";
                ReportViewer1.LocalReport.EnableExternalImages = true;
                ReportViewer1.LocalReport.DataSources.Clear();
                ReportViewer1.LocalReport.DataSources.Add(dataSource);
                ReportViewer1.LocalReport.Refresh();
            }

        }

    }
}
