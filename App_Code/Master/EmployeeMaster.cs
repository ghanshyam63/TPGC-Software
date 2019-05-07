using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using PegasusDataAccess;

/// <summary>
/// Summary description for EmployeeMaster
/// </summary>
public class EmployeeMaster
{
	public EmployeeMaster()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    private string _CompanyId;
    private string _BrandId;
    private string _LocationId;
    private string _EmpId;
    private string _EmpCardNo;
    private string _EEmpFirstName;
    private string _LEmpFirstName;
    private string _EEmpMiddleName;
    private string _LEmpMiddleName;
    private string _EEmpLastName;
    private string _LEmpLastName;
    private string _EF_H_Name;
    private string _LF_H_Name;
    private string _DepartmentId;
    private string _DesignationId;
    private string _NationalityId;
    private string _Photo;
    private string _Per_CityId;
    private string _Per_StateId;
    private string _Per_CountryId;
    private string _ReligionId;
    private string _DOB;
    private string _Gender;
    private string _Marital_Status;
    private string _Per_EAddress;
    private string _Per_LAddress;
    private string _Per_Pincode;
    private string _Cur_EAddress;
    private string _Cur_LAddress;
    private string _Cur_CityId;
    private string _Cur_StateId;
    private string _Cur_CountryId;
    private string _Block;
    private string _Street;
    private string _BldgNo;
    private string _FlatNo;
    private string _PhoneNo;
    private string _MobileNo;
    private string _EmailId;
    private string _Service_Status;
    private string _AccountNo;
    private string _BankId;
    private string _AccountType;
    private string _BankBranchDetail;
    private string _PanNo;
    private string _CivilId;
    private string _DOJ;
    private string _DOL;
    private string _Handicaped;
    private string _NoOfChild;
    private string _ReferenceName;
    private string _IdentityMark;
    private string _Category;
    private string _Remark;
    private string _EmpType;
    private string _ImagePath;
    private string _Education;
    private string _Project1;
    private string _Project2;
    private string _CreatedBy;
    private string _CreatedDate;
    private string _ModifiedBy;
    private string _ModifiedDate;
    private string _IsActive;




    public string CompanyId
    {
        get
        {
            return _CompanyId;

        }
        set
        {
            _CompanyId = value;

        }

    }
    public string BrandId
    {
        get
        {
            return _BrandId;

        }
        set
        {
            _BrandId = value;

        }

    }

    public string LocationId
    {
        get { return _LocationId; }
        set { _LocationId = value; }
    }

