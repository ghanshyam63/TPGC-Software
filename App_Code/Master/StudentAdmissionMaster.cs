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
/// Summary description for Set_BankMaster
/// </summary>
public class StudentAdmissionMaster
{
    DataAccessClass daClass = new DataAccessClass();
    public StudentAdmissionMaster()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public int InsertStudentAdmissionMaster(string CompanyId, string EStudentName, string LStudentName, string NCC, string No, string srno, string regno, string category, string EnNo, string permit, string Religion, string Nationality, string Course, string class1, string doa, string dob, string Place, string caste, string Status, string PH, string Village, string Grampanchayat, string Tehsil, string District, string State, string Post, string PinCode, string PhoneNo, string Mobile1, string Mobile2, string Email, string BankName, string branch, string AccountNo, string Father_Name, string Profession, string Position, string TotalIncome, string OfficePhoneNo, string MobileNo3, string OfficeAddr, string MotherName, string Profession1, string Position1, string TotalIncome1, string OfficePhoneNo1, string MobileNo4, string OfficeAddr1, string LocalGuardian, string MobileNo5, string Address, string Village1, string Docotor, string MobileNo6, string BloodGroup, string Exam, string Year, string RollNo, string EnrNo, string X_Board, string XII_Board, string Graduation, string PostGraduation, string OtherBoard, string X_Inst, string XII_Inst, string Grd_Inst, string Post_Inst, string Other_Inst, string X_Year, string XII_Year, string Grd_Year, string Post_Year, string Other_Year, string X_MM, string XII_MM, string Graducate_Marks, string Post_MM, string Other_MM, string X_ObtMarks, string XII_ObtMarks, string Graducate_ObtMarks, string PostGraducate_ObtMarks, string Other_ObtMarks, string X_Per, string XII_Per, string Graducate_Per, string Post_Per, string Other_Per, string CompSub1, string CompSub2, string CompSub3, string CompSub4, string OpSub1, string OpSub2, string OpSub3, string OpSub4, string OpSub5, string OpSub6, string OpSub7, string OpSub8, string OpSub9, string OpSub10, string OpSub11, string OpSub12, string OpSub13, string OpSub14, string OpSub15, string DC, string Year1, string Year2, string RollNo1, string RollNo2, string Result1, string Result2, string photo, string Sig1, string sig2, string Mq1, string Tc, string Mig, string sc, string Sports, string Bonafide, string SE, string CC, string PP, string pars, string DComSub1, string DComSub2, string DComSub3, string DComSub4, string DOpSub1, string DOpSub2, string DOpSub3, string Noe1, string Noe2, string Noi1, string Noi2, string Board1, string Board2, string Field1, string Field2, string Field3, string Field4, string Field5, string Field6, string Field7, string IsActive, string CreatedBy, string CreatedDate, string ModifiedBy, string ModifiedDate, string Student_Code, string Street)
  
