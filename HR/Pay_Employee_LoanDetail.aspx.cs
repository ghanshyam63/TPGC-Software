using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

public partial class HR_Pay_Employee_LoanDetail : System.Web.UI.Page
{
    Pay_Employee_Loan ObjLoan = new Pay_Employee_Loan();
    string strCompId = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        Session["AccordianId"] = "19";
        Session["HeaderText"] = "HR";

        strCompId = Session["CompId"].ToString();
        if (!IsPostBack)
        {
            GridBind();
            
        }

    }
    void GridBind()
    {
        DataTable Dt = new DataTable();
        Dt = ObjLoan.GetRecord_From_PayEmployeeLoan(strCompId, "0", "Running");
        if (Dt.Rows.Count > 0)
        {
            GridViewLoan.DataSource = Dt;
            GridViewLoan.DataBind();
            Session["dtFilter"] = Dt;
        }
        else
        {
            DataTable Dtclear = new DataTable();
            GridViewLoan.DataSource = Dtclear;
            GridViewLoan.DataBind();
        }
    }
    void Gridbind_LoanDetail(string LoanId)
    {
        DataTable dt = new DataTable();
        dt = ObjLoan.GetRecord_From_PayEmployeeLoanDetail(LoanId);
        if (dt.Rows.Count > 0)
        {
            GridViewLoanDetailrecord.DataSource = dt;
            GridViewLoanDetailrecord.DataBind();
            Session["dtLoanDetail"] = dt;
        }
        else
        {
            DataTable Dtclear = new DataTable();
            GridViewLoanDetailrecord.DataSource = Dtclear;
            GridViewLoanDetailrecord.DataBind();

        }
    }
    protected void GridViewLoan_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridViewLoan.PageIndex = e.NewPageIndex;
        GridBind();
    }
    protected void GridViewLoan_Sorting(object sender, GridViewSortEventArgs e)
    {
        HDFSort.Value = HDFSort.Value == "ASC" ? "DESC" : "ASC";
        DataTable dt = new DataTable();
        dt = (DataTable)Session["dtFilter"];
        DataView dv = new DataView(dt);
        string Query = "" + e.SortExpression + " " + HDFSort.Value + "";
        dv.Sort = Query;
        dt = dv.ToTable();
        Session["dtFilter"] = dt;

        GridViewLoan.DataSource = dt;
        GridViewLoan.DataBind();
    }
    protected void btnEdit_command(object sender, CommandEventArgs e)
    {
        hiddenid.Value = e.CommandArgument.ToString();
        DataTable Dt = new DataTable();
        Dt = ObjLoan.GetRecord_From_PayEmployeeLoan_usingLoanId(strCompId, hiddenid.Value, "Pending");


        Gridbind_LoanDetail(hiddenid.Value);
        Lblemployeeid.Visible = true;
        Lblemployeename.Visible = true;
        SetEmployeeId.Visible = true;
        setemployeename.Visible = true;
        setemployeename.Text = Dt.Rows[0]["Emp_Name"].ToString();
        //    SetEmployeeId.Text = Dt.Rows[0]["Emp_Id"].ToString();
        //}
    }
    protected void GridViewLoanDetailrecord_Sorting(object sender, GridViewSortEventArgs e)
    {
        HdfSortDetail.Value = HdfSortDetail.Value == "ASC" ? "DESC" : "ASC";
        DataTable dt = new DataTable();
        dt = (DataTable)Session["dtLoanDetail"];
        DataView dv = new DataView(dt);
        string Query = "" + e.SortExpression + " " + HDFSort.Value + "";
        dv.Sort = Query;
        dt = dv.ToTable();
        Session["dtLoanDetail"] = dt;

        GridViewLoanDetailrecord.DataSource = dt;
        GridViewLoanDetailrecord.DataBind();
   
    }
    public string GetType(string Type)
    {
        if (Type == "1")
        {
            Type = "January";
        }
         if (Type == "2")
        {
            Type = "February";

        }
         if (Type == "3")
         {
             Type = "March";

         }
         if (Type == "4")
         {
             Type = "April";

         }
         if (Type == "5")
         {
             Type = "May";

         }
         if (Type == "6")
         {
             Type = "June";

         }
         if (Type == "7")
         {
             Type = "July";

         }
         if (Type == "8")
         {
             Type = "August";

         }
         if (Type == "9")
         {
             Type = "september";

         }
         if (Type == "10")
         {
             Type = "October";

         }
         if (Type == "11")
         {
             Type = "November";

         }
         if (Type == "12")
         {
             Type = "December";

         }
        return Type;
    }
}
