using SGRSalary.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGRSalary.Repository
{

    public class Repository
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
            string cmd = $@"select per.*,com.Name compony,pro.Name project,cit.Name city from 
                            [dbo].[BI_Personel] per
                            inner join BI_Company com on per.Company_ID=com.Company_ID
                            inner join BI_Project pro  on per.Project_ID=pro.Project_ID
                            inner join BI_City cit on cit.City_ID=per.City_ID";
            return cnt.BI_Personel.ToList();
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
    }
}