    public string EmpId
    {
        get { return _EmpId; }
        set { _EmpId = value; }
    }
    public string EmpCardNo
    {
        get { return _EmpCardNo; }
        set { _EmpCardNo = value; }
    }
    public string EEmpFirstName
    {
        get { return _EEmpFirstName; }
        set { _EEmpFirstName = value; }
    }
    public string LEmpFirstName
    {
        get { return _LEmpFirstName; }
        set { _LEmpFirstName = value; }
    }
    public string EEmpMiddleName
    {
        get { return _EEmpMiddleName; }
        set { _EEmpMiddleName = value; }
    }
    public string LEmpMiddleName
    {
        get { return _LEmpMiddleName; }
        set { _LEmpMiddleName = value; }
    }
    public string EEmpLastName
    {
        get { return _EEmpLastName; }
        set { _EEmpLastName = value; }
    }
    public string LEmpLastName
    {
        get { return _LEmpLastName; }
        set { _LEmpLastName = value; }
    }
    public string EF_H_Name
    {
        get { return _EF_H_Name; }
        set { _EF_H_Name = value; }
    }
    public string LF_H_Name
    {
        get { return _LF_H_Name; }
        set { _LF_H_Name = value; }
    }
    public string DepartmentId
    {
        get { return _DepartmentId; }
        set { _DepartmentId = value; }
    }
    public string DesignationId
    {
        get { return _DesignationId; }
        set { _DesignationId = value; }
    }
    public string NationalityId
    {
        get { return _NationalityId; }
        set { _NationalityId = value; }
    }
    public string Photo
    {
        get { return _Photo; }
        set { _Photo = value; }
    }
    public string Per_CityId
    {
        get { return _Per_CityId; }
        set { _Per_CityId = value; }
    }
    public string Per_StateId
    {
        get { return _Per_StateId; }
        set { _Per_StateId = value; }
    }
    public string Per_CountryId
    {
        get { return _Per_CountryId; }
        set { _Per_CountryId = value; }
    }
    public string ReligionId
    {
        get { return _ReligionId; }
        set { _ReligionId = value; }
    }
    public string DOB
    {
        get { return _DOB; }
        set { _DOB = value; }
    }
    public string Gender
    {
        get { return _Gender; }
        set { _Gender = value; }
    }
    public string Marital_Status
    {
        get { return _Marital_Status; }
        set { _Marital_Status = value; }
    }
    public string Per_EAddress
    {
        get { return _Per_EAddress; }
        set { _Per_EAddress = value; }
    }
    public string Per_LAddress
    {
        get { return _Per_LAddress; }
        set { _Per_LAddress = value; }
    }
    public string Per_Pincode
    {
        get { return _Per_Pincode; }
        set { _Per_Pincode = value; }
    }
    public string Cur_EAddress
    {
        get { return _Cur_EAddress; }
        set { _Cur_EAddress = value; }
    }
    public string Cur_LAddress
    {
        get { return _Cur_LAddress; }
        set { _Cur_LAddress = value; }
    }
    public string Cur_CityId
    {
        get { return _Cur_CityId; }
        set { _Cur_CityId = value; }
    }
    public string Cur_StateId
    {
        get { return _Cur_StateId; }
        set { _Cur_StateId = value; }
    }
    public string Cur_CountryId
    {
        get { return _Cur_CountryId; }
        set { _Cur_CountryId = value; }
    }
    public string Block
    {
        get { return _Block; }
        set { _Block = value; }
    }
    public string Street
    {
        get { return _Street; }
        set { _Street = value; }
    }
    public string BldgNo
    {
        get { return _BldgNo; }
        set { _BldgNo = value; }
    }
    public string FlatNo
    {
        get { return _FlatNo; }
        set { _FlatNo = value; }
    }
    public string PhoneNo
    {
        get { return _PhoneNo; }
        set { _PhoneNo = value; }
    }
    public string MobileNo
    {
        get { return _MobileNo; }
        set { _MobileNo = value; }
    }
    public string EmailId
    {
        get { return _EmailId; }
        set { _EmailId = value; }
    }
    public string Service_Status
    {
        get { return _Service_Status; }
        set { _Service_Status = value; }
    }
    public string AccountNo
    {
        get { return _AccountNo; }
        set { _AccountNo = value; }
    }
    public string BankId
    {
        get { return _BankId; }
        set { _BankId = value; }
    }
    public string AccountType
    {
        get { return _AccountType; }
        set { _AccountType = value; }

    }
    public string BankBranchDetail
    {
        get { return _BankBranchDetail; }
        set { _BankBranchDetail = value; }
    }
    public string PanNo
    {
        get
        {
            return _PanNo;
        }
        set { _PanNo = value; }
    }
    public string CivilId
    {
        get { return _CivilId; }
        set { _CivilId = value; }
    }
    public string DOJ
    {
        get { return _DOJ; }
        set { _DOJ = value; }
    }
    public string DOL
    {
        get { return _DOL; }
        set { _DOL = value; }
    }
    public string Handicaped
    {
        get { return _Handicaped; }
        set { _Handicaped = value; }
    }
    public string NoOfChild
    {
        get { return _NoOfChild; }
        set { _NoOfChild = value; }
    }
    public string ReferenceName
    {
        get { return _ReferenceName; }
        set { _ReferenceName = value; }
    }
    public string IdentityMark
    {
        get { return _IdentityMark; }
        set { _IdentityMark = value; }
    }
    public string Category
    {
        get { return _Category; }
        set { _Category = value; }
    }
    public string Remark
    {
        get { return _Remark; }
        set { _Remark = value; }
    }

