﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data.Objects;

using CfsNamespace;

public partial class backend_view_talent_search_results : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        CfsCommon.CheckPageAccess((int)CfsCommon.Section.EmployeeMgmt, Session, Response);
        
        if (!IsPostBack)
        {
            if (Request.Params["inactive"] != null)
            {
                LoadInactiveData();
            }
            else
            {
                string type = (string)Request.Params["type"];
                string livesIn = (string)Request.Params["livesin"];
                string worksIn = (string)Request.Params["worksin"];

                LoadSearchData(type, livesIn, worksIn);

            }
            
        }
    }
    protected Boolean IsInactive()
    {
        if (Request.Params["inactive"] != null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    protected void OnClick_btnDetails(object sender, EventArgs arrrggs)
    {
        Button btn = (Button)sender;

        if (!string.IsNullOrEmpty(btn.CommandArgument))
        {
            Response.Redirect("add_edit_employee.aspx?empid=" + btn.CommandArgument);
        }
    }
    protected void OnClick_btnDelete(object sender, EventArgs arrrggs)
    {
        Button btn = (Button)sender;

        if (!string.IsNullOrEmpty(btn.CommandArgument))
        {
            CfsEntity cfsEntity = new CfsEntity();
            bool re = CfsCommon.DeleteTalent(cfsEntity, btn.CommandArgument);
            Response.Redirect("view_talent_search_results.aspx?inactive=true");
        }
    }
    private void LoadInactiveData()
    {
        CfsEntity cfsEntity = new CfsEntity();

        rptrResults.DataSource = cfsEntity.Talent.Where("it.IsActive = false AND it.TalentId <> 0").OrderBy("it.DisplayName");
        rptrResults.DataBind();
        
        headerSearchResults.InnerText = "Inactive Employees";
    }

    private void LoadSearchData(string type, string livesIn, string worksIn)
    {
        /* Do this the old way, until we figure out w/ entities */
        
        string conn = System.Configuration.ConfigurationManager.ConnectionStrings["CenterfoldConn"].ConnectionString;
        string select = "SELECT DISTINCT t.TalentId, t.City, t.State, t.Country, t.DisplayName, t.EmailPrimary, t.HomePhone, t.CellPhone, t.SpecialNotes";
        select += " FROM Talent t, TalentWorksIn w ";

        string whereStmt = "WHERE t.TalentId = w.TalentId AND t.IsActive ='true' ";
        string header = "Search results for ";

        if (!string.IsNullOrEmpty(type))
        {
            //whereStmt += "AND t.TalentType LIKE '%" + type + "%' ";
            
            whereStmt += "AND t.TalentType LIKE '%" + type + "%' ";
            if (type == "female")
            {
                whereStmt += "AND t.TalentType NOT LIKE '%minifemale%' ";
            }
            if (type == "male")
            {
                whereStmt += "AND t.TalentType NOT LIKE '%minimale%' ";
                whereStmt += "AND t.TalentType NOT LIKE '%female%' ";
                whereStmt += "AND t.TalentType NOT LIKE '%minifemale%' ";
            }
            header += " TalentType: [" + type + "]";
        }
        else
        {
            header += " TalentType: [ALL]";
        }

        if (!string.IsNullOrEmpty(livesIn))
        {
            whereStmt += "AND t.State = '" + livesIn + "' ";
            header += " and Lives In: [" + livesIn + "]";
        }
        else
        {
            header += " and Lives In: [ALL]";
        }

        if (!string.IsNullOrEmpty(worksIn))
        {
            whereStmt += "AND w.State = '" + worksIn + "' ";
            header += " and Works In: [" + worksIn + "]";
        }
        else
        {
            header += " and Works In: [ALL]";
        }


        SqlDataSource sqlData = new SqlDataSource(conn, select + whereStmt + " ORDER BY DisplayName" );


        //rptrResults.DataSource = cfsEntity.Talent.Where(whereStmt).OrderBy("it.DisplayName");
        rptrResults.DataSource = sqlData;
        rptrResults.DataBind();
        
        headerSearchResults.InnerText = header;
    }

    protected string FormatLocation(string city, string state, string country)
    {
        /* Format like this: City, State [,Country, if not USA] */
        string retString = "";

        if (!string.IsNullOrEmpty(city))
        {
            retString = city;
        }

        if (!string.IsNullOrEmpty(state))
        {
            if (retString == "")
            {
                retString = state;
            }
            else
            {
                retString += ", " + state;
            }
        }

        if (!string.IsNullOrEmpty(country))
        {
            if (country.ToUpper() != "UNITED STATES")
            {
                if (retString == "")
                {
                    retString = country;
                }
                else
                {
                    retString += ", " + country;
                }
            }
        }
        
        return retString;
    }

}
