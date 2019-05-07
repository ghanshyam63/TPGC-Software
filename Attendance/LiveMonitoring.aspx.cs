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


public partial class Attendance_LiveMonitoring : System.Web.UI.Page
{

    Att_AttendanceLog objAttLog = new Att_AttendanceLog();
    SystemParameter objSys = new SystemParameter();
    protected void Page_Load(object sender, EventArgs e)
    {

        if (Session["UserId"] == null)
        {
            Response.Redirect("~/ERPLogin.aspx");
        }
        Session["AccordianId"] = "9";
        Session["HeaderText"] = "Attendance Setup";
        if(!IsPostBack)
        {
            GetLog("10");

        }
        Page.Title = objSys.GetSysTitle();
    }

    public string GetDate(object obj)
    {

        DateTime Date = new DateTime();
        Date = Convert.ToDateTime(obj.ToString());



        return Date.ToString(objSys.SetDateFormat());

    }
    public void GetLog(string pagesize)
    {
        DataTable dtlog = objAttLog.GetAttendanceLog(Session["CompId"].ToString());
        

        DataTable dtlogdata = new DataTable();

        dtlogdata = dtlog.Clone();


        if (dtlog.Rows.Count > 0)
        {

            if (pagesize == "all")
            {

                dtlogdata = dtlog;


            }
            else
            {
                dtlogdata = SelectTopDataRow(dtlog, int.Parse(pagesize));



            }
            gvTheGrid.DataSource = dtlogdata;
            gvTheGrid.DataBind();
        }
        else
        {
            gvTheGrid.DataSource = null;
            gvTheGrid.DataBind();
        }

    }
    
    public DataTable SelectTopDataRow(DataTable dt, int count)
    {
        DataTable dtn = dt.Clone();
        if (dt.Rows.Count == 0)
        {
            return null;
        }

        if (dt.Rows.Count >= count)
        {
            for (int i = 0; i < count; i++)
            {
                dtn.ImportRow(dt.Rows[i]);
            }
        }
        else
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dtn.ImportRow(dt.Rows[i]);
            }

        }
        return dtn;
    }
    protected void gvTheGrid_PreRender(object sender, EventArgs e)
    {

        // You only need the following 2 lines of code if you are not 
        // using an ObjectDataSource of SqlDataSource
        if (!Page.IsPostBack)
        {
            ddlSelectRecord.SelectedValue = "10";
            Session["Size"] = "10";
            ddlSelectRecord_SelectedIndexChanged(null, null);
        }
        if (gvTheGrid.Rows.Count > 0)
        {
            //This replaces <td> with <th> and adds the scope attribute
            gvTheGrid.UseAccessibleHeader = true;

            //This will add the <thead> and <tbody> elements
            gvTheGrid.HeaderRow.TableSection = TableRowSection.TableHeader;

            //This adds the <tfoot> element. 
            //Remove if you don't have a footer row
            //gvTheGrid.FooterRow.TableSection = TableRowSection.TableFooter;
        }
    }

    protected void ddlSelectRecord_SelectedIndexChanged(object sender, EventArgs e)
    {
        Session["Size"] = ddlSelectRecord.SelectedValue;
        GetLog(Session["Size"].ToString());
    }
    protected void Timer1_Tick(object sender, EventArgs e)
    {
        Session["Size"] = ddlSelectRecord.SelectedValue;
        GetLog(ddlSelectRecord.SelectedValue);
    }
}