    public string EmpType
    {
        get { return _EmpType; }
        set { _EmpType = value; }
    }
    public string ImagePath
    {
        get { return _ImagePath; }
        set { _ImagePath = value; }
    }
    public string Education
    {
        get { return _Education; }
        set { _Education = value; }
    }
    public string Project1
    {
        get { return _Project1; }
        set { _Project1 = value; }
    }
    public string Project2
    {
        get { return _Project2; }
        set { _Project2 = value; }
    }
    public string CreatedBy
    {
        get { return _CreatedBy; }
        set { _CreatedBy = value; }
    }
    public string CreatedDate
    {
        get { return _CreatedDate; }
        set { _CreatedDate = value; }
    }
    public string ModifiedBy
    {
        get { return _ModifiedBy; }
        set { _ModifiedBy = value; }
    }

    public string ModifiedDate
    {
        get { return _ModifiedDate; }
        set { _ModifiedDate = value; }
    }
    public string IsActive
    {
        get { return _IsActive; }
        set { _IsActive = value; }
    }
    DataAccessClass daClass = new DataAccessClass();

    public DataTable GetFieldName(bool isnec)
    {
        string query = "select Distinct(Column_Name),'1'  Nec  from INFORMATION_SCHEMA.COLUMNS where TABLE_NAME In ('Set_EmployeeMaster') and Column_name  in ('Emp_Name','DOJ','Department_Id','Designation_Id','DOB','Brand_Id','Location_Id','Emp_Id','Email_Id')";
        string query1 = "select Distinct(Column_Name),'0'  Nec  from INFORMATION_SCHEMA.COLUMNS where TABLE_NAME In ('Set_EmployeeMaster') and Column_name  in ('Religion_Id','Nationality_Id','Qualification_Id','Gender','Phone_No','Civil_Id')";


        DataTable dt = daClass.return_DataTable(query);
        if (!isnec)
        {
            dt.Merge(daClass.return_DataTable(query1));
        }
        return dt;
    }

