using SGRSalary.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGRSalary.Classes;

namespace SGRSalary.Repository
{
    public class RepositorySgr
    {
        SGR_SALARYEntities cnt = new SGR_SALARYEntities();
      
        #region BI_Bank
        public void AddBank(BI_Bank bank)
        {
            cnt.BI_Bank.Add(bank);
            cnt.SaveChanges();
        }
        public List<BI_Bank> GetBanks()
        {
            return cnt.BI_Bank.ToList();
        }
        public BI_Bank GetBanksByID(Int64 bankID)
        {
            return cnt.BI_Bank.FirstOrDefault(x => x.Bank_ID == bankID);
        }
        public void SaveBank()
        {
            cnt.SaveChanges();
        }
        public void DeleteBank(BI_Bank bank)
        {
            cnt.BI_Bank.Remove(bank);
            cnt.SaveChanges();
        }
        #endregion

        #region BI_Bazar
        public void AddBank(BI_Bazar bazar)
        {
            cnt.BI_Bazar.Add(bazar);
            cnt.SaveChanges();
        }
        public List<BI_Bazar> Getbazars()
        {
            return cnt.BI_Bazar.ToList();
        }
        public BI_Bazar GetbazarsByID(Int64 bazarID)
        {
            return cnt.BI_Bazar.FirstOrDefault(x => x.Bazar_ID == bazarID);
        }
        public void Savebazar()
        {
            cnt.SaveChanges();
        }
        public void Deletebazar(BI_Bazar bazar)
        {
            cnt.BI_Bazar.Remove(bazar);
            cnt.SaveChanges();
        }
        #endregion

        #region BI_Personele
        public void AddPerson(BI_Personel person)
        {
            cnt.BI_Personel.Add(person);
            cnt.SaveChanges();
        }
        public List<BI_Personel> GetPersons()
        {
            return cnt.BI_Personel.Include(x => x.BI_Company).Include(x => x.BI_Project).Include(x => x.BI_City).ToList();
           // string cmd = $@"select per.*,com.Name compony,pro.Name project,cit.Name city from 
           //                 [dbo].[BI_Personel] per
           //                 inner join BI_Company com on per.Company_ID=com.Company_ID
           //                 inner join BI_Project pro  on per.Project_ID=pro.Project_ID
           //                 inner join BI_City cit on cit.City_ID=per.City_ID";
           //return cnt.Database.SqlQuery<BI_Personel>(cmd).ToList();
        }
        public BI_Personel GetPersonsByID(Int64 personID)
        {
            return cnt.BI_Personel.FirstOrDefault(x => x.Personel_ID == personID);
        }
        public void SavePerson()
        {
            cnt.SaveChanges();
        }
        public void DeletePerson(BI_Personel person)
        {
            cnt.BI_Personel.Remove(person);
            cnt.SaveChanges();
        }
        #endregion
        
        #region BI_City
        public void AddBank(BI_City city)
        {
            cnt.BI_City.Add(city);
            cnt.SaveChanges();
        }
        public List<BI_City> GetCitys()
        {
            return cnt.BI_City.ToList();
        }
        public BI_City GetCitysByID(Int64 cityID)
        {
            return cnt.BI_City.FirstOrDefault(x => x.City_ID == cityID);
        }
        public void SaveCity()
        {
            cnt.SaveChanges();
        }
        public void DeleteCity(BI_City city)
        {
            cnt.BI_City.Remove(city);
            cnt.SaveChanges();
        }
        #endregion

        #region BI_Company
        public void AddCompony(BI_Company compony)
        {
            cnt.BI_Company.Add(compony);
            cnt.SaveChanges();
        }
        public List<BI_Company> GetComponys()
        {
            return cnt.BI_Company.ToList();
        }
        public BI_Company GetComponyByID(Int64 ComponyID)
        {
            return cnt.BI_Company.FirstOrDefault(x => x.Company_ID == ComponyID);
        }
        public void SaveCompony()
        {
            cnt.SaveChanges();
        }
        public void DeleteCompony(BI_Company compony)
        {
            cnt.BI_Company.Remove(compony);
            cnt.SaveChanges();
        }
        #endregion

        #region BI_Condition
        public void AddCondition(BI_Condition condition)
        {
            cnt.BI_Condition.Add(condition);
            cnt.SaveChanges();
        }
        public List<BI_Condition> GetConditions()
        {
            return cnt.BI_Condition.ToList();
        }
        public BI_Condition GetConditionByID(Int64 conditionID)
        {
            return cnt.BI_Condition.FirstOrDefault(x => x.Condition_ID == conditionID);
        }
        public void SaveCondition()
        {
            cnt.SaveChanges();
        }
        public void DeleteCondition(BI_Condition condition)
        {
            cnt.BI_Condition.Remove(condition);
            cnt.SaveChanges();
        }
        #endregion

        #region BI_Deductions
        public void AddDeduction(BI_Deductions deduction)
        {
            cnt.BI_Deductions.Add(deduction);
            cnt.SaveChanges();
        }
        public List<BI_Deductions> GetDeductions()
        {
            return cnt.BI_Deductions.ToList();
        }
        public BI_Deductions GetDeductionByID(Int64 deductionID)
        {
            return cnt.BI_Deductions.FirstOrDefault(x => x.Deductions_ID == deductionID);
        }
        public void SaveDeduction()
        {
            cnt.SaveChanges();
        }
        public void DeleteDeduction(BI_Deductions deduction)
        {
            cnt.BI_Deductions.Remove(deduction);
            cnt.SaveChanges();
        }
        #endregion
    }
}
