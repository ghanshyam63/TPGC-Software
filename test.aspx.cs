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
using System.Collections.Generic;
using System.Text.RegularExpressions;

public partial class test : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        var text = "111,111A,222,411G,300,411Z,G411,AG500,A111,AZ600,ABQ,ZZZ,AAN";
        var list = text.Split(',').ToList();
        var result = list.OrderBy(i => i, new StringCompare());
        string s = string.Empty;
        foreach (var item in result)
        {

            s += item + ",";
        }
        Label1.Text = s;

    }
    class StringCompare : IComparer<string>
    {
        string[] exps = new[] { @"^\d+$", @"^\d+[a-zA-Z]+$", @"^[a-zA-Z]\d+$", @"^[a-zA-Z]+\d+$" };
        public int Compare(string x, string y)
        {
            for (int i = 0; i < exps.Length; i++)
            {
                var isNumberx = Regex.IsMatch(x, exps[i]);
                var isNumbery = Regex.IsMatch(y, exps[i]);

                if (isNumberx && isNumbery)
                    return string.Compare(x, y);
                else if (isNumberx)
                    return -1;
                else if (isNumbery)
                    return 1;
                //return string.Compare(x, y);
            }
            return string.Compare(x, y);
        }
    }
}
