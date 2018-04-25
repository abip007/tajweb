using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication2
{
    public partial class rptDues : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                ReportViewer1.Reset();
                DataTable dt = getData();
                ReportDataSource ds = new ReportDataSource("DataSet1", dt);
                ReportViewer1.LocalReport.DataSources.Add(ds);
                ReportViewer1.LocalReport.ReportPath = "report/rptDues.rdlc";
            }
        }
        private DataTable getData()
        {
            DataTable dt = new DataTable();
            string conString = System.Configuration.ConfigurationManager.ConnectionStrings["MMS"].ConnectionString;
            using (SqlConnection con = new SqlConnection(conString))
            {

                SqlCommand cmd = new SqlCommand("spAllCustomerLeader", con);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);

            }
            return dt;
        }
    }
}