    public int SaveEmpData()
    {

        PassDataToSql[] paramList = new PassDataToSql[33];
        paramList[0] = new PassDataToSql("@Company_Id", CompanyId, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);

        paramList[1] = new PassDataToSql("@Emp_Name",EEmpFirstName, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[2] = new PassDataToSql("@Emp_Name_L", EEmpFirstName, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[3] = new PassDataToSql("@Emp_Code", EmpId, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[4] = new PassDataToSql("@Brand_Id", BrandId, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        paramList[5] = new PassDataToSql("@Location_Id", LocationId, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        paramList[6] = new PassDataToSql("@Department_Id", DepartmentId, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        paramList[7] = new PassDataToSql("@Civil_Id", CivilId, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[8] = new PassDataToSql("@Designation_Id", DesignationId, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        paramList[9] = new PassDataToSql("@Religion_Id", ReligionId, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        paramList[10] = new PassDataToSql("@Nationality_Id", NationalityId, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        paramList[11] = new PassDataToSql("@Qualification_Id", "0", PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);

        paramList[12] = new PassDataToSql("@DOB", DOB, PassDataToSql.ParaTypeList.Date, PassDataToSql.ParaDirectonList.Input);
        paramList[13] = new PassDataToSql("@DOJ", DOJ, PassDataToSql.ParaTypeList.Date, PassDataToSql.ParaDirectonList.Input);
        paramList[14] = new PassDataToSql("@Emp_Type", "On Role", PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[15] = new PassDataToSql("@Termination_Date", DateTime.Now.ToString(), PassDataToSql.ParaTypeList.Date, PassDataToSql.ParaDirectonList.Input);
        paramList[16] = new PassDataToSql("@Gender", Gender, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);

        paramList[17] = new PassDataToSql("@Field1", "", PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);

        paramList[18] = new PassDataToSql("@Field2", "", PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[19] = new PassDataToSql("@Field3", "", PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[20] = new PassDataToSql("@Field4", "", PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[21] = new PassDataToSql("@Field5", "", PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[22] = new PassDataToSql("@Field6", true.ToString(), PassDataToSql.ParaTypeList.Bit, PassDataToSql.ParaDirectonList.Input);
        paramList[23] = new PassDataToSql("@Field7", DateTime.Now.ToString(), PassDataToSql.ParaTypeList.Date, PassDataToSql.ParaDirectonList.Input);

        paramList[24] = new PassDataToSql("@IsActive", IsActive, PassDataToSql.ParaTypeList.Bit, PassDataToSql.ParaDirectonList.Input);
        paramList[25] = new PassDataToSql("@CreatedBy", CreatedBy, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[26] = new PassDataToSql("@CreatedDate", CreatedDate, PassDataToSql.ParaTypeList.Date, PassDataToSql.ParaDirectonList.Input);
        paramList[27] = new PassDataToSql("@ModifiedBy", ModifiedBy, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[28] = new PassDataToSql("@ModifiedDate", ModifiedDate, PassDataToSql.ParaTypeList.Date, PassDataToSql.ParaDirectonList.Input);
        paramList[29] = new PassDataToSql("@ReferenceID", "0", PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Output);
        paramList[30] = new PassDataToSql("@Emp_Image", "", PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[31] = new PassDataToSql("@Email_Id", EmailId, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[32] = new PassDataToSql("@Phone_No", PhoneNo, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);


        daClass.execute_Sp("sp_Set_EmployeeMaster_Insert", paramList);
        return Convert.ToInt32(paramList[29].ParaValue);
    }

    public int InsertEmployeeMaster(string CompanyId, string EEmpName, string LEmpName, string EmpCode, string EmpImage, string Brand_Id, string Location_Id, string Department_Id, string Civil_Id, string Designation_Id, string Religion_Id, string Nationality_Id, string Qualification_Id, string DOB, string DOJ, string Emp_Type, string Termination_Date, string Gender, string Field1, string Field2, string Field3, string Field4, string Field5, string Field6, string Field7, string IsActive, string CreatedBy, string CreatedDate, string ModifiedBy, string ModifiedDate,string emailid,string phoneno)
    {

        PassDataToSql[] paramList = new PassDataToSql[33];
        paramList[0] = new PassDataToSql("@Company_Id", CompanyId, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);

        paramList[1] = new PassDataToSql("@Emp_Name", EEmpName, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[2] = new PassDataToSql("@Emp_Name_L", LEmpName, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[3] = new PassDataToSql("@Emp_Code", EmpCode, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[4] = new PassDataToSql("@Brand_Id", Brand_Id, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        paramList[5] = new PassDataToSql("@Location_Id", Location_Id, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        paramList[6] = new PassDataToSql("@Department_Id", Department_Id, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        paramList[7] = new PassDataToSql("@Civil_Id", Civil_Id, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[8] = new PassDataToSql("@Designation_Id", Designation_Id, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        paramList[9] = new PassDataToSql("@Religion_Id", Religion_Id, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        paramList[10] = new PassDataToSql("@Nationality_Id", Nationality_Id, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        paramList[11] = new PassDataToSql("@Qualification_Id", Qualification_Id, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);

        paramList[12] = new PassDataToSql("@DOB", DOB, PassDataToSql.ParaTypeList.Date, PassDataToSql.ParaDirectonList.Input);
        paramList[13] = new PassDataToSql("@DOJ", DOJ, PassDataToSql.ParaTypeList.Date, PassDataToSql.ParaDirectonList.Input);
        paramList[14] = new PassDataToSql("@Emp_Type",Emp_Type, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[15] = new PassDataToSql("@Termination_Date", Termination_Date, PassDataToSql.ParaTypeList.Date, PassDataToSql.ParaDirectonList.Input);
        paramList[16] = new PassDataToSql("@Gender", Gender, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);

        paramList[17] = new PassDataToSql("@Field1", Field1, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);

        paramList[18] = new PassDataToSql("@Field2", Field2, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[19] = new PassDataToSql("@Field3", Field3, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[20] = new PassDataToSql("@Field4", Field4, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[21] = new PassDataToSql("@Field5", Field5, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[22] = new PassDataToSql("@Field6", Field6, PassDataToSql.ParaTypeList.Bit, PassDataToSql.ParaDirectonList.Input);
        paramList[23] = new PassDataToSql("@Field7", Field7, PassDataToSql.ParaTypeList.Date, PassDataToSql.ParaDirectonList.Input);


        paramList[24] = new PassDataToSql("@IsActive", IsActive, PassDataToSql.ParaTypeList.Bit, PassDataToSql.ParaDirectonList.Input);
        paramList[25] = new PassDataToSql("@CreatedBy", CreatedBy, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[26] = new PassDataToSql("@CreatedDate", CreatedDate, PassDataToSql.ParaTypeList.Date, PassDataToSql.ParaDirectonList.Input);
        paramList[27] = new PassDataToSql("@ModifiedBy", ModifiedBy, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[28] = new PassDataToSql("@ModifiedDate", ModifiedDate, PassDataToSql.ParaTypeList.Date, PassDataToSql.ParaDirectonList.Input);
        paramList[29] = new PassDataToSql("@ReferenceID", "0", PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Output);
        paramList[30] = new PassDataToSql("@Emp_Image", EmpImage, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[31] = new PassDataToSql("@Email_Id", emailid, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[32] = new PassDataToSql("@Phone_No", phoneno, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
      
        
        daClass.execute_Sp("sp_Set_EmployeeMaster_Insert", paramList);
        return Convert.ToInt32(paramList[29].ParaValue);
    }

    public int UpdateEmployeeMaster(string EmpId, string CompanyId, string EEmpName, string LEmpName, string EmpCode, string EmpImage, string Brand_Id, string Location_Id, string Department_Id, string Civil_Id, string Designation_Id, string Religion_Id, string Nationality_Id, string Qualification_Id, string DOB, string DOJ, string Emp_Type, string Termination_Date, string Gender, string Field1, string Field2, string Field3, string Field4, string Field5, string Field6, string Field7, string IsActive, string ModifiedBy, string ModifiedDate,string emailid,string phoneno)
    {
      
        PassDataToSql[] paramList = new PassDataToSql[32];
        paramList[0] = new PassDataToSql("@Company_Id", CompanyId, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);

        paramList[1] = new PassDataToSql("@Emp_Name", EEmpName, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[2] = new PassDataToSql("@Emp_Name_L", LEmpName, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[3] = new PassDataToSql("@Emp_Code", EmpCode, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[4] = new PassDataToSql("@Brand_Id", Brand_Id, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        paramList[5] = new PassDataToSql("@Location_Id", Location_Id, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        paramList[6] = new PassDataToSql("@Department_Id", Department_Id, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        paramList[7] = new PassDataToSql("@Civil_Id", Civil_Id, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[8] = new PassDataToSql("@Designation_Id", Designation_Id, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        paramList[9] = new PassDataToSql("@Religion_Id", Religion_Id, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        paramList[10] = new PassDataToSql("@Nationality_Id", Nationality_Id, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        paramList[11] = new PassDataToSql("@Qualification_Id", Qualification_Id, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);

        paramList[12] = new PassDataToSql("@DOB", DOB, PassDataToSql.ParaTypeList.Date, PassDataToSql.ParaDirectonList.Input);
        paramList[13] = new PassDataToSql("@DOJ", DOJ, PassDataToSql.ParaTypeList.Date, PassDataToSql.ParaDirectonList.Input);
        paramList[14] = new PassDataToSql("@Emp_Type", Emp_Type, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[15] = new PassDataToSql("@Termination_Date", Termination_Date, PassDataToSql.ParaTypeList.Date, PassDataToSql.ParaDirectonList.Input);
        paramList[16] = new PassDataToSql("@Gender", Gender, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);

        paramList[17] = new PassDataToSql("@Field1", Field1, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);

        paramList[18] = new PassDataToSql("@Field2", Field2, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[19] = new PassDataToSql("@Field3", Field3, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[20] = new PassDataToSql("@Field4", Field4, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[21] = new PassDataToSql("@Field5", Field5, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[22] = new PassDataToSql("@Field6", Field6, PassDataToSql.ParaTypeList.Bit, PassDataToSql.ParaDirectonList.Input);
        paramList[23] = new PassDataToSql("@Field7", Field7, PassDataToSql.ParaTypeList.Date, PassDataToSql.ParaDirectonList.Input);


        paramList[24] = new PassDataToSql("@IsActive", IsActive, PassDataToSql.ParaTypeList.Bit, PassDataToSql.ParaDirectonList.Input);
        paramList[25] = new PassDataToSql("@ModifiedBy", ModifiedBy, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[26] = new PassDataToSql("@ModifiedDate", ModifiedDate, PassDataToSql.ParaTypeList.Date, PassDataToSql.ParaDirectonList.Input);
        paramList[27] = new PassDataToSql("@ReferenceID", "0", PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Output);

        paramList[28] = new PassDataToSql("@Emp_Id", EmpId, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        paramList[29] = new PassDataToSql("@Emp_Image", EmpImage, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[30] = new PassDataToSql("@Email_Id", emailid, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[31] = new PassDataToSql("@Phone_No", phoneno, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
      
        daClass.execute_Sp("sp_Set_EmployeeMaster_Update", paramList);
        return Convert.ToInt32(paramList[27].ParaValue);
    }

    public int DeleteEmployeeMaster(string CompanyId, string EmpId, string IsActive, string ModifiedBy, string ModifiedDate)
    {

        PassDataToSql[] paramList = new PassDataToSql[6];
        paramList[0] = new PassDataToSql("@Company_Id", CompanyId, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        paramList[1] = new PassDataToSql("@Emp_Id", EmpId, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        paramList[2] = new PassDataToSql("@IsActive", IsActive, PassDataToSql.ParaTypeList.Bit, PassDataToSql.ParaDirectonList.Input);
        paramList[3] = new PassDataToSql("@ModifiedBy", ModifiedBy, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[4] = new PassDataToSql("@ModifiedDate", ModifiedDate, PassDataToSql.ParaTypeList.Date, PassDataToSql.ParaDirectonList.Input);
        paramList[5] = new PassDataToSql("@ReferenceID", "0", PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Output);
        daClass.execute_Sp("sp_Set_EmployeeMaster_RowStatus", paramList);
        return Convert.ToInt32(paramList[5].ParaValue);
    }
    public DataTable GetEmployeeMasterById(string CompanyId, string EmpId)
    {

        DataTable dtInfo = new DataTable();
        PassDataToSql[] paramList = new PassDataToSql[4];
        paramList[0] = new PassDataToSql("@Company_Id", CompanyId, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        paramList[1] = new PassDataToSql("@Emp_Id", EmpId, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[2] = new PassDataToSql("@Emp_Name", "", PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[3] = new PassDataToSql("@optype", "4", PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        dtInfo = daClass.Reuturn_Datatable_Search("sp_Set_EmployeeMaster_SelectRow", paramList);

        return dtInfo;
    }

    public DataTable GetEmployeeMasterByEmpCode(string CompanyId, string Empcode)
    {

        DataTable dtInfo = new DataTable();
        PassDataToSql[] paramList = new PassDataToSql[4];
        paramList[0] = new PassDataToSql("@Company_Id", CompanyId, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        paramList[1] = new PassDataToSql("@Emp_Id", Empcode, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[2] = new PassDataToSql("@Emp_Name", "", PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[3] = new PassDataToSql("@optype", "6", PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        dtInfo = daClass.Reuturn_Datatable_Search("sp_Set_EmployeeMaster_SelectRow", paramList);

        return dtInfo;
    }
    public DataTable GetEmployeeMaster(string CompanyId)
    {

        DataTable dtInfo = new DataTable();
        PassDataToSql[] paramList = new PassDataToSql[4];
        paramList[0] = new PassDataToSql("@Company_Id", CompanyId, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        paramList[1] = new PassDataToSql("@Emp_Id", "0", PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[2] = new PassDataToSql("@Emp_Name", "", PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[3] = new PassDataToSql("@optype", "2", PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        dtInfo = daClass.Reuturn_Datatable_Search("sp_Set_EmployeeMaster_SelectRow", paramList);

        return dtInfo;
    }
    public DataTable GetEmployeeMasterForReport(string CompanyId)
    {

        DataTable dtInfo = new DataTable();
        PassDataToSql[] paramList = new PassDataToSql[4];
        paramList[0] = new PassDataToSql("@Company_Id", CompanyId, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        paramList[1] = new PassDataToSql("@Emp_Id", "0", PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[2] = new PassDataToSql("@Emp_Name", "", PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[3] = new PassDataToSql("@optype", "8", PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        dtInfo = daClass.Reuturn_Datatable_Search("sp_Set_EmployeeMaster_SelectRow", paramList);

        return dtInfo;
    }

    public DataTable GetEmployeeMasterWithDeviceData(string CompanyId)
    {

        DataTable dtInfo = new DataTable();
        PassDataToSql[] paramList = new PassDataToSql[4];
        paramList[0] = new PassDataToSql("@Company_Id", CompanyId, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        paramList[1] = new PassDataToSql("@Emp_Id", "0", PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[2] = new PassDataToSql("@Emp_Name", "", PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[3] = new PassDataToSql("@optype", "7", PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        dtInfo = daClass.Reuturn_Datatable_Search("sp_Set_EmployeeMaster_SelectRow", paramList);

        return dtInfo;
    }
    public DataTable GetEmployeeMasterAllData(string CompanyId)
    {

        DataTable dtInfo = new DataTable();
        PassDataToSql[] paramList = new PassDataToSql[4];
        paramList[0] = new PassDataToSql("@Company_Id", CompanyId, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        paramList[1] = new PassDataToSql("@Emp_Id", "0", PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[2] = new PassDataToSql("@Emp_Name", "", PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[3] = new PassDataToSql("@optype", "1", PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        dtInfo = daClass.Reuturn_Datatable_Search("sp_Set_EmployeeMaster_SelectRow", paramList);

        return dtInfo;
    }



    public DataTable GetEmployeeMasterInactive(string CompanyId)
    {
        DataTable dtInfo = new DataTable();
        PassDataToSql[] paramList = new PassDataToSql[4];
        paramList[0] = new PassDataToSql("@Company_Id", CompanyId, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        paramList[1] = new PassDataToSql("@Emp_Id", "0", PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[2] = new PassDataToSql("@Emp_Name", "", PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[3] = new PassDataToSql("@optype", "3", PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        dtInfo = daClass.Reuturn_Datatable_Search("sp_Set_EmployeeMaster_SelectRow", paramList);

        return dtInfo;
    }
    public DataTable GetEmployeeMasterByEmpName(string CompanyId, string strEmpName)
    {
        DataTable dtInfo = new DataTable();
        PassDataToSql[] paramList = new PassDataToSql[4];
        paramList[0] = new PassDataToSql("@Company_Id", CompanyId, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        paramList[1] = new PassDataToSql("@Emp_Id", "0", PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[2] = new PassDataToSql("@Emp_Name", strEmpName, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[3] = new PassDataToSql("@optype", "5", PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        dtInfo = daClass.Reuturn_Datatable_Search("sp_Set_EmployeeMaster_SelectRow", paramList);

        return dtInfo;
    }
}
