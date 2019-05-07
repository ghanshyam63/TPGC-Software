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
using Device_SDK;
public partial class Temp_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        Device_Operation_Lan obj = new Device_Operation_Lan();
        DataTable dt = new DataTable();
        dt = obj.GetUserFace ("192.168.5.186", 4370);
        obj.InitializeDevice("192.168.5.186", 4370);
        obj.UploadUserFace("192.168.5.186", 4370, dt);



    }
}
