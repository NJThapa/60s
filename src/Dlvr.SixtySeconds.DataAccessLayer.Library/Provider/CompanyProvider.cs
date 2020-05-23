using Dlvr.SixtySeconds.DataAccessLayer.Library.Data;
using Dlvr.SixtySeconds.DataAccessLayer.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dlvr.SixtySeconds.DataAccessLayer.Library.Provider
{
    public class CompanyProvider
    {
        public DataContext _context;
        public CompanyProvider(DataContext dataContext)
        {
            _context = dataContext;
        }

        public Company Get(int companyId)
        {
            Company company = _context.Companies.Where(x => x.CompanyId == companyId).FirstOrDefault();
            return company;
        }

        public List<Company> GetAll(Pagination pagination)
        {
            return _context.Companies.Where(x => x.DeletedOn == null).Skip((pagination.PageNo - 1) * pagination.PerPage).Take(pagination.PerPage).ToList();
        }

        public Company Save(Company company)
        {
            if (company.CompanyId > 0)
            {
                var cmny = _context.Companies.Find(company.CompanyId);

                if (cmny != null)
                {
                    cmny.CompanyName = company.CompanyName;
                    cmny.BrandName = company.BrandName;
                    cmny.SubDomain = company.SubDomain;
                    cmny.Phone = company.Phone;
                    cmny.Email = company.Email;
                    cmny.Country = company.Country;
                    cmny.State = company.State;
                    cmny.Terms = company.Terms;
                    cmny.ScriptFieldCollection = company.ScriptFieldCollection;
                    cmny.UpdatedBy = company.UpdatedBy;
                    cmny.UpdatedOn = DateTime.UtcNow;

                    _context.SaveChanges();
                }
                //Update
            }
            else
            {
                company.CreatedOn = DateTime.UtcNow;

                _context.Companies.Add(company);
                _context.SaveChanges();
            }

            return company;
        }       

        public bool Delete(int id)
        {
            bool success = false;

            if (id > 0)
            {
                var cmny = _context.Companies.Find(id);

                if (cmny != null)
                {
                    cmny.DeletedOn = DateTime.UtcNow;

                    _context.SaveChanges();

                    success = true;
                }
            }

            return success;
        }
    }
}