    {

        PassDataToSql[] paramList = new PassDataToSql[157];
        paramList[0] = new PassDataToSql("@Company_Id", CompanyId, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        paramList[1] = new PassDataToSql("@Student_Name", EStudentName, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[2] = new PassDataToSql("@Student_Name_L", LStudentName, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[3] = new PassDataToSql("@NCC", NCC, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[4] = new PassDataToSql("@FormNo", No, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[5] = new PassDataToSql("@Sr_No", srno, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[6] = new PassDataToSql("@Reg_No", regno, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[7] = new PassDataToSql("@Category", category, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[8] = new PassDataToSql("@Enroll_No", EnNo, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[9] = new PassDataToSql("@PermittedBy", permit, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[10] = new PassDataToSql("@Religion", Religion, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[11] = new PassDataToSql("@Nationality", Nationality, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[12] = new PassDataToSql("@Course", Course, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[13] = new PassDataToSql("@Class", class1, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[14] = new PassDataToSql("@Doa", doa, PassDataToSql.ParaTypeList.Date, PassDataToSql.ParaDirectonList.Input);
        paramList[15] = new PassDataToSql("@Dob", dob, PassDataToSql.ParaTypeList.Date, PassDataToSql.ParaDirectonList.Input);
        paramList[16] = new PassDataToSql("@Place", Place, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[17] = new PassDataToSql("@Caste", caste, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[18] = new PassDataToSql("@Status", Status, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[19] = new PassDataToSql("@PH", PH, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[20] = new PassDataToSql("@Village", Village, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[21] = new PassDataToSql("@Grampanchayat", Grampanchayat, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[22] = new PassDataToSql("@Tehsil", Tehsil, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[23] = new PassDataToSql("@District", District, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[24] = new PassDataToSql("@State", State, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[25] = new PassDataToSql("@Post", Post, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[26] = new PassDataToSql("@PinCode", PinCode, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[27] = new PassDataToSql("@PhoneNo", PhoneNo, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[28] = new PassDataToSql("@Mobile1", Mobile1, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[29] = new PassDataToSql("@Mobile2", Mobile2, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[30] = new PassDataToSql("@Email", Email, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[31] = new PassDataToSql("@BankName", BankName, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[32] = new PassDataToSql("@Branch", branch, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[33] = new PassDataToSql("@AccountNo", AccountNo, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[34] = new PassDataToSql("@Father_Name", Father_Name, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[35] = new PassDataToSql("@Profession", Profession, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[36] = new PassDataToSql("@Position", Position, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[37] = new PassDataToSql("@ToatalIncome", TotalIncome, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[38] = new PassDataToSql("@OfficePhoneNo", OfficePhoneNo, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[39] = new PassDataToSql("@MobileNo3", MobileNo3, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[40] = new PassDataToSql("@OfficeAddr", OfficeAddr, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[41] = new PassDataToSql("@Mother_Name", MotherName, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[42] = new PassDataToSql("@Profession1", Profession1, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[43] = new PassDataToSql("@Position1", Position1, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[44] = new PassDataToSql("@ToatalIncome1", TotalIncome1, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[45] = new PassDataToSql("@OfficePhoneNo1", OfficePhoneNo1, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[46] = new PassDataToSql("@MobileNo4", MobileNo4, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[47] = new PassDataToSql("@OfficeAddr1", OfficeAddr1, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[48] = new PassDataToSql("@LocalGuardian", LocalGuardian, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[49] = new PassDataToSql("@MobileNo5", MobileNo5, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[50] = new PassDataToSql("@Address", Address, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[51] = new PassDataToSql("@Village1", Village1, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[52] = new PassDataToSql("@Docotor", Docotor, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[53] = new PassDataToSql("@MobileNo6", MobileNo6, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[54] = new PassDataToSql("@BloodGroup", BloodGroup, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[55] = new PassDataToSql("@Exam", Exam, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[56] = new PassDataToSql("@Year", Year, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[57] = new PassDataToSql("@RollNo", RollNo, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[58] = new PassDataToSql("@EnrNo", EnrNo, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[59] = new PassDataToSql("@X_Board", X_Board, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[60] = new PassDataToSql("@XII_Board", XII_Board, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[61] = new PassDataToSql("@Graduation", Graduation, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[62] = new PassDataToSql("@PostGraduation", PostGraduation, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[63] = new PassDataToSql("@OtherBoard", OtherBoard, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[64] = new PassDataToSql("@X_Inst", X_Inst, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[65] = new PassDataToSql("@XII_Inst", XII_Inst, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[66] = new PassDataToSql("@Grd_Inst", Grd_Inst, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[67] = new PassDataToSql("@Post_Inst", Post_Inst, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[68] = new PassDataToSql("@Other_Inst", Other_Inst, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[69] = new PassDataToSql("@X_Year", X_Year, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[70] = new PassDataToSql("@XII_Year", XII_Year, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[71] = new PassDataToSql("@Grd_Year", Grd_Year, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[72] = new PassDataToSql("@Post_Year", Post_Year, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[73] = new PassDataToSql("@Other_Year", Other_Year, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[74] = new PassDataToSql("@X_MM", X_MM, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[75] = new PassDataToSql("@XII_MM", XII_MM, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[76] = new PassDataToSql("@Grd_MM", Graducate_Marks, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[77] = new PassDataToSql("@Post_MM", Post_MM, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[78] = new PassDataToSql("@Other_MM", Other_MM, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[79] = new PassDataToSql("@X_Obt", X_ObtMarks, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[80] = new PassDataToSql("@XII_Obt", XII_ObtMarks, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[81] = new PassDataToSql("@Grd_Obt", Graducate_ObtMarks, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[82] = new PassDataToSql("@Post_Obt", PostGraducate_ObtMarks, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[83] = new PassDataToSql("@Other_Obt", Other_ObtMarks, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[84] = new PassDataToSql("@X_Per", X_Per, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[85] = new PassDataToSql("@XII_Per", XII_Per, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[86] = new PassDataToSql("@Grd_Per", Graducate_Per, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[87] = new PassDataToSql("@Post_Per", Post_Per, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[88] = new PassDataToSql("@Other_Per", Other_Per, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[89] = new PassDataToSql("@CompSub1", CompSub1, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[90] = new PassDataToSql("@CompSub2", CompSub2, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[91] = new PassDataToSql("@CompSub3", CompSub3, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[92] = new PassDataToSql("@CompSub4", CompSub4, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[93] = new PassDataToSql("@OpSub1", OpSub1, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[94] = new PassDataToSql("@OpSub2", OpSub2, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[95] = new PassDataToSql("@OpSu3", OpSub3, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[96] = new PassDataToSql("@OpSub4", OpSub4, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[97] = new PassDataToSql("@OpSub5", OpSub5, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[98] = new PassDataToSql("@OpSub6", OpSub6, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[99] = new PassDataToSql("@OpSub7", OpSub7, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[100] = new PassDataToSql("@OpSub8", OpSub8, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[101] = new PassDataToSql("@OpSub9", OpSub9, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[102] = new PassDataToSql("@OpSub10", OpSub10, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[103] = new PassDataToSql("@OpSub11", OpSub11, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[104] = new PassDataToSql("@OpSub12", OpSub12, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[105] = new PassDataToSql("@OpSub13", OpSub13, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[106] = new PassDataToSql("@OpSub14", OpSub14, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[107] = new PassDataToSql("@OpSu15", OpSub15, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[108] = new PassDataToSql("@DC", DC, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[109] = new PassDataToSql("@Yera1", Year1, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[110] = new PassDataToSql("@Year2", Year2, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[111] = new PassDataToSql("@RollNo1", RollNo1, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[112] = new PassDataToSql("@RollNo2", RollNo2, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[113] = new PassDataToSql("@Result1", Result1, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[114] = new PassDataToSql("@Result2", Result2, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[115] = new PassDataToSql("@Photo", photo, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[116] = new PassDataToSql("@Sig1", Sig1, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[117] = new PassDataToSql("@Sig2", sig2, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[118] = new PassDataToSql("@Mq1", Mq1, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[119] = new PassDataToSql("@Tc", Tc, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[120] = new PassDataToSql("@Mig", Mig, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[121] = new PassDataToSql("@sc", sc, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[122] = new PassDataToSql("@Sports", Sports, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[123] = new PassDataToSql("@Bonafide", Bonafide, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[124] = new PassDataToSql("@SE", SE, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[125] = new PassDataToSql("@CC", CC, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[126] = new PassDataToSql("@PP", PP, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[127] = new PassDataToSql("@PAR", pars, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[128] = new PassDataToSql("@DCompSub1", DComSub1, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[129] = new PassDataToSql("@DCompSub2", DComSub2, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[130] = new PassDataToSql("@DCompSub3", DComSub3, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[131] = new PassDataToSql("@DCompSub4", DComSub4, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[132] = new PassDataToSql("@DOpSub1", DOpSub1, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[133] = new PassDataToSql("@DOpSub2", DOpSub2, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[134] = new PassDataToSql("@DOpSub3", DOpSub3, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[135] = new PassDataToSql("@Noe1", Noe1, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[136] = new PassDataToSql("@Noe2", Noe2, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[137] = new PassDataToSql("@Noi1", Noi1, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[138] = new PassDataToSql("@Noi2", Noi2, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[139] = new PassDataToSql("@Board1", Board1, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[140] = new PassDataToSql("@Board2", Board2, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[141] = new PassDataToSql("@Field1", Field1, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[142] = new PassDataToSql("@Field2", Field2, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[143] = new PassDataToSql("@Field3", Field3, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[144] = new PassDataToSql("@Field4", Field4, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[145] = new PassDataToSql("@Field5", Field5, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[146] = new PassDataToSql("@Field6", Field6, PassDataToSql.ParaTypeList.Bit, PassDataToSql.ParaDirectonList.Input);
        paramList[147] = new PassDataToSql("@Field7", Field7, PassDataToSql.ParaTypeList.Date, PassDataToSql.ParaDirectonList.Input);
        paramList[148] = new PassDataToSql("@IsActive", IsActive, PassDataToSql.ParaTypeList.Bit, PassDataToSql.ParaDirectonList.Input);
        paramList[149] = new PassDataToSql("@CreatedBy", CreatedBy, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[150] = new PassDataToSql("@CreatedDate", CreatedDate, PassDataToSql.ParaTypeList.Date, PassDataToSql.ParaDirectonList.Input);
        paramList[151] = new PassDataToSql("@ModifiedBy", ModifiedBy, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[152] = new PassDataToSql("@ModifiedDate", ModifiedDate, PassDataToSql.ParaTypeList.Date, PassDataToSql.ParaDirectonList.Input);
        paramList[153] = new PassDataToSql("@ReferenceID", "0", PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Output);
        paramList[154] = new PassDataToSql("@Student_Code", Student_Code, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[155] = new PassDataToSql("@Street", Street, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        daClass.execute_Sp("sp_Set_StudentAdmissionMaster_Insert", paramList);
        return Convert.ToInt32(paramList[152].ParaValue);
    }

    public int UpdateStudentAdmissionMaster(string CompanyId, string EStudentName, string LStudentName, string NCC, string No, string srno, string regno, string category, string EnNo, string permit, string Religion, string Nationality, string Course, string class1, string doa, string dob, string Place, string caste, string Status, string PH, string Village, string Grampanchayat, string Tehsil, string District, string State, string Post, string PinCode, string PhoneNo, string Mobile1, string Mobile2, string Email, string BankName, string branch, string AccountNo, string Father_Name, string Profession, string Position, string TotalIncome, string OfficePhoneNo, string MobileNo3, string OfficeAddr, string MotherName, string Profession1, string Position1, string TotalIncome1, string OfficePhoneNo1, string MobileNo4, string OfficeAddr1, string LocalGuardian, string MobileNo5, string Address, string Village1, string Docotor, string MobileNo6, string BloodGroup, string Exam, string Year, string RollNo, string EnrNo, string X_Board, string XII_Board, string Graduation, string PostGraduation, string OtherBoard, string X_Inst, string XII_Inst, string Grd_Inst, string Post_Inst, string Other_Inst, string X_Year, string XII_Year, string Grd_Year, string Post_Year, string Other_Year, string X_MM, string XII_MM, string Graducate_Marks, string Post_MM, string Other_MM, string X_ObtMarks, string XII_ObtMarks, string Graducate_ObtMarks, string PostGraducate_ObtMarks, string Other_ObtMarks, string X_Per, string XII_Per, string Graducate_Per, string Post_Per, string Other_Per, string CompSub1, string CompSub2, string CompSub3, string CompSub4, string OpSub1, string OpSub2, string OpSub3, string OpSub4, string OpSub5, string OpSub6, string OpSub7, string OpSub8, string OpSub9, string OpSub10, string OpSub11, string OpSub12, string OpSub13, string OpSub14, string OpSub15, string DC, string Year1, string Year2, string RollNo1, string RollNo2, string Result1, string Result2, string photo, string Sig1, string sig2, string Mq1, string Tc, string Mig, string sc, string Sports, string Bonafide, string SE, string CC, string PP, string pars, string DComSub1, string DComSub2, string DComSub3, string DComSub4, string DOpSub1, string DOpSub2, string DOpSub3, string Noe1, string Noe2, string Noi1, string Noi2, string Board1, string Board2, string Field1, string Field2, string Field3, string Field4, string Field5, string Field6, string Field7, string IsActive, string ModifiedBy, string ModifiedDate, string Student_Code, string Street, string StudentId)
    {

        PassDataToSql[] paramList = new PassDataToSql[155];
        paramList[0] = new PassDataToSql("@Company_Id", CompanyId, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        paramList[1] = new PassDataToSql("@Student_Name", EStudentName, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[2] = new PassDataToSql("@Student_Name_L", LStudentName, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[3] = new PassDataToSql("@NCC", NCC, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[4] = new PassDataToSql("@FormNo", No, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[5] = new PassDataToSql("@Sr_No", srno, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[6] = new PassDataToSql("@Reg_No", regno, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[7] = new PassDataToSql("@Category", category, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[8] = new PassDataToSql("@Enroll_No", EnNo, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[9] = new PassDataToSql("@PermittedBy", permit, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[10] = new PassDataToSql("@Religion", Religion, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[11] = new PassDataToSql("@Nationality", Nationality, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[12] = new PassDataToSql("@Course", Course, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[13] = new PassDataToSql("@Class", class1, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[14] = new PassDataToSql("@Doa", doa, PassDataToSql.ParaTypeList.Date, PassDataToSql.ParaDirectonList.Input);
        paramList[15] = new PassDataToSql("@Dob", dob, PassDataToSql.ParaTypeList.Date, PassDataToSql.ParaDirectonList.Input);
        paramList[16] = new PassDataToSql("@Place", Place, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[17] = new PassDataToSql("@Caste", caste, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[18] = new PassDataToSql("@Status", Status, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[19] = new PassDataToSql("@PH", PH, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[20] = new PassDataToSql("@Village", Village, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[21] = new PassDataToSql("@Grampanchayat", Grampanchayat, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[22] = new PassDataToSql("@Tehsil", Tehsil, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[23] = new PassDataToSql("@District", District, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[24] = new PassDataToSql("@State", State, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[25] = new PassDataToSql("@Post", Post, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[26] = new PassDataToSql("@PinCode", PinCode, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[27] = new PassDataToSql("@PhoneNo", PhoneNo, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[28] = new PassDataToSql("@Mobile1", Mobile1, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[29] = new PassDataToSql("@Mobile2", Mobile2, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[30] = new PassDataToSql("@Email", Email, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[31] = new PassDataToSql("@BankName", BankName, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[32] = new PassDataToSql("@Branch", branch, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[33] = new PassDataToSql("@AccountNo", AccountNo, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[34] = new PassDataToSql("@Father_Name", Father_Name, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[35] = new PassDataToSql("@Profession", Profession, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[36] = new PassDataToSql("@Position", Position, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[37] = new PassDataToSql("@ToatalIncome", TotalIncome, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[38] = new PassDataToSql("@OfficePhoneNo", OfficePhoneNo, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[39] = new PassDataToSql("@MobileNo3", MobileNo3, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[40] = new PassDataToSql("@OfficeAddr", OfficeAddr, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[41] = new PassDataToSql("@Mother_Name", MotherName, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[42] = new PassDataToSql("@Profession1", Profession1, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[43] = new PassDataToSql("@Position1", Position1, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[44] = new PassDataToSql("@ToatalIncome1", TotalIncome1, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[45] = new PassDataToSql("@OfficePhoneNo1", OfficePhoneNo1, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[46] = new PassDataToSql("@MobileNo4", MobileNo4, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[47] = new PassDataToSql("@OfficeAddr1", OfficeAddr1, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[48] = new PassDataToSql("@LocalGuardian", LocalGuardian, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[49] = new PassDataToSql("@MobileNo5", MobileNo5, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[50] = new PassDataToSql("@Address", Address, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[51] = new PassDataToSql("@Village1", Village1, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[52] = new PassDataToSql("@Docotor", Docotor, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[53] = new PassDataToSql("@MobileNo6", MobileNo6, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[54] = new PassDataToSql("@BloodGroup", BloodGroup, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[55] = new PassDataToSql("@Exam", Exam, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[56] = new PassDataToSql("@Year", Year, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[57] = new PassDataToSql("@RollNo", RollNo, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[58] = new PassDataToSql("@EnrNo", EnrNo, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[59] = new PassDataToSql("@X_Board", X_Board, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[60] = new PassDataToSql("@XII_Board", XII_Board, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[61] = new PassDataToSql("@Graduation", Graduation, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[62] = new PassDataToSql("@PostGraduation", PostGraduation, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[63] = new PassDataToSql("@OtherBoard", OtherBoard, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[64] = new PassDataToSql("@X_Inst", X_Inst, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[65] = new PassDataToSql("@XII_Inst", XII_Inst, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[66] = new PassDataToSql("@Grd_Inst", Grd_Inst, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[67] = new PassDataToSql("@Post_Inst", Post_Inst, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[68] = new PassDataToSql("@Other_Inst", Other_Inst, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[69] = new PassDataToSql("@X_Year", X_Year, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[70] = new PassDataToSql("@XII_Year", XII_Year, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[71] = new PassDataToSql("@Grd_Year", Grd_Year, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[72] = new PassDataToSql("@Post_Year", Post_Year, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[73] = new PassDataToSql("@Other_Year", Other_Year, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[74] = new PassDataToSql("@X_MM", X_MM, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[75] = new PassDataToSql("@XII_MM", XII_MM, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[76] = new PassDataToSql("@Grd_MM", Graducate_Marks, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[77] = new PassDataToSql("@Post_MM", Post_MM, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[78] = new PassDataToSql("@Other_MM", Other_MM, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[79] = new PassDataToSql("@X_Obt", X_ObtMarks, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[80] = new PassDataToSql("@XII_Obt", XII_ObtMarks, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[81] = new PassDataToSql("@Grd_Obt", Graducate_ObtMarks, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[82] = new PassDataToSql("@Post_Obt", PostGraducate_ObtMarks, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[83] = new PassDataToSql("@Other_Obt", Other_ObtMarks, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[84] = new PassDataToSql("@X_Per", X_Per, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[85] = new PassDataToSql("@XII_Per", XII_Per, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[86] = new PassDataToSql("@Grd_Per", Graducate_Per, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[87] = new PassDataToSql("@Post_Per", Post_Per, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[88] = new PassDataToSql("@Other_Per", Other_Per, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[89] = new PassDataToSql("@CompSub1", CompSub1, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[90] = new PassDataToSql("@CompSub2", CompSub2, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[91] = new PassDataToSql("@CompSub3", CompSub3, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[92] = new PassDataToSql("@CompSub4", CompSub4, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[93] = new PassDataToSql("@OpSub1", OpSub1, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[94] = new PassDataToSql("@OpSub2", OpSub2, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[95] = new PassDataToSql("@OpSu3", OpSub3, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[96] = new PassDataToSql("@OpSub4", OpSub4, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[97] = new PassDataToSql("@OpSub5", OpSub5, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[98] = new PassDataToSql("@OpSub6", OpSub6, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[99] = new PassDataToSql("@OpSub7", OpSub7, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[100] = new PassDataToSql("@OpSub8", OpSub8, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[101] = new PassDataToSql("@OpSub9", OpSub9, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[102] = new PassDataToSql("@OpSub10", OpSub10, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[103] = new PassDataToSql("@OpSub11", OpSub11, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[104] = new PassDataToSql("@OpSub12", OpSub12, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[105] = new PassDataToSql("@OpSub13", OpSub13, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[106] = new PassDataToSql("@OpSub14", OpSub14, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[107] = new PassDataToSql("@OpSu15", OpSub15, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[108] = new PassDataToSql("@DC", DC, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[109] = new PassDataToSql("@Yera1", Year1, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[110] = new PassDataToSql("@Year2", Year2, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[111] = new PassDataToSql("@RollNo1", RollNo1, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[112] = new PassDataToSql("@RollNo2", RollNo2, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[113] = new PassDataToSql("@Result1", Result1, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[114] = new PassDataToSql("@Result2", Result2, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[115] = new PassDataToSql("@Photo", photo, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[116] = new PassDataToSql("@Sig1", Sig1, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[117] = new PassDataToSql("@Sig2", sig2, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[118] = new PassDataToSql("@Mq1", Mq1, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[119] = new PassDataToSql("@Tc", Tc, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[120] = new PassDataToSql("@Mig", Mig, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[121] = new PassDataToSql("@sc", sc, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[122] = new PassDataToSql("@Sports", Sports, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[123] = new PassDataToSql("@Bonafide", Bonafide, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[124] = new PassDataToSql("@SE", SE, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[125] = new PassDataToSql("@CC", CC, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[126] = new PassDataToSql("@PP", PP, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[127] = new PassDataToSql("@PAR", pars, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[128] = new PassDataToSql("@DCompSub1", DComSub1, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[129] = new PassDataToSql("@DCompSub2", DComSub2, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[130] = new PassDataToSql("@DCompSub3", DComSub3, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[131] = new PassDataToSql("@DCompSub4", DComSub4, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[132] = new PassDataToSql("@DOpSub1", DOpSub1, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[133] = new PassDataToSql("@DOpSub2", DOpSub2, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[134] = new PassDataToSql("@DOpSub3", DOpSub3, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[135] = new PassDataToSql("@Noe1", Noe1, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[136] = new PassDataToSql("@Noe2", Noe2, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[137] = new PassDataToSql("@Noi1", Noi1, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[138] = new PassDataToSql("@Noi2", Noi2, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[139] = new PassDataToSql("@Board1", Board1, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[140] = new PassDataToSql("@Board2", Board2, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
       
        paramList[141] = new PassDataToSql("@Field1", Field1, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[142] = new PassDataToSql("@Field2", Field2, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[143] = new PassDataToSql("@Field3", Field3, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[144] = new PassDataToSql("@Field4", Field4, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[145] = new PassDataToSql("@Field5", Field5, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[146] = new PassDataToSql("@Field6", Field6, PassDataToSql.ParaTypeList.Bit, PassDataToSql.ParaDirectonList.Input);
        paramList[147] = new PassDataToSql("@Field7", Field7, PassDataToSql.ParaTypeList.Date, PassDataToSql.ParaDirectonList.Input);
        paramList[148] = new PassDataToSql("@IsActive", IsActive, PassDataToSql.ParaTypeList.Bit, PassDataToSql.ParaDirectonList.Input);
        paramList[149] = new PassDataToSql("@ModifiedBy", ModifiedBy, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[150] = new PassDataToSql("@ModifiedDate", ModifiedDate, PassDataToSql.ParaTypeList.Date, PassDataToSql.ParaDirectonList.Input);
        paramList[151] = new PassDataToSql("@ReferenceID", "0", PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Output);
        paramList[152] = new PassDataToSql("@Student_Id", StudentId, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        paramList[153] = new PassDataToSql("@Student_Code", Student_Code, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[154] = new PassDataToSql("@Street", Street, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        daClass.execute_Sp("sp_Set_StudentAdmissionMaster_Update", paramList);
        return Convert.ToInt32(paramList[150].ParaValue);
    }

    public int DeleteStudentAdmissionMaster(string CompanyId, string StudentId, string IsActive, string ModifiedBy, string ModifiedDate)
    {

        PassDataToSql[] paramList = new PassDataToSql[6];
        paramList[0] = new PassDataToSql("@Company_Id", CompanyId, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        paramList[1] = new PassDataToSql("@Student_Id", StudentId, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        paramList[2] = new PassDataToSql("@IsActive", IsActive, PassDataToSql.ParaTypeList.Bit, PassDataToSql.ParaDirectonList.Input);
        paramList[3] = new PassDataToSql("@ModifiedBy", ModifiedBy, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[4] = new PassDataToSql("@ModifiedDate", ModifiedDate, PassDataToSql.ParaTypeList.Date, PassDataToSql.ParaDirectonList.Input);
        paramList[5] = new PassDataToSql("@ReferenceID", "0", PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Output);
        daClass.execute_Sp("sp_Set_StudentAdmissionMaster_RowStatus", paramList);
        return Convert.ToInt32(paramList[5].ParaValue);
    }
    public DataTable GetStudentAdmissionMasterById(string CompanyId, string StudentId)
    {

        DataTable dtInfo = new DataTable();
        PassDataToSql[] paramList = new PassDataToSql[4];
        paramList[0] = new PassDataToSql("@Company_Id", CompanyId, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        paramList[1] = new PassDataToSql("@Student_Id", StudentId, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[2] = new PassDataToSql("@Student_Name", "", PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[3] = new PassDataToSql("@optype", "4", PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        dtInfo = daClass.Reuturn_Datatable_Search("sp_Set_StudentAdmissionMaster_SelectRow", paramList);

        return dtInfo;
    }
    public DataTable GetStudentAdmissionMaster(string CompanyId)
    {

        DataTable dtInfo = new DataTable();
        PassDataToSql[] paramList = new PassDataToSql[4];
        paramList[0] = new PassDataToSql("@Company_Id", CompanyId, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        paramList[1] = new PassDataToSql("@Student_Id", "0", PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[2] = new PassDataToSql("@Student_Name", "", PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[3] = new PassDataToSql("@optype", "2", PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        dtInfo = daClass.Reuturn_Datatable_Search("sp_Set_StudentAdmissionMaster_SelectRow", paramList);
        return dtInfo;
    }


    public DataTable GetStudentAdmissionMasterInactive(string CompanyId)
    {
        DataTable dtInfo = new DataTable();
        PassDataToSql[] paramList = new PassDataToSql[4];
        paramList[0] = new PassDataToSql("@Company_Id", CompanyId, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        paramList[1] = new PassDataToSql("@Student_Id", "0", PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[2] = new PassDataToSql("@Student_Name", "", PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[3] = new PassDataToSql("@optype", "3", PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        dtInfo = daClass.Reuturn_Datatable_Search("sp_Set_StudentAdmissionMaster_SelectRow", paramList);

        return dtInfo;
    }
    public DataTable GetStudentAdmissionMasterByStudentName(string CompanyId, string strStudentName)
    {
        DataTable dtInfo = new DataTable();
        PassDataToSql[] paramList = new PassDataToSql[4];
        paramList[0] = new PassDataToSql("@Company_Id", CompanyId, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        paramList[1] = new PassDataToSql("@Student_Id", "0", PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[2] = new PassDataToSql("@Student_Name", strStudentName, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[3] = new PassDataToSql("@optype", "5", PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        dtInfo = daClass.Reuturn_Datatable_Search("sp_Set_StudentAdmissionMaster_SelectRow", paramList);
        return dtInfo;
    }










  
}
