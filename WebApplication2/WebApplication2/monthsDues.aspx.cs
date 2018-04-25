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
    public partial class monthsDues : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            ReportViewer1.Reset();
            DataTable dt = getData(Convert.ToInt32(TextBox1.Text));
            ReportDataSource ds = new ReportDataSource("DataSet1", dt);
            ReportParameter[] rparam = new ReportParameter[]
            {
                new ReportParameter("num",TextBox1.Text)
                
            };
            ReportViewer1.LocalReport.DataSources.Add(ds);
            ReportViewer1.LocalReport.ReportPath = "report/UnpaidBillByMonth.rdlc";
            ReportViewer1.LocalReport.SetParameters(rparam);
        }
        private DataTable getData(int num)
        {
            DataTable dt = new DataTable();
            string conString = System.Configuration.ConfigurationManager.ConnectionStrings["MMS"].ConnectionString;
            using (SqlConnection con = new SqlConnection(conString))
            {

                SqlCommand cmd = new SqlCommand("[NumberOfUnpaidBillByCustomer]", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@num", SqlDbType.Int).Value = num;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);

            }
            return dt;
        }
    }
